using UnityEngine;

public class BalaScript : MonoBehaviour
{
    Rigidbody balaRigid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        balaRigid = GetComponent<Rigidbody>();
        balaRigid.isKinematic = false;
        balaRigid.AddRelativeForce(0, 0, 3000);

    }


    void Update()
    {
        balaRigid.AddTorque(1, 1, 1);
    }
}