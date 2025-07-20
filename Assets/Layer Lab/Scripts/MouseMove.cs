using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 originalPosition;

    // ����� ��� (Board)
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
            // Board ���� �ִٸ� ���� ��ġ �״�� ����
        }
        else
        {
            // Board ���̸� ���� �ڸ��� ����
            rectTransform.position = originalPosition;
        }
    }

    private bool IsInsideBoard()
    {
        if (boardRect == null)
            return false;

        // ���� ����� �߽� ��ǥ
        Vector3 worldPos = rectTransform.position;

        // Board ������ Rect �ȿ� �ִ��� üũ
        return RectTransformUtility.RectangleContainsScreenPoint(
            boardRect,
            worldPos,
            canvas.worldCamera
        );
    }
}