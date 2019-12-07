using Alexa.NET.SkillFlow.Interpreter;
using Xunit;

namespace Alexa.NET.Interpreter.CoreExtensions.Tests
{
    public class BGMTests
    {
        [Fact]
        public void CheckCandidates()
        {
            var interpreter = new BGMInterpreter();
            Assert.True(interpreter.CanInterpret("BGM Test", new SkillFlowInterpretationContext(new SkillFlowInterpretationOptions())));
        }

        [Fact]
        public void CheckInvalidUrl()
        {
            var interpreter = new BGMInterpreter();
            var result = interpreter.Interpret("BGM Test", new SkillFlowInterpretationContext(new SkillFlowInterpretationOptions()));
            Assert.Null(result.Component);
        }

        [Fact]
        public void CheckValidUrl()
        {
            var interpreter = new BGMInterpreter();
            var result = interpreter.Interpret("BGM http://www.example.com/test.mp3", new SkillFlowInterpretationContext(new SkillFlowInterpretationOptions()));
            Assert.NotNull(result.Component);
        }
    }
}
