using _220403Y_Assignment.Model;
using _220403Y_Assignment.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

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

                // Validate password using regex pattern
                var passwordRegex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zAZ]).{12,}$");
                if (!passwordRegex.IsMatch(RModel.Password))
                {
                    ModelState.AddModelError("RModel.Password", "Password does not meet the required criteria.");
                    return Page();
                }
                // Validate First_Name and Last_Name are alphabetical only
                if (!IsAlphabetical(RModel.First_Name) || !IsAlphabetical(RModel.Last_Name))
                {
                    ModelState.AddModelError("RModel.First_Name", "First Name must be alphabetical only.");
                    ModelState.AddModelError("RModel.Last_Name", "Last Name must be alphabetical only.");
                    return Page();
                }

                // Validate Mobile_No is a valid phone number
                if (!IsValidPhoneNumber(RModel.Mobile_No))
                {
                    ModelState.AddModelError("RModel.Mobile_No", "Invalid Mobile Number.");
                    return Page();
                }

                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                // Create a new user if the email is not in use and the password is valid
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
        private bool IsAlphabetical(string input)
        {
            // Check if the input consists of alphabetical characters only
            return input.All(char.IsLetter);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Add your logic to validate phone number format
            // Example: Use regex to check for a valid phone number pattern
            var phoneRegex = new Regex("^[0-9]{8,}$");
            return phoneRegex.IsMatch(phoneNumber);
        }
    }

}
