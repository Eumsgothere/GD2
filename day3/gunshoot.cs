using UnityEngine;
using UnityEngine.InputSystem;
public class gunshoot : MonoBehaviour
{
    [SerializeField]
    InputAction shoot;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform bulletlocationtrans;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shoot.performed += Shoot_performed; ;
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        Instantiate(bulletPrefab, bulletlocationtrans.position, bulletlocationtrans.rotation);
    }
    private void OnEnable()
    {
        shoot.Enable();
    }
    private void OnDisable()
    {
        shoot.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
