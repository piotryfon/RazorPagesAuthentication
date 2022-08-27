using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesAuthenticationTest.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()//propaguje na front
        {
            //this.Credential = new Credential 
            //{ 
            //    UserName = "admin", 
            //    Password = "1234" 
            //};
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) 
            {
                return Page();
            }

            if (Credential.UserName == "admin" && Credential.Password == "1234")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@o2.pl"),
                    new Claim("Department", "HR"),
                    new Claim("Admin", ""),
                    //new Claim("Manager", ""),
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal); //serializuje na string, szyfruje i zapisuje jako cookie

                return RedirectToPage("/Index", TempData["Message"] = $"Logged as {Credential.UserName}");
            }

            TempData["Message"] = $"Login failed";

            return Page();
        }
    }
    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
