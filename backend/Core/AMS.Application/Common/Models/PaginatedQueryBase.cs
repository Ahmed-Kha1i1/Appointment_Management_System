namespace Ecommerce.Application.Common.Models
{
    public abstract class PaginatedQueryBase
    {
        protected virtual short MaxPageSize { get; set; } = 50;
        private short _pageSize = 10;
        private int _minPageNumber = 1;
        private int _pageNumber = 1;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = (value < _minPageNumber) ? _minPageNumber : value;
        }
        public short PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
