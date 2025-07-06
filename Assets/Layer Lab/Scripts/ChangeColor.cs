using UnityEngine;

public class SetChildrenGray : MonoBehaviour
{
    void Start()
    {
        // Board�� ��� �ڽĵ��� ��ȸ
        foreach (Transform child in transform)
        {
            // SpriteRenderer�� �ִٸ� ȸ������ ����
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = Color.gray;
            }

            // UI�� Image ������Ʈ ó�� (���û���)
            UnityEngine.UI.Image img = child.GetComponent<UnityEngine.UI.Image>();
            if (img != null)
            {
                img.color = Color.gray;
            }
        }
    }
}