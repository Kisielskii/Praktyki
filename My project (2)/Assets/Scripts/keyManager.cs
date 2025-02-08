using UnityEngine;

public class keyManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    public bool isPickedUp;
    private Vector2 vel;
    public float smoothTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPickedUp)
        {
            Vector3 offset = new Vector3(0, 1, 0);
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position + offset, ref vel, smoothTime);
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
        }
    }
}
