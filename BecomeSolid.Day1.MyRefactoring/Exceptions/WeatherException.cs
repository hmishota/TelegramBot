using System;
using System.Runtime.Serialization;

namespace BecomeSolid.Day1.MyRefactoring
{
    [Serializable]
    internal class WeatherException : Exception
    {
        public WeatherException()
        {
        }

        public WeatherException(string message) : base(message)
        {
        }

        public WeatherException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WeatherException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}