using UnityEngine;
using UnityEngine.UI;

public class QuitButtonHandler : MonoBehaviour
{
    public Button quitButton;  // Quit 버튼

    void Start()
    {
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }
        else
        {
            Debug.LogWarning("Quit 버튼이 연결되지 않았습니다.");
        }
    }

    void OnQuitButtonClicked()
    {
        // 에디터/실행 중엔 게임 일시정지
        Time.timeScale = 0f;

        Debug.Log("게임이 멈췄습니다 (Time.timeScale = 0)");

        // 빌드된 게임에서 실행 시 게임 종료
#if UNITY_EDITOR
        // 에디터에서는 종료 대신 멈춤만
#else
            Application.Quit();
#endif
    }
}