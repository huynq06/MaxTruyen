using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxStory.Data.Entities
{
	public class Story
	{
		public string id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public string thumnail { get; set; }
		public string author { get; set; }
		public string status { get; set; }
		public string rate { get; set; }
		public string totalPage { get; set; }
		public string urlDetail { get; set; }
		public string lastTimeUpdated { get; set; }
		public string aggregateRating { get; set; }
		public string chapterTotal { get; set; }
		public Chapter chapter { get; set; }
		public Category category { get; set; }
		public References reference { get; set; }
		public List<Chapter> chapterList { get; set; }

		public Story()
		{

		}

		public Story(string id, string name, string description, string thumnail, string author, string status, string rate, string totalPage, Chapter chapter, Category category, References reference, string urlDetail, string lastTimeUpdated, string aggregateRating, List<Chapter> chapterList, string chapterTotal)
		{
			this.id = id;
			this.name = name;
			this.description = description;
			this.thumnail = thumnail;
			this.author = author;
			this.status = status;
			this.rate = rate;
			this.totalPage = totalPage;
			this.chapter = chapter;
			this.category = category;
			this.reference = reference;
			this.urlDetail = urlDetail;
			this.lastTimeUpdated = lastTimeUpdated;
			this.aggregateRating = aggregateRating;
			this.chapterList = chapterList;
			this.chapterTotal = chapterTotal;
		}
	}
}
