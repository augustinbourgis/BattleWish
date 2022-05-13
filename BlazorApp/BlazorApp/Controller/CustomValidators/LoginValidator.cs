using DataLibrary;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using BlazorApp.Models;
namespace BlazorApp.Controller.CustomValidators
{
    
    public class LoginValidator : ValidationAttribute
    {
        [Inject]
        protected IDataAccess Data { get; set; } = default!;
        [Inject]
        protected IConfiguration Config { get; set; } = default!;


        public bool IsUnique { get; set; }
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            LoginValidation(value);
            if (IsUnique == true)
            {
                return null;
            }

            return new ValidationResult($"Login already taken",
            new[] { validationContext.MemberName });
        }

        private async Task LoginValidation(object value)
        {
            List<UserModel> user;
            IsUnique = false;
            string sql = "select login from user where login like'" + value.ToString() + "'";

            user = await Data.LoadData<UserModel, dynamic>(sql, new { }, Config.GetConnectionString("default"));

            if (user == null)
            {
                IsUnique = true;
            }
        }

    }
}
