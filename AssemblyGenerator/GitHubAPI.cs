/*
 * Creado por SharpDevelop.
 * Fecha: 26/09/2020
 * SavanDev - MIT License
 */
using System;

namespace GitHub
{
	public class Owner
	{
		public string login;
	}

	public class Root
	{
		public string name;
		public string description;
		public Owner owner;
		public DateTime created_at;
	}
}