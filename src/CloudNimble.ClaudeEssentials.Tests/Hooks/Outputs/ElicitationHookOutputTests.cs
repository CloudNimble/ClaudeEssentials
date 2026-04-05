using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="ElicitationHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class ElicitationHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeElicitationOutput_WithAcceptAction_ShouldSerializeCorrectly()
        {
            var output = new ElicitationHookOutput
            {
                Continue = true,
                HookSpecificOutput = new ElicitationSpecificOutput
                {
                    Action = ElicitationAction.Accept,
                    Content = JsonDocument.Parse("""{"name": "test-value"}""").RootElement
                }
            };

            var json = ClaudeHooksSerializer.SerializeElicitationOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("action").GetString().Should().Be("accept");
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("content").GetProperty("name").GetString().Should().Be("test-value");
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("hookEventName").GetString().Should().Be("Elicitation");
        }

        [TestMethod]
        public void SerializeElicitationOutput_WithDeclineAction_ShouldSerializeCorrectly()
        {
            var output = new ElicitationHookOutput
            {
                Continue = true,
                HookSpecificOutput = new ElicitationSpecificOutput
                {
                    Action = ElicitationAction.Decline
                }
            };

            var json = ClaudeHooksSerializer.SerializeElicitationOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("action").GetString().Should().Be("decline");
        }

        #endregion

    }

}
