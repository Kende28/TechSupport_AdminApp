using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.AdminApp.Models
{
	public class LoginDto
	{
        public LoginDto(string userEmailName, string userPassword)
        {
            this.userEmailName = userEmailName;
            this.userPassword = userPassword;
        }

        public string userEmailName { get; set; }
		public string userPassword { get; set; }
	}
}
