using UnityEngine;
using UnityEngine.InputSystem;

public class LegacyMouseRaycast : MonoBehaviour
{
  static LegacyMouseRaycast instance;
  static Camera targetCamera;

  // To be selected in the inspector
  [SerializeField] LayerMask raycastLayerMask;
  [SerializeField] LayerMask unitsLayerMask;

  void Start()
  {
    targetCamera = Camera.main;
    instance = this;
  }

  void Update()
  {
    transform.position = GetPosition();
  }

  // Returns the world position of the mouse cursor based on a raycast from the camera
  // This method can be called anywhere
  public static Vector3 GetPosition()
  {
    var mousePos = Mouse.current.position.ReadValue();
    var ray = targetCamera.ScreenPointToRay(mousePos);

    if (Physics.Raycast(ray, out var hit, float.MaxValue, instance.raycastLayerMask))
    {
      if (hit.collider.CompareTag("SelectableUnits"))
      {
        return Vector3.zero;
      }
      return hit.point;
    }
    return Vector3.zero;
  }

  public static Unit GetUnit()
  {
    var mousePos = Mouse.current.position.ReadValue();
    var ray = targetCamera.ScreenPointToRay(mousePos);
    
    if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.unitsLayerMask))
    {
      return raycastHit.transform.TryGetComponent<Unit>(out Unit unit) ? unit : null;
    }
    
    return null;
  }
}
