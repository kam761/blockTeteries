using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 originalPosition;

    // 드롭할 대상 (Board)
    public RectTransform boardRect;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPoint
        );

        rectTransform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsInsideBoard())
        {
            // Board 위에 있다면 현재 위치 그대로 유지
        }
        else
        {
            // Board 밖이면 원래 자리로 복귀
            rectTransform.position = originalPosition;
        }
    }

    private bool IsInsideBoard()
    {
        if (boardRect == null)
            return false;

        // 현재 블록의 중심 좌표
        Vector3 worldPos = rectTransform.position;

        // Board 영역의 Rect 안에 있는지 체크
        return RectTransformUtility.RectangleContainsScreenPoint(
            boardRect,
            worldPos,
            canvas.worldCamera
        );
    }
}