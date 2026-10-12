using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRaycast : MonoBehaviour
{
  private static MouseRaycast instance;
  private static Camera targetCamera;

  // To be selected in the inspector
  [SerializeField] private LayerMask raycastLayerMask;

  private void Start()
  {
    instance = this;
    targetCamera = Camera.main;
  }

  private void Update()
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
}
