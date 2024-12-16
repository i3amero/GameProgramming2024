using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI; // �Ͻ����� UI (Canvas)
    private bool isPaused = false; // ������ �Ͻ����� �������� ����

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
        Debug.Log("����!");
        pauseMenuUI.SetActive(false); // UI ��Ȱ��ȭ
        Time.timeScale = 1f;          // �ð� �簳
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked; // Ŀ�� ���
        Cursor.visible = false;                  // Ŀ�� �����
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);  // UI Ȱ��ȭ
        Time.timeScale = 0f;         // �ð� ����
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;  // Ŀ�� ����
        Cursor.visible = true;                   // Ŀ�� ���̱�
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); 
    }
}
