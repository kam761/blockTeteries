using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RandomParentActivationAndSprites : MonoBehaviour
{
    [Header("스프라이트 적용 대상 부모들")]
    [SerializeField] private List<Transform> parentTransforms = new List<Transform>();

    [Header("Resources 폴더 내 스프라이트 이름 (확장자 제외)")]
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
        // Resources 폴더에서 모든 스프라이트 로드
        foreach (var name in spriteNames)
        {
            Sprite sp = Resources.Load<Sprite>(name);
            if (sp != null) spriteList.Add(sp);
            else Debug.LogWarning($"Resources에서 '{name}' 스프라이트를 찾을 수 없습니다.");
        }
        if (spriteList.Count == 0)
            Debug.LogError("스프라이트가 하나도 로드되지 않았습니다.");

        // 부모 중 무작위 3개만 활성화, 나머지는 비활성화
        int total = parentTransforms.Count;
        if (total <= 3)
        {
            // 모두 활성화
            foreach (Transform p in parentTransforms) p.gameObject.SetActive(true);
        }
        else
        {
            // 인덱스 배열 생성 및 셔플
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

            // 활성된 부모마다 하나의 랜덤 스프라이트 선택
            Sprite chosen = spriteList[Random.Range(0, spriteList.Count)];

            // 자식 Image 모두 동일 스프라이트로 변경
            Image[] images = parent.GetComponentsInChildren<Image>(true);
            foreach (Image img in images)
                img.sprite = chosen;

            Debug.Log($"{parent.name} 활성화 → 자식 {images.Length}개에 '{chosen.name}' 적용");
        }
    }
}
