/*
 * Creado por SharpDevelop.
 * Fecha: 26/09/2020
 * SavanDev - MIT License
 */
using System;
using System.IO;
using System.Net;
using System.Text;
using GitHub;
using Newtonsoft.Json;

namespace SavanDev
{
	class Program
	{
		public static void Main(string[] args)
		{
			string majorVersion = "1";
			string minorVersion = "0";
			string repository = null;
			bool helpOnly = false;
			
			Console.WriteLine("AssemblyInfo Generator [Version " + new Program().GetAssemblyVersion() + "]");
			Console.WriteLine("(SavanDev 2020 - MIT License)\n");
			
			for (int i = 0; i < args.Length; i++) {
				switch (args[i]) {
					case "--repo":
						repository = args[i+1];
						break;
					case "--major":
						majorVersion = args[i+1];
						break;
					case "--minor":
						minorVersion = args[i+1];
						break;
					case "--help":
						Console.WriteLine("--repo <User/Repo>: Set GitHub repository to get the data.");
						Console.WriteLine("--major <number>: Set major version.");
						Console.WriteLine("--minor <number>: Set minor version.");
						Console.WriteLine("--help: Shows the help screen (this).");
						helpOnly = true;
						break;
				}
			}
			
			if (repository != null)
			{
				Console.WriteLine("Getting response from GitHub...");
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				WebClient client = new WebClient();
				client.Headers.Add(HttpRequestHeader.UserAgent, "Terminal");
				string json = client.DownloadString("https://api.github.com/repos/" + repository);
				Root github = JsonConvert.DeserializeObject<Root>(json);
				
				short buildNumber = (short)(DateTime.Now - github.created_at).TotalDays;
				
				StringBuilder assemblyContent = new StringBuilder();
				assemblyContent.AppendLine("using System;");
				assemblyContent.AppendLine("using System.Reflection;");
				assemblyContent.AppendLine("using System.Runtime.InteropServices;").AppendLine();
				
				assemblyContent.AppendLine("[assembly: AssemblyCompany(\"" + github.owner.login + "\")]");
				assemblyContent.AppendLine("[assembly: AssemblyTitle(\"" + github.description + "\")]");
				assemblyContent.AppendLine("[assembly: AssemblyProduct(\"" + github.name + "\")]");
				assemblyContent.AppendLine("[assembly: ComVisible(false)]");
				assemblyContent.AppendLine(string.Format("[assembly: AssemblyVersion(\"{0}.{1}.{2}.*\")]", majorVersion, minorVersion, buildNumber));
				File.WriteAllText("AssemblyInfo.cs", assemblyContent.ToString());
				Console.WriteLine("Success!");
			}
			else if (!helpOnly)
			{
				Console.WriteLine("ERROR: The GitHub repository has not been set up.");
			}
		}
		
		public string GetAssemblyVersion()
		{
			return GetType().Assembly.GetName().Version.ToString();
		}
	}
}