using System;
using System.Runtime.Serialization;

namespace TimeCheckerWPF5._0.Exceptions
{
    [Serializable]
    internal class DBAccessException : Exception
    {
        public DBAccessException()
        {
        }

        public DBAccessException(string message) : base(message)
        {
        }

        public DBAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DBAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}