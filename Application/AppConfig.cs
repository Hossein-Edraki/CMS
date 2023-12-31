﻿namespace Application
{
    public class AppConfig
    {
        public JwtOptions JwtOptions { get; set; }
    }

    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public string Encryptkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
        public int RefreshTokenExpirationDays { get; set; }
    }
}
