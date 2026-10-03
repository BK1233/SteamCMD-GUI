using SteamCMD_GUI;
using Xunit;

namespace SteamCMD_GUI.Tests
{
    public class ArgumentSanitizerTests
    {
        [Fact]
        public void Sanitize_RedactsRconPasswordInQuotes()
        {
            string input = "-console -game tf +maxplayers 24 +map 2fort -port 27015 +rcon_password \"secret123\"";
            string expected = "-console -game tf +maxplayers 24 +map 2fort -port 27015 +rcon_password \"[REDACTED]\"";

            string? result = ArgumentSanitizer.Sanitize(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Sanitize_RedactsRconPasswordWithoutQuotes()
        {
            string input = "-console -game tf +maxplayers 24 +map 2fort -port 27015 +rcon_password mysecret";
            string expected = "-console -game tf +maxplayers 24 +map 2fort -port 27015 +rcon_password \"[REDACTED]\"";

            string? result = ArgumentSanitizer.Sanitize(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Sanitize_RedactsEmptyRconPassword()
        {
            string input = "-console -game tf +maxplayers 24 +map 2fort -port 27015 +rcon_password \"\"";
            string expected = "-console -game tf +maxplayers 24 +map 2fort -port 27015 +rcon_password \"[REDACTED]\"";

            string? result = ArgumentSanitizer.Sanitize(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Sanitize_RedactsLoginPassword()
        {
            string input = "+login user123 pass456 +force_install_dir \"C:\\server\"";
            string expected = "+login user123 \"[REDACTED]\" +force_install_dir \"C:\\server\"";

            string? result = ArgumentSanitizer.Sanitize(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Sanitize_PreservesAnonymousLogin()
        {
            string input = "+login anonymous +force_install_dir \"C:\\server\"";
            string expected = "+login anonymous +force_install_dir \"C:\\server\"";

            string? result = ArgumentSanitizer.Sanitize(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Sanitize_HandlesNullOrEmpty()
        {
            Assert.Null(ArgumentSanitizer.Sanitize(null));
            Assert.Equal("", ArgumentSanitizer.Sanitize(""));
        }

        [Fact]
        public void Sanitize_PreservesNormalArgsWithoutSensitiveData()
        {
            string input = "-console -game cstrike +maxplayers 16 +map de_dust2 -port 27015";

            string? result = ArgumentSanitizer.Sanitize(input);

            Assert.Equal(input, result);
        }

        [Theory]
        [InlineData("simpleUser", "\"simpleUser\"")]
        [InlineData("user name", "\"user name\"")]
        [InlineData("user\"inj", "\"user\\\"inj\"")]
        [InlineData("pass\" +quit +app_update 740", "\"pass\\\" +quit +app_update 740\"")]
        [InlineData("C:\\Program Files\\Server\\", "\"C:\\Program Files\\Server\\\\\"")]
        [InlineData("", "\"\"")]
        [InlineData(null, "\"\"")]
        public void EscapeArgument_EscapesSpecialCharactersCorrectly(string? input, string expected)
        {
            string result = ArgumentSanitizer.EscapeArgument(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Sanitize_RedactsEscapedQuotedLoginPassword()
        {
            string input = "+login \"user\\\"name\" \"pass\\\"word\" +force_install_dir \"C:\\\\server\"";
            string expected = "+login \"user\\\"name\" \"[REDACTED]\" +force_install_dir \"C:\\\\server\"";

            string? result = ArgumentSanitizer.Sanitize(input);

            Assert.Equal(expected, result);
        }
    }
}
