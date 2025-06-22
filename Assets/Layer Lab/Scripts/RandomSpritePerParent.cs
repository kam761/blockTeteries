using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpritePerParent : MonoBehaviour
{
    [Header("���� �̹��� ���� ��� �θ��")]
    [SerializeField] private List<Transform> parentTransforms = new List<Transform>();

    [Header("Resources ���� �� ��������Ʈ �̸� (Ȯ���� ����)")]
    [SerializeField]
    // �迭�� ��� ������Ʈ��
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
        // ��� ��������Ʈ �ε�
        // spriteNames�� ����� �̹����̸����� name�̶�� var���� ������ ��ȸ�Ѵ�
        foreach (string name in spriteNames)
        {
            // Resources�� ��� Sprite�̹������� �ҷ����� �ڵ�
            Sprite sp = Resources.Load<Sprite>(name);
            // �ҷ��� �̹����� ������ �� �̹����� List�� �߰���
            if (sp != null)
                spriteList.Add(sp);
            // �װ� �ƴ϶�� (sprite �����̹��� �̸�) ��������Ʈ�� �������� ã���� ���ٰ� ���� �� ���� ���� ������
            else
                Debug.LogWarning($"'{name}' ��������Ʈ�� Resources �������� ã�� �� �����ϴ�.");
        }
        //���� spriteList �迭�� ����� ���� �ƹ��͵� ���ٸ� �ε��� ��������Ʈ�� ���ٰ� ���� ����ϱ�
        if (spriteList.Count == 0)
            Debug.LogError("�ε��� ��������Ʈ�� �����ϴ�. Resources ������ Ȯ���ϼ���.");
    }

    void Start()
    {
        //���������� �Լ� �����ϱ�
        ApplyRandomSpriteForEachParent();
    }

    
    public void ApplyRandomSpriteForEachParent()
    {
        // ���� �迭�� ����Ȱ��� �ƹ��͵� ���ٸ� �ؿ� �Լ��� �������� �ʱ�
        if (spriteList.Count == 0) return;

        // �����̹����� ������ �θ�������� parent��� Transform���� �����Ѵ� ����
       // �θ𸶴� �� �ϳ��� ���� ��������Ʈ �����ϱ�
            Sprite chosen = spriteList[Random.Range(0, spriteList.Count)];        
        foreach (Transform parent in parentTransforms)
        {
            // �θ�� �� �ڽĵ� �� Image ������Ʈ ���� ��������
            Image[] images = parent.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                img.sprite = chosen;
            }
            // ����� �θ������ �̸��� ����� �̹����� �� �ڽ����̹����� �����ٴ� �޼��� ���
            Debug.Log($"{parent.name}�� {images.Length}�� �ڽ� �̹����� '{chosen.name}' �����");
        }
    }
}