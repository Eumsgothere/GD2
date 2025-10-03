using UnityEngine;

public class TargetScript : MonoBehaviour
{
    int ctr;
    GameObject PlayerObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ctr = 0;
        PlayerObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        ctr += 1;

        // Destroy bullet
        GameObject collisionObj = collision.gameObject;
        Destroy(collisionObj);

        switch (ctr)
        {
            case 1:
                GetComponent<MeshRenderer>( ).material.color = Color.red;
                break;
            case 2:
                GetComponent<MeshRenderer>( ).material.color = Color.green;
                break;
            case 3:
                GetComponent<MeshRenderer>( ).material.color = Color.blue;
                break;
            case 4:
                PlayerObject.GetComponent<PlayerScript>( ).Kills += 1;
                Destroy(gameObject);
                break;
        }
    }
}
