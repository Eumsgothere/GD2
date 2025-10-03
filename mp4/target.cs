using UnityEngine;

public class Target : MonoBehaviour
{
    private int hitCount = 0;
    public int maxHits = 4;
    private MeshRenderer myRenderer;

    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();

        if (myRenderer != null)
        {
            myRenderer.material.color = Color.white;
        }
    }

    public void ActivateTarget()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!collision.gameObject.CompareTag("Bullet")) return;

        hitCount++;

        if (hitCount == 1)
        {
            myRenderer.material.color = Color.red;
        }
        else if (hitCount == 2)
        {
            myRenderer.material.color = Color.green;
        }
        else if (hitCount == 3)
        {
            myRenderer.material.color = Color.blue;
        }
        else if (hitCount >= maxHits)
        {
            Player_script player = FindObjectOfType<Player_script>();
            if (player != null)
            {
                player.Kills++;
                Debug.Log("Kill registered! Total kills = " + player.Kills);
            }

            Destroy(gameObject);
        }


        Destroy(collision.gameObject);
    }
}
