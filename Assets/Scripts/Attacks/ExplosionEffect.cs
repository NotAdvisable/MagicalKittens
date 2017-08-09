using UnityEngine;

public class ExplosionEffect : MonoBehaviour {

    [SerializeField] private bool _triggerScreenShake;

	void Start () {
        if (_triggerScreenShake && EventController.Singleton != null) EventController.Singleton.ScreenShake();
	}
}
