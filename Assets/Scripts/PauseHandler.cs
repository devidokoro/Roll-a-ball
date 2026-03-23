using UnityEngine;
using TMPro;

public class PauseHandler : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject pauseText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;

        pauseText.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        pauseText.SetActive(false);
    }
}