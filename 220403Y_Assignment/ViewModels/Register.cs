using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace _220403Y_Assignment.ViewModels
{
	public class Register
	{
        [Required(ErrorMessage = "First Name is required.")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "First Name must be alphabetical only.")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last Name must be alphabetical only.")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Credit Card Number is required.")]
        [DataType(DataType.CreditCard)]
        [CreditCard(ErrorMessage = "Invalid Credit Card Number.")]
        public string Credit_No { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]{8,}$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile_No { get; set; }

        [Required(ErrorMessage = "Billing Address is required.")]
        [DataType(DataType.Text)]
        public string Billing_Address { get; set; }

        [Required(ErrorMessage = "Shipping Address is required.")]
        [DataType(DataType.Text)]
        public string Shipping_Address { get; set; }

        [Required(ErrorMessage = "Email Address is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zAZ]).{12,}$", ErrorMessage = "Password does not meet the required criteria.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Photo is required.")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "jpg", ErrorMessage = "Photo must be in .JPG format.")]
        public string Photo { get; set; }

        public bool IsPhotoValid()
        {
            string[] allowedExtensions = { ".jpg" };
            string extension = Path.GetExtension(Photo);
            return allowedExtensions.Contains(extension.ToLower());
        }
    }
}
