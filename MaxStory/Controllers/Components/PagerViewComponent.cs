using MaxStory.Utilities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MaxStory.Controllers.Components
{
	public class PagerViewComponent : ViewComponent
	{
		public Task<IViewComponentResult> InvokeAsync(PagedResultBase result)
		{
			return Task.FromResult((IViewComponentResult)View("Default", result));
		}
	}
}
