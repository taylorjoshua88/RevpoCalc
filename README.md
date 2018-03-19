RevPoCalc - Reverse Polish Calculator
=====================================
RevPoCalc is a basic, Reverse Polish notation calculator
written in C# for the .NET Core 2.0 platform.

Dependencies
------------
RevPoCalc depends on the .NET Core 2.0 runtime for whichever
operating system you are using. Visit the following URL for
installation instructions on Windows, Linux, and macOS:

https://www.microsoft.com/net/download/

Building and running
--------------------
With the .NET Core runtime / SDK installed, the dotnet
CLI utility can be used to build and run RevpoCalc:

> cd RevpoCalc
> dotnet build
> dotnet run

Operation
---------
The operation of Reverse Polish notation may be unusual at
first. Consult the following for more information about
Reverse Polish notation:

https://en.wikipedia.org/wiki/Reverse_Polish_notation

Sequences of expressions can be entered which work on each
others' output. For instance, the following will produce the
answer of 12:

> 24 12 + 3 /
> 12

The calculator adds 12 to 24 (36) and then divides that answer
by 3 (12). You will know that the calculator has operated
successfully if it provides you with "Anwer: " followed by
your expected result. Otherwise, an error will be presented to
the user. Pressing "?" at any point in your statement will
bring up a basic help prompt with a listing of all of the
understood operators.

In its present form, RevPoCalc can do basic addition, subtraction,
multiplication, and division. It was designed with extensibility in
mind; therefore, new operations can be added as lambda operations
within the Defaults.ExpressionEvaluators dictionary. This is located
in the DefaultExpressionEvaluators.cs source file.

Reflection
==========

RevpoCalc took approximately four hours to complete. It would have been
a much shorter process if I hadn't aimed for the "Ivory Tower" approach
of supporting extensibility for what is essentially a small demo program.

I struggled mostly with organizing the calculator into logical classes.
In my past experience with less OOP-style languages, I would have written
my code in a much more straightforward, imperative style. By separating
the functionality into multiple classes, I did start the framework for
a quite extensible calculator platform that could be delivered via CLI,
SSH, browser, etc., via the separation of concerns.

This assignment reinforced the idea that I should probably be a bit more
pragmatic in designing my programs. I have been used to developing code
as a hobbyist in which there are no real time constraints. When I begin
writing software professionally, it will be very important for me to
balance functionality, extensibility, and project constraints such as
scheduling and deadlines. I have not tested the code very completely and
know that there are some bugs which I simply ran out of time in addressing
due to falling ill over the weekend.

I consulted the C# language reference extensively while producing this
program. I did not make use of any other resources while developing
or designing my code.

https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/