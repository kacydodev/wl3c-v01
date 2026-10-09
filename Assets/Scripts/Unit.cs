using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
  [SerializeField] float moveSpeed = 5f;
  [SerializeField] float rotateSpeed = 10f;
  [SerializeField] float stoppingDistance = 0.05f;
  private Animator unitAnimator;
  private Vector3 targetPosition;

  void Awake()
  {
    targetPosition = transform.position;
    unitAnimator = GetComponentInChildren<Animator>();
  }

  void Update()
  {
    // Mouse Raycast Control
    if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
    {
      if (MouseRaycast.GetPosition() != Vector3.zero)
      {
        Move(MouseRaycast.GetPosition());
      }
      else
      {
        // TODO: remove on prod
        Debug.Log("MouseRaycast: No ground collider detected.");
      }
    }

    // Movement and Rotation Control
    if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
    {
      var moveDirection = (targetPosition - transform.position).normalized;
      unitAnimator.SetBool("IsWalking", true);

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
    else
    {
      unitAnimator.SetBool("IsWalking", false);
    }
  }

  public void Move(Vector3 targetPosition)
  {
    this.targetPosition = targetPosition;
  }
}
