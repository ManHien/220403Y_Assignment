using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace _220403Y_Assignment.ViewModels
{
	public class Register
	{
		[Required]
		[DataType(DataType.Text)]
		public string First_Name { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string Last_Name { get; set; }

		[Required]
		[DataType(DataType.CreditCard)]
		[CreditCard]
		public string Credit_No { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		public string Mobile_No { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string Billing_Address { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string Shipping_Address { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Required]
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
