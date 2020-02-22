using System;
using System.Globalization;

namespace IrigationSystem.Helpers
{
    /// <summary>
    /// Custom exception class for throwing application specific exceptions (e.g. for validation) 
    /// that can be caught and handled within the application
    /// </summary>
    // TODO: if you are using custom exception to identify app level exceptions is there a way to throw the normal type rebranded?
    public class AppException : Exception
    {
        public AppException() : base() {}

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args) 
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
