using UnityEngine;
using UnityEngine.InputSystem;

public class SphereScript : MonoBehaviour
{
    [SerializeField]
    InputAction move, rotate, changeColor, resize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        changeColor.performed += ChangeColor_performed;
    }
    private void ChangeColor_performed(InputAction.CallbackContext obj)
    {
        GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
    private void OnEnable()
    {
        changeColor.Enable();
        move.Enable();
        rotate.Enable();
        resize.Enable();
    }
    private void OnDisable()
    {
        changeColor.Disable();
        move.Disable();
        rotate.Disable();
        resize.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 sMove = move.ReadValue<Vector2>() * .1f;
        transform.Translate(sMove.x, 0, sMove.y);
        float sRotate = rotate.ReadValue<float>();
        transform.Rotate(0, sRotate, 0);
        //if (changeColor.IsPressed())
        //{
        //    GetComponent<MeshRenderer>().material.color =
        //        new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        //}
        if (resize.IsPressed())
        {
            transform.localScale = new Vector3(transform.localScale.x + 0.01f, transform.localScale.y + 0.01f, transform.localScale.z + 0.01f);
        }

    }
}
