using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.ObjectModel;
using System.Net;
using HtmlAgilityPack;
using MaxStory.Data.Entities;
using MaxStory.Models;
using MaxStory.Application.Interfaces;
using MaxStory.Utilities.Constants;
using System;
using MaxStory.Utilities.Dtos;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace MaxStory.Controllers
{
	public class HomeController : Controller
	{
		private IHomeService _homeService;
		public HomeController(IHomeService homeService)
		{
			_homeService = homeService;
		}
		public async Task<IActionResult> Index()
		{
			var homeVm = new HomeViewModel();
			//System.Diagnostics.Debug.WriteLine("test api:" + test);
			var handler = new System.Net.Http.HttpClientHandler
			{
				AllowAutoRedirect = true,
				UseDefaultCredentials = false,
				UseCookies = false,
				ClientCertificateOptions = System.Net.Http.ClientCertificateOption.Manual,
				AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
			};
			var client = new System.Net.Http.HttpClient(handler);
			client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:49.0) Gecko/20100101 Firefox/49.0");
			client.DefaultRequestHeaders.Add("Host", CommonConstants.Host);
			client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
			client.DefaultRequestHeaders.Add("Accept-Language", "en-US;q=0.5,en;q=0.3");
			client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
			client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

			HttpResponseMessage response1 = null;
			string result = "";
			try
			{
				response1 = await client.GetAsync(CommonConstants.DomainDefault);
				result = await response1.Content.ReadAsStringAsync();
			}
			catch (Exception ex)
			{
				return View();
			}
			HttpStatusCode statusCode = response1.StatusCode;


			try
			{
				/**************** Parse HTML ****************/
				homeVm.HotStories = _homeService.getListStoriesHot(result);
				homeVm.NewStoriesUpdate = _homeService.getListStoriesNewUpdated(result);
				homeVm.FinishedStories = _homeService.getListStoriesFinished(result);
			}
			catch (Exception e)
			{
				throw new Exception("Invalid JSON string");
			}

			return View(homeVm);
		}
		[Route("{alias}-s.{id}.html", Name = "Details")]
		public async Task<IActionResult> Details(string alias, int page = 1)
		{
			Story _storyDetail;
			var detailViewModel = new DetailViewModel();
			detailViewModel.StoryAlias = alias;
			var handler = new System.Net.Http.HttpClientHandler
			{
				AllowAutoRedirect = true,
				UseDefaultCredentials = false,
				UseCookies = false,
				ClientCertificateOptions = System.Net.Http.ClientCertificateOption.Manual,
				AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
			};
			string url = CommonConstants.DomainDefault + alias;
			if(page != 1)
			{
				url = CommonConstants.DomainDefault  + alias + "/" + "trang-" + page + "/#list-chapter";
			}	
			var client = new System.Net.Http.HttpClient(handler);
			client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:49.0) Gecko/20100101 Firefox/49.0");
			client.DefaultRequestHeaders.Add("Host", CommonConstants.Host);
			client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
			client.DefaultRequestHeaders.Add("Accept-Language", "en-US;q=0.5,en;q=0.3");
			client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
			client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

			HttpResponseMessage response1 = null;
			IEnumerable<HtmlNode> sameAuthorLst = Enumerable.Empty<HtmlNode>();
			IEnumerable<HtmlNode> similarLst = Enumerable.Empty<HtmlNode>();
			HtmlNode clHtml = null;
			string result = "";
			try
			{
				response1 = await client.GetAsync(url);
				result = await response1.Content.ReadAsStringAsync();
			}
			catch (Exception e)
			{
				return null;
			}
			HttpStatusCode statusCode = response1.StatusCode;
			try
			{
				/**************** Parse HTML ****************/
				detailViewModel.StoryDetail = _homeService.getStoryDetail(result,ref sameAuthorLst, ref  similarLst,ref clHtml);
				detailViewModel.listStoriesSimilar = _homeService.getListStoriesSimilar(similarLst);
				detailViewModel.listStoriesSameAuthor = _homeService.getListStoriesSameAuthor(sameAuthorLst);
				var cl = _homeService.getChapterList(clHtml);
				int pageCount = int.Parse(detailViewModel.StoryDetail.totalPage);
				var paginationSet = new PagedResult<Chapter>()
				{
					Results = cl,
					CurrentPage = page,
					PageCount = pageCount,
					RowCount = pageCount * 50,
					PageSize = 50
				};
				detailViewModel.Data = paginationSet;
				//detailViewModel.Data.PageCount = int.Parse(detailViewModel.StoryDetail.totalPage);
				//detailViewModel.Data.RowCount = 50 * detailViewModel.Data.PageCount;
				//detailViewModel.Data.CurrentPage = 1;
				//detailViewModel.Data.Results = cl;
			}
			catch (Exception e)
			{
				throw new Exception("Invalid JSON string");
			}
			return View(detailViewModel);
		}

		[Route("{alias}-c.{id}.html", Name = "Content")]
		public async Task<IActionResult> Content(string alias,int id, int chapter = 1)
		{
			Chapter _chapter;
			string url = CommonConstants.DomainDefault  + alias + "/chuong-" +id + "/";
			if(chapter != 1)
			{
				 url = CommonConstants.DomainDefault  + alias + "/chuong-" + chapter + "/";
			}	
			var handler = new System.Net.Http.HttpClientHandler
			{
				AllowAutoRedirect = true,
				UseDefaultCredentials = false,
				UseCookies = false,
				ClientCertificateOptions = System.Net.Http.ClientCertificateOption.Manual,
				AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
			};
			var client = new System.Net.Http.HttpClient(handler);
			client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:49.0) Gecko/20100101 Firefox/49.0");
			client.DefaultRequestHeaders.Add("Host", CommonConstants.Host);
			client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
			client.DefaultRequestHeaders.Add("Accept-Language", "en-US;q=0.5,en;q=0.3");
			client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
			client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

			HttpResponseMessage response1 = null;
			string result = "";
			try
			{
				response1 = await client.GetAsync(url);
				result = await response1.Content.ReadAsStringAsync();
			}
			catch (Exception e)
			{
				throw e;
			}
			HttpStatusCode statusCode = response1.StatusCode;


			try
			{
				/**************** Parse HTML ****************/
				_chapter = _homeService.getDetailChapter(result);
			}
			catch (Exception e)
			{
				throw new Exception("Invalid JSON string");
			}

			System.Diagnostics.Debug.WriteLine("Load content chapter done");
			int currentChapter = id;
			if(chapter != 1)
			{
				currentChapter = chapter;
			}
			_chapter.currentChapter = currentChapter.ToString();
			return View(_chapter);
		}

		[Route("{alias}/{category}/index.html", Name = "Category")]
		public async Task<IActionResult> Category(string alias,string category,int page = 1)
		{
			var catVM = new CategoryViewModel();
			var handler = new System.Net.Http.HttpClientHandler
			{
				AllowAutoRedirect = true,
				UseDefaultCredentials = false,
				UseCookies = false,
				ClientCertificateOptions = System.Net.Http.ClientCertificateOption.Manual,
				AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
			};
			string url = CommonConstants.DomainDefault + alias + "/" + category + "/index.html";
			if(page != 1)
			{
				url = CommonConstants.DomainDefault  + alias + "/" + category + "/trang-"+ page + "/index.html";
			}	
			var client = new System.Net.Http.HttpClient(handler);
			client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:49.0) Gecko/20100101 Firefox/49.0");
			client.DefaultRequestHeaders.Add("Host", CommonConstants.Host);
			client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
			client.DefaultRequestHeaders.Add("Accept-Language", "en-US;q=0.5,en;q=0.3");
			client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
			client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

			HttpResponseMessage response1 = null;
			string result = "";
			try
			{
				response1 = await client.GetAsync(url);
				result = await response1.Content.ReadAsStringAsync();
			}
			catch (Exception e)
			{
				throw e;
			}
			HttpStatusCode statusCode = response1.StatusCode;


			try
			{
				/**************** Parse HTML ****************/
				catVM.Stories = _homeService.getListStoriesCategory(result);

				catVM.MaxPage = _homeService.getCategoryMaxPage(url, result,page);
				var paginationSet = new PagedResult<Story>()
				{
					Results = catVM.Stories,
					CurrentPage = page,
					PageCount = int.Parse(catVM.MaxPage),
					RowCount = int.Parse(catVM.MaxPage) * catVM.Stories.Count,
					PageSize = catVM.Stories.Count
				};
				catVM.Data = paginationSet;
			}
			catch (Exception e)
			{
				throw new Exception("Invalid JSON string");
			}

			return View(catVM);
		}

		[Route("tim-kiem", Name = "Search")]
		public async Task<IActionResult> Search(string tukhoa, int page = 1)
		{
			var catVM = new CategoryViewModel();
			var handler = new System.Net.Http.HttpClientHandler
			{
				AllowAutoRedirect = true,
				UseDefaultCredentials = false,
				UseCookies = false,
				ClientCertificateOptions = System.Net.Http.ClientCertificateOption.Manual,
				AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
			};
			string url = CommonConstants.DomainDefault + "tim-kiem/?tukhoa=" + tukhoa;
			if (page != 1)
			{
				url = CommonConstants.DomainDefault + "tim-kiem/?tukhoa=" + tukhoa + "&page=" + page;
			}
			var client = new System.Net.Http.HttpClient(handler);
			client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:49.0) Gecko/20100101 Firefox/49.0");
			client.DefaultRequestHeaders.Add("Host", CommonConstants.Host);
			client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
			client.DefaultRequestHeaders.Add("Accept-Language", "en-US;q=0.5,en;q=0.3");
			client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
			client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

			HttpResponseMessage response1 = null;
			string result = "";
			try
			{
				response1 = await client.GetAsync(url);
				result = await response1.Content.ReadAsStringAsync();
			}
			catch (Exception e)
			{
				throw e;
			}
			HttpStatusCode statusCode = response1.StatusCode;


			try
			{
				/**************** Parse HTML ****************/
				catVM.Stories = _homeService.getListStoriesSearch(result);

				catVM.MaxPage = _homeService.getSearchMaxPage(url, result,page);

				var paginationSet = new PagedResult<Story>()
				{
					Results = catVM.Stories,
					CurrentPage = page,
					PageCount = int.Parse(catVM.MaxPage),
					RowCount = int.Parse(catVM.MaxPage) * catVM.Stories.Count,
					PageSize = catVM.Stories.Count
				};
				catVM.Data = paginationSet;
			}
			catch (Exception e)
			{
				throw new Exception("Invalid JSON string");
			}

			return View(catVM);
		}
	}
}
