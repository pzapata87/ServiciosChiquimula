using System.Collections.Generic;

namespace VIPAC.Common
{
    public class PagingResult<T> where T : class
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int Records { get; set; }
        public int Start { get; set; }
        public IEnumerable<T> Rows { get; set; }
    }
}
