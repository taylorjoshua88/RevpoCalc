using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace RevpoCalc
{
    /// <summary>
    /// Represents a single, mathematical expression.
    /// Methods are provided to create Expression structs from
    /// strings or Regex matches in Reverse Polish notation.
    /// </summary>
    public struct Expression
    {
        /// <summary>
        /// Regex match string used to identify an expression.
        /// $1's captures represent the operands. $2 is the operator.
        /// </summary>
        public const string MatchString = @"(\d*\.?\d*\s+)+(\D)\s*";

        /// <summary>
        /// Delegate for a method which takes an expression, evaluates it,
        /// and returns a result as a double.
        /// </summary>
        /// <param name="e">The expression to evaluate.</param>
        /// <param name="s">Seed value to include in evaluation.
        ///  This is useful when evaluating an entire statement of
        ///  expressions.</param>
        /// <returns>The result of evaluating the expression as a double.</returns>
        public delegate double ExpressionEvaluator(Expression e, double? s);

        /// <summary>
        /// A list of operands for the expression
        /// </summary>
        public List<double> Operands { get; set; }

        /// <summary>
        /// The expression's mathematical operator as a char
        /// </summary>
        public char Operator { get; set; }

        /// <summary>
        /// Basic constructor for Expression
        /// </summary>
        /// <param name="op">The operator for the expression as a char</param>
        /// <param name="operands">The operands of the expression as doubles</param>
        public Expression(char op, IEnumerable<double> operands)
        {
            Operands = new List<double>(operands);
            Operator = op;
        }

        /// <summary>
        /// Convenience constructor for Expression.
        /// Operands are passed to the basic constructor as an IEnumerable.
        /// </summary>
        /// <param name="op">The operator for the expression as a char</param>
        /// <param name="operands">The operands of the epxression as doubles</param>
        public Expression(char op, params double[] operands) :
            this(op, operands as IEnumerable<double>)
        { }

        /// <summary>
        /// Creates a new Expression struct from a Regex match.
        /// Useful in cases like statements where multiple expressions are
        /// already matched via regex.
        /// </summary>
        /// <param name="match">A regex match representing a single 
        /// expression in Reverse Polish notation.</param>
        /// <returns>A new Expression struct containing the operands
        /// and operator from the provided match.</returns>
        /// <exception cref="FormatException">One of the operands
        /// were not able to be converted to a double.</exception>
        public static Expression FromMatch(Match match)
        {
            // Stores the operands to be passed to the Expression ctor
            var operands = 
                new List<double>(match.Groups[1].Captures.Count);

            // Try to parse all of the operands and throw a FormatException
            // upon failure with the position of the failure from the
            // original source string.
            foreach (Capture capture in match.Groups[1].Captures)
            {
                if (double.TryParse(capture.Value, out double operand))
                {
                    operands.Add(operand);
                }
                else
                {
                    throw new FormatException(
                        $"Operand at position {capture.Index} is not" +
                        $" within the limits of a double-precision" +
                        $" floating point number or is formatted" +
                        $" incorrectly.");
                }
            }

            return new Expression(match.Groups.Last().Value[0],
                operands);
        }
            
        /// <summary>
        /// Creates a new Expression struct from a string.
        /// The provided string will be matched via regex on
        /// Expression.MatchString and passed to the FromMatch() method.
        /// </summary>
        /// <param name="input">A string representing a single
        /// mathematical expression in Reverse Polish notation.</param>
        /// <returns>A new Expression struct with an operator and operands
        /// extracted from the provided string.</returns>
        public static Expression FromString(string input) =>
            FromMatch(Regex.Match(input, MatchString));
    }
}
