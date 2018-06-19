namespace AspCoreVue.Entities
{
    public class SearchPageParameter
    {
        public string Search { get; set; }
        public int PageNumber { get; set; } = 1;
        public int MaxRowsPerPage { get; set; } = 25;
    }
}