﻿
namespace ScavengerHunt.Domain.Entities
{
    public sealed class Participant
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
