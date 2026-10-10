using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
  // A HashSet prevents duplicate references and provides O(1) containment checks
  private HashSet<GameObject> SelectableUnits = new HashSet<GameObject>();
  GameObject SelectedUnit;
  [SerializeField] GameObject DefaultUnit;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    PopulateSelectableUnits();
    SetSelectedUnit(DefaultUnit);
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
          SelectedUnit.GetComponent<Unit>().isSelected = false;
          SelectedUnit = null;
          SetSelectedUnit(unit);
        }
      }
    }
  }

  void SetSelectedUnit(GameObject unit)
  {
    float delay = 0.25f;
    
    SelectedUnit = unit;
    SelectedUnit.GetComponent<Unit>().isSelected = true;
    SelectedUnit.GetComponent<Unit>().selectedDelay = delay;
    // Debug.Log($"Selected: {SelectedUnit}");
  }

  // Finds all objects with the 'SelectableUnits' tag and stores them uniquely.
  void PopulateSelectableUnits()
  {
    GameObject[] foundUnits = GameObject.FindGameObjectsWithTag("SelectableUnits");

    foreach (GameObject unit in foundUnits)
    {
      if (unit)
      {
        SelectableUnits.Add(unit);
      }
    }

    // Debug.Log($"Successfully registered {SelectableUnits.Count} unique selectable unit(s).");
  }
}
