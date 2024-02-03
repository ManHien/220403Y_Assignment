using Microsoft.AspNetCore.Identity;

namespace _220403Y_Assignment.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set;}
        public string Credit_No { get; set; }
        public string Mobile_No { get; set; }
        public string Billing_Address {  get; set; }
        public string Shipping_Address {  get; set; }
        public string Photo {  get; set; }

    }
}
