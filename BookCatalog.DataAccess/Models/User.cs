
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace BookCatalog.DataAccess.Models
{
    public class User : IdentityUser
    {
        [MaxLength(100)]
        public string AccessToken { get; set; } = GenerateOpaqueToken();

        public void ResetAccessToken()
        {
            AccessToken = GenerateOpaqueToken();
        }

        private static string GenerateOpaqueToken()
        {
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
