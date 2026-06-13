# StevenSoftware - AI EXECUTION CONTRACT

This file defines strict rules for any AI-assisted code changes in this repository.

---

## 1. CORE PRINCIPLE (MOST IMPORTANT)

The AI must ONLY make changes that are explicitly requested.

- Do NOT refactor code unless explicitly asked
- Do NOT rename endpoints, methods, or DTOs unless explicitly asked
- Do NOT remove existing logic unless required to implement the requested change
- Do NOT "improve", "clean up", or "modernize" unrelated code
- Do NOT change architecture decisions unless explicitly instructed

If something must be changed to complete the requested task, it is allowed ONLY if it is directly required for that task.

---

## 2. SAFE CHANGE RULE

All changes must satisfy:

- Minimal diff principle (smallest possible change)
- Localized edits only (do not touch unrelated files or logic)
- Preserve existing API contracts unless explicitly told otherwise
- Preserve database, caching, and authentication logic unless explicitly told otherwise

---

## 3. API RULES (ABSOLUTE)

- Do not change HTTP methods (GET/POST/PUT/DELETE) unless requested
- Do not rename endpoints unless requested
- Do not change request/response DTO shapes unless requested
- Do not change authentication behavior unless requested

---

## 4. BACKEND RULES (.NET)

- Keep controllers thin
- Do not move logic between layers unless requested
- Do not remove IMemoryCache or similar infrastructure unless requested
- Preserve service and repository structure unless explicitly asked to change

---

## 5. FRONTEND RULES (VUE)

- Do not change API usage patterns unless required
- Respect existing response shapes
- Do not refactor components unless requested

---

## 6. CHANGE INTERPRETATION RULE

If a request is ambiguous:

- Prefer the smallest possible change
- Ask a clarification question instead of guessing
- Never perform architectural refactors as a "best guess"

---

## 7. WHEN CHANGES ARE ALLOWED TO REMOVE/REPLACE CODE

Only if ALL conditions are true:

- It is required to implement the requested feature
- There is no way to implement it without modifying that code
- The change is minimal and directly related to the request

---

## 8. BUSINESS CONTEXT (REFERENCE ONLY)

- Lead generation website
- SEO-focused marketing platform
- Software development services

These are guiding priorities, NOT reasons to modify code unless explicitly requested.

---

## 9. FINAL RULE

When executing tasks:

- Follow instructions exactly
- Prefer correctness over creativity
- Never surprise the user with unrelated changes