using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LegacySelectionManager : MonoBehaviour
{
  // A HashSet prevents duplicate references and provides O(1) containment checks
  private HashSet<Unit> SelectableUnits = new HashSet<Unit>();
  Unit selectedUnit;
  [SerializeField] Unit defaultUnit;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Awake()
  {
    PopulateSelectableUnits();
    SetSelectedUnit(defaultUnit);
    // Debug.Log($"Default: {SelectedUnit}");
  }

  void Update()
  {
    var unit = MouseRaycast.GetUnit();

    if (unit)
    {
      if (Mouse.current.leftButton.wasPressedThisFrame)
      {
        if (unit && SelectableUnits.Contains(unit))
        {
          selectedUnit.GetComponent<Unit>().isSelected = false;
          selectedUnit = null;
          SetSelectedUnit(unit);
        }
      }
    }
  }

  void SetSelectedUnit(Unit unit)
  {
    float delay = 0.25f;
    
    selectedUnit = unit;
    selectedUnit.GetComponent<Unit>().isSelected = true;
    selectedUnit.GetComponent<Unit>().selectedDelay = delay;
    // Debug.Log($"Selected: {SelectedUnit}");
  }

  // Finds all objects with the 'SelectableUnits' tag and stores them uniquely.
  void PopulateSelectableUnits()
  {
    GameObject[] foundUnits = GameObject.FindGameObjectsWithTag("SelectableUnits");
    
    foreach(GameObject unit in foundUnits)
    {
      if (unit)
      {
        var unitComponent = unit.GetComponent<Unit>();
        if (unitComponent != null)
        {
          SelectableUnits.Add(unitComponent);
        }
      }
    }

    // foreach (GameObject unit in foundUnits)
    // {
    //   if (unit)
    //   {
    //     SelectableUnits.Add(unit);
    //   }
    // }

    // Debug.Log($"Successfully registered {SelectableUnits.Count} unique selectable unit(s).");
  }
}
