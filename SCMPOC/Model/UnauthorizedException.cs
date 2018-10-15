using System;
using System.Runtime.Serialization;

namespace SCMPOC.Model
{
    [Serializable]
    internal class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}