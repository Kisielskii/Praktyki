using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Ustawienia ruchu")]
    [Tooltip("Prędkość patrolu")]
    public float patrolSpeed = 2f;
    [Tooltip("Prędkość biegu za graczem")]
    public float chaseSpeed = 4f;
    [Tooltip("Zasięg wykrywania gracza (odległość na osi X)")]
    public float detectionRange = 5f;

    [Header("Referencje")]
    [Tooltip("Transform gracza")]
    public Transform player;
    [Tooltip("Lewy punkt patrolu")]
    public Transform leftPatrolPoint;
    [Tooltip("Prawy punkt patrolu")]
    public Transform rightPatrolPoint;

    // Flaga określająca kierunek patrolu – true: ruch w prawo, false: ruch w lewo
    private bool movingRight = true;

    void Update()
    {
        // Sprawdzamy, czy gracz jest w zasięgu wykrywania (tylko na osi X)
        if (Mathf.Abs(transform.position.x - player.position.x) <= detectionRange)
        {
            // Gracz został wykryty – biegnij w jego stronę
            ChasePlayer();
        }
        else
        {
            // Nie wykryto gracza – wykonaj patrol
            Patrol();
        }
    }

    // Metoda odpowiadająca za bieganie w stronę gracza
    void ChasePlayer()
    {
        // Ustalamy cel ruchu: pozycja gracza, ale zachowujemy bieżące położenie na osiach Y i Z
        Vector3 target = new Vector3(player.position.x, transform.position.y, transform.position.z);

        // Przesuwamy przeciwnika w stronę celu z prędkością chaseSpeed
        transform.position = Vector3.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);
    }

    // Metoda odpowiadająca za patrol pomiędzy lewym i prawym punktem
    void Patrol()
    {
        // Określamy cel ruchu na podstawie aktualnego kierunku
        Vector3 target;
        if (movingRight)
        {
            target = new Vector3(rightPatrolPoint.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            target = new Vector3(leftPatrolPoint.position.x, transform.position.y, transform.position.z);
        }

        // Przesuwamy przeciwnika w stronę celu z prędkością patrolSpeed
        transform.position = Vector3.MoveTowards(transform.position, target, patrolSpeed * Time.deltaTime);

        // Jeśli osiągniemy bliskość celu (odległość mniejsza niż 0.1 jednostki), zmieniamy kierunek
        if (Mathf.Abs(transform.position.x - target.x) < 0.1f)
        {
            movingRight = !movingRight;
        }
    }

    // Opcjonalnie: wizualizacja zasięgu wykrywania w edytorze
    private void OnDrawGizmosSelected()
    {
        // Rysujemy linię poziomą o długości detectionRange (z obu stron) od pozycji przeciwnika
        Gizmos.color = Color.yellow;
        Vector3 leftLimit = new Vector3(transform.position.x - detectionRange, transform.position.y, transform.position.z);
        Vector3 rightLimit = new Vector3(transform.position.x + detectionRange, transform.position.y, transform.position.z);
        Gizmos.DrawLine(leftLimit, rightLimit);
    }
}
