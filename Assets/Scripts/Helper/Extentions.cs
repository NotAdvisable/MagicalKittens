using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Extentions{
    #region Transform
    ///<summary> Returns the distance to another transform </summary>
    public static float DistanceToTransform(this Transform origin, Transform target) {
        return Vector3.Distance(origin.position, target.position);
    }
    ///<summary> Returns the closest transform in the collection; null if there is none </summary>
    public static Transform ClosestTransform(this Transform origin, ICollection<Transform> targets)
    {
        return targets.Select(p => new { Point = p, Distance = Vector3.Distance(origin.position, p.position) })
                       .Aggregate((p1, p2) => p1.Distance < p2.Distance ? p1 : p2).Point;
    }
    ///<summary> Returns the closest vector3 in the given collection </summary>
    public static Vector3 ClosestVector3(this Transform origin, ICollection<Vector3> targets)
    {
        return targets.Select(p => new { Point = p, Distance = Vector3.Distance(origin.position, p) })
                       .Aggregate((p1, p2) => p1.Distance < p2.Distance ? p1 : p2).Point;
    }
    ///<summary> Returns the closest transform in the collection within the given distance; null if there is none </summary>
    public static Transform ClosestTransformWithinDistance(this Transform origin, ICollection<Transform> targets, float distance)
    {
        var closest = origin.ClosestTransform(targets);
        return (Vector3.Distance(origin.position, closest.position) < distance) ? closest : null;
    }
    ///<summary> Returns true if the target angle is within given angle; based on the origins forward vector</summary>
    public static bool WithinEulerAngle(this Transform origin, Transform target, float eulerAngle)
    {
        var direction = (target.position - origin.position).normalized;
        return Mathf.Acos(Vector3.Dot(direction, origin.forward)) * Mathf.Rad2Deg < eulerAngle / 2;
    }
    ///<summary> Returns the Vector3 facing the given angle </summary>
    public static Vector3 DirectionFromAngle(this Transform origin, float angleDeg)
    {
        angleDeg += origin.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleDeg * Mathf.Deg2Rad));
    }
    #endregion

    #region Vector3

    /// <summary> Scales each value of the Vector by the multiplier and returns it </summary>
    public static Vector3 MutliplyByValue(this Vector3 original, float multiplier) {
        return new Vector3(original.x * multiplier, original.y * multiplier, original.z * multiplier);
    }

    /// <summary> Returns the first Transform within Distance; null if there is none </summary>
    public static Transform FirstWithinDistance(this Vector3 origin, ICollection<Transform> targets, float distance)
    {
        return targets.FirstOrDefault(elem => Vector3.Distance(origin, elem.position) < distance);
    }
    #endregion

}
