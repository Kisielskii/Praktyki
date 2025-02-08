using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private RespawnTriggerScript respawnTriggerScript;
    private BoxCollider checkCollider;
    
    private void Awake() 
    {
        checkCollider = GetComponent<BoxCollider>();
        respawnTriggerScript = GameObject.FindGameObjectWithTag("RespawnTrigger").GetComponent<RespawnTriggerScript>();
    }

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Checkpoint");
            respawnTriggerScript.respawnPoint = this.gameObject;
            checkCollider.enabled = false;
        }
    }
}
