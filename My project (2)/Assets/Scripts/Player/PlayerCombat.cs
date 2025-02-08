using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    
    public float attackRange = 0.5f;
    public int playerDamage = 50;

    public void Attack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
           
            enemy.GetComponent<EnemyHP>().TakeDamage(playerDamage);
            Debug.Log("We hit " + enemy.name);
        }
    }

    void OnDrawGizmosSelected() 
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
