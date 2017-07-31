using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatJumpState : StateMachineBehaviour {

    int _mask;
    CatMovement _catMovement;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _mask = 1 << LayerMask.NameToLayer("Default");
        _catMovement = animator.GetComponent<CatMovement>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.DrawLine(animator.transform.position, animator.transform.position + (Vector3.up * 0.01f) + Vector3.down * .2f, Color.red);
        if (Physics.Raycast(animator.transform.position + (Vector3.up * 0.01f),Vector3.down, .1f, _mask)) {
            _catMovement.Land();
            return;
        }
        _catMovement.StillJumping();
    }
}
