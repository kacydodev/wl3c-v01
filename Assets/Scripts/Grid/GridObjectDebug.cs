using TMPro;
using UnityEngine;

public class GridObjectDebug : MonoBehaviour
{
    [SerializeField] private TextMeshPro debugGridTextMark;

    GridObject gridObject;
    
    public virtual void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
    
    public void SetText(string text)
    {
        if (debugGridTextMark != null)
        {
            debugGridTextMark.text = text;
        }
    }
}
