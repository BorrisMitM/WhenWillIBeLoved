using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension {

    public static Vector3 RoundToNearestPixel(this Vector3 origin)
    {
        origin.x = RoundToNearestPixel(origin.x);
        origin.y = RoundToNearestPixel(origin.y);

        return origin;
    }
    static float RoundToNearestPixel(float unityUnits)
    {
        float valueInPixels = Mathf.Round(unityUnits * 512f);
        return valueInPixels / 512f;
    }

    public static Vector3 Rotate(this Vector3 origin, float angle, Vector3 rotationAxis){
        return Quaternion.AngleAxis(angle, rotationAxis) * origin;
    }

    // public static Vector3 operator * (Vector3 a, Vector3 b){
    //     return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    // }
}
