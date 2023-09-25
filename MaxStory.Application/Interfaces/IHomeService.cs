using HtmlAgilityPack;
using MaxStory.Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxStory.Application.Interfaces
{
	public interface IHomeService
	{
		ObservableCollection<Story> getListStoriesHot(string html);
		ObservableCollection<Story> getListStoriesNewUpdated(string html);
		ObservableCollection<Story> getListStoriesFinished(string html);
		Story getStoryDetail(string html, ref IEnumerable<HtmlNode> sameAuthorLst, ref IEnumerable<HtmlNode> similarLst,ref HtmlNode clHtml);
		List<Chapter> getChapterList(HtmlNode html);
		ObservableCollection<Story> getListStoriesSameAuthor(IEnumerable<HtmlNode> _htmlNode);
		ObservableCollection<Story> getListStoriesSimilar(IEnumerable<HtmlNode> _htmlNode);
		ObservableCollection<Story> getListStoriesCategory(string html);
		ObservableCollection<Story> getListStoriesSearch(string html);
		string getCategoryMaxPage(string url, string html,int page);
		string getSearchMaxPage(string url, string html, int page);
		Chapter getDetailChapter(string html);
	}
}
