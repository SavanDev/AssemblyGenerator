/*
 * (c) SavanDev 2020 - MIT License
 */
using System;

namespace GitHub
{
	public class Owner
	{
		public string login { get; set; }
	}
	
	public class License
	{
		public string name { get; set; }
	}

	public class Root
	{
		public string name { get; set; }
		public string description { get; set; }
		public Owner owner { get; set; }
		public DateTime created_at { get; set; }
		public License license { get; set; }
	}
}