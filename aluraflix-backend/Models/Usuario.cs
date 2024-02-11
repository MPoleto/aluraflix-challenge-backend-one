using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace aluraflix_backend.Models
{
    public class Usuario : IdentityUser
    {
        public Usuario() : base() { }
    }
}