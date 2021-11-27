using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Zero_Hunger.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync("ZeroHungerCookie");
            return RedirectToPage("/login");
        }
    }
}
