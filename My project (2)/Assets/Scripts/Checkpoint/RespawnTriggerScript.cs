using UnityEngine;

public class RespawnTriggerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject respawnPoint;
    public PlayerHP pHealth;
   
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            pHealth.TakeDamage(20);
            player.transform.position = respawnPoint.transform.position;
        }
    }
}
