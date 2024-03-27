using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgeUpUI.Controllers
{
 
    public class ErrorPage : Controller
    {
		
			public IActionResult Error(int code)
			{
				if (code == 404)
				{
					return View("Error404");
				}
				else if (code == 500)
				{
					return View("Error500");
				}
		
				return View("Error404");
			}

			public IActionResult Error500()
			{
				
				return View("Error500");
			}
		public IActionResult Error404()
		{
			
			return View("Error404");
		}
	}
	}
