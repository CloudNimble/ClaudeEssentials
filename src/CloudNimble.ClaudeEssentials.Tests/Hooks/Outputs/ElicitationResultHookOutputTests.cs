using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="ElicitationResultHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class ElicitationResultHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeElicitationResultOutput_WithAcceptAction_ShouldSerializeCorrectly()
        {
            var output = new ElicitationResultHookOutput
            {
                Continue = true,
                HookSpecificOutput = new ElicitationResultSpecificOutput
                {
                    Action = ElicitationAction.Accept,
                    Content = JsonDocument.Parse("""{"confirmed": true}""").RootElement
                }
            };

            var json = ClaudeHooksSerializer.SerializeElicitationResultOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("action").GetString().Should().Be("accept");
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("content").GetProperty("confirmed").GetBoolean().Should().BeTrue();
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("hookEventName").GetString().Should().Be("ElicitationResult");
        }

        [TestMethod]
        public void SerializeElicitationResultOutput_WithCancelAction_ShouldSerializeCorrectly()
        {
            var output = new ElicitationResultHookOutput
            {
                Continue = true,
                HookSpecificOutput = new ElicitationResultSpecificOutput
                {
                    Action = ElicitationAction.Cancel
                }
            };

            var json = ClaudeHooksSerializer.SerializeElicitationResultOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("action").GetString().Should().Be("cancel");
        }

        #endregion

    }

}
