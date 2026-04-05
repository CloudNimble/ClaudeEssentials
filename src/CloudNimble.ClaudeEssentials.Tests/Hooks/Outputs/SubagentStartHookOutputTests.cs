using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="SubagentStartHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class SubagentStartHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeSubagentStartOutput_WithAdditionalContext_ShouldSerializeCorrectly()
        {
            var output = new SubagentStartHookOutput
            {
                Continue = true,
                HookSpecificOutput = new SubagentStartSpecificOutput
                {
                    AdditionalContext = "Use the testing framework for this task"
                }
            };

            var json = ClaudeHooksSerializer.SerializeSubagentStartOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("additionalContext").GetString()
                .Should().Be("Use the testing framework for this task");
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("hookEventName").GetString().Should().Be("SubagentStart");
        }

        #endregion

    }

}
