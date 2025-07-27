using UnityEngine;
using System.Collections.Generic;

public class RandomActiveBlocksMover : MonoBehaviour
{
    public Transform position1;
    public Transform position2;
    public Transform position3;

    void Start()
    {
        // 위치 배열 준비
        List<Transform> targetPositions = new List<Transform> { position1, position2, position3 };

        // 활성화된 자식만 필터링
        List<Transform> activeChildren = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                activeChildren.Add(child);
            }
        }

        // 활성화된 자식이 3개 미만이면 경고
        if (activeChildren.Count < 3)
        {
            Debug.LogWarning("활성화된 Blocks의 자식 오브젝트가 3개 미만입니다.");
            return;
        }

        // 활성화된 자식 중 랜덤으로 3개 선택 (중복 없이)
        List<Transform> selectedChildren = new List<Transform>();
        while (selectedChildren.Count < 3)
        {
            int randomIndex = Random.Range(0, activeChildren.Count);
            Transform selected = activeChildren[randomIndex];
            if (!selectedChildren.Contains(selected))
            {
                selectedChildren.Add(selected);
            }
        }

        // 위치 섞기 (Fisher–Yates Shuffle)
        for (int i = 0; i < targetPositions.Count; i++)
        {
            int j = Random.Range(i, targetPositions.Count);
            (targetPositions[i], targetPositions[j]) = (targetPositions[j], targetPositions[i]);
        }

        // 각각 위치로 이동
        for (int i = 0; i < 3; i++)
        {
            selectedChildren[i].position = targetPositions[i].position;
        }
    }
}