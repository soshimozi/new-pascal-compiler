using NewPascalCompiler.FrontEnd;
using System.Text;

//var text = @"
//PROGRAM newton (input, output);
//	CONST
//	epsilon = 1e-06, foo = 'bar';

//	VAR
//	number, root, sqroot : real;

//	BEGIN
//    END.
//";

var text = @"PROGRAM newton(input, output);
CONST
epsilon 1e-06, foo = 'bar';

VAR
number, root, sqroot : real;

BEGIN
END.
";



var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(text));
var bufferedStream = new BufferedStream(memoryStream);

var source = new Source(bufferedStream);

var parser = FrontEndFactory.CreateParser("Pascal", source);

parser.ParseCompleted += (sender, eventArgs) =>
{
    Console.WriteLine("Parse Completed");
    Console.WriteLine($"Elapsed Time: {eventArgs.ElapsedTime}");
	Console.WriteLine($"Lines: {eventArgs.LineNumber}");
    Console.WriteLine($"Syntax Errors: {eventArgs.ErrorCount}");
};

parser.Parse();


