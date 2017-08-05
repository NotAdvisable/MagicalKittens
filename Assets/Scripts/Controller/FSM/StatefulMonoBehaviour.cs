using UnityEngine;

public class StatefulMonoBehaviour<T> : MonoBehaviour {
    protected FSM<T> fsm;

	public void ChangeState(IFSMState<T> e) {
		fsm.ChangeState(e);
	}

	protected virtual void Update() {
		fsm.Update();
	}
}
