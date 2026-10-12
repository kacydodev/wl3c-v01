using UnityEngine;

public class GridObject
{
  private GridSystem gridSystem;
  private GridPosition gridPosition;
  private Unit unit;

  public GridObject(GridSystem gridSystem, GridPosition gridPosition, Unit unit)
  {
    this.gridSystem = gridSystem;
    this.gridPosition = gridPosition;
    this.unit = unit;
  }

  public GridPosition GetGridPosition() => gridPosition;

  public override string ToString() => gridPosition.ToString();
  // return $"{gridPosition.ToString()} {unit.name.ToString()}";
}
