

namespace Lands.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string token = "Token";
        private const string tokenType = "TokenType";
        private static readonly string stringDefault  = string.Empty;

        #endregion

            
        public static string Token
        {
            get
            {
                return AppSettings.GetValueOrDefault(token, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(token, value);
            }
        }

        public static string TokenType
        {
            get
            {
                return AppSettings.GetValueOrDefault(tokenType, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(tokenType, value);
            }
        }


    }
}