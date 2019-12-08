using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Alexa.NET.SkillFlow.CoreExtensions;
using Alexa.NET.SkillFlow.Interpreter;

namespace Alexa.NET.Interpreter.CoreExtensions
{
    public class BuyInterpreter : ISkillFlowInterpreter
    {
        Regex _regex = new Regex(@"(?<key>[\w]+?)=\""(?<value>[\w\s]+?)\""", RegexOptions.Compiled);
        public bool CanInterpret(string candidate, SkillFlowInterpretationContext context)
        {
            if (context.CurrentComponent is Buy)
            {
                return _regex.Replace(candidate, string.Empty).Trim() == string.Empty;
            }

            return candidate.StartsWith("buy") && _regex.Replace(candidate, string.Empty).Trim().ToLower() == "buy";
        }

        public InterpreterResult Interpret(string candidate, SkillFlowInterpretationContext context)
        {
            var buy = new Buy();
            if (context.CurrentComponent is Buy currentBuy)
            {
                buy = currentBuy;
            }

            var matches = _regex.Matches(candidate).Cast<Match>();

            foreach (var match in matches)
            {
                var key = match.Groups["key"].Value;
                var value = match.Groups["value"].Value;
                switch (key)
                {
                    case "success":
                        buy.SuccessScene = value;
                        break;
                    case "fail":
                        buy.FailureScene = value;
                        break;
                    case "declined":
                        buy.DeclinedScene = value;
                        break;
                    case "already_purchased":
                        buy.AlreadyPurchasedScene = value;
                        break;
                    case "error":
                        buy.ErrorScene = value;
                        break;
                    default:
                        throw new InvalidSkillFlowDefinitionException($"Unknown buy property {key}", context.LineNumber);
                }
            }

            if (context.CurrentComponent is Buy)
            {
                return InterpreterResult.Empty;
            }
            return new InterpreterResult(buy);
        }
    }
}
