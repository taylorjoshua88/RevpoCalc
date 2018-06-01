using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace RevpoCalc
{
    /// <summary>
    /// Represents an interactive CLI session using System.Console
    /// </summary>
    class InteractiveSession
    {
        const string _welcomeString = "Welcome to RevpoCalc, a basic" +
            " calcy using Reverse Polish whacamole.\nPress ? followed by enter" +
            " for help and a list of available operators.\n\n";
        const string _promptString = "Statement: ";
        const string _answerString = "   I knows it!!!  It's: {0}\n\n";
        const string _errorString = "Unexpected operator: {0}\n" +
            "Please press ? followed by enter for a list of available operators.\n";
        const string _helpString = "RevpoCalc is a the bestest Reverse Polish notation calcy ever.\n" +
            "Operands are placed before operators, duh, such as 5 7 + to add 5 and 7.\n\n" +
            "Available operators:";

        /// <summary>
        /// Begins the interactive session, taking control of the calling thread
        /// </summary>
        /// <returns></returns>
        public double Begin()
        {
            Console.Write(_welcomeString);
            
            for (;;)
            {
                // Print the prompt
                Console.Write(_promptString);

                // Take in user input. If a question mark is in the input, print the help string
                var userInput = Console.ReadLine();
                if (userInput.Contains('?'))
                {
                    Console.WriteLine(_helpString);
                    foreach (var key in Defaults.ExpressionEvaluators.Keys)
                    {
                        Console.Write("{0} ", key);
                    }
                    Console.WriteLine("\n");
                }

                var inputStatement = new Statement(userInput);

                // Answer is initially set to null to signal the first expression
                // in the statement to the expression evaluator
                double? answer = null;

                // Evaluate each expression, saving the results to answer
                foreach (var expression in inputStatement.Expressions)
                {
                    if (Defaults.ExpressionEvaluators.ContainsKey(expression.Operator))
                    {
                        answer = Defaults.ExpressionEvaluators[
                            expression.Operator](expression, answer);
                    }
                    else
                    {
                        // Unknown operator. Remind user of the help command
                        Console.WriteLine(_errorString, expression.Operator);
                        answer = null;
                        break;
                    }
                }

                // Write the answer to the console if no errors occurred
                if (answer.HasValue)
                {
                    Console.Write(_answerString, answer);
                }
            }
        }
    }
}
