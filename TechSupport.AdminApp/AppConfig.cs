using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TechSupport.AdminApp
{
    // Globális alkalmazás beállítások és közös adatok
    public static class AppConfig
    {
        // Backend API alap URL
        public static string ApiBaseUrl { get; } = "http://localhost:3000/";
        
        // Bejelentkezéskor kapott hitelesítési token
        public static string BearerToken { get; set; } = string.Empty;
        
        // Jelenlegi felhasználó név
        public static string CurrentUserName { get; set; } = string.Empty;
        
        // Jelenlegi felhasználó email
        public static string CurrentUserEmail { get; set; } = string.Empty;
    }
}

