using UnityEngine;
using UnityEngine.InputSystem;

public class LegacyUnit : MonoBehaviour
{
  [SerializeField] float moveSpeed = 5f;
  [SerializeField] float rotateSpeed = 10f;
  [SerializeField] float stoppingDistance = 0.05f;
  [SerializeField] public bool isSelected;
  [SerializeField] public float selectedDelay;
  [SerializeField] GameObject uiSelectRing;
  private Animator unitAnimator;
  private Vector3 targetPosition;

  void Awake()
  {
    targetPosition = transform.position;
    unitAnimator = GetComponentInChildren<Animator>();
    selectedDelay = 0;
  }

  void Update()
  {
    SetSelectRing(isSelected);
    
    // Mouse Raycast Control
    if (isSelected && selectedDelay <= 0)
    {
      if (Mouse.current.leftButton.wasPressedThisFrame)
      {
        if (MouseRaycast.GetPosition() != Vector3.zero)
        {
          Move(MouseRaycast.GetPosition());
        }
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

  void SetSelectRing(bool isActive)
  {
    if(selectedDelay > 0)
    {
      selectedDelay -= Time.deltaTime;
    }
    uiSelectRing.SetActive(isActive);
  }

  public void Move(Vector3 targetPosition)
  {
    this.targetPosition = targetPosition;
  }
}
