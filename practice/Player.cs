using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] CharacterController charController;


    [SerializeField] InputActionAsset inputAction;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletLoc;
    [SerializeField] GameController gameController;
    InputAction move, look;
    [SerializeField] float moveSpeed = 20f;
    Vector2 moveInput, lookInput;
    Vector3 moveVector;
    float gravity = 9.8f;
    Vector3 verticalMove = Vector3.up;
    Vector3 originalPosition;
    int ammo = 10;
    public int Ammo { get => ammo; set => ammo = value; }
    public int GetAmmo()
    {
        return ammo;
    }

    void Start()
    {
        originalPosition = transform.position;
    }

    private void OnEnable()
    {
        InputActionMap map = inputAction.FindActionMap("Player");
        map.Enable();

        move = map.FindAction("Move");
        look = map.FindAction("Look");

        map.FindAction("Attack").performed += Shoot_e;
        map.FindAction("Reload").performed += Reload_e;
    }

    private void Reload_e(InputAction.CallbackContext obj)
    {
        if (!gameController.IsOk) return;
        ammo = 10;
    }
    private void Shoot_e(InputAction.CallbackContext obj)
    {
        if (!gameController.IsOk) return;
        if (Ammo <= 0) return;
        Instantiate(bulletPrefab, bulletLoc.transform.position, bulletLoc.transform.rotation);
        Ammo--;
    }

    private void OnDisable()
    {
        inputAction.FindActionMap("Player").Disable();
    }

    private void FixedUpdate()
    {
        if (gameController.IsOk)
        {

            moveInput = move.ReadValue<Vector2>() * Time.deltaTime * moveSpeed;

            if (charController.isGrounded)
            {
                verticalMove.y = 0f;
            }
            else
            {

                verticalMove.y -= gravity * Time.deltaTime;
            }
            moveVector = transform.TransformDirection(new Vector3(moveInput.x, verticalMove.y, moveInput.y));

            charController.Move(moveVector);
            lookInput = look.ReadValue<Vector2>();

            transform.Rotate(0f, lookInput.x, 0f);
        }

        if (transform.position.y < -50f)
            transform.position = originalPosition;
    }

    // Allows external scripts to change the player�s material color.
    public void SetPlayerColor(Color newColor)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = newColor;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject objectHit = hit.gameObject;
        if (objectHit.name == "finalPlatform")
        {
            gameController.FinalPlatformReached();
        }
    }

    void Update()
    {

    }
}