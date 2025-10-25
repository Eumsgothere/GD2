using UnityEngine;

public class Target : MonoBehaviour
{
    int life = 1;

    [SerializeField] GameController gameController;

    public void SetLife(int val)
    {
        life = val;
    }

    public void TakeDamage(int val)
    {
        life -= val;

        if (life <= 0)
        {
            gameController.RemoveFromList(gameObject);
            Destroy(gameObject);
        }
    }

    public void ChangeToRed()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = Color.red;
    }

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {

    }
}