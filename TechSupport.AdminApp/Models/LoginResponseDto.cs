namespace TechSupport.AdminApp.Models
{
    // Backend bejelentkezési válasz DTO
    public class LoginResponseDto
    {
        // JWT token
        public string Token { get; set; }
        
        // Felhasználó szerepe (pl. admin, user)
        public string Role { get; set; }
        
        // Bejelentkezési felhasználónév
        public string UserName { get; set; }
        
        // Felhasználó email cím
        public string UserEmail { get; set; }
    }
}
