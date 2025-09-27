using UnityEngine;

public class Cubescript : MonoBehaviour
{
    [SerializeField]
    BoxCollider[] cubeCollider  ;
    [SerializeField]
    MeshRenderer cuberenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cuberenderer.material.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
