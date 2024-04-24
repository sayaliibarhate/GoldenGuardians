using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using GoldenGuardians.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using MySqlX.XDevAPI.Common;
using System;
using Mysqlx.Resultset;

namespace GoldenGuardians.Pages
{
    public class LoginModel : PageModel
    {
		[BindProperty]
		public required Login log { get; set; }

		public RedirectToPageResult OnPost()
		{
			string email = Request.Form["email"];
			string password = Request.Form["pass"];
			string utype = Request.Form["utype"];

			using (MySqlConnection con = new MySqlConnection("server=localhost;username=root;database=gyg;port=3306;password=sau@271202"))
			{
				//opening connection
				con.Open();

				string query = "select utype from members where email = ?email and password = ?password";



				MySqlCommand cmd = new MySqlCommand(query, con);
				cmd.Parameters.AddWithValue("?email", email);
				cmd.Parameters.AddWithValue("?password", password);

				var i  = cmd.ExecuteScalar()?.ToString();
				con.Close();

                if (!string.IsNullOrEmpty(utype))
                {
                    // Redirect based on the user type
                    switch (i)
                    {
                        case "Admin":
                            return RedirectToPage("/AdminProfile");
                        case "Advisor":
                            return RedirectToPage("/AdvisorProfile");
                        case "Client":
                            return RedirectToPage("/UserProfile");
                        default:
                            return RedirectToPage("/Login"); // Redirect to login if no matching utype found
                    }
                }
                else
                {
                    return RedirectToPage("/Login"); // Redirect to login if no record found matching the credentials
                }
            }
        }

    }

		}




