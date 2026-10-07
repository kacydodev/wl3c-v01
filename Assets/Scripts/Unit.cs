using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private float rotateSpeed = 10f;
  [SerializeField] private float stoppingDistance = 0.05f;
  
  private Vector3 targetPosition;

  private void Start()
  {
    // Prevent unintended movement toward (0, 0, 0) at start
    targetPosition = transform.position;
  }

  private void Update()
  {
    if (Keyboard.current != null && Keyboard.current.tKey.wasPressedThisFrame)
    {
      Move(new Vector3(5f, 0f, 5f));
    }
    
    // Only compute movement and rotation when distance remains
    if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
    {
      Vector3 moveDirection = (targetPosition - transform.position).normalized;

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
