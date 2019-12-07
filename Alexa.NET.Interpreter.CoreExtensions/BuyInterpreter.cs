using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.SkillFlow.Interpreter;

namespace Alexa.NET.Interpreter.CoreExtensions
{
    public class BuyInterpreter:ISkillFlowInterpreter
    {
        public bool CanInterpret(string candidate, SkillFlowInterpretationContext context)
        {
            
        }

        public InterpreterResult Interpret(string candidate, SkillFlowInterpretationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
