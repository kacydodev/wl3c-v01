using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
  [SerializeField] float moveSpeed = 5f;
  [SerializeField] float rotateSpeed = 10f;
  [SerializeField] float stoppingDistance = 0.05f;

  Vector3 targetPosition;

  void Awake()
  {
    // Prevent unintended movement toward (0, 0, 0) at start
    targetPosition = transform.position;
  }

  void Update()
  {
    if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
    {
      // Only move if the mouse raycast hits a valid position (not Vector3.zero)
      if (MouseRaycast.GetPosition() != Vector3.zero)
      {
        Move(MouseRaycast.GetPosition());
      }
      // TODO: remove on prod
      else
      {
        Debug.Log("MouseRaycast: No ground collider detected.");
      }
    }

    // Only compute movement and rotation when distance remains
    if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
    {
      var moveDirection = (targetPosition - transform.position).normalized;

      if (moveDirection != Vector3.zero)
      {
        transform.forward = Vector3.Lerp(
          transform.forward,
          moveDirection,
          Time.deltaTime * rotateSpeed
        );
      }

      transform.position = Vector3.MoveTowards(
        transform.position,
        targetPosition,
        moveSpeed * Time.deltaTime
      );
    }
  }

  public void Move(Vector3 targetPosition)
  {
    this.targetPosition = targetPosition;
  }
}
