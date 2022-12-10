using System;
using ZoeyDesktopNavigationParser.FrontEnd.Pascal;

namespace NewPascalCompiler.Intermediate.Type
{
    public class TypeForm
    {

        public static readonly TypeForm Scalar = new(0, "Scalar");
        public static readonly TypeForm Enumeration = new(1, "Enumeration");
        public static readonly TypeForm SubRange = new(2, "SubRange");
        public static readonly TypeForm Array = new(3, "Array");
        public static readonly TypeForm Record = new(4, "Record");

        private readonly int _index;
        private readonly string _name;
        private TypeForm(int index, string name)
        {
            _index = index;
            _name = name;
        }

        public override string ToString()
        {
            return _name.ToLower();
        }

        public static implicit operator string(TypeForm tf) => tf.ToString();
    }
}

