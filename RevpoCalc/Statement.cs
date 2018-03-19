using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace RevpoCalc
{
    /// <summary>
    /// Represents a full statement of mathematical expressions.
    /// </summary>
    public struct Statement
    {
        /// <summary>
        /// List of Expression structs which make up this Statement
        /// </summary>
        public List<Expression> Expressions { get; set; }

        /// <summary>
        /// Basic constructor which extracts Expression structs from
        /// a statement string in Reverse Polish notation.
        /// </summary>
        /// <param name="input"></param>
        public Statement(string input)
        {
            // Extract expression substrings and initialize the Expressions
            // list to hold the number of expressions
            var matches = Regex.Matches(input, Expression.MatchString);
            Expressions = new List<Expression>(matches.Count);

            // Add each expression match to the Expressions list using
            // the FromMatch static method of the Expression struct
            foreach (Match match in matches)
            {
                Expressions.Add(Expression.FromMatch(match));
            }
        }
    }
}
