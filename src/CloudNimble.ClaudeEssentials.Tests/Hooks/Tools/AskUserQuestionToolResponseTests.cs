using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="AskUserQuestionToolResponse"/> and <see cref="AskUserQuestionAnnotation"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class AskUserQuestionToolResponseTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeAskUserQuestion_WithAnnotations_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "session_id": "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "AskUserQuestion",
                    "tool_input": {
                        "questions": [
                            {
                                "question": "Which library should we use for date formatting?",
                                "header": "Library",
                                "options": [
                                    { "label": "date-fns", "description": "Lightweight, tree-shakeable" },
                                    { "label": "Day.js", "description": "2KB Moment.js alternative" }
                                ],
                                "multiSelect": false
                            }
                        ]
                    },
                    "tool_response": {
                        "questions": [
                            {
                                "question": "Which library should we use for date formatting?",
                                "header": "Library",
                                "options": [
                                    { "label": "date-fns", "description": "Lightweight, tree-shakeable" },
                                    { "label": "Day.js", "description": "2KB Moment.js alternative" }
                                ],
                                "multiSelect": false
                            }
                        ],
                        "answers": {
                            "Which library should we use for date formatting?": "date-fns"
                        },
                        "annotations": {
                            "Which library should we use for date formatting?": {
                                "preview": "import { format } from 'date-fns';",
                                "notes": "I prefer this because it is tree-shakeable"
                            }
                        }
                    },
                    "tool_use_id": "toolu_01AAAAAAAAAAAAAAAAAAAAAA"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Questions.Should().HaveCount(1);
            result.ToolResponse.Questions[0].Question.Should().Contain("date formatting");
            result.ToolResponse.Questions[0].Header.Should().Be("Library");
            result.ToolResponse.Questions[0].Options.Should().HaveCount(2);
            result.ToolResponse.Questions[0].Options[0].Label.Should().Be("date-fns");
            result.ToolResponse.Questions[0].MultiSelect.Should().BeFalse();

            result.ToolResponse.Answers.Should().HaveCount(1);
            result.ToolResponse.Answers["Which library should we use for date formatting?"].Should().Be("date-fns");

            result.ToolResponse.Annotations.Should().NotBeNull();
            result.ToolResponse.Annotations.Should().HaveCount(1);
            var annotation = result.ToolResponse.Annotations!["Which library should we use for date formatting?"];
            annotation.Preview.Should().Contain("date-fns");
            annotation.Notes.Should().Contain("tree-shakeable");
        }

        [TestMethod]
        public void DeserializeAskUserQuestion_WithoutAnnotations_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "AskUserQuestion",
                    "tool_input": {
                        "questions": [
                            {
                                "question": "Which approach do you prefer?",
                                "header": "Approach",
                                "options": [
                                    { "label": "Option A", "description": "First approach" },
                                    { "label": "Option B", "description": "Second approach" }
                                ],
                                "multiSelect": false
                            }
                        ]
                    },
                    "tool_response": {
                        "questions": [
                            {
                                "question": "Which approach do you prefer?",
                                "header": "Approach",
                                "options": [
                                    { "label": "Option A", "description": "First approach" },
                                    { "label": "Option B", "description": "Second approach" }
                                ],
                                "multiSelect": false
                            }
                        ],
                        "answers": {
                            "Which approach do you prefer?": "Option A"
                        }
                    },
                    "tool_use_id": "toolu_01BBBBBBBBBBBBBBBBBBBBBB"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Questions.Should().HaveCount(1);
            result.ToolResponse.Answers.Should().HaveCount(1);
            result.ToolResponse.Answers["Which approach do you prefer?"].Should().Be("Option A");
            result.ToolResponse.Annotations.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeAskUserQuestion_MultiSelect_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "tool_name": "AskUserQuestion",
                    "tool_input": {
                        "questions": [
                            {
                                "question": "Which features do you want to enable?",
                                "header": "Features",
                                "options": [
                                    { "label": "Logging", "description": "Enable structured logging" },
                                    { "label": "Caching", "description": "Enable response caching" },
                                    { "label": "Auth", "description": "Enable authentication" }
                                ],
                                "multiSelect": true
                            }
                        ]
                    },
                    "tool_response": {
                        "questions": [
                            {
                                "question": "Which features do you want to enable?",
                                "header": "Features",
                                "options": [
                                    { "label": "Logging", "description": "Enable structured logging" },
                                    { "label": "Caching", "description": "Enable response caching" },
                                    { "label": "Auth", "description": "Enable authentication" }
                                ],
                                "multiSelect": true
                            }
                        ],
                        "answers": {
                            "Which features do you want to enable?": "Logging,Auth"
                        }
                    },
                    "tool_use_id": "toolu_01CCCCCCCCCCCCCCCCCCCCCC"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionPostToolUsePayload);

            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.Questions[0].MultiSelect.Should().BeTrue();
            result.ToolResponse.Answers["Which features do you want to enable?"].Should().Be("Logging,Auth");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void AskUserQuestionToolResponse_RoundTrip_WithAnnotations_ShouldMaintainData()
        {
            var response = new AskUserQuestionToolResponse
            {
                Questions =
                [
                    new AskUserQuestion
                    {
                        Question = "Which framework?",
                        Header = "Framework",
                        Options =
                        [
                            new AskUserQuestionOption { Label = "React", Description = "Component-based UI" },
                            new AskUserQuestionOption { Label = "Vue", Description = "Progressive framework" }
                        ],
                        MultiSelect = false
                    }
                ],
                Answers = new Dictionary<string, string>
                {
                    ["Which framework?"] = "React"
                },
                Annotations = new Dictionary<string, AskUserQuestionAnnotation>
                {
                    ["Which framework?"] = new AskUserQuestionAnnotation
                    {
                        Preview = "import React from 'react';",
                        Notes = "Team already knows React"
                    }
                }
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.AskUserQuestionToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionToolResponse);

            result.Should().NotBeNull();
            result!.Questions.Should().HaveCount(1);
            result.Questions[0].Question.Should().Be("Which framework?");
            result.Questions[0].Options.Should().HaveCount(2);
            result.Answers["Which framework?"].Should().Be("React");
            result.Annotations.Should().NotBeNull();
            result.Annotations!["Which framework?"].Preview.Should().Contain("React");
            result.Annotations["Which framework?"].Notes.Should().Contain("Team already knows");
        }

        [TestMethod]
        public void AskUserQuestionToolResponse_RoundTrip_WithoutAnnotations_ShouldOmitNull()
        {
            var response = new AskUserQuestionToolResponse
            {
                Questions =
                [
                    new AskUserQuestion
                    {
                        Question = "Which style?",
                        Header = "Style",
                        Options =
                        [
                            new AskUserQuestionOption { Label = "CSS Modules", Description = "Scoped styles" },
                            new AskUserQuestionOption { Label = "Tailwind", Description = "Utility-first CSS" }
                        ],
                        MultiSelect = false
                    }
                ],
                Answers = new Dictionary<string, string>
                {
                    ["Which style?"] = "Tailwind"
                }
            };

            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.AskUserQuestionToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionToolResponse);

            result.Should().NotBeNull();
            result!.Answers["Which style?"].Should().Be("Tailwind");
            result.Annotations.Should().BeNull();
        }

        [TestMethod]
        public void AskUserQuestionAnnotation_RoundTrip_PartialFields_ShouldMaintainData()
        {
            var annotation = new AskUserQuestionAnnotation
            {
                Notes = "User preference noted"
            };

            var json = JsonSerializer.Serialize(annotation, ClaudeHooksJsonContext.Default.AskUserQuestionAnnotation);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.AskUserQuestionAnnotation);

            result.Should().NotBeNull();
            result!.Preview.Should().BeNull();
            result.Notes.Should().Be("User preference noted");
        }

        #endregion

    }

}
