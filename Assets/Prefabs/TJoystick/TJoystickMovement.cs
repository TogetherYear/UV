using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TJoystickMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private RectTransform o;

    [SerializeField]
    private RectTransform i;

    private Vector2 originIPosition;

    private Vector2 originOSize;

    private Vector2 centerPosition;


    private void Start()
    {
        originIPosition = o.anchoredPosition;
        originOSize = o.rect.size;
        centerPosition = originIPosition + originOSize * 0.5f;
    }

    public void OnBeginDrag(PointerEventData e)
    {
        SetIPosition(e.position);
    }

    public void OnDrag(PointerEventData e)
    {
        SetIPosition(e.position);
    }

    public void OnEndDrag(PointerEventData e)
    {
        ResetIPosition();
    }

    public void OnPointerDown(PointerEventData e)
    {
        SetIPosition(e.position);
    }

    public void OnPointerUp(PointerEventData e)
    {
        ResetIPosition();
    }

    private void SetIPosition(Vector2 inputPosition)
    {
        Vector2 delta = inputPosition - centerPosition;
        Vector2 direction = delta.normalized;
        float distance = Vector2.Distance(inputPosition, centerPosition);
        float length = Mathf.Min(distance, originOSize.x / 2);
        float force = length / (originOSize.x / 2);
        Vector2 result = direction * length;
        i.anchoredPosition = result;
        TInputManager.Instance.tc.movement = direction;
        TInputManager.Instance.tc.forceMovement = direction * force;
    }

    private void ResetIPosition()
    {
        i.anchoredPosition = Vector2.zero;
        TInputManager.Instance.tc.forceMovement = Vector2.zero;
        TInputManager.Instance.tc.movement = Vector2.zero;
    }
}
