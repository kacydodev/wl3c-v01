using UnityEngine;

public class GridLevelAlpha : MonoBehaviour
{
  public static GridLevelAlpha Instance { get; private set; }
  
  private GridSystem gridSystem;
  private GridObject gridObject;
  
  [SerializeField] private Unit unit;
  [SerializeField] private Transform gridObjectDebug;

  private void Awake()
  {
    Instance = this;
    gridSystem = new GridSystem(10, 10, 2f);
    gridSystem.CreateDebugObjects(gridObjectDebug);
  }

  private void Start()
  {
  }

  public void SetUnitToGridPosition(GridPosition gridPosition, Unit unit)
  {
    var gridObject = gridSystem.GetGridObject(gridPosition);
    gridObject.SetUnit(unit);
  }

  public void GetUnitAtGridPosition(GridPosition gridPosition)
  {
    var gridObject = gridSystem.GetGridObject(gridPosition);
    gridObject.GetUnit();
  }

  public void ClearUnitAtGridPosition(GridPosition gridPosition)
  {
    var gridObject = gridSystem.GetGridObject(gridPosition);
    gridObject.SetUnit(null);
  }
}
