using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxStory.Data.Entities
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public string url { get; set; }

        public Category(string name, string url, string des)
        {
            this.name = name;
            this.url = url;
            description = des;
        }
    }
}
