namespace BlazorApp.Controller
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class SignUp
    {

        [Required]
        [StringLength(20, ErrorMessage = "Login too long (20 character limit).")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public bool? IsMale { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password too long (100 character limit).")]
        [RegularExpression(@"^(?=.*[\W])(?=.*[0-9])(?=.*[a-z]).{8,128}$", ErrorMessage = "The password must have at least 1 number, 1 special character and a length of at least 8 characters")]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password must be similar")]
        public string? ConfirmPassword { get; set; }

    }
}
