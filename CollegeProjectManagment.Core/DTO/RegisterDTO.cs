using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.DTO;

public class RegisterDTO
{
    [Required(ErrorMessage = "Person Name can't be blank")]
    public string PersonName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email can't be blank")]
    [EmailAddress(ErrorMessage = "Email should be in a proper email address format")]
    [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already is use")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password can't be blank")]
    public string Password { get; set; } = string.Empty;

    public string Role { get; set; }
}