using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewPascalCompiler.FrontEnd.Pascal;

namespace NewPascalCompiler.FrontEnd;

public static class FrontEndFactory
{
    public static Parser CreateParser(string language, Source source)
    {
        if (language.Equals("Pascal", StringComparison.InvariantCultureIgnoreCase))
        {
            var scanner = new PascalScanner(source);
            return new PascalParser(scanner);
        }

        throw new Exception("Invalid language");
    }
}
