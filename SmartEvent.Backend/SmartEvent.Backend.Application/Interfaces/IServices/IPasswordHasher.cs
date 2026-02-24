using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Backend.Application.Interfaces.IServices
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string hash, string password);
    }
}
