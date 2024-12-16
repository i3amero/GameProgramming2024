using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI; // 일시정지 UI (Canvas)
    private bool isPaused = false; // 게임이 일시정지 상태인지 추적

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }
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
                Pause();
            }
        }
    }

    public void ResumeGame()
    {
        Debug.Log("눌림!");
        pauseMenuUI.SetActive(false); // UI 비활성화
        Time.timeScale = 1f;          // 시간 재개
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked; // 커서 잠금
        Cursor.visible = false;                  // 커서 숨기기
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);  // UI 활성화
        Time.timeScale = 0f;         // 시간 정지
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;  // 커서 해제
        Cursor.visible = true;                   // 커서 보이기
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); 
    }
}
