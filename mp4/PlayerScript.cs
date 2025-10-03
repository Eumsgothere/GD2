using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    InputActionAsset controlAsset;

    CharacterController playerController;

    float drop;

    Vector3 respownPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       playerController = GetComponent<CharacterController>();
        drop = 0;
        respownPosition = transform.position;
    }

    private void OnEnable()
    {
        controlAsset.FindActionMap("Player").Enable();
    }


    private void OnDisable()
    {
        controlAsset.FindActionMap("Player").Disable();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 move = InputSystem.actions.FindAction("Move").ReadValue<Vector2>() * .1f;
        playerController.Move(transform.TransformDirection(new Vector3(move.x,drop,move.y)));
        float rotation = InputSystem.actions.FindAction("Look").ReadValue<Vector2>().x;
        transform.Rotate(0, rotation, 0);
        if (!playerController.isGrounded)
        {
            drop = -.1f;
        }
        else
        {
            drop = 0;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.name == "Safety Net")
        {
            transform.position = respownPosition;
        }
    }
}
