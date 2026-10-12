using UnityEngine;

public class GridLevelAlpha : MonoBehaviour
{
  [SerializeField] Transform gridObjectDebug;

  GridSystem gridSystem;

  private void Awake()
  {
    gridSystem = new GridSystem(10, 10, 2f);
    gridSystem.CreateDebugObjects(gridObjectDebug);
  }
}
