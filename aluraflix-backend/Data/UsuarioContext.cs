using System;
using aluraflix_backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aluraflix_backend.Data
{
    public class UsuarioContext : IdentityDbContext<Usuario>
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        { }
    }
}