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
                @"(\+rcon_password\s+)(?:""(?:\\""|[^""])*""|\S+)",
                "$1\"[REDACTED]\"",
                RegexOptions.IgnoreCase);

            // Redact +login <user> <pass> (excluding anonymous)
            sanitized = Regex.Replace(
                sanitized,
                @"(\+login\s+(?!anonymous\b)(?:""(?:\\""|[^""])*""|\S+)\s+)(?:""(?:\\""|[^""])*""|\S+)",
                "$1\"[REDACTED]\"",
                RegexOptions.IgnoreCase);

            return sanitized;
        }

        public static string EscapeArgument(string? argument)
        {
            if (string.IsNullOrEmpty(argument))
            {
                return "\"\"";
            }

            var sb = new System.Text.StringBuilder();
            sb.Append('"');

            for (int i = 0; i < argument.Length; i++)
            {
                int backslashCount = 0;
                while (i < argument.Length && argument[i] == '\\')
                {
                    backslashCount++;
                    i++;
                }

                if (i == argument.Length)
                {
                    sb.Append('\\', backslashCount * 2);
                    break;
                }
                else if (argument[i] == '"')
                {
                    sb.Append('\\', backslashCount * 2 + 1);
                    sb.Append('"');
                }
                else
                {
                    sb.Append('\\', backslashCount);
                    sb.Append(argument[i]);
                }
            }

            sb.Append('"');
            return sb.ToString();
        }
    }
}
