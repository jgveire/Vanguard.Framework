namespace Vanguard.Framework.Website.Models
{
    public class FilterModel
    {
        public FilterModel(int page = 1, int size = 20, string filter = null)
        {
            Guard.ArgumentInRange(1, int.MaxValue, nameof(page));
            Guard.ArgumentInRange(1, int.MaxValue, nameof(size));

            Page = page;
            Size = size;
            Filter = filter;
        }

        public int Page { get; set; } = 1;

        public int Size { get; set; } = 20;

        public string Filter { get; set; }
    }
}
