using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody bulletRigid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletRigid = GetComponent<Rigidbody>();
        bulletRigid.AddRelativeForce(0, 0, 3000);
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}