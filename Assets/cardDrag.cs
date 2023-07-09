using UnityEngine;
using UnityEngine.EventSystems;

public class cardDrag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform draggableTransform;
    public CanvasGroup canvasGroup;

    public void Awake()
    {
        draggableTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggableTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Check if the object was dropped on a valid target
        // Add your logic here
    }
}
