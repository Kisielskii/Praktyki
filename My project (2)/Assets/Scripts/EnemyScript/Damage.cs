using UnityEngine;

public class Damage : MonoBehaviour
{
    public PlayerHP pHealth;
    public int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        pHealth.TakeDamage(damage);
    }
}
