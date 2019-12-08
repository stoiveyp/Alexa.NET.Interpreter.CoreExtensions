using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.SkillFlow;
using Alexa.NET.SkillFlow.CoreExtensions;
using Alexa.NET.SkillFlow.Interpreter;
using Xunit;

namespace Alexa.NET.Interpreter.CoreExtensions.Tests
{
    public class BuyTests
    {
        [Theory]
        [InlineData("buy success=\"pass\" fail=\"fail\"")]
        [InlineData("buy success=\"pass\" fail=\"fail\" declined=\"declined\"")]
        public void CandidateWorks(string candidate)
        {
            var interpreter = new BuyInterpreter();
            Assert.True(interpreter.CanInterpret(candidate,
                new SkillFlowInterpretationContext(new SkillFlowInterpretationOptions())));
        }

        [Theory]
        [InlineData("buying success=\"pass\" fail=\"fail\"")]
        [InlineData("buy success=\"pass\" fail=\"fail\" stuff=\"")]
        public void BadCandidateFails(string candidate)
        {
            var interpreter = new BuyInterpreter();
            Assert.False(interpreter.CanInterpret(candidate,
                new SkillFlowInterpretationContext(new SkillFlowInterpretationOptions())));
        }

        public static IEnumerable<object[]> LineBrokenBuyCommand()
        {
            yield return new object[]
            {
                $"buy {Environment.NewLine} success=\"pass\" fail=\"fail\" {Environment.NewLine}declined=\"declined\" already_purchased=\"already\" error=\"error\"", 
                "pass", "fail", "declined", "already", "error"
            };
        }

        [Theory]
        [InlineData("buy success=\"pass\" fail=\"fail\" declined=\"declined\"","pass","fail","declined",null,null)]
        [InlineData("buy success=\"pass\" fail=\"fail\" declined=\"declined\" already_purchased=\"already\" error=\"error\"", "pass", "fail", "declined", "already", "error")]
        [MemberData(nameof(LineBrokenBuyCommand))]
        public async Task InterpretsCorrectly(string candidate, string success, string fail, string declined, string alreadyPurchased, string error)
        {
            var interpreter = new SkillFlowInterpreter(new SkillFlowInterpretationOptions());
            interpreter.AddCoreExtensions();

            var defaultScene = $@"
@scene test
*say
there's a thing
*then
{candidate}
time
";

            var output = await interpreter.Interpret(defaultScene);
            var instructions = output.Scenes.First().Value.Instructions.Instructions;
            Assert.Equal(2, instructions.Count);
            var buy = Assert.IsType<Buy>(instructions.First());

            Assert.Equal(success,buy.SuccessScene);
            Assert.Equal(fail, buy.FailureScene);
            Assert.Equal(declined,buy.DeclinedScene);
            Assert.Equal(alreadyPurchased,buy.AlreadyPurchasedScene);
            Assert.Equal(error,buy.ErrorScene);
        }
    }
}
