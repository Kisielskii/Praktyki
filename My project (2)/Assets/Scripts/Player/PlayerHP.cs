using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public float currentHealth;
    public Image healthBar;
    public bool isAlive = true;
    
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    private void Update() 
    {
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died");
        Destroy(gameObject);
        isAlive = false;
        SceneManager.LoadScene("MovementDemo");
    }
  
}
