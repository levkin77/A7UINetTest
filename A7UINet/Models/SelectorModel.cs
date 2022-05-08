using System.Collections.Generic;

namespace A7UINet
{
    public class SelectorModel
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Kind { get; set; }
        public SelectorModel Parent { get; set; }

        public List<SelectorModel> Elements { get; }=new List<SelectorModel>();
    }
}