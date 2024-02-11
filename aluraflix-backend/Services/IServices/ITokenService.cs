using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aluraflix_backend.Models;

namespace aluraflix_backend.Services.IServices
{
    public interface ITokenService
    {
        string GenerateToken(Usuario usuario);
    }
}