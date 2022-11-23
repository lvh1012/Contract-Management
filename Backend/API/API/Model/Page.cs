namespace API.Model
{
    public class Page
    {
        private const int minPageSize = 15;
        private const int maxPageSize = 50;

        public int TotalPage { get; set; }
        public int TotalRow { get; set; }
        private int _pageIndex = 1;

        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                if (value < 1) _pageIndex = 1;
                else _pageIndex = value;
            }
        }

        private int _pageSize = 15;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value switch
                {
                    var _value when _value > maxPageSize => maxPageSize,
                    50 => 50,
                    30 => 30,
                    15 => 15,
                    var _value when _value < minPageSize => minPageSize,
                    _ => 15,
                };
            }
        }
    }
}