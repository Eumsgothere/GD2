using UnityEngine;

public class TagScript : MonoBehaviour
{
    GameObject[] go;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        go = GameObject.FindGameObjectsWithTag("TX34Forever");
        foreach (GameObject g in go)
        {
            Debug.Log(g.name);
            //Destroy(g, 2);
            if(g.name == "Sphere")
            {
                g.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
