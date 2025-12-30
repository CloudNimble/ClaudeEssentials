using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for TodoWrite tool payload serialization and deserialization.
    /// Payloads are derived from real Claude Code hook interactions with PII removed.
    /// </summary>
    [TestClass]
    public class TodoWritePayloadTests
    {

        #region PreToolUse Tests

        [TestMethod]
        public void DeserializeTodoWritePreToolUse_WithMultipleTodos_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code
            var json = """
                {
                    "session_id": "test-session-todo-001",
                    "transcript_path": "C:\\Users\\TestUser\\.claude\\projects\\TestProject\\test-session-todo-001.jsonl",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PreToolUse",
                    "tool_name": "TodoWrite",
                    "tool_input": {
                        "todos": [
                            {
                                "content": "Capture payloads from all built-in tools",
                                "status": "in_progress",
                                "activeForm": "Capturing payloads from all built-in tools"
                            },
                            {
                                "content": "Create tool response models based on captured payloads",
                                "status": "pending",
                                "activeForm": "Creating tool response models"
                            },
                            {
                                "content": "Update ClaudeHooksJsonContext with response types",
                                "status": "pending",
                                "activeForm": "Updating ClaudeHooksJsonContext"
                            }
                        ]
                    },
                    "tool_use_id": "toolu_01TestTodo001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TodoWritePreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("TodoWrite");
            result.HookEventName.Should().Be(HookEventName.PreToolUse);
            result.ToolInput.Should().NotBeNull();
            result.ToolInput!.Todos.Should().NotBeNull();
            result.ToolInput.Todos.Should().HaveCount(3);
        }

        [TestMethod]
        public void DeserializeTodoWritePreToolUse_TodoItems_ShouldHaveCorrectProperties()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "TodoWrite",
                    "tool_input": {
                        "todos": [
                            {
                                "content": "Fix the bug in login",
                                "status": "in_progress",
                                "activeForm": "Fixing the bug in login"
                            },
                            {
                                "content": "Write unit tests",
                                "status": "pending",
                                "activeForm": "Writing unit tests"
                            },
                            {
                                "content": "Review code",
                                "status": "completed",
                                "activeForm": "Reviewing code"
                            }
                        ]
                    },
                    "tool_use_id": "toolu_01TestTodoItems001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TodoWritePreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();

            var todos = result.ToolInput!.Todos;
            todos.Should().HaveCount(3);

            todos[0].Content.Should().Be("Fix the bug in login");
            todos[0].Status.Should().Be("in_progress");
            todos[0].ActiveForm.Should().Be("Fixing the bug in login");

            todos[1].Status.Should().Be("pending");
            todos[2].Status.Should().Be("completed");
        }

        [TestMethod]
        public void DeserializeTodoWritePreToolUse_EmptyTodos_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "TodoWrite",
                    "tool_input": {
                        "todos": []
                    },
                    "tool_use_id": "toolu_01TestTodoEmpty001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TodoWritePreToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolInput.Should().NotBeNull();
            result.ToolInput!.Todos.Should().BeEmpty();
        }

        #endregion

        #region PostToolUse Tests

        [TestMethod]
        public void DeserializeTodoWritePostToolUse_WithOldAndNewTodos_ShouldDeserializeCorrectly()
        {
            // Arrange - Real payload structure from Claude Code
            var json = """
                {
                    "session_id": "test-session-todo-002",
                    "cwd": "D:\\Projects\\TestProject",
                    "permission_mode": "default",
                    "hook_event_name": "PostToolUse",
                    "tool_name": "TodoWrite",
                    "tool_input": {
                        "todos": [
                            {
                                "content": "Capture payloads from all built-in tools",
                                "status": "in_progress",
                                "activeForm": "Capturing payloads from all built-in tools"
                            },
                            {
                                "content": "Create tool response models",
                                "status": "pending",
                                "activeForm": "Creating tool response models"
                            }
                        ]
                    },
                    "tool_response": {
                        "oldTodos": [],
                        "newTodos": [
                            {
                                "content": "Capture payloads from all built-in tools",
                                "status": "in_progress",
                                "activeForm": "Capturing payloads from all built-in tools"
                            },
                            {
                                "content": "Create tool response models",
                                "status": "pending",
                                "activeForm": "Creating tool response models"
                            }
                        ]
                    },
                    "tool_use_id": "toolu_01TestTodoPost001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TodoWritePostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolName.Should().Be("TodoWrite");
            result.HookEventName.Should().Be(HookEventName.PostToolUse);
            result.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.OldTodos.Should().BeEmpty();
            result.ToolResponse.NewTodos.Should().HaveCount(2);
        }

        [TestMethod]
        public void DeserializeTodoWritePostToolUse_WithUpdatedTodos_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = """
                {
                    "tool_name": "TodoWrite",
                    "tool_input": {
                        "todos": [
                            {
                                "content": "Task 1",
                                "status": "completed",
                                "activeForm": "Completing Task 1"
                            },
                            {
                                "content": "Task 2",
                                "status": "in_progress",
                                "activeForm": "Working on Task 2"
                            }
                        ]
                    },
                    "tool_response": {
                        "oldTodos": [
                            {
                                "content": "Task 1",
                                "status": "in_progress",
                                "activeForm": "Working on Task 1"
                            },
                            {
                                "content": "Task 2",
                                "status": "pending",
                                "activeForm": "Starting Task 2"
                            }
                        ],
                        "newTodos": [
                            {
                                "content": "Task 1",
                                "status": "completed",
                                "activeForm": "Completing Task 1"
                            },
                            {
                                "content": "Task 2",
                                "status": "in_progress",
                                "activeForm": "Working on Task 2"
                            }
                        ]
                    },
                    "tool_use_id": "toolu_01TestTodoUpdate001"
                }
                """;

            // Act
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TodoWritePostToolUsePayload);

            // Assert
            result.Should().NotBeNull();
            result!.ToolResponse.Should().NotBeNull();
            result.ToolResponse!.OldTodos.Should().HaveCount(2);
            result.ToolResponse.NewTodos.Should().HaveCount(2);

            // Verify status changed
            result.ToolResponse.OldTodos[0].Status.Should().Be("in_progress");
            result.ToolResponse.NewTodos[0].Status.Should().Be("completed");
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void TodoItem_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var item = new TodoItem
            {
                Content = "Test task",
                Status = "in_progress",
                ActiveForm = "Testing task"
            };

            // Act
            var json = JsonSerializer.Serialize(item, ClaudeHooksJsonContext.Default.TodoItem);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TodoItem);

            // Assert
            result.Should().NotBeNull();
            result!.Content.Should().Be(item.Content);
            result.Status.Should().Be(item.Status);
            result.ActiveForm.Should().Be(item.ActiveForm);
        }

        [TestMethod]
        public void TodoWriteToolInput_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var input = new TodoWriteToolInput
            {
                Todos =
                [
                    new TodoItem { Content = "Task 1", Status = "pending", ActiveForm = "Starting Task 1" },
                    new TodoItem { Content = "Task 2", Status = "in_progress", ActiveForm = "Working on Task 2" }
                ]
            };

            // Act
            var json = JsonSerializer.Serialize(input, ClaudeHooksJsonContext.Default.TodoWriteToolInput);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TodoWriteToolInput);

            // Assert
            result.Should().NotBeNull();
            result!.Todos.Should().HaveCount(2);
            result.Todos[0].Content.Should().Be("Task 1");
            result.Todos[1].Status.Should().Be("in_progress");
        }

        [TestMethod]
        public void TodoWriteToolResponse_RoundTrip_ShouldMaintainData()
        {
            // Arrange
            var response = new TodoWriteToolResponse
            {
                OldTodos =
                [
                    new TodoItem { Content = "Old Task", Status = "pending", ActiveForm = "Starting Old Task" }
                ],
                NewTodos =
                [
                    new TodoItem { Content = "Old Task", Status = "completed", ActiveForm = "Completing Old Task" },
                    new TodoItem { Content = "New Task", Status = "pending", ActiveForm = "Starting New Task" }
                ]
            };

            // Act
            var json = JsonSerializer.Serialize(response, ClaudeHooksJsonContext.Default.TodoWriteToolResponse);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.TodoWriteToolResponse);

            // Assert
            result.Should().NotBeNull();
            result!.OldTodos.Should().HaveCount(1);
            result.NewTodos.Should().HaveCount(2);
        }

        #endregion

    }

}
