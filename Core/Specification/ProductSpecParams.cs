namespace Core.Specification
{
    public class ProductSpecParams
    {
		private List<string> _brands = [];
        private List<string> _types = [];
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }

        private int _pageSize = 6;

        private string _search;

        public string Search
        {
            get => _search ?? "";
            set => _search = value.ToLower();
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }


        public List<string> Brands
		{
			get => _brands; 
			set { _brands = value.SelectMany(x=>x.Split(',', 
				StringSplitOptions.RemoveEmptyEntries)).ToList(); }
		}

        public List<string> Types
        {
            get => _types;
            set
            {
                _types = value.SelectMany(x => x.Split(',',
                StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

    }
}
