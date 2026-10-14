using System;
using UnityEngine;

public class UnitSelectUI : MonoBehaviour
{
  private MeshRenderer selectRingUI;
  
  [SerializeField] private Unit unit;

  private void Awake()
  {
    selectRingUI = GetComponent<MeshRenderer>();
  }

  private void Start()
  {
    UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
    updateSelectUI();
  }

  private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
  {
    updateSelectUI();
  }

  private void updateSelectUI()
  {
    if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
    {
      selectRingUI.enabled = true;
    }
    else
    {
      selectRingUI.enabled = false;
    }
  }
}
