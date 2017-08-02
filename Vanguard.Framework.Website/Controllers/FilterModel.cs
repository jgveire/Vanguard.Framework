namespace Vanguard.Framework.Website.Controllers
{
    public class FilterModel
    {
        public FilterModel()
        {
        }

        public int? Page { get; set; }

        public int? PageSize { get; set; }

        public string Filter { get; set; }
    }
}
