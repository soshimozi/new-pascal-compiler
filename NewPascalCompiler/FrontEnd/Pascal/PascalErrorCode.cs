using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewPascalCompiler.FrontEnd.Pascal
{
    public class PascalErrorCode
    {
        public static readonly PascalErrorCode UnexpectedEOF = new("Unexpected end of file");
        public static readonly PascalErrorCode UnexpectedToken = new("Unexpected token");
        public static readonly PascalErrorCode MissingBegin = new("Missing BEGIN");
        public static readonly PascalErrorCode InvalidCharacter = new("Invalid character");
        public static readonly PascalErrorCode InvalidNumber = new("Invalid number");
        public static readonly PascalErrorCode RangeInteger = new("Integer literal out of range");
        public static readonly PascalErrorCode RangeReal = new("Real literal out of range");
        public static readonly PascalErrorCode MissingPeriod = new("Missing .");
        public static readonly PascalErrorCode AlreadyForwarded = new("Already forwarded");
        public static readonly PascalErrorCode MissingSemicolon = new("Missing semi-colon");

        //private readonly int _index;
        private readonly string _message;

        private PascalErrorCode(string message)
        {
            //_index = index;
            _message = message;
        }

        public override string ToString()
        {
            return this;
        }

        //public static implicit operator int(PascalErrorCode pe) => pe._index;
        public static implicit operator string(PascalErrorCode pe) => pe._message;
    }
}
