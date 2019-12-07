using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Alexa.NET.SkillFlow.CoreExtensions;
using Alexa.NET.SkillFlow.Interpreter;

namespace Alexa.NET.Interpreter.CoreExtensions
{
    public class RollInterpreter:ISkillFlowInterpreter
    {
        Regex DiceRoll = new Regex(@"(?<number>\d+)d(?<sides>\d+)(k(?<top>\d+))?(\s?(?<sign>[+-])\s?(?<modifier>\d+))?");
        public bool CanInterpret(string candidate, SkillFlowInterpretationContext context)
        {
            return candidate.StartsWith("roll ") && DiceRoll.IsMatch(candidate,5);
        }

        public InterpreterResult Interpret(string candidate, SkillFlowInterpretationContext context)
        {
            var data = DiceRoll.Match(candidate, 5);
            if (!data.Success)
            {
                return InterpreterResult.Empty;
            }

            return new InterpreterResult(new Roll
            {
                Dice = int.Parse(data.Groups["number"].Value),
                Sides = int.Parse(data.Groups["sides"].Value),
                Top = data.Groups["top"].Success ? int.Parse(data.Groups["top"].Value) : (int?) null,
                Modifier = data.Groups["sign"].Success ? data.Groups["sign"].Value[0] : (char?) null,
                ModifyAmount = data.Groups["modifier"].Success ? int.Parse(data.Groups["modifier"].Value) : (int?) null
            });
        }
    }
}
