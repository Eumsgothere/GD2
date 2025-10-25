using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletSc : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] float force = 2000f;

    void Start()
    {
        rigidBody.AddRelativeForce(Vector3.forward * force);
        rigidBody.isKinematic = false;
        Destroy(gameObject, 2f);
    }

    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {

        GameObject objectHit = collision.gameObject;

        if (objectHit != null && objectHit.CompareTag("Target"))
        {
            Target target = objectHit.GetComponent<Target>();
            if (target != null)
            {

                target.TakeDamage(1);
                target.ChangeToRed();
            }
        }
        Destroy(gameObject);
    }
}