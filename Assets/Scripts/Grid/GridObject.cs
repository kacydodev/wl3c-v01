using UnityEngine;

public class GridObject
{
  private GridSystem gridSystem;
  private GridPosition gridPosition;
  private Unit unit;

  public GridObject(GridSystem gridSystem, GridPosition gridPosition)
  {
    this.gridSystem = gridSystem;
    this.gridPosition = gridPosition;
  }

  public GridPosition GridPosition() => gridPosition;

  public override string ToString() => $"{gridPosition}\n{unit}";

  public void SetUnit(Unit unit)
  {
    if (unit == null)
    {
      this.unit = null;
      return;
    }
    
    this.unit = unit;

    Vector3 worldPosition = gridSystem.GetWorldPosition(gridPosition);
    
    unit.SetTargetPosition(worldPosition);
    // unit.Move(worldPosition);
    // OnGridPositionChanged?.Invoke(this, EventArgs.Empty);
  }

  public Unit GetUnit() => unit;
}
