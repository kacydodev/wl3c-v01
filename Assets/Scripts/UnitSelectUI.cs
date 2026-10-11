using System;
using UnityEngine;

public class UnitSelectUI : MonoBehaviour
{
  [SerializeField] Unit unit;
  MeshRenderer selectRingUI;

  void Awake()
  {
    selectRingUI = GetComponent<MeshRenderer>();
  }

  void Start()
  {
    UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
    updateSelectUI();
  }

  void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
  {
    updateSelectUI();
  }

  void updateSelectUI()
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
