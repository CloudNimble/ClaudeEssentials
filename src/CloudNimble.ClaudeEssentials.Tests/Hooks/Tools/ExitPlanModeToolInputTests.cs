using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="ExitPlanModeToolInput"/> deserialization and round-trips.
    /// </summary>
    [TestClass]
    public class ExitPlanModeToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void ExitPlanModeToolInput_DeserializeWithAllowedPrompts_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "allowedPrompts": [
                        {
                            "tool": "Bash",
                            "prompt": "run tests"
                        },
                        {
                            "tool": "Write",
                            "prompt": "create configuration files"
                        }
                    ]
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitPlanModeToolInput);

            result.Should().NotBeNull();
            result!.AllowedPrompts.Should().NotBeNull();
            result.AllowedPrompts.Should().HaveCount(2);
            result.AllowedPrompts![0].Tool.Should().Be("Bash");
            result.AllowedPrompts[0].Prompt.Should().Be("run tests");
            result.AllowedPrompts[1].Tool.Should().Be("Write");
            result.AllowedPrompts[1].Prompt.Should().Be("create configuration files");
        }

        [TestMethod]
        public void ExitPlanModeToolInput_DeserializeWithoutAllowedPrompts_ShouldDeserializeCorrectly()
        {
            var json = """
                {}
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitPlanModeToolInput);

            result.Should().NotBeNull();
            result!.AllowedPrompts.Should().BeNull();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void ExitPlanModeToolInput_RoundTrip_WithAllowedPrompts_ShouldMaintainData()
        {
            var input = new ExitPlanModeToolInput
            {
                AllowedPrompts = new List<ExitPlanModeAllowedPrompt>
                {
                    new() { Tool = "Bash", Prompt = "install dependencies" },
                    new() { Tool = "Edit", Prompt = "modify source files" }
                }
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.ExitPlanModeToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.ExitPlanModeToolInput);

            result.Should().NotBeNull();
            result!.AllowedPrompts.Should().NotBeNull();
            result.AllowedPrompts.Should().HaveCount(2);
            result.AllowedPrompts![0].Tool.Should().Be(input.AllowedPrompts[0].Tool);
            result.AllowedPrompts[0].Prompt.Should().Be(input.AllowedPrompts[0].Prompt);
            result.AllowedPrompts[1].Tool.Should().Be(input.AllowedPrompts[1].Tool);
            result.AllowedPrompts[1].Prompt.Should().Be(input.AllowedPrompts[1].Prompt);
        }

        [TestMethod]
        public void ExitPlanModeToolInput_RoundTrip_WithoutAllowedPrompts_ShouldOmitNullFields()
        {
            var input = new ExitPlanModeToolInput();

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.ExitPlanModeToolInput);

            json.Should().NotContain("allowedPrompts");
        }

        #endregion

    }

}
