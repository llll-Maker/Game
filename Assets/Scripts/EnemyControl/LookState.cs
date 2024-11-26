using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookState : IState
{
    private FSM manager;
    private Parameter parameter;
    private int patrolPosition;//用于下标查找和切换巡逻点

    public LookState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Walk");

    }

    public void OnExit()
    {
        patrolPosition++;
        if (patrolPosition >= parameter.patrolPoints.Length)
        {
            patrolPosition = 0;
        }
    }

    public void OnUpdate()
    {
        manager.FlipTo(parameter.patrolPoints[patrolPosition]);
        manager.transform.position = Vector3.MoveTowards(manager.transform.position,
            parameter.patrolPoints[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);
        if (parameter.target != null && parameter.target.position.x >= parameter.chasePoints[0].position.x &&
           parameter.target.position.x <= parameter.chasePoints[1].position.x)
        {
            Collider[] colliders = Physics.OverlapSphere(manager.transform.position, 3f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    manager.TransitionState(StateType.Attack);
                    manager.transform.LookAt(parameter.target.position);
                }
            }
        }
        if (Vector3.Distance(manager.transform.position, parameter.patrolPoints[patrolPosition].position) < 0.5f)
        {
            manager.TransitionState(StateType.Idle);//到达巡逻点播放动画
        }
        //if (manager.distance < 2f && manager.currentState != manager.states[StateType.Attack] && parameter.target != null)
        //{
        //    manager.TransitionState(StateType.Walk_Back);
        //}
    }
}
