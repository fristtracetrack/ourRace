using UnityEngine;
using UnityEngine.EventSystems;

public class EverythingMover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float moveSpeed = 150f; // ÿ���ƶ���������
    private bool isMoving = false;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isMoving)
        {
            rectTransform.anchoredPosition += Vector2.left * moveSpeed * Time.deltaTime;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMoving = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMoving = false;
    }
}