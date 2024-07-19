﻿namespace PoliziaWebApp.Models
{
    public class Utente
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public List<string> Roles { get; set; } = [];
    }
}
