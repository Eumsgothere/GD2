using UnityEngine;

public class CapsuleScript : MonoBehaviour
{
    //GameObject cylinderGO;
    CylinderScript cs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //cylinderGO = GameObject.Find("Cylinder");
        //cs = cylinderGO.GetComponent<CylinderScript>();
        cs = FindAnyObjectByType<CylinderScript>();
        cs.count = 20;
        cs.getScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
