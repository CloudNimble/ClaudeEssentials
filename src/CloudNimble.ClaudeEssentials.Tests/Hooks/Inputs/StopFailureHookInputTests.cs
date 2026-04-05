using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Inputs
{

    /// <summary>
    /// Tests for deserializing <see cref="StopFailureHookInput"/> payloads from JSON.
    /// </summary>
    [TestClass]
    public class StopFailureHookInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeStopFailureInput_WithRateLimitError_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "sess-sf-01",
                    "error_type": "rate_limit"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeStopFailureInput(json);

            result.Should().NotBeNull();
            result!.SessionId.Should().Be("sess-sf-01");
            result.ErrorType.Should().Be(StopFailureErrorType.RateLimit);
        }

        [TestMethod]
        public void DeserializeStopFailureInput_WithAuthenticationError_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "error_type": "authentication_failed"
                }
                """;

            var result = ClaudeHooksSerializer.DeserializeStopFailureInput(json);

            result.Should().NotBeNull();
            result!.ErrorType.Should().Be(StopFailureErrorType.AuthenticationFailed);
        }

        #endregion

        #region Malformed Payload Tests

        [TestMethod]
        public void DeserializeStopFailureInput_WithGarbageJson_ShouldThrowJsonException()
        {
            var action = () => ClaudeHooksSerializer.DeserializeStopFailureInput("not json");
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void DeserializeStopFailureInput_WithEmptyObject_ShouldReturnObjectWithDefaults()
        {
            var result = ClaudeHooksSerializer.DeserializeStopFailureInput("{}");
            result.Should().NotBeNull();
            result!.SessionId.Should().BeEmpty();
        }

        #endregion

    }

}
