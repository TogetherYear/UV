using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TJoystickRotate : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public void OnDrag(PointerEventData e)
    {
        TInputManager.Instance.mc.isDrag = true;
        TInputManager.Instance.mc.Drag?.Invoke(e);
    }

    public void OnEndDrag(PointerEventData e)
    {
        TInputManager.Instance.mc.isDrag = false;
    }
}
