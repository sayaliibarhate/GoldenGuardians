using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using GoldenGuardians.Models;

namespace GoldenGuardians.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public required Login log { get; set; }
		public void OnPost()
        {
              string email = Request.Form["email"];
              string password = Request.Form["pass"];
        }
    }
}
