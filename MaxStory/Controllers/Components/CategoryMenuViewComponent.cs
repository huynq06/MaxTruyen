﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MaxStory.Controllers.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
