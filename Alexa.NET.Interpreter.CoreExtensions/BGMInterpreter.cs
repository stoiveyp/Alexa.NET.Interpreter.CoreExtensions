using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.SkillFlow.CoreExtensions;
using Alexa.NET.SkillFlow.Interpreter;

namespace Alexa.NET.Interpreter.CoreExtensions
{
    public class BGMInterpreter:ISkillFlowInterpreter
    {
        public bool CanInterpret(string candidate, SkillFlowInterpretationContext context)
        {
            return candidate.StartsWith("BGM ",StringComparison.OrdinalIgnoreCase);
        }

        public InterpreterResult Interpret(string candidate, SkillFlowInterpretationContext context)
        {
            if(Uri.TryCreate(candidate.Substring(4),UriKind.Absolute, out var result))
            {
                return new InterpreterResult(new BGM{Uri = result});
            }
            return InterpreterResult.Empty;
        }
    }
}
