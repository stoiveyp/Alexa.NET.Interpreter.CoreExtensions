using Alexa.NET.SkillFlow.CoreExtensions;
using Alexa.NET.SkillFlow.Interpreter;

namespace Alexa.NET.Interpreter.CoreExtensions
{
    public class TimeInterpreter:ISkillFlowInterpreter
    {
        public bool CanInterpret(string candidate, SkillFlowInterpretationContext context)
        {
            return candidate.Equals("time");
        }

        public InterpreterResult Interpret(string candidate, SkillFlowInterpretationContext context)
        {
            return new InterpreterResult(new Time());
        }
    }
}
