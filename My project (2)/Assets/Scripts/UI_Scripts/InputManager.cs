using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Obiekt reprezentujący menu pauzy. Upewnij się, że jest przypisany w Inspectorze.
    public GameObject pauseMenu;
    
    // Flaga określająca, czy gra jest obecnie w trybie pauzy.
    private bool isPaused = false;

    void Update()
    {
        // Sprawdzamy, czy naciśnięto klawisz Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Metoda przełączająca stan pauzy
    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Włącz menu pauzy oraz zatrzymaj grę
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            // Wyłącz menu pauzy oraz wznow grę
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
