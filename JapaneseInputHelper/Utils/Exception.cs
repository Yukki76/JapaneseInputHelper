using System;

namespace Utils {
    internal class IsAdministratorException : Exception {
        public IsAdministratorException() : base() { }
        public IsAdministratorException(string message) : base(message) { }
        public IsAdministratorException(string message, Exception innerException) : base(message, innerException) { }
    }

    internal class MultipleRunningException : Exception {
        public MultipleRunningException() : base() { }
        public MultipleRunningException(string message) : base(message) { }
        public MultipleRunningException(string message, Exception innerException) : base(message, innerException) { }
    }
}
