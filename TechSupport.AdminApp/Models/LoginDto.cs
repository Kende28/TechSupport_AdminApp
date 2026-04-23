using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechSupport.AdminApp.Models
{
    // Backend bejelentkezési kérés DTO
    public class LoginDto
    {
        // Konstruktor hitelesítési adatokkal
        public LoginDto(string userEmailName, string userPassword)
        {
            this.userEmailName = userEmailName;
            this.userPassword = userPassword;
        }

        // A backend a felhasználónév/jelszó kulcsokból ezen nevekre számít (kis-/nagybetű érzékeny)
        // Email vagy felhasználónév
        public string userEmailName { get; set; }

        // Felhasználó jelszava
        public string userPassword { get; set; }
    }
}
