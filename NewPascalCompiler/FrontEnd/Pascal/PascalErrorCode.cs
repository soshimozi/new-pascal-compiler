using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPascalCompiler.FrontEnd.Pascal
{
    public class PascalErrorCode
    {
        public static readonly PascalErrorCode UnexpectedEOF = new(30, "Unexpected end of file");
        public static readonly PascalErrorCode UnexpectedToken = new(31, "Unexpected token");
        public static readonly PascalErrorCode MissingBegin = new(32, "Missing BEGIN");
        public static readonly PascalErrorCode InvalidCharacter = new(33, "Invalid character");
        public static readonly PascalErrorCode InvalidNumber = new(34, "Invalid number");
        public static readonly PascalErrorCode RangeInteger = new(35, "Integer literal out of range");
        public static readonly PascalErrorCode RangeReal = new(36, "Real literal out of range");

        private readonly int _index;
        private readonly string _message;

        private PascalErrorCode(int index, string message)
        {
            _index = index;
            _message = message;
        }

        public static implicit operator int(PascalErrorCode pe) => pe._index;
        public static implicit operator string(PascalErrorCode pe) => pe._message;
    }
}
