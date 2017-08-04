using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Extentions{
    #region Transform

    public static float DistanceToTransform(this Transform origin, Transform target) {
        return Vector3.Distance(origin.position, target.position);
    }

    public static Vector3 ClosestVector3(this Transform origin, ICollection<Vector3> targets)
    {
        return targets.Select(p => new { Point = p, Distance = Vector3.Distance(origin.position, p) })
                       .Aggregate((p1, p2) => p1.Distance < p2.Distance ? p1 : p2).Point;
    }

    #endregion

    #region Vector3

    public static Vector3 MutliplyByValue(this Vector3 original, float multiplier) {
        return new Vector3(original.x * multiplier, original.y * multiplier, original.z * multiplier);
    }

    public static Vector3 FirstWithinDistance(this Vector3 origin, ICollection<Vector3> targets, float distance)
    {
        return targets.FirstOrDefault(elem => Vector3.Distance(origin, elem) < distance);
    }
    #endregion

}
