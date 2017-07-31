using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions{
    public static float DistanceToTransform(this Transform origin, Transform target) {
        return Vector3.Distance(origin.position, target.position);
    }
    public static Vector3 MutliplyByValue(this Vector3 original, float multiplier) {
        return new Vector3(original.x * multiplier, original.y * multiplier, original.z * multiplier);
    }

}
