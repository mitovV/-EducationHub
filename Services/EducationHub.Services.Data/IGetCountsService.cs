namespace EducationHub.Services.Data
{
    using System.Threading.Tasks;

    using Web.ViewModels.Forum;
    using Web.ViewModels.Home;

    public interface IGetCountsService
    {
        IndexViewModel GetCounts();

        Task<HomePageViewModel> GetForumPostsCountsAsync();
    }
}
