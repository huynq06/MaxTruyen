using Microsoft.AspNetCore.Mvc;

namespace MaxStory.Controllers
{
	public class ChapterController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
