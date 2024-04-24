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
	public class AdminProfileModel : PageModel
	{
		public List<Admin> clients =  new List<Admin>();	
		public void OnGet()
		{
			string constr = "server=localhost;username=root;database=gyg;port=3306;password=sau@271202";
			using (MySqlConnection con = new MySqlConnection(constr))
			{
				string query = "SELECT * FROM purchase p left join members m on p.name = m.name";
				using (MySqlCommand cmd = new MySqlCommand(query, con))
				{
					con.Open();
					using (MySqlDataReader sda = cmd.ExecuteReader())
					{
						
						while (sda.Read())
						{
							Admin admin = new Admin();
							admin.Id = sda.GetInt32(0);
							admin.Name = sda.GetString(1);
							admin.Email = sda.GetString(5);
							admin.Mobileno = sda.GetString(2);
							admin.Relation = sda.GetString(3);
							admin.Package = sda.GetString(4);
							admin.Bloodgrp = sda.GetString(6);
							admin.Medicalhist = sda.GetString(7);
							clients.Add(admin);
						}
					}
					con.Close();
				}
			}
		}
	}
}
