using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop3D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 offset;
    private Vector3 originalPosition;
    private Camera mainCamera;

    public Transform boardArea; // Board 오브젝트 (Collider 필요)

    void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;

        // 마우스 월드 좌표와 오브젝트 사이 오프셋 계산
        Vector3 mouseWorld = GetMouseWorldPos();
        offset = transform.position - mouseWorld;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mouseWorld = GetMouseWorldPos();
        transform.position = mouseWorld + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsInsideBoard())
        {
            // Board 영역 밖이면 원래 자리로
            transform.position = originalPosition;
        }
        // Board 위에 있으면 그냥 현재 위치 유지
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(mainCamera.transform.position.z); // 카메라에서의 거리
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    private bool IsInsideBoard()
    {
        if (boardArea == null) return false;

        Collider boardCollider = boardArea.GetComponent<Collider>();
        if (boardCollider == null) return false;

        return boardCollider.bounds.Contains(transform.position);
    }
}