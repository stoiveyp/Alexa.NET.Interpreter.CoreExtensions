using System.Collections.Generic;
using System.Linq;
using Alexa.NET.SkillFlow;
using Alexa.NET.SkillFlow.CoreExtensions;
using Alexa.NET.SkillFlow.Interpreter;
using Xunit;

namespace Alexa.NET.Interpreter.CoreExtensions.Tests
{
    public class RollTests
    {
        public static IEnumerable<object[]> GetRolls()
        {
            return new[]
            {
                "roll 3d20",
                "roll 3d6k1",
                "roll 3d20 + 1",
                "roll 3d20 - 10",
                "roll 3d20k2 + 4"
            }.Select(s => new object[] { s });
        }

        [Theory]
        [MemberData(nameof(GetRolls))]
        public void CheckCandidates(string candidate)
        {
            var interpreter = new RollInterpreter();
            Assert.True(interpreter.CanInterpret(candidate, new SkillFlowInterpretationContext(new SkillFlowInterpretationOptions())));
        }

        public static IEnumerable<object[]> GetProperties()
        {
            yield return new object[] { "roll 3d20", 3, 20, null, null, null };
            yield return new object[] { "roll 3d6k1", 3, 6, 1, null, null };
            yield return new object[] { "roll 3d20 + 1", 3, 20, null, '+', 1 };
            yield return new object[] { "roll 3d20 - 10", 3, 20, null, '-', 10 };
            yield return new object[] { "roll 3d20k2 + 4", 3, 20, 2, '+', 4 };
        }

        [Theory]
        [MemberData(nameof(GetProperties))]
        public void CheckOutput(string candidate, int number, int sides, int? top, char? modifier, int? modifiedAmount)
        {
            var context = new SkillFlowInterpretationContext(new SkillFlowInterpretationOptions());

            var interpreter = new RollInterpreter();
            var result = interpreter.Interpret(candidate, context);

            Assert.NotNull(result.Component);
            var roll = Assert.IsType<Roll>(result.Component);

            Assert.Equal(number,roll.Dice);
            Assert.Equal(sides, roll.Sides);
            Assert.Equal(top, roll.Top);
            Assert.Equal(modifier, roll.Modifier);
            Assert.Equal(modifiedAmount, roll.ModifyAmount);
        }

    }
}
