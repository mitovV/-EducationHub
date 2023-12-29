namespace EducationHub.Web.ViewModels
{
    using System;

    public class BasePagingViewModel
    {
        public string Actoin { get; set; }

        public int? RouteId { get; set; }

        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber
        {
            get
            {
                if (!this.HasNextPage)
                {
                    return this.PageNumber;
                }
                else
                {
                    return this.PageNumber + 1;
                }
            }
        }

        public int PagesCount => (int)Math.Ceiling((double)this.ItemsCount / this.ItemsPerPage);

        public int ItemsCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
