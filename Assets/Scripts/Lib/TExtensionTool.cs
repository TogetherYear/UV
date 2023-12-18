using UnityEngine;

public class TExtensionTool
{
    public static bool IsUserHold()
    {
        return Input.touchCount != 0 || Input.GetMouseButtonDown(0);
    }

    public static Vector2 GetUserHoldPosition()
    {
        return Input.touchCount != 0 ? Input.GetTouch(0).position : Input.mousePosition;
    }
}
