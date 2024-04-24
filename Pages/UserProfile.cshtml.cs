using GoldenGuardians.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Net.Cache;

namespace GoldenGuardians.Pages
{
	public class UserProfileModel : PageModel
	{
		public List<Register> person = new List<Register>();
		public void OnGet()
		{
			string constr = "server=localhost;username=root;database=gyg;port=3306;password=sau@271202";
			using (MySqlConnection con = new MySqlConnection(constr))
			{
				string query = "SELECT * FROM register";
				using (MySqlCommand cmd = new MySqlCommand(query, con))
				{
					con.Open();
					using (MySqlDataReader sda = cmd.ExecuteReader())
					{

						while (sda.Read())
						{
							Register user = new Register();
							
							user.Name = sda.GetString(1);
							user.Email = sda.GetString(5);
							user.Phoneno = sda.GetString(4);
							user.Birthdate = sda.GetString(3);
							user.Gender = sda.GetString(2);
							user.City = sda.GetString(7);
						
							person.Add(user);
						}
					}
					con.Close();
				}
			}
		}
	}
}
