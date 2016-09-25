using System;

namespace VIPAC.Common.CustomExceptions
{
    public sealed class InvalidDataException : Exception
    {
        public InvalidDataException()
        {
        }

        public InvalidDataException(string message)
            : base(message)
        {
        }

        public InvalidDataException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public InvalidDataException(string message, int codeError)
            : base(message)
        {
            HResult = codeError;
        }

        public InvalidDataException(string message, int codeError, Exception inner)
            : base(message, inner)
        {
            HResult = codeError;
        }
    }
}
