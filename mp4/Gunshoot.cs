using UnityEngine;
using UnityEngine.InputSystem;

public class Gunshoot : MonoBehaviour
{
    [SerializeField] InputAction shoot;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletlocationtrans;

    private int ammo = 12;      
    private int maxAmmo = 12;   
    private bool isReloading = false;

    void Start()
    {
        shoot.performed += Shoot_performed;
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        if (isReloading) return; 

        if (ammo > 0)
        {
            // spawn bullet
            Instantiate(bulletPrefab, bulletlocationtrans.position, bulletlocationtrans.rotation);

            ammo--;
            Debug.Log("Shot fired! Bullets left: " + ammo);

            // check if empty
            if (ammo <= 0)
            {
                Debug.Log("Reloading...");
                StartCoroutine(Reload());
            }
        }
    }

    private System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(2f);
        ammo = maxAmmo;
        isReloading = false;
        Debug.Log("Reload complete. Bullets reset to " + ammo);
    }

    private void OnEnable()
    {
        shoot.Enable();
    }

    private void OnDisable()
    {
        shoot.Disable();
    }
}
