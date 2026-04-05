using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="PostToolUseFailureHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class PostToolUseFailureHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializePostToolUseFailureOutput_WithAdditionalContext_ShouldSerializeCorrectly()
        {
            var output = new PostToolUseFailureHookOutput
            {
                Continue = true,
                HookSpecificOutput = new PostToolUseFailureSpecificOutput
                {
                    AdditionalContext = "File was locked by another process"
                }
            };

            var json = ClaudeHooksSerializer.SerializePostToolUseFailureOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("additionalContext").GetString()
                .Should().Be("File was locked by another process");
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("hookEventName").GetString().Should().Be("PostToolUseFailure");
        }

        #endregion

    }

}
