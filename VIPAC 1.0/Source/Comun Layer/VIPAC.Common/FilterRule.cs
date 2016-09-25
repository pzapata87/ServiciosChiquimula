using System.Collections.Generic;

namespace VIPAC.Common
{
    public class FilterRule
    {
        public string GroupOp { get; set; }
        public List<Rule> Rules { get; set; }
        public List<FilterRule> Groups { get; set; }
    }
}
