using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.SkillFlow.Interpreter;
using Xunit;

namespace Alexa.NET.Interpreter.CoreExtensions.Tests
{
    public class TimeTests
    {
        [Fact]
        public void CheckCandidates()
        {
            var interpreter = new TimeInterpreter();
            Assert.True(interpreter.CanInterpret("time", new SkillFlowInterpretationContext(new SkillFlowInterpretationOptions())));
        }

        [Fact]
        public void CheckInvalidUrl()
        {
            var interpreter = new TimeInterpreter();
            var result = interpreter.Interpret("time", new SkillFlowInterpretationContext(new SkillFlowInterpretationOptions()));
            Assert.NotNull(result.Component);
        }
    }
}
