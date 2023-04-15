namespace SocialClint._services.Classes
{
    public class Pager<T>
    {
        public int TotalItem { set; get; }
        public int CurrentPage { set; get; }
        public int PageSize { set; get; }

        public int TotalPages { set; get; }
        public int StartPage { set; get; }
        public int EndPage { set; get; }
        public IEnumerable<T> Items { set; get; } 

        public Pager()
        {

        }
        public Pager(int totalItems, int Page, int pageSize = 10)
        {

            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = Page;

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;


            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }

                TotalItem = totalItems;
                CurrentPage = currentPage<=0?1:currentPage;
                PageSize = pageSize > 10?10:pageSize ;
                TotalPages = totalPages;
                StartPage = startPage;
                EndPage = endPage;
            }
        }
    }
}
