

using BookCatalog.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BookCatalog.DataAccess.Services
{
    public class UsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersService(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task RegisterAsync(User user, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(user.UserName!);
            if (existingUser != null)
                throw new InvalidDataException("Az email cím már foglalt");

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new InvalidDataException($"A felhasználó létrehozása sikertelen.");
        }

        public async Task<User> LoginAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new AccessViolationException("A felhasználónév vagy jelszó hibás");

            var result = await _signInManager.PasswordSignInAsync(user.UserName!, password, false, true);
            if (result.IsLockedOut)
                throw new AccessViolationException("Túl sok sikertelen kísérlet");
            if (!result.Succeeded)
                throw new AccessViolationException("A felhasználónév vagy jelszó hibás");

            await _signInManager.SignInAsync(user, isPersistent: false);

            user.ResetAccessToken();
            await _userManager.UpdateAsync(user);

            return user;
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();
            if (userId == null)
                return null;

            return await _userManager.FindByIdAsync(userId);
        }

        private string? GetCurrentUserId()
        {
            var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier); 

            return id;
        }

        public async Task LogoutAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                user.ResetAccessToken();
                await _userManager.UpdateAsync(user);
            }
            await _signInManager.SignOutAsync();
        }
    }
}
