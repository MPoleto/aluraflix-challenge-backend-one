using System;
using System.ComponentModel.DataAnnotations;

namespace aluraflix_backend.Data.DTOs;
    public class CreateUsuarioDTO
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }
    }