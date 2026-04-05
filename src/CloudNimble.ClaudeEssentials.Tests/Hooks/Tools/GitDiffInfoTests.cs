using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Tests.Hooks.Tools
{

    /// <summary>
    /// Tests for <see cref="GitDiffInfo"/> deserialization and round-trips.
    /// Payload structures match real captured hook interactions with all PII removed.
    /// </summary>
    [TestClass]
    public class GitDiffInfoTests
    {

        #region Deserialization Tests

        [TestMethod]
        public void DeserializeGitDiffInfo_WithRepository_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "filename": "src/Models/User.cs",
                    "status": "modified",
                    "additions": 5,
                    "deletions": 2,
                    "changes": 7,
                    "patch": "@@ -10,7 +10,10 @@ namespace TestProject.Models\n     public class User\n     {\n         public int Id { get; set; }\n-        public string Name { get; set; }\n+        public string Name { get; set; } = string.Empty;\n+        public string Email { get; set; } = string.Empty;\n+        public DateTime CreatedAt { get; set; }\n     }\n }",
                    "repository": "TestOrg/TestProject"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GitDiffInfo);

            result.Should().NotBeNull();
            result!.Filename.Should().Be("src/Models/User.cs");
            result.Status.Should().Be(GitDiffStatus.Modified);
            result.Additions.Should().Be(5);
            result.Deletions.Should().Be(2);
            result.Changes.Should().Be(7);
            result.Patch.Should().Contain("public string Name");
            result.Repository.Should().Be("TestOrg/TestProject");
        }

        [TestMethod]
        public void DeserializeGitDiffInfo_WithoutRepository_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "filename": "src/Services/DataService.cs",
                    "status": "added",
                    "additions": 25,
                    "deletions": 0,
                    "changes": 25,
                    "patch": "@@ -0,0 +1,25 @@\n+using System;\n+\n+namespace TestProject.Services\n+{\n+    public class DataService { }\n+}"
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GitDiffInfo);

            result.Should().NotBeNull();
            result!.Filename.Should().Be("src/Services/DataService.cs");
            result.Status.Should().Be(GitDiffStatus.Added);
            result.Additions.Should().Be(25);
            result.Deletions.Should().Be(0);
            result.Changes.Should().Be(25);
            result.Patch.Should().Contain("DataService");
            result.Repository.Should().BeNull();
        }

        [TestMethod]
        public void DeserializeGitDiffInfo_ZeroChanges_ShouldDeserializeCorrectly()
        {
            var json = """
                {
                    "filename": "README.md",
                    "status": "modified",
                    "additions": 0,
                    "deletions": 0,
                    "changes": 0,
                    "patch": ""
                }
                """;

            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GitDiffInfo);

            result.Should().NotBeNull();
            result!.Filename.Should().Be("README.md");
            result.Additions.Should().Be(0);
            result.Deletions.Should().Be(0);
            result.Changes.Should().Be(0);
            result.Patch.Should().BeEmpty();
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void GitDiffInfo_RoundTrip_WithRepository_ShouldMaintainData()
        {
            var diffInfo = new GitDiffInfo
            {
                Filename = "src/Program.cs",
                Status = GitDiffStatus.Modified,
                Additions = 3,
                Deletions = 1,
                Changes = 4,
                Patch = "@@ -1,5 +1,7 @@\n using System;\n+using System.Linq;\n",
                Repository = "TestOrg/TestProject"
            };

            var json = JsonSerializer.Serialize(diffInfo, ClaudeHooksJsonContext.Default.GitDiffInfo);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GitDiffInfo);

            result.Should().NotBeNull();
            result!.Filename.Should().Be("src/Program.cs");
            result.Status.Should().Be(GitDiffStatus.Modified);
            result.Additions.Should().Be(3);
            result.Deletions.Should().Be(1);
            result.Changes.Should().Be(4);
            result.Patch.Should().Contain("System.Linq");
            result.Repository.Should().Be("TestOrg/TestProject");
        }

        [TestMethod]
        public void GitDiffInfo_RoundTrip_WithoutRepository_ShouldOmitNull()
        {
            var diffInfo = new GitDiffInfo
            {
                Filename = "src/NewFile.cs",
                Status = GitDiffStatus.Added,
                Additions = 10,
                Deletions = 0,
                Changes = 10,
                Patch = "@@ -0,0 +1,10 @@\n+namespace Test { }"
            };

            var json = JsonSerializer.Serialize(diffInfo, ClaudeHooksJsonContext.Default.GitDiffInfo);
            var result = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.GitDiffInfo);

            result.Should().NotBeNull();
            result!.Filename.Should().Be("src/NewFile.cs");
            result.Status.Should().Be(GitDiffStatus.Added);
            result.Repository.Should().BeNull();
        }

        #endregion

    }

}
