using System;
using System.Globalization;

namespace Irrigation.Helpers
{
    /// <summary>
    /// App exception for user updates.
    /// </summary>
    public class UserUpdateException : AppException
    {
        public UserUpdateException() : base() {}

        public UserUpdateException(string message) : base(message) { }

        public UserUpdateException(string message, params object[] args) 
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
