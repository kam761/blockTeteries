using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop3D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 offset;
    private Vector3 originalPosition;
    private Camera mainCamera;

    public Transform boardArea; // Board ������Ʈ (Collider �ʿ�)

    void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = transform.position;

        // ���콺 ���� ��ǥ�� ������Ʈ ���� ������ ���
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
            // Board ���� ���̸� ���� �ڸ���
            transform.position = originalPosition;
        }
        // Board ���� ������ �׳� ���� ��ġ ����
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(mainCamera.transform.position.z); // ī�޶󿡼��� �Ÿ�
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