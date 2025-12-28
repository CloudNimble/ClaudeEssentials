# GitHub Actions Workflows

This directory contains the GitHub Actions workflows for ClaudeEssentials.

## Workflows

### Build and Deploy (`build-and-deploy.yml`)

Builds, tests, and deploys the ClaudeEssentials NuGet package.

**Triggers:**
- Push to `main` or `dev` branches
- Manual dispatch with optional NuGet deployment

**Jobs:**
1. **build** - Restore, build, test, and pack
2. **deploy** - Push to NuGet.org (main/dev branches only)
3. **create-release** - Create GitHub release (main branch only)

### PR Validation (`pr-validation.yml`)

Validates pull requests before merge.

**Triggers:**
- Pull requests to `main` or `dev` branches

**Checks:**
- Build succeeds
- All tests pass
- Package can be created

## Configuration

### Repository Variables

Set in Settings → Secrets and variables → Actions → Variables:

| Variable | Description | Default |
|----------|-------------|---------|
| `VERSION_MAJOR` | Major version | `1` |
| `VERSION_MINOR` | Minor version | `0` |
| `VERSION_PATCH` | Patch version (auto-incremented on main) | `0` |
| `VERSION_PREVIEW_SUFFIX` | Preview counter (auto-incremented on dev) | `0` |

### Repository Secrets

| Secret | Description |
|--------|-------------|
| `NUGET_API_KEY` | NuGet.org API key |
| `REPO_ACCESS_TOKEN` | GitHub PAT for updating variables |

## Versioning

| Branch | Format | Example |
|--------|--------|---------|
| `main` | `{major}.{minor}.{patch}` | `1.0.5` |
| `dev` | `{major}.{minor}.{patch}-preview.{n}` | `1.0.5-preview.3` |
| PR/Feature | `{major}.{minor}.0-PR-{timestamp}` | `1.0.0-PR-20250101-120000` |

Version variables are automatically updated after successful deployments.

## Requirements

- .NET 10 (installed automatically)
- Windows runner (for .NET 10 compatibility)
