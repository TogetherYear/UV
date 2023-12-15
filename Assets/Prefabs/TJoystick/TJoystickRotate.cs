using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TJoystickRotate : MonoBehaviour, IDragHandler, IEndDragHandler
{

    public void OnDrag(PointerEventData e)
    {
        TInputManager.Instance.tc.isDrag = true;
        TInputManager.Instance.tc.Drag?.Invoke(e);
    }

    public void OnEndDrag(PointerEventData e)
    {
        TInputManager.Instance.tc.isDrag = false;
    }
}
