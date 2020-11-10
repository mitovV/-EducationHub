namespace EducationHub.Services.Data.Categories
{
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task CreateAsync(string name, string pictureUrl, string userId);
    }
}
