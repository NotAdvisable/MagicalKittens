using UnityEngine;

public interface IHitable {
    void Hit(float dmg, GameObject aggressor);
}
