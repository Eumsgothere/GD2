using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSc : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController charController;
    [SerializeField] private InputActionAsset inputAction;
    [SerializeField] private Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private Transform respawnPoint;

    private InputAction move, look, sprint, jump, melee;
    private Vector2 moveInput, lookInput;
    private Vector3 velocity;
    private bool isSprinting;
    private bool hasWon = false;
    private bool isJumping = false;

    [Header("Combat")]
    public Collider handHitbox;

    private bool isGrounded => charController.isGrounded;

    private void OnEnable()
    {
        var map = inputAction.FindActionMap("Player");
        map.Enable();

        move = map.FindAction("Move");
        look = map.FindAction("Look");
        sprint = map.FindAction("Sprint");
        jump = map.FindAction("Jump");
        melee = map.FindAction("Meele");

        sprint.performed += ctx => { if (!hasWon) isSprinting = true; };
        sprint.canceled += ctx => { if (!hasWon) isSprinting = false; };
        jump.performed += JumpInput;
        melee.performed += MeleeAttack;
    }

    private void Update()
    {
        if (!hasWon)
        {
            HandleMovement();
            HandleRotation();

            if (transform.position.y < -10f)
                Respawn();
        }
    }

    private void HandleMovement()
    {
        moveInput = move.ReadValue<Vector2>();
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
        Vector3 moveDir = transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y)) * currentSpeed;

        // Vertical movement
        if (isGrounded && velocity.y <= 0)
        {
            velocity.y = -1f;
            animator.SetBool("Fall", false);

            if (isJumping)
                isJumping = false;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
            if (velocity.y < -0.01f)
                animator.SetBool("Fall", true);
        }

        charController.Move((moveDir + velocity) * Time.deltaTime);

        // Animator blend tree
        float forward = moveInput.y;
        float walkAmount = Mathf.Abs(moveInput.y);
        animator.SetFloat("Walk", walkAmount);
        animator.SetFloat("Forward", forward);
        animator.SetBool("Run", isSprinting && forward > 0.1f);
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        if (hasWon || isJumping || !isGrounded) return;

        isJumping = true;
        velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);

        // Trigger jump animation immediately
        animator.ResetTrigger("Meele");
        animator.SetTrigger("Jump");
        animator.SetBool("Fall", false);
    }

    private void MeleeAttack(InputAction.CallbackContext context)
    {
        if (hasWon || isJumping) return;

        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Meele");
        animator.SetTrigger("Meele");
    }

    private void HandleRotation()
    {
        lookInput = look.ReadValue<Vector2>();
        transform.Rotate(0f, lookInput.x, 0f);
    }

    private void Respawn()
    {
        velocity = Vector3.zero;
        charController.enabled = false;
        transform.position = respawnPoint.position;
        charController.enabled = true;
    }

    public void Win()
    {
        hasWon = true;
        charController.enabled = false;

        animator.SetFloat("Walk", 0f);
        animator.SetBool("Run", false);
        animator.SetBool("Fall", false);

        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Meele");
        animator.SetTrigger("Win");

        Debug.Log("Win animation triggered!");
    }
}
