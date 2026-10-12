---
sessionId: session-261012-192148-xt40
---

# Requirements

### Overview & Goals
Provide a structured method to instantiate grid debug text objects as children of the existing `GridDebugObject` in the scene hierarchy, ensuring a clean and manageable Unity hierarchy without altering unnecessary components.

### Scope
- **In Scope**:
  - Updating `GridSystem.CreateDebugObjects` to accept a parent `Transform`.
  - Passing parent reference from caller `Testing.cs`.
- **Out Scope**:
  - Modifying grid data representation or coordinate calculation logic.
  - Runtime modifications to non-debug game objects.

### User Stories
- As a developer, I want all instantiated grid debug objects to be grouped under `GridDebugObject` in the scene hierarchy so that the hierarchy remains clean and easily toggleable.

### Functional Requirements
- Instantiated debug objects must be placed under the specified parent `Transform` in the Unity scene hierarchy.
- Debug objects must retain their correct world position and rotation (`Quaternion.Euler(90, 0, 0)`).

# Technical Design

### Current Implementation
- `GridSystem.CreateDebugObjects(Transform debugPrefab)` instantiates `debugPrefab` at root level using `GameObject.Instantiate(debugPrefab, GetWorldPosition(x, z), Quaternion.Euler(90, 0, 0))`.
- `Testing.cs` calls `gridSystem.CreateDebugObjects(debugGridTextMark)` during `Start()`.

### Key Decisions
- **Pass Transform from MonoBehaviour**: Since `GridSystem` is a standard C# class and does not inherit from `MonoBehaviour`, scene references cannot be directly serialized inside it. Exposing the parent field on `Testing.cs` and passing it via method argument maintains separation of concerns and avoids costly runtime scene searches.

### Proposed Changes
- In `GridSystem.cs`:
  - Modify `CreateDebugObjects(Transform debugPrefab, Transform parent = null)` to pass `parent` to `GameObject.Instantiate`.
- In `Testing.cs`:
  - Add `[SerializeField] private Transform gridDebugObject;` and pass it to `gridSystem.CreateDebugObjects(debugGridTextMark, gridDebugObject)`.

### Architecture Diagram
```mermaid
graph LR
    Testing[Testing MonoBehaviour] -->|Passes Parent & Prefab| GridSystem[GridSystem]
    GridSystem -->|Instantiates under Parent| GridDebugObject[GridDebugObject in Scene]
```

# Delivery Steps

### ✓ Step 1: Update GridSystem to accept parent transform parameter
Support parenting debug instantiated objects under a designated transform container.

- Extend `CreateDebugObjects` method in `GridSystem.cs` to accept an optional `Transform parent` parameter.
- Pass the parent transform to `GameObject.Instantiate(debugPrefab, GetWorldPosition(x, z), Quaternion.Euler(90, 0, 0), parent)`.

### ✓ Step 2: Wire parent transform through Testing MonoBehaviour
Link the scene GameObject `GridDebugObject` to `Testing` and pass it to grid debug instantiation.

- Add `[SerializeField] private Transform gridDebugObjectParent;` field in `Testing.cs`.
- Update `Start()` in `Testing.cs` to pass `gridDebugObjectParent` when calling `gridSystem.CreateDebugObjects(debugGridTextMark, gridDebugObjectParent)`.
- Assign the `GridDebugObject` from the scene hierarchy in the Unity Inspector.