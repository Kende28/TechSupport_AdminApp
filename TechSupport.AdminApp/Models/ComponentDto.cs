using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.AdminApp.Models
{
	public class ComponentDto
	{
		public int id { get; set; }
		public string name { get; set; }
		public string brand { get; set; }
		public string category { get; set; }
		public int price { get; set; }
	}
}
