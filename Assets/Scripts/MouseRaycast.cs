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
    targetCamera = Camera.main;
    instance = this;
  }

  private void Update()
  {
    transform.position = MouseRaycast.GetPositon();
  }
  
  // Returns the world position of the mouse cursor based on a raycast from the camera
  // This method can be called anywhere
  public static Vector3 GetPositon()
  {
    Vector2 mousePos = Mouse.current.position.ReadValue();
    Ray ray = targetCamera.ScreenPointToRay(mousePos);
    Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, instance.raycastLayerMask);
    return hit.point;
  }
}
