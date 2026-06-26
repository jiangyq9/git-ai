---
name: ask
description: "Answer questions about AI-authored code by inspecting supported Git AI authorship data for selected lines."
argument-hint: "[a question about selected code or a specific file/range]"
allowed-tools: ["Bash(git-ai:*)", "Read", "Glob", "Grep", "Task"]
---

# Ask Skill

Answer questions about AI-written code by finding the original prompts and conversations that produced it, then **embodying the author agent's perspective** to answer.

## Main Agent's Job (you)

You do the prep work, then hand off to a **fast, tightly scoped subagent**:

1. **Resolve the file path and line range** — check these sources in order:

   **a) Editor selection context (most common).** When the user has selected code, extract file + lines from system-reminder.

   **b) Explicit file/line references** — use directly.

   **c) Named symbol** — locate definition via Read.

   **d) File only** — whole file (no line range).

   **e) No reference** → respond:
   > Select some code or mention a specific file/symbol, then `/ask` your question.

2. Spawn one subagent with `max_turns: 4`

3. Relay result

## Subagent Configuration

```text
Task tool settings:
  subagent_type: "general-purpose"
  max_turns: 4
  allowed tools: Bash, Read
```

## Command Usage

Use:

```bash
git-ai blame <file> -L <start>,<end> --show-prompt | cat
```

Prefer `blame --show-prompt` over search when possible.

## Subagent Prompt Template

```text
QUESTION: {question}
FILE: {file_path}
LINES: {start}-{end}

STEP 1: run git-ai blame
STEP 2: read file
STEP 3: answer
```

## Constraints

- Do NOT use Glob, Grep, Task in subagent
- Do NOT exceed 1 blame command per run
- Do NOT call git-ai search, git-ai prompts
- Do NOT read agent logs
