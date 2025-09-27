using UnityEngine;
using UnityEngine.InputSystem;

public class gunmovement : MonoBehaviour
{
    [SerializeField]
    InputAction move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnEnable()
    {
        move.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 gMove = move.ReadValue<Vector2>() * .1f;
        transform.Translate(gMove);

    }
}
