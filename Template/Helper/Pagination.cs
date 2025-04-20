namespace Template.Helper
{
    public class Pagination<T>
    {
        public Pagination(int pagesize, int index, int count, IEnumerable<T> data)
        {
            PageSize = pagesize;
            PageIndex = index;
            Count = count;
            Data = data;
        }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
