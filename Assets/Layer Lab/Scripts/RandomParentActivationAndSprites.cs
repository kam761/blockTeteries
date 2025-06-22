using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RandomParentActivationAndSprites : MonoBehaviour
{
    [Header("��������Ʈ ���� ��� �θ��")]
    [SerializeField] private List<Transform> parentTransforms = new List<Transform>();

    [Header("Resources ���� �� ��������Ʈ �̸� (Ȯ���� ����)")]
    [SerializeField]
    private string[] spriteNames = new string[]
    {
        "blockBlueDimond", "blockCreamDimond", "blockCrownDimond",
        "blockGreenDimond", "blockLightBlueDimond", "blockOrangeDimond",
        "blockPinkDimond", "blockPurepleDimond", "blockRedDimond",
        "blockYellowDimond"
    };

    private List<Sprite> spriteList = new List<Sprite>();

    void Awake()
    {
        // Resources �������� ��� ��������Ʈ �ε�
        foreach (var name in spriteNames)
        {
            Sprite sp = Resources.Load<Sprite>(name);
            if (sp != null) spriteList.Add(sp);
            else Debug.LogWarning($"Resources���� '{name}' ��������Ʈ�� ã�� �� �����ϴ�.");
        }
        if (spriteList.Count == 0)
            Debug.LogError("��������Ʈ�� �ϳ��� �ε���� �ʾҽ��ϴ�.");

        // �θ� �� ������ 3���� Ȱ��ȭ, �������� ��Ȱ��ȭ
        int total = parentTransforms.Count;
        if (total <= 3)
        {
            // ��� Ȱ��ȭ
            foreach (Transform p in parentTransforms) p.gameObject.SetActive(true);
        }
        else
        {
            // �ε��� �迭 ���� �� ����
            List<int> idxs = Enumerable.Range(0, total).OrderBy(x => Random.value).ToList();
            HashSet<int> chosen = new HashSet<int>(idxs.Take(3));

            for (int i = 0; i < total; ++i)
            {
                parentTransforms[i].gameObject.SetActive(chosen.Contains(i));
            }
        }
    }

    void Start()
    {
        ApplyRandomSpritePerActiveParent();
    }

    [ContextMenu("Apply Random Sprite Per Active Parent")]
    public void ApplyRandomSpritePerActiveParent()
    {
        if (spriteList.Count == 0) return;

        foreach (Transform parent in parentTransforms)
        {
            if (!parent.gameObject.activeSelf)
                continue;

            // Ȱ���� �θ𸶴� �ϳ��� ���� ��������Ʈ ����
            Sprite chosen = spriteList[Random.Range(0, spriteList.Count)];

            // �ڽ� Image ��� ���� ��������Ʈ�� ����
            Image[] images = parent.GetComponentsInChildren<Image>(true);
            foreach (Image img in images)
                img.sprite = chosen;

            Debug.Log($"{parent.name} Ȱ��ȭ �� �ڽ� {images.Length}���� '{chosen.name}' ����");
        }
    }
}
