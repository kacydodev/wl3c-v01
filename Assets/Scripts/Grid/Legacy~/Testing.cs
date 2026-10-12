using UnityEngine.InputSystem;
using UnityEngine;

public class Testing : MonoBehaviour
{
  [SerializeField] private int width;
  [SerializeField] private int height;
  [SerializeField] private float cellSize;
  
  // DEBUG_GridTextMark Object
  [SerializeField] private Transform gridObjectDebug;
  [SerializeField] private Transform gridObjectDebugParent;
  
  private GridSystem gridSystem;

  void Start()
  {
    width = 10;
    height = 10;
    cellSize = 2f;
    gridSystem = new GridSystem(width, height, cellSize);
    
    gridSystem.CreateDebugObjects(gridObjectDebug, gridObjectDebugParent);
  }

  void Update()
  {
    if (Mouse.current.leftButton.wasPressedThisFrame)
    {
      Debug.Log(gridSystem.GetGridPosition(MouseRaycast.GetPosition()));
    }
    
    for (int x = 0; x < width; x++)
    {
      for (int z = 0; z < height; z++)
      {
        gridSystem.DrawGridObject(x, z, Color.slateBlue);
      }
    }
  }
}
