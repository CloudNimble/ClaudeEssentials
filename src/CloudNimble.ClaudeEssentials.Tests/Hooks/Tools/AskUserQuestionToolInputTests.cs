using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="AskUserQuestionToolInput"/> deserialization and round-trips.
    /// </summary>
    [TestClass]
    public class AskUserQuestionToolInputTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void AskUserQuestionToolInput_DeserializeSingleQuestion_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "questions": [
                        {
                            "question": "Which library should we use for date formatting?",
                            "header": "Library",
                            "options": [
                                {
                                    "label": "date-fns",
                                    "description": "Lightweight, tree-shakeable date utility library"
                                },
                                {
                                    "label": "Day.js",
                                    "description": "2KB immutable date library, Moment.js alternative"
                                }
                            ],
                            "multiSelect": false
                        }
                    ]
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionToolInput);

            result.Should().NotBeNull();
            result!.Questions.Should().HaveCount(1);
            result.Questions[0].Question.Should().Be("Which library should we use for date formatting?");
            result.Questions[0].Header.Should().Be("Library");
            result.Questions[0].MultiSelect.Should().BeFalse();
            result.Questions[0].Options.Should().HaveCount(2);
            result.Questions[0].Options[0].Label.Should().Be("date-fns");
            result.Questions[0].Options[0].Description.Should().NotBeNullOrWhiteSpace();
            result.Questions[0].Options[1].Label.Should().Be("Day.js");
        }

        [TestMethod]
        public void AskUserQuestionToolInput_DeserializeWithPreviewAndMultiSelect_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "questions": [
                        {
                            "question": "Which features do you want to enable?",
                            "header": "Features",
                            "options": [
                                {
                                    "label": "Authentication",
                                    "description": "Add JWT-based auth middleware",
                                    "preview": "app.UseAuthentication();\napp.UseAuthorization();"
                                },
                                {
                                    "label": "Logging",
                                    "description": "Add structured logging with Serilog",
                                    "preview": "builder.Host.UseSerilog();"
                                },
                                {
                                    "label": "Caching",
                                    "description": "Add in-memory response caching"
                                }
                            ],
                            "multiSelect": true
                        }
                    ]
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionToolInput);

            result.Should().NotBeNull();
            result!.Questions.Should().HaveCount(1);
            result.Questions[0].MultiSelect.Should().BeTrue();
            result.Questions[0].Options.Should().HaveCount(3);
            result.Questions[0].Options[0].Preview.Should().NotBeNullOrWhiteSpace();
            result.Questions[0].Options[1].Preview.Should().NotBeNullOrWhiteSpace();
            result.Questions[0].Options[2].Preview.Should().BeNull();
        }

        [TestMethod]
        public void AskUserQuestionToolInput_DeserializeMultipleQuestions_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "questions": [
                        {
                            "question": "Which CSS approach?",
                            "header": "Styling",
                            "options": [
                                { "label": "Tailwind", "description": "Utility-first CSS framework" },
                                { "label": "CSS Modules", "description": "Scoped CSS with module imports" }
                            ],
                            "multiSelect": false
                        },
                        {
                            "question": "Which test runner?",
                            "header": "Testing",
                            "options": [
                                { "label": "xUnit", "description": "Popular .NET test framework" },
                                { "label": "MSTest", "description": "Microsoft test framework" }
                            ],
                            "multiSelect": false
                        }
                    ]
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionToolInput);

            result.Should().NotBeNull();
            result!.Questions.Should().HaveCount(2);
            result.Questions[0].Header.Should().Be("Styling");
            result.Questions[1].Header.Should().Be("Testing");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void AskUserQuestionToolInput_RoundTrip_ShouldMaintainData()
        {
            var input = new AskUserQuestionToolInput
            {
                Questions = new List<AskUserQuestion>
                {
                    new()
                    {
                        Question = "Which database provider?",
                        Header = "Database",
                        MultiSelect = false,
                        Options = new List<AskUserQuestionOption>
                        {
                            new() { Label = "SQL Server", Description = "Microsoft relational database", Preview = "builder.Services.AddSqlServer();" },
                            new() { Label = "PostgreSQL", Description = "Open-source relational database" }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.AskUserQuestionToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionToolInput);

            result.Should().NotBeNull();
            result!.Questions.Should().HaveCount(1);
            result.Questions[0].Question.Should().Be(input.Questions[0].Question);
            result.Questions[0].Header.Should().Be(input.Questions[0].Header);
            result.Questions[0].MultiSelect.Should().BeFalse();
            result.Questions[0].Options.Should().HaveCount(2);
            result.Questions[0].Options[0].Label.Should().Be("SQL Server");
            result.Questions[0].Options[0].Preview.Should().NotBeNullOrWhiteSpace();
            result.Questions[0].Options[1].Label.Should().Be("PostgreSQL");
            result.Questions[0].Options[1].Preview.Should().BeNull();
        }

        [TestMethod]
        public void AskUserQuestionToolInput_RoundTrip_WithMultiSelect_ShouldMaintainData()
        {
            var input = new AskUserQuestionToolInput
            {
                Questions = new List<AskUserQuestion>
                {
                    new()
                    {
                        Question = "Which middleware to add?",
                        Header = "Middleware",
                        MultiSelect = true,
                        Options = new List<AskUserQuestionOption>
                        {
                            new() { Label = "CORS", Description = "Cross-origin resource sharing" },
                            new() { Label = "Rate Limiting", Description = "Request throttling" }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.AskUserQuestionToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionToolInput);

            result.Should().NotBeNull();
            result!.Questions[0].MultiSelect.Should().BeTrue();
        }

        #endregion

    }

}
