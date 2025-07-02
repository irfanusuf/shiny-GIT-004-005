using System;

namespace Armado_Razor.Models;

  public class User
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password  { get; set; }

    }