using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="PermissionDeniedHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class PermissionDeniedHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializePermissionDeniedOutput_WithRetry_ShouldSerializeCorrectly()
        {
            var output = new PermissionDeniedHookOutput
            {
                Continue = true,
                HookSpecificOutput = new PermissionDeniedSpecificOutput
                {
                    Retry = true
                }
            };

            var json = ClaudeHooksSerializer.SerializePermissionDeniedOutput(output);
            var doc = JsonDocument.Parse(json);

            doc.RootElement.GetProperty("continue").GetBoolean().Should().BeTrue();
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("retry").GetBoolean().Should().BeTrue();
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("hookEventName").GetString().Should().Be("PermissionDenied");
        }

        #endregion

    }

}
