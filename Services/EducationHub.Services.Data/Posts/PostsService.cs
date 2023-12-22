namespace EducationHub.Services.Data.Posts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EducationHub.Data.Common.Repositories;
    using EducationHub.Data.Models;
    using Mapping;
    using Microsoft.EntityFrameworkCore;

    public class PostsService : IPostsService
    {
        private readonly IRepository<Post> postsRepository;

        public PostsService(IRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task<int> CratePostAsync(string title, string content, string userId, int categoryId)
        {
            var post = new Post
            {
                Title = title,
                Content = content,
                CategoryId = categoryId,
                UserId = userId,
            };

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();

            return post.Id;
        }

        public async Task<IEnumerable<T>> GetByCategoryIdAsync<T>(int id, int page, int itemsPerPage)
        => await this.postsRepository
                .AllAsNoTracking()
                .Where(p => p.CategoryId == id)
                .OrderByDescending(p => p.CreatedOn)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToListAsync();

        public async Task<T> GetByIdAsync<T>(int id)
            => await this.postsRepository
                .AllAsNoTracking()
                .Where(p => p.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetByUserIdAsync<T>(string userId)
            => await this.postsRepository
                .AllAsNoTracking()
                .Where(p => p.UserId == userId)
                .To<T>()
                .ToListAsync();

        public int GetCountByCategory(int id)
        => this.postsRepository
                .All()
                .Where(p => p.CategoryId == id)
                .Count();

        public async Task<IEnumerable<T>> GetPostsAsync<T>(int count)
            => await this.postsRepository
                .AllAsNoTracking()
                .OrderByDescending(p => p.CreatedOn)
                .To<T>()
                .Take(count)
                .ToListAsync();

        public async Task<IEnumerable<T>> GetPostsByCategoryAsync<T>(int categoryId)
            => await this.postsRepository
                .AllAsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .To<T>()
                .ToListAsync();
    }
}
