using Microsoft.AspNetCore.Mvc;

namespace EgeUpUI.Controllers
{
    public class LayoutController : Controller
    {
        public IActionResult _HeaderPartialLayout()
        {
            return PartialView();
        }
        public IActionResult _ScriptPartial()
        {
            return PartialView();
        }
        public IActionResult _NavBarPartial()
        {
            return PartialView();
        }
        public IActionResult _AdminNavBarPartial()
        {
            return PartialView();
        }
    }
}
