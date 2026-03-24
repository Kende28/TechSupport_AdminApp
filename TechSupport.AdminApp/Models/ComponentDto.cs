using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.AdminApp.Models
{
	public class ComponentDto
	{
		public int PartId { get; set; }
		public string PartName { get; set; }
		public string? PartDescription { get; set; }
		public bool PartVisible { get; set; }
	}
}
