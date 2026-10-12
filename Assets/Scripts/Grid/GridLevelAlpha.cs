using UnityEngine;

public class GridLevelAlpha : MonoBehaviour
{
  [SerializeField] private Unit unit;
  [SerializeField] private Transform gridObjectDebug;

  private GridSystem gridSystem;

  private void Awake()
  {
    gridSystem = new GridSystem(10, 10, 2f, unit);
    gridSystem.CreateDebugObjects(gridObjectDebug);
  }

  public void SetUnitToGridPosition(GridPosition gridPosition, Unit unit)
  {
  }

  public void GetUnitAtGridPosition(GridPosition gridPosition)
  {
  }

  public void ClearUnitAtGridPosition(GridPosition gridPosition)
  {
  }
}
