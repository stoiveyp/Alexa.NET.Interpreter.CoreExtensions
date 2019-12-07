using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.SkillFlow;

namespace Alexa.NET.Interpreter.CoreExtensions
{
    public static class ExtensionSupport
    {
        public static void AddCoreExtensions(this SkillFlowInterpreter interpreter)
        {
            if (!interpreter.TypedInterpreters.ContainsKey(typeof(SceneInstructionContainer)))
            {
                return;
            }

            var instructionSet = interpreter.TypedInterpreters[typeof(SceneInstructionContainer)];
            instructionSet.Add(new RollInterpreter());
            instructionSet.Add(new TimeInterpreter());
            instructionSet.Add(new BGMInterpreter());
        }
    }
}
