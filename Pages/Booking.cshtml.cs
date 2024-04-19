using GoldenGuardians.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace GoldenGuardians.Pages
{
    public class BookingModel : PageModel
    {
		[BindProperty]
		public required Booking book { get; set; }
		public void OnPost()
		{
			string sname = Request.Form["sname"];
			string mobileno = Request.Form["srel"];
			string relation = Request.Form["package"];
			string package = Request.Form["smno"];
			string email = Request.Form["semail"];
			string bloodgrp = Request.Form["sbloodgrp"];
			string medicalhist = Request.Form["medhist"];

			using (MySqlConnection con = new MySqlConnection("server=localhost;username=root;database=gyg;port=3306;password=sau@271202"))
			{
				//opening connection
				con.Open();

				//Inserting values
				MySqlCommand comm = con.CreateCommand();
				comm.CommandText = "INSERT INTO purchase(name,mobileno,relation,package,email,bloodgrp,medicalhist) VALUES(?name, ?mobileno, ?relation, ?package, ?email, ?bloodgrp, ?medicalhist)";

				comm.Parameters.AddWithValue("?name", sname);
				comm.Parameters.AddWithValue("?mobileno", mobileno);
				comm.Parameters.AddWithValue("?relation", relation);
				comm.Parameters.AddWithValue("?package", package);
				comm.Parameters.AddWithValue("?email", email);
				comm.Parameters.AddWithValue("?bloodgrp", bloodgrp);
				comm.Parameters.AddWithValue("?medicalhist", medicalhist);

				//passing the query and connection var to command
				//MySqlCommand cmd = new MySqlCommand(query, con);

				//Execute command
				comm.ExecuteNonQuery();

				con.Close();
			}
		}
    }
}
