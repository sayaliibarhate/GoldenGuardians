using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using GoldenGuardians.Models;

namespace GoldenGuardians.Pages
{
	public class RegisterModel : PageModel
	{
		[BindProperty]
		public required Register Reg { get; set; }
		public void OnPost()
		{
			string name = Request.Form["name"];
			string gender = Request.Form["gen"];
			string date = Request.Form["date"];
			string phoneno = Request.Form["phno"];
			string email = Request.Form["email"];
			string password = Request.Form["pass"];
			string city = Request.Form["city"];

			using (MySqlConnection con = new MySqlConnection("server=localhost;username=root;database=gyg;port=3306;password=sau@271202"))
			{
				//opening connection
				con.Open();

				//Inserting values
				MySqlCommand comm = con.CreateCommand();
				comm.CommandText = "INSERT INTO reg(name,gender,date,phoneno,email,password,city) VALUES(?name, ?gender, ?date, ?phoneno, ?email, ?password, ?city)";

				comm.Parameters.AddWithValue("?name", name);
				comm.Parameters.AddWithValue("?gender", gender);
				comm.Parameters.AddWithValue("?date", date);
				comm.Parameters.AddWithValue("?phoneno", phoneno);
				comm.Parameters.AddWithValue("?email", email);
				comm.Parameters.AddWithValue("?password", password);
				comm.Parameters.AddWithValue("?city", city);

				//passing the query and connection var to command
				//MySqlCommand cmd = new MySqlCommand(query, con);

				//Execute command
				comm.ExecuteNonQuery();

				con.Close();
			}

		}
	}
}
