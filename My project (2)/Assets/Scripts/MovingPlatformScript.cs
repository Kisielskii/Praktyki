using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControllerScript : MonoBehaviour
{
    [Header("References")]
    public Transform platform;     // Referencja do obiektu platformy
    public Transform startPoint;   // Punkt początkowy
    public Transform endPoint;     // Punkt końcowy
    // public Transform middlePoint; // Jeśli nie używasz, możesz usunąć

    [Header("Movement Settings")]
    public float speed = 2f;
    
    // Zmienna sterująca kierunkiem ruchu
    // Kiedy direction == 1 platforma zmierza do endPoint, a kiedy -1 wraca do startPoint.
    // (Możesz dostosować logikę, zależnie od tego, jak chcesz aby platforma się poruszała)
    private int direction = 1;

    private void Update() 
    {
        Vector3 target = CurrentMovementTarget();

        // Przesuwamy platformę w stronę celu
        platform.position = Vector3.MoveTowards(platform.position, target, speed * Time.deltaTime);

        // Obliczamy odległość do celu
        float distance = (target - platform.position).magnitude;

        // Jeśli platforma jest blisko celu, zmieniamy kierunek ruchu
        if (distance <= 0.1f)
        {
            direction *= -1;
        }
    }

    // Metoda zwracająca bieżący cel ruchu platformy
    Vector3 CurrentMovementTarget()
    {
        // Przykładowa logika: gdy kierunek dodatni, jedziemy do endPoint, gdy ujemny – do startPoint.
        // Dostosuj to do swoich potrzeb.
        if (direction == 1)
        {
            return endPoint.position;
        }
        else 
        {
            return startPoint.position;
        }
    }

    // Wizualizacja ścieżki ruchu platformy w edytorze
    private void OnDrawGizmos() 
    {
        if(startPoint != null && endPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(startPoint.position, endPoint.position);
        }

        if(platform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(platform.position, 0.1f);
        }
    }

    // Ustawianie rodzica przy wejściu gracza na platformę
    private void OnCollisionEnter(Collision collision) 
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    // Usuwanie rodzica przy opuszczeniu platformy przez gracza
    private void OnCollisionExit(Collision collision) 
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
