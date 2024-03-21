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

			using (MySqlConnection con = new MySqlConnection("server=localhost;username=root;database=gyg;port=3306;password=sau@271202"))
			{
				//opening connection
				con.Open();

				string query = "select * from members where email = ?email and password = ?password";



				MySqlCommand cmd = new MySqlCommand(query, con);
				cmd.Parameters.AddWithValue("?email", email);
				cmd.Parameters.AddWithValue("?password", password);

				int i = cmd.ExecuteNonQuery();
				con.Close();

				if (i == 1)
				{
					return RedirectToPage("/Profile");
				}
				else
				{
					return RedirectToPage("/Login");
				}
				con.Close(); 

			}

		}

	}
}
