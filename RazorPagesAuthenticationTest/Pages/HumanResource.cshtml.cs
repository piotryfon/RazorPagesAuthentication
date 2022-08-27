using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesAuthenticationTest.Pages
{
    [Authorize(Policy = "MustBelongToHRDepartmnt")]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
