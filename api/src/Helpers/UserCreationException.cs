using System;
using System.Globalization;

namespace Irrigation.Helpers
{
    /// <summary>
    /// App exception for user creation.
    /// </summary>
    public class UserCreationException : AppException
    {
        public UserCreationException() : base() {}

        public UserCreationException(string message) : base(message) { }

        public UserCreationException(string message, params object[] args) 
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
