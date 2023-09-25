using HtmlAgilityPack;
using MaxStory.Application.Interfaces;
using MaxStory.Data;
using MaxStory.Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxStory.Application.Implementation
{
	public class HomeService : IHomeService
	{
		public HomeService()
		{

		}
		public ObservableCollection<Story> getListStoriesHot(string html)
		{
			ObservableCollection<Story> _listStoriesHot = new ObservableCollection<Story>();
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(html);
			var wrapperInputs = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("id", "").Equals("intro-index"));
			var inputs = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("index-intro"));
			if (inputs.Count() == 0)
			{
				return _listStoriesHot;
			}
			var lists = inputs.ElementAt(0).Descendants("div").Where(n => n.GetAttributeValue("class", "").Contains("item"));
			try
			{
				foreach (var input in lists)
				{
					var img = input.ChildNodes["a"].ChildNodes["img"].Attributes["src"].Value;
					var alt = input.ChildNodes["a"].ChildNodes["img"].Attributes["alt"].Value;
					var _url = input.ChildNodes["a"].Attributes["href"].Value;
					_listStoriesHot.Add(new Story()
					{
						name = alt,
						thumnail = img,
						urlDetail = _url
					});

				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);

			}
			System.Diagnostics.Debug.WriteLine("_listStoriesHot done");
			return _listStoriesHot;
		}

		public ObservableCollection<Story> getListStoriesNewUpdated(string html)
		{
			ObservableCollection<Story> listStoriesNewUpdated = new ObservableCollection<Story>();
			ObservableCollection<Story> _listStoriesNewUpdated = new ObservableCollection<Story>();
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(html);
			//var wrapperInputs = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("id", "").Equals("intro-index"));
			var inputs = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("list list-truyen list-new col-xs-12 col-sm-12 col-md-8 col-truyen-main"));
			if (inputs.Count() == 0)
			{
				return listStoriesNewUpdated;
			}
			var lists = inputs.ElementAt(0).Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("row"));
			try
			{
				foreach (var input in lists)
				{
					var wrapperUrl = input.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("col-xs-9 col-sm-6 col-md-5 col-title"));
					var url = wrapperUrl.Last().ChildNodes["h3"].ChildNodes["a"].Attributes["href"].Value;
					var name = wrapperUrl.Last().ChildNodes["h3"].ChildNodes["a"].Attributes["title"].Value;

					var wrapperCategroy = input.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("hidden-xs col-sm-3 col-md-3 col-cat"));
					var categoryName = wrapperCategroy.First().ChildNodes["a"].InnerText;
					var categoryUrl = wrapperCategroy.First().ChildNodes["a"].Attributes["href"].Value;

					var wrapperChapter = input.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("col-xs-3 col-sm-3 col-md-2 col-chap text-info"));
					var chapterName = wrapperChapter.First().ChildNodes["a"].InnerText;
					var chapterUrl = wrapperChapter.First().ChildNodes["a"].Attributes["href"].Value;
					var chapterDes = wrapperChapter.First().ChildNodes["a"].Attributes["title"].Value;

					var wrapperLastTimeUpdated = input.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("hidden-xs hidden-sm col-md-2 col-time"));
					var lastTimeUpdated = wrapperLastTimeUpdated.First().InnerText;

					_listStoriesNewUpdated.Add(new Story()
					{
						urlDetail = url,
						name = name,
						category = new Category(categoryName, categoryUrl, ""),
						chapter = new Chapter(chapterName, chapterUrl, chapterDes, "", "", "", "","",""),
						lastTimeUpdated = lastTimeUpdated
					});

				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);

			}
			System.Diagnostics.Debug.WriteLine("get listStoriesNewUpdated done");
			return _listStoriesNewUpdated;
		}

		public ObservableCollection<Story> getListStoriesFinished(string html)
		{
			ObservableCollection<Story> _listStoriesFinished = new ObservableCollection<Story>();
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(html);
			var wrapperInputs = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("id", "").Equals("truyen-slide"));
			var inputs = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("col-xs-3 col-sm-2 col-md-2"));
			if (inputs.Count() == 0)
			{
				return _listStoriesFinished;
			}
			//var lists = inputs.ElementAt(0).Descendants("div").Where(n => n.GetAttributeValue("class", "").Contains("item"));
			var lists = inputs;
			try
			{
				foreach (var input in lists)
				{
					var url = input.ChildNodes["a"].Attributes["href"].Value;
					var name = input.ChildNodes["a"].ChildNodes["div"].Attributes["data-alt"].Value;
					var img = input.ChildNodes["a"].ChildNodes["div"].Attributes["data-image"].Value;

					var WrapperDesChaper = input.ChildNodes["a"].Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("caption"));
					var desChaper = WrapperDesChaper.Last().ChildNodes["small"].InnerText;

					_listStoriesFinished.Add(new Story()
					{
						name = name,
						thumnail = img,
						urlDetail = url,
						chapter = new Chapter(name, url, desChaper,"","","","","","")
					});

				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);

			}
			System.Diagnostics.Debug.WriteLine("get listStoriesFinished done");
			return _listStoriesFinished;
		}
		public Story getStoryDetail(string html,ref IEnumerable<HtmlNode> sameAuthorLst, ref IEnumerable<HtmlNode> similarLst, ref HtmlNode clHtml)
		{
			Story _story = new Story();
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(html);
			var info = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("col-xs-12 col-info-desc"));
			 sameAuthorLst = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("class", "").Contains("list-same-author"));
			 similarLst = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("class", "").Contains("list-similar"));


			if (info.Count() == 0)
			{
				return _story;
			}
			var detail = info.ElementAt(0);
			try
			{
				var nameDetails = detail.ChildNodes["h3"].InnerText;

				var detailHolder = detail.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("col-xs-12 col-sm-4 col-md-4 info-holder")).ElementAt(0);
				var img_detailHolder = detailHolder.ChildNodes["div"].ChildNodes["div"].ChildNodes["img"].Attributes["src"].Value;

				var info_detailHoder = detailHolder.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("info")).ElementAt(0);
				var author = info_detailHoder.ChildNodes["div"].ChildNodes["h3"].ChildNodes["a"].InnerText;

				var descriptionFull = detail.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("col-xs-12 col-sm-8 col-md-8 desc")).ElementAt(0);
				var description = descriptionFull.ChildNodes["div"].InnerHtml.ToString();

			    clHtml = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("id", "").Equals("list-chapter")).ElementAt(0);
				var chapterList = getChapterList(clHtml);

				var totalPage = clHtml.Descendants("input").Where(n => n.GetAttributeValue("id", "").Equals("total-page")).ElementAt(0).Attributes["value"].Value;
				var storyId = clHtml.Descendants("input").Where(n => n.GetAttributeValue("id", "").Equals("truyen-id")).ElementAt(0).Attributes["value"].Value;

				_story.name = nameDetails;
				_story.thumnail = img_detailHolder;
				_story.author = author;
				_story.description = description;
				_story.chapterList = chapterList;
				_story.totalPage = totalPage;
				_story.id = storyId;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}

			System.Diagnostics.Debug.WriteLine("get detail done");
			//listStoriesSameAuthor = getListStoriesSameAuthor(sameAuthorLst);
			//listStoriesSameAuthor = getListStoriesSimilar(similarLst);

			return _story;
		}
		public List<Chapter> getChapterList(HtmlNode html)
		{
			List<Chapter> list = new List<Chapter>();
			var ulChapterListHtml = html.Descendants("ul").Where(n => n.GetAttributeValue("class", "").Equals("list-chapter"));
			try
			{
				foreach (var input in ulChapterListHtml)
				{
					var listLiHtml = input.Descendants("li");
					foreach (var li in listLiHtml)
					{
						var _name = li.ChildNodes["a"].InnerText;
						var _url = li.ChildNodes["a"].Attributes["href"].Value;

						list.Add(new Chapter(_name, _url, "", "", "", "", "","",""));
					}
				}
			}
			catch (Exception e)
			{

			}

			return list;
		}
		public ObservableCollection<Story> getListStoriesSameAuthor(IEnumerable<HtmlNode> _htmlNode)
		{
			ObservableCollection<Story> stories = new ObservableCollection<Story>();
			if (_htmlNode.Count() == 0)
			{
				return stories;
			}

			var sameAuthor = _htmlNode.ElementAt(0).Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("row"));
			try
			{
				foreach (var el in sameAuthor)
				{
					var _name = el.ChildNodes["div"].ChildNodes["h3"].ChildNodes["a"].InnerText;
					var _url = el.ChildNodes["div"].ChildNodes["h3"].ChildNodes["a"].Attributes["href"].Value;

					stories.Add(new Story()
					{
						name = _name,
						urlDetail = _url
					});
				}

			}
			catch (Exception e)
			{

			}

			return stories;
		}

		public ObservableCollection<Story> getListStoriesSimilar(IEnumerable<HtmlNode> _htmlNode)
		{
			ObservableCollection<Story> stories = new ObservableCollection<Story>();
			if (_htmlNode.Count() == 0)
			{
				return stories;
			}

			var sameAuthor = _htmlNode.ElementAt(0).Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("row"));
			try
			{
				foreach (var el in sameAuthor)
				{
					var _name = el.ChildNodes["div"].ChildNodes["h3"].ChildNodes["a"].InnerText;
					var _url = el.ChildNodes["div"].ChildNodes["h3"].ChildNodes["a"].Attributes["href"].Value;

					stories.Add(new Story()
					{
						name = _name,
						urlDetail = _url
					});
				}

			}
			catch (Exception e)
			{

			}

			return stories;
		}
		public ObservableCollection<Story> getListStoriesCategory(string html)
		{
			ObservableCollection<Story> _stories = new ObservableCollection<Story>();
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(html);
			var info = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("class", "").Contains("list-truyen"));

			if (info.Count() == 0)
			{
				return _stories;
			}
			var list = info.ElementAt(0).Descendants("div").Where(n => n.GetAttributeValue("class", "").Contains("row"));
			try
			{
				foreach (var item in list)
				{
					var imgHtml = item.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("col-xs-3 col-sm-2 col-md-3 col-list-image"));
					var img = imgHtml.ElementAt(0).ChildNodes["div"].ChildNodes["img"] != null ?
						imgHtml.ElementAt(0).ChildNodes["div"].ChildNodes["img"].Attributes["src"].Value :
						imgHtml.ElementAt(0).ChildNodes["div"].ChildNodes["div"].Attributes["data-image"].Value;
					//var img = imgHtml.ElementAt(0).ChildNodes["div"].ChildNodes["div"].Attributes["data-image"].Value;

					var contentHtml = item.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("col-xs-9 col-sm-10 col-md-9 col-list-info"));
					var url = contentHtml.ElementAt(0).ChildNodes["div"].ChildNodes["h3"].ChildNodes["a"].Attributes["href"].Value;
					var name = contentHtml.ElementAt(0).ChildNodes["div"].ChildNodes["h3"].ChildNodes["a"].Attributes["title"].Value;

					var authorHtml = contentHtml.ElementAt(0).ChildNodes["div"].Descendants("span").Where(n => n.GetAttributeValue("class", "").Equals("author"));
					var authorName = authorHtml.ElementAt(0).InnerText;
					var chapterTotal = authorHtml.ElementAt(1).InnerText;

					_stories.Add(new Story()
					{
						thumnail = img,
						urlDetail = url,
						name = name,
						author = authorName,
						chapterTotal = chapterTotal
					});
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}

			System.Diagnostics.Debug.WriteLine("get category lst done");

			return _stories;
		}

		public string getCategoryMaxPage(string url, string html,int page)
		{
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(html);
			var info = document.DocumentNode.Descendants("ul").Where(n => n.GetAttributeValue("class", "").Equals("pagination"));

			if (info.Count() == 0)
			{
				return "0";
			}
			var _list = info.ElementAt(0).Descendants("li");
			//var _list = info;
			try
			{
				var maxpage = _list.ElementAt(_list.Count() - 2);
				var categoryMaxPageLink = maxpage.ChildNodes["a"].Attributes["href"].Value;
				var urlTemp = url.Replace("/index.html","") + "trang-";
				string[] arrayItem = categoryMaxPageLink.Trim('/').Split('/');
				string[] arrayPage = arrayItem[arrayItem.Length - 1].Split('-');
				string maxPage = "";
				if (arrayPage.Length > 1)
				{
					maxPage = arrayItem[arrayItem.Length - 1].Split('-')[1].Trim('/');
				}
				else
					maxPage = page.ToString();
				return maxPage;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}

			System.Diagnostics.Debug.WriteLine("get max category page done");

			return "0";
		}
		public ObservableCollection<Story> getListStoriesSearch(string html)
		{
			ObservableCollection<Story> _stories = new ObservableCollection<Story>();
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(html);
			var info = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("class", "").Contains("list-truyen"));

			if (info.Count() == 0)
			{
				return _stories;
			}
			var list = info.ElementAt(0).Descendants("div").Where(n => n.GetAttributeValue("class", "").Contains("row"));
			try
			{
				foreach (var item in list)
				{
					var imgHtml = item.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("col-xs-3 col-sm-2 col-md-3 col-list-image"));
					var img = imgHtml.ElementAt(0).ChildNodes["div"].ChildNodes["img"] != null ?
						imgHtml.ElementAt(0).ChildNodes["div"].ChildNodes["img"].Attributes["src"].Value :
						imgHtml.ElementAt(0).ChildNodes["div"].ChildNodes["div"].Attributes["data-image"].Value;
					//var img = imgHtml.ElementAt(0).ChildNodes["div"].ChildNodes["div"].Attributes["data-image"].Value;

					var contentHtml = item.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("col-xs-9 col-sm-10 col-md-9 col-list-info"));
					var url = contentHtml.ElementAt(0).ChildNodes["div"].ChildNodes["h3"].ChildNodes["a"].Attributes["href"].Value;
					var name = contentHtml.ElementAt(0).ChildNodes["div"].ChildNodes["h3"].ChildNodes["a"].Attributes["title"].Value;

					var authorHtml = contentHtml.ElementAt(0).ChildNodes["div"].Descendants("span").Where(n => n.GetAttributeValue("class", "").Equals("author"));
					var authorName = authorHtml.ElementAt(0).InnerText;
					var chapterTotal = authorHtml.ElementAt(1).InnerText;

					_stories.Add(new Story()
					{
						thumnail = img,
						urlDetail = url,
						name = name,
						author = authorName,
						chapterTotal = chapterTotal
					});
				}
			}
			catch (Exception e)
			{
				throw e;
			}

			//System.Diagnostics.Debug.WriteLine("get search lst done");

			return _stories;
		}

		public string getSearchMaxPage(string url, string html,int page)
		{
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(html);
			var info = document.DocumentNode.Descendants("ul").Where(n => n.GetAttributeValue("class", "").Equals("pagination"));

			if (info.Count() == 0)
			{
				return "0";
			}
			var _list = info.ElementAt(0).Descendants("li");
			//var _list = info;
			try
			{
				var maxpage = _list.ElementAt(_list.Count() - 2);
				var searchMaxPageLink = maxpage.ChildNodes["a"].Attributes["href"].Value;
				var urlTemp = url + "&page=";
				string maxPage = "";
				if(searchMaxPageLink.Length > urlTemp.Length)
					maxPage = searchMaxPageLink.Substring(urlTemp.Length, (searchMaxPageLink.Length - urlTemp.Length));
				if(string.IsNullOrEmpty(maxPage))
				{
					maxPage = page.ToString();
				}	
				return maxPage;
			}
			catch (Exception e)
			{
				throw e;
			}

			//System.Diagnostics.Debug.WriteLine("get max search page done");

			return "0";
		}
		public Chapter getDetailChapter(string html)
		{
			Chapter _chapter = new Chapter();
			HtmlDocument document = new HtmlDocument();
			document.LoadHtml(html);
			var info = document.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("id", "").Contains("chapter-big-container"));

			if (info.Count() == 0)
			{
				return _chapter;
			}

			var detailChapterHtml = info.ElementAt(0);
			try
			{
				var nameHtml = detailChapterHtml.Descendants("a").Where(n => n.GetAttributeValue("class", "").Contains("truyen-title")).ElementAt(0);
				var name = nameHtml.Attributes["title"].Value;

				var titleHtml = detailChapterHtml.Descendants("a").Where(n => n.GetAttributeValue("class", "").Contains("chapter-title")).ElementAt(0);
				var title = titleHtml.Attributes["title"].Value;

				var preChapterHtml = detailChapterHtml.Descendants("a").Where(n => n.GetAttributeValue("class", "").Contains("prev-chap")).ElementAt(0);
				var preChapter = preChapterHtml.Attributes["href"].Value;

				var nextChapterHtml = detailChapterHtml.Descendants("a").Where(n => n.GetAttributeValue("class", "").Contains("next-chap")).ElementAt(0);
				var nextChapter = nextChapterHtml.Attributes["href"].Value;

				var content = detailChapterHtml.Descendants("div").Where(n => n.GetAttributeValue("id", "").Equals("chapter-c")).ElementAt(0);

				_chapter.name = name;
				_chapter.title = title;
				_chapter.preChapter = preChapter;
				_chapter.nextChapter = nextChapter;
				_chapter.content = content.InnerHtml;
			}
			catch (Exception e)
			{
				throw e;
			}

		

			return _chapter;
		}

	}
}
