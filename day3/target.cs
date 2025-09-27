using UnityEngine;

public class target : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        MeshRenderer target = collision.collider.GetComponent<MeshRenderer>();
        target.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)); ;
        GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        //if (collision = 3)
        //{
        //    Destroy(gameObject);
        //}
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
