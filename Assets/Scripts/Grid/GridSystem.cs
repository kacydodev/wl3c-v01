using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GridSystem
{
  private int width;
  private int height;
  private float cellSize;
  private GridObject[,] gridObjectArray;

  private Unit unit;

  public GridSystem(int width, int height, float cellSize, Unit unit)
  {
    this.width = width;
    this.height = height;
    this.cellSize = cellSize;

    gridObjectArray = new GridObject[width, height];

    for (var x = 0; x < width; x++)
    {
      for (var z = 0; z < height; z++)
      {
        var gridPosition = new GridPosition(x, z);
        gridObjectArray[x, z] = new GridObject(this, gridPosition, unit);
      }
    }
  }

  public Vector3 GetWorldPosition(int x, int z) => new Vector3(x, 0, z) * cellSize;

  // Convert world position to grid position imposed by custom grid system
  public GridPosition GetGridPosition(Vector3 worldPosition) => new(
    // Use Mathf to convert float world pos to int
    Mathf.FloorToInt(worldPosition.x / cellSize),
    Mathf.FloorToInt(worldPosition.z / cellSize)
  );

  // TODO: Remove on prod
  public void CreateDebugObjects(Transform debugPrefab)
  {
    Debug.Log(unit);
    var gridObjectDebugParent = new GameObject("DEBUG_GridObjectDebugParent").transform;

    for (var x = 0; x < width; x++)
    {
      for (var z = 0; z < height; z++)
      {
        var debugTransform = GameObject.Instantiate(
          debugPrefab,
          GetWorldPosition(x, z),
          Quaternion.identity,
          gridObjectDebugParent
        );

        var gridObjectDebug = debugTransform.GetComponent<GridObjectDebug>();

        if (gridObjectDebug != null)
        {
          gridObjectDebug.SetGridObject(gridObjectArray[x, z]);
          gridObjectDebug.SetText(gridObjectArray[x, z].ToString());
        }
      }
    }
  }

  // TODO: Remove on prod
  public void DrawGridObject(int x, int z, Color color, float duration = 0f)
  {
    var bottomLeft = GetWorldPosition(x, z);
    var bottomRight = GetWorldPosition(x + 1, z);
    var topLeft = GetWorldPosition(x, z + 1);
    var topRight = GetWorldPosition(x + 1, z + 1);

    // Bottom edge
    Debug.DrawLine(bottomLeft, bottomRight, color, duration);
    // Left edge
    Debug.DrawLine(bottomLeft, topLeft, color, duration);
    // Top edge
    Debug.DrawLine(topLeft, topRight, color, duration);
    // Right edge
    Debug.DrawLine(bottomRight, topRight, color, duration);
  }
}
