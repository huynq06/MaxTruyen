using Microsoft.AspNetCore.Mvc;

namespace MaxStory.Controllers
{
	public class StoryController : Controller
	{
		//[Route("{alias}.html", Name = "StoryDetail")]
		public IActionResult Index(string url)
		{
			return View();
		}
	}
}
