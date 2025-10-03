using UnityEngine;
using UnityEngine.InputSystem;

public class Player_script : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputAction;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject targetPrefab;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    private int kills;
    private Vector3 spawnPoint;

    private GameObject[] firstTargetLocation;
    private GameObject[] secondTargetLocation;

    private bool platform2col, platform3col;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 30f;
    [SerializeField] private float gravity = -9.81f;
    private float verticalVelocity;

    public int Kills { get => kills; set => kills = value; }

    private void Start()
    {

        var playerMap = inputAction.FindActionMap("Player");
        moveAction = playerMap.FindAction("Move");
        lookAction = playerMap.FindAction("Look");
        jumpAction = playerMap.FindAction("Jump");

        jumpAction.performed += Jump_performed;

        firstTargetLocation = GameObject.FindGameObjectsWithTag("plat2");
        secondTargetLocation = GameObject.FindGameObjectsWithTag("plat3");

        characterController = GetComponent<CharacterController>();
        spawnPoint = transform.position;

        platform2col = true;
        platform3col = true;
    }

    private void OnEnable()
    {
        inputAction.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputAction.FindActionMap("Player").Disable();
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = jumpForce;
        }
    }

    private void FixedUpdate()
    {

        Vector2 move = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = transform.TransformDirection(new Vector3(move.x, 0, move.y)) * moveSpeed;


        if (characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; 
        }
        verticalVelocity += gravity * Time.fixedDeltaTime;


        moveDirection.y = verticalVelocity;

        characterController.Move(moveDirection * Time.fixedDeltaTime);

        float rotation = lookAction.ReadValue<Vector2>().x;
        transform.Rotate(0, rotation, 0);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
  
        GameObject collisionObj = hit.gameObject;

        if (collisionObj.CompareTag("dead"))
        {
            transform.position = spawnPoint;
        }


        if (collisionObj.CompareTag("Plat2") && platform2col)
        {
            Debug.Log("Spawning enemies on Platform 2!"); 
            SpawnPlatform2Targets();
        }


        if (collisionObj.CompareTag("Plat3") && platform3col)
        {
            SpawnPlatform3Targets();
        }
    }

    private void SpawnPlatform3Targets()
    {
        if (kills >= 4)  
        {
            platform3col = false;

            foreach (GameObject location in secondTargetLocation)
            {
                Instantiate(targetPrefab, location.transform.position + Vector3.up * 1f, location.transform.rotation);
                Debug.Log("Plat3 enemy spawned at: " + location.transform.position);
            }
        }
        else
        {
            Debug.Log("Not enough kills yet! Current kills = " + kills);
        }
    }

    private void SpawnPlatform2Targets()
    {
        platform2col = false;

        foreach (GameObject location in firstTargetLocation)
        {
            Instantiate(targetPrefab, location.transform.position + Vector3.up * 1f, location.transform.rotation);

            Debug.Log("Enemy spawned at: " + location.transform.position);
        }
    }

}
