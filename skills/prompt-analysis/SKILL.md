---
name: prompt-analysis
description: "Analyze AI prompting patterns using Git AI CLI"
argument-hint: "[question about prompts]"
allowed-tools: ["Bash(git-ai:*)", "Read", "Glob", "Grep", "Task"]
---

# Prompt Analysis Skill

This version removes `prompts.db` dependency and uses only git-ai CLI.

## Supported commands
- git-ai stats
- git-ai diff --include-stats --all-prompts
- git-ai blame --show-prompt
- git-ai show

## Example
```bash
git-ai stats <commit> --json
```
