using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxStory.Data.Entities
{
	public class Chapter
	{
		public string name { get; set; }
		public string url { get; set; }
		public string description { get; set; }
		public string title { get; set; }
		public string content { get; set; }
		public string preChapter { get; set; }
		public string nextChapter { get; set; }
		public string currentChapter { get; set; }
		public Chapter() { }

		public Chapter(string name, string url, string description, string content, string preChapter, string nextChapter, string title, string currentChapter, string v)
		{
			this.name = name;
			this.url = url;
			this.description = description;
			this.content = content;
			this.preChapter = preChapter;
			this.nextChapter = nextChapter;
			this.title = title;
			this.currentChapter = currentChapter;
		}
	}
}
