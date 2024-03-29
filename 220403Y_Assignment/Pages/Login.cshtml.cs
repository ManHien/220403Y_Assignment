using _220403Y_Assignment.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using _220403Y_Assignment.googlecaptcha;
using _220403Y_Assignment.Model;

namespace _220403Y_Assignment.Pages
{


	public class LoginModel : PageModel
	{
		private SignInManager<ApplicationUser> signInManager { get; }

		private GoogleCaptchaService _captchaService { get; }
		[BindProperty]
		public Login LModel { get; set; }
		public LoginModel(SignInManager<ApplicationUser> signInManager, GoogleCaptchaService captchaService)
		{
			this.signInManager = signInManager;
			_captchaService = captchaService;
		}
		public void OnGet()
		{
		}
		public async Task<IActionResult> OnPostAsync()
		{

            //verifiying the response
            var captchaResult = await _captchaService.ValidateToken(LModel.Token);
				if (!captchaResult)
			{
				return Page();
			}
			if (ModelState.IsValid)
			{
				var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, lockoutOnFailure: true);

				if (identityResult.Succeeded)
				{
                    HttpContext.Session.SetString("UserID", LModel.Email);
                    return RedirectToPage("Index");
				}
                if (identityResult.IsLockedOut)
                {
                    ViewData["ErrorMessage"] = "Your account is locked. Please try again later.";
                }
                else
                {
                    ViewData["ErrorMessage"] = "Email or password incorrect";
                }
            }

			return Page();
		}

	}
}