using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Outputs
{

    /// <summary>
    /// Tests for serializing <see cref="SessionStartHookOutput"/> payloads to JSON.
    /// </summary>
    [TestClass]
    public class SessionStartHookOutputTests
    {

        #region Serialization Tests

        [TestMethod]
        public void SerializeSessionStartOutput_WithContext_ShouldSerializeCorrectly()
        {
            // Arrange
            var output = new SessionStartHookOutput
            {
                HookSpecificOutput = new SessionStartSpecificOutput
                {
                    AdditionalContext = "Project uses .NET 10"
                }
            };

            // Act
            var json = ClaudeHooksSerializer.SerializeSessionStartOutput(output);
            var doc = JsonDocument.Parse(json);

            // Assert
            doc.RootElement.GetProperty("hookSpecificOutput")
                .GetProperty("additionalContext").GetString().Should().Be("Project uses .NET 10");
        }

        #endregion

    }

}
