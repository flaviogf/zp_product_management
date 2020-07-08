using System.Collections.Generic;

namespace ZPProductManagement.Common
{
    public class Pagination<T>
    {
        public Pagination(IEnumerable<T> content, int total, int page, int pages)
        {
            Content = content;
            Total = total;
            Page = page;
            Pages = pages;
        }

        public IEnumerable<T> Content { get; }

        public int Total { get; }

        public int Page { get; }

        public int Pages { get; }

        public bool IsFirst => Page == 1;

        public bool IsLast => Page == Pages;

        public bool HasNext => (Page + 1) <= Pages;

        public bool HasPrevious => (Page - 1) >= 1;
    }
}