
namespace VIPAC.Common
{
    public class FilterGeneric
    {
        public string ColumnOrder { get; set; }
        public string OrderType { get; set; }
        public int CurrentPage { get; set; }
        public int AmountRows { get; set; }
        public FilterRule WhereRule { get; set; }
    }
}
