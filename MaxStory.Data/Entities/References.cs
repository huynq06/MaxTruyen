using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxStory.Data.Entities
{
	public class References
	{
		public string name { get; set; }
		public string url { get; set; }
		public References(string name, string url)
		{
			this.name = name;
			this.url = url;
		}
	}
}
