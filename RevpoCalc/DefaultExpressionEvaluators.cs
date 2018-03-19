using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RevpoCalc
{
    public static class Defaults
    {
        public static Dictionary<char, Expression.ExpressionEvaluator>
            ExpressionEvaluators =
            new Dictionary<char, Expression.ExpressionEvaluator>
            {
                { '+', (e, s) => (s ?? 0.0) + e.Operands.Sum() },
                { '-', (e, s) => (s ?? 0.0) - e.Operands[0] - 
                    e.Operands.Skip(1).Sum() },
                { '*', (e, s) => (s ?? 1.0) * e.Operands.Aggregate(
                    (product, next) => product * next) },
                { '/', (e, s) => s.HasValue ? (s.Value /
                    e.Operands.Aggregate((product, next) => product / next)) :
                    (e.Operands.Aggregate((product, next) => product / next)) },
            };
    }
}
