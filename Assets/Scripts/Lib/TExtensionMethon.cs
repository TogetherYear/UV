using UnityEngine;

public static class TExtensionMethon
{
    public static bool IsFacingTarget(this Transform transform, Transform target, float dotThreshold = 0.5f)
    {
        var vectorToTarget = target.position - transform.position;
        vectorToTarget.Normalize();
        float dot = Vector3.Dot(transform.forward, vectorToTarget);
        return dot < dotThreshold;
    }

    public static float GetAnlgeFromPoint(this Vector2 point, Vector2 origin)
    {
        Vector2 dir = new Vector2(point.x - origin.x, point.y - origin.y).normalized;
        float angle = Mathf.Acos(dir.y) * Mathf.Rad2Deg;
        return point.x > origin.x ? angle : 360.0f - angle;
    }

    public static Vector3 GetWorldPosition(this Vector2 point, float z = 0.0f)
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(point);
        position.z = z;
        return position;
    }
}
