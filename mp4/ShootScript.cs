using UnityEngine;
using UnityEngine.InputSystem;

public class ShootScript : MonoBehaviour
{
    [SerializeField]
    InputActionAsset inputAction;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform bulletLocationTrans;

    int bulletCount;
    float timeSinceReload;
    bool reloading;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputSystem.actions.FindAction("Attack").performed += Shoot_performed;

        reloading = false;
        bulletCount = 12;
        timeSinceReload = 0.0f;
        Debug.Log("Bullets: " + bulletCount);
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        if (reloading) return;
        if (bulletCount <= 0)
        {
            reloading = true;
            Debug.Log("Reloading...");
            
        }
        else
        {
            bulletCount -= 1;
            Debug.Log("Bullets: " + bulletCount);
            Instantiate(bulletPrefab, bulletLocationTrans.position, bulletLocationTrans.rotation);
        }
    }

    private void OnEnable( )
    {
        inputAction.FindActionMap("Player").Enable( );
    }

    private void OnDisable( )
    {
        inputAction.FindActionMap("Player").Disable( );
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading)
            timeSinceReload += Time.deltaTime;


        float reloadDuration = 1.2f;

        if (reloading && timeSinceReload >= reloadDuration)
        {
            bulletCount = 12;
            timeSinceReload = 0.0f;
            reloading = false;
            Debug.Log("Reloaded Bullets: " + bulletCount);
        }

    }
}
