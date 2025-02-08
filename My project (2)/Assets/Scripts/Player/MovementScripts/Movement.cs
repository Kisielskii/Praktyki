using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private float horizontalMovement;
    private bool isFacingRight = true;
    private bool hasJumped = false;
    
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpHeight = 5f; // Dostosuj wartość, aby uzyskać odpowiedni skok
    [SerializeField] private float jumpCooldown = 0.5f;
    
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    
    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] private float gravityMultiplier = 1.5f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody rb;  // Upewnij się, że obiekt ma komponent Rigidbody (3D)

    void Update()
    {
        // Aktualizacja tzw. "coyote time" – pozwala na skok tuż po oderwaniu od ziemi
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Obracanie postaci: odwracamy skalę X, aby postać patrzyła w kierunku ruchu
        if (!isFacingRight && horizontalMovement > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontalMovement < 0f)
        {
            Flip();
        }
        
        // Możesz tu dodać dodatkowe logowanie lub inne operacje
        Debug.Log(IsGrounded());
    }

    void FixedUpdate() 
    {
        // Ruch poziomy – modyfikujemy tylko składową X prędkości,
        // pozostawiając bez zmian prędkość w osiach Y (skok) oraz Z (ewentualny ruch w głąb sceny)
        Vector3 velocity = rb.linearVelocity;
        velocity.x = horizontalMovement * movementSpeed;
        rb.linearVelocity = velocity;
        
        // Dodatkowy wpływ grawitacji, gdy postać opada i nie jest uziemiona
        // W Unity 3D nie ma właściwości gravityScale, więc symulujemy ją przez dodatkową siłę
        if (rb.linearVelocity.y <= 0 && !IsGrounded())
        {
            // Physics.gravity.y jest ujemne, stąd (gravityMultiplier - 1) zwiększa siłę opadania
            rb.AddForce(Vector3.up * Physics.gravity.y * (gravityMultiplier - 1), ForceMode.Acceleration);
        }
    }

    // Metoda wywoływana przez Input System – pobiera wartość ruchu horyzontalnego
    public void Move(InputAction.CallbackContext context)  
    {
        horizontalMovement = context.ReadValue<Vector2>().x; 
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !hasJumped)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Jeśli wciąż jesteśmy w tzw. "coyote time" i mamy zapisany bufor skoku, wykonujemy skok
        if (coyoteTimeCounter > 0 && jumpBufferCounter > 0 && !hasJumped)
        {
            Vector3 vel = rb.linearVelocity;
            vel.y = jumpHeight; // Ustawiamy prędkość pionową na wartość skoku
            rb.linearVelocity = vel;
            
            jumpBufferCounter = 0f;
            hasJumped = true;
            StartCoroutine(WaitForJump());
        }
        
        // Jeśli gracz puści przycisk skoku, a postać jeszcze wznosi się, skracamy wysokość skoku
        if (context.canceled && rb.linearVelocity.y > 0f) 
        {
            Vector3 vel = rb.linearVelocity;
            vel.y *= 0.5f;
            rb.linearVelocity = vel;
            coyoteTimeCounter = 0f;
        }
    }

    // Sprawdzamy, czy postać znajduje się na ziemi – używamy sferycznego sprawdzania kolizji
    public bool IsGrounded()
    {
        // Metoda OverlapSphere zwraca tablicę colliderów, jeśli znajdzie jakikolwiek należący do warstwy groundLayer,
        // to postać jest uziemiona.
        return Physics.OverlapSphere(groundCheck.position, 0.3f, groundLayer).Length > 0;
    }

    // Odwracanie postaci – zmiana znaku skali X (możesz też zastosować rotację, jeśli wolisz)
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    // Prosty cooldown, aby zapobiec natychmiastowemu kolejnemu skokowi
    IEnumerator WaitForJump()
    {
        yield return new WaitForSeconds(jumpCooldown);
        hasJumped = false;
    }
}
