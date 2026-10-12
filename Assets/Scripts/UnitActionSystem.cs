using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
  public static UnitActionSystem Instance { get; private set; }
  public event EventHandler OnSelectedUnitChanged;

  [SerializeField] public Unit defaultUnit;

  private Unit selectedUnit;
  [SerializeField] private LayerMask unitsLayerMask;

  private void Awake()
  {
    Instance = this;
    SetSelectedUnit(defaultUnit);
  }

  private void Update()
  {
    if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
    {
      if (TryHandleUnitSelection()) return; // Stops issue with selection and moving on the same frame

      if (MouseRaycast.GetPosition() != Vector3.zero)
      {
        selectedUnit.Move(MouseRaycast.GetPosition());
      }
    }
  }

  private bool TryHandleUnitSelection()
  {
    var mousePos = Mouse.current.position.ReadValue();
    var ray = Camera.main.ScreenPointToRay(mousePos);

    if (Physics.Raycast(ray, out var raycastHit, float.MaxValue, unitsLayerMask))
    {
      if (raycastHit.transform.TryGetComponent<Unit>(out var unit))
      {
        SetSelectedUnit(unit);
        return true;
      }
    }
    return false;
  }

  private void SetSelectedUnit(Unit unit)
  {
    selectedUnit = unit;
    OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
  }

  // This function prevents exposing selectedUnit variable
  public Unit GetSelectedUnit() => selectedUnit;
}
