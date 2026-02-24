using SmartEvent.Backend.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;

namespace SmartEvent.Backend.Application.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<object> _passwordHasher = new();
        public string Hash(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool Verify(string hash, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
