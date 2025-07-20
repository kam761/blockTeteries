using UnityEngine;
using UnityEngine.UI;

public class QuitButtonHandler : MonoBehaviour
{
    public Button quitButton;  // Quit ��ư

    void Start()
    {
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }
        else
        {
            Debug.LogWarning("Quit ��ư�� ������� �ʾҽ��ϴ�.");
        }
    }

    void OnQuitButtonClicked()
    {
        // ������/���� �߿� ���� �Ͻ�����
        Time.timeScale = 0f;

        Debug.Log("������ ������ϴ� (Time.timeScale = 0)");

        // ����� ���ӿ��� ���� �� ���� ����
#if UNITY_EDITOR
        // �����Ϳ����� ���� ��� ���㸸
#else
            Application.Quit();
#endif
    }
}