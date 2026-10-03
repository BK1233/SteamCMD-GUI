#nullable enable
using System.Text.RegularExpressions;

namespace SteamCMD_GUI
{
    public static class ArgumentSanitizer
    {
        public static string? Sanitize(string? args)
        {
            if (string.IsNullOrEmpty(args))
            {
                return args;
            }

            // Redact +rcon_password values (e.g. +rcon_password "secret" or +rcon_password secret)
            string sanitized = Regex.Replace(
                args,
                @"(\+rcon_password\s+)(?:""[^""]*""|\S+)",
                "$1\"[REDACTED]\"",
                RegexOptions.IgnoreCase);

            // Redact +login <user> <pass> (excluding anonymous)
            sanitized = Regex.Replace(
                sanitized,
                @"(\+login\s+(?!anonymous\b)\S+\s+)(?:""[^""]*""|\S+)",
                "$1\"[REDACTED]\"",
                RegexOptions.IgnoreCase);

            return sanitized;
        }
    }
}
