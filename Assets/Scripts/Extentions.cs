using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions{
    public static float DistanceToTransform(this Transform origin, Transform target) {
        return Vector3.Distance(origin.position, target.position);
    }
}
