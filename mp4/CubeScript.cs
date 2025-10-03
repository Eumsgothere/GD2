using UnityEngine;
using UnityEngine.InputSystem;
public class CubeScript : MonoBehaviour
{
    [SerializeField]
    InputActionAsset controlAsset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputSystem.actions.FindAction("Attack").performed += CubeScript_performed;
    }

    private void CubeScript_performed(InputAction.CallbackContext obj)
    {
        GetComponent<MeshRenderer>().material.color = 
            new Color(Random.Range(0f, 1f), 
            Random.Range(0f,1f), 
            Random.Range(0f,1f));
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
        transform.Translate(move.x,0,move.y);
        Vector2 rotation = InputSystem.actions.FindAction("Look").ReadValue<Vector2>() * .1f;
        transform.Rotate(0, rotation.x, 0);
    }
}
