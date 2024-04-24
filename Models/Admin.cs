using System.ComponentModel.DataAnnotations;

namespace GoldenGuardians.Models
{
	public class Admin
	{
		[Key] public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Mobileno { get; set; }
		public string Relation { get; set; }
		public string Package { get; set; }
		public string Bloodgrp { get; set; }
		public string Medicalhist { get; set; }
	}
}
