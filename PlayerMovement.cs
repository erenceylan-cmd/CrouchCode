using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float crouchSpeed = 2f;
    public float crouchHeght = 1f;
    public float standHeight = 2f;
    public Transform cameraTransform;

    private CharacterController controller;
    private bool isCrouching = false;
    private float crouchHeight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        float moveAmount = Mathf.Clamp01(direction.magnitude);
        animator.SetFloat("Speed", moveAmount);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = rotation;
            Vector3 moveDir = rotation * Vector3.forward;
            float currentSpeed = isCrouching ? crouchSpeed : speed;
            controller.Move(moveDir * currentSpeed * Time.deltaTime);

        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
            controller.height = isCrouching ? crouchHeight : standHeight;
            animator.SetBool("IsCrouching", isCrouching);
        }

    }
}