using Microsoft.AspNetCore.Identity;
using SmartEvent.Backend.Application.Interfaces.IServices;

namespace SmartEvent.Backend.Infrastructure.Security
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
