using UnityEngine;

public class EnemyIdleStateBehaviour : StateMachineBehaviour
{
    private float deltaTime;

    private int waitInSeconds;
    public int waitSecondsMax = 5;
    public int waitSecondsMin;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        deltaTime = 0;
        waitInSeconds = Random.Range(waitSecondsMin, waitSecondsMax);
        Debug.Log(this + " waitInSeconds " + waitInSeconds);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        deltaTime += Time.deltaTime;

        //Debug.Log(this + " deltaTime " + deltaTime % 1000);

        if (deltaTime % 1000 >= waitInSeconds)
        {
            animator.SetTrigger("DoThrow");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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