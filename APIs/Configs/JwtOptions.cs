﻿namespace APIs.Configs
{
    public class JwtOptions
    {
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required string Lifetime { get; set; }
        public required string SigningKey { get; set; }
    }
}
