using TMPro;
using UnityEngine;

public class GridObjectDebug : MonoBehaviour
{
  private GridObject gridObject;

  public virtual void SetGridObject(GridObject gridObject)
  {
    this.gridObject = gridObject;
  }

  public void SetText(string text)
  {
    var debugText = GetComponentInChildren<TextMeshPro>();
    if (debugText != null)
    {
      debugText.text = text;
    }
  }
}
