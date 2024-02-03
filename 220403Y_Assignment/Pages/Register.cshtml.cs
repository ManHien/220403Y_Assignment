using _220403Y_Assignment.Model;
using _220403Y_Assignment.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _220403Y_Assignment.Pages
{
	public class RegisterModel : PageModel
	{
		private UserManager<ApplicationUser> userManager { get; }
		private SignInManager<ApplicationUser> signInManager { get; }
		[BindProperty]
		public Register RModel { get; set; }
		public RegisterModel(UserManager<ApplicationUser> userManager,
		SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
		public void OnGet()
        {
        }
		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				// Check for duplicate email
				var existingUser = await userManager.FindByEmailAsync(RModel.Email);
				if (existingUser != null)
				{
					ModelState.AddModelError("RModel.Email", "Email address is already in use.");
					return Page();
				}

				var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
				var protector = dataProtectionProvider.CreateProtector("MySecretKey");

				// Create a new user if the email is not in use
				var user = new ApplicationUser()
				{
					UserName = RModel.Email,
					Email = RModel.Email,
					Billing_Address = RModel.Billing_Address,
					Shipping_Address = RModel.Shipping_Address,
					Mobile_No = RModel.Mobile_No,
					First_Name = RModel.First_Name,
					Last_Name = RModel.Last_Name,
					Credit_No = protector.Protect(RModel.Credit_No),
					Photo = RModel.Photo,
				};

				var result = await userManager.CreateAsync(user, RModel.Password);
				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user, false);
					return RedirectToPage("Index");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}

			return Page();
		}

	}

}
