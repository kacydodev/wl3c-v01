using UnityEngine;
using UnityEngine.InputSystem;

public class Unit_Alt : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  [SerializeField] private float rotateSpeed = 10f;
  private Vector3 targetPosition;

  private void Start()
  {
    // Default target to starting position to avoid moving to (0,0,0) on start
    targetPosition = transform.position;
  }

  private void Update()
  {
    // 1. Check for key press to assign destination
    if (Keyboard.current != null && Keyboard.current.tKey.wasPressedThisFrame)
    {
      Move(new Vector3(5f, 0f, 5f));
    }
    
    transform.position = Vector3.MoveTowards(
      transform.position,
      targetPosition,
      moveSpeed * Time.deltaTime
    );

    // 2. Continuous movement & rotation towards target
    // float stoppingDistance = 0.05f;
    // if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
    // {
    //   Vector3 moveDirection = (targetPosition - transform.position).normalized;
    //
    //   // Rotate toward destination
    //   if (moveDirection != Vector3.zero)
    //   {
    //     transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    //   }
    //
    //   // Move toward destination
    //   transform.position = Vector3.MoveTowards(
    //     transform.position,
    //     targetPosition,
    //     moveSpeed * Time.deltaTime
    //   );
    // }
  }

  public void Move(Vector3 targetPosition)
  {
    this.targetPosition = targetPosition;
  }
}
