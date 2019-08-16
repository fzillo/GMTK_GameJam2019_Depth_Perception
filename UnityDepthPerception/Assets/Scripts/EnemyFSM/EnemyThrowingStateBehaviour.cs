using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowingStateBehaviour : StateMachineBehaviour
{
    //public Transform prefabBall;
    //public Vector3 ballSpawnShiftVector = new Vector3(0, 0, 0);
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(this + " clipLength" + animator.GetCurrentAnimatorClipInfo(layerIndex).Length);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //Debug.Log("Ball Spawn Position: " + (animator.transform.position + ballSpawnShiftVector));
        //Instantiate(prefabBall, animator.transform.position + ballSpawnShiftVector, animator.transform.rotation);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
