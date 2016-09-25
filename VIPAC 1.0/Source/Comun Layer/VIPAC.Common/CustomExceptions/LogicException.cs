using System;

namespace VIPAC.Common.CustomExceptions
{
    public class LogicException : Exception
    {
        public LogicException()
        {
        }

        public LogicException(string message)
            : base(message)
        {
        }

        public LogicException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public LogicException(string message, int codeError)
            : base(message)
        {
            HResult = codeError;
        }

        public LogicException(string message, int codeError, Exception inner)
            : base(message, inner)
        {
            HResult = codeError;
        }
    }
}