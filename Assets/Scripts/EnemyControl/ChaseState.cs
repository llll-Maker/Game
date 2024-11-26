using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChaseState : IState
{
    private Parameter parameter;
    private FSM manager;

    public ChaseState(FSM manager)
    {
        this.parameter = manager.parameter;
        this.manager = manager;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Run");
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {
        manager.FlipTo(parameter.target);//朝向目标
        if (parameter.target)
        {
            manager.transform.position = Vector3.MoveTowards(manager.transform.position,
                parameter.target.position, parameter.chaseSpeed * Time.deltaTime);
        }
        if (parameter.target == null || parameter.target.position.x < parameter.chasePoints[0].position.x
            || parameter.target.position.x > parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.Idle);
        }
        if (parameter.target != null && parameter.target.position.x >= parameter.chasePoints[0].position.x
            && parameter.target.position.x <= parameter.chasePoints[1].position.x)
        {
            Collider[] colliders = Physics.OverlapSphere(manager.transform.position, 2f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    manager.TransitionState(StateType.Attack);
                    manager.transform.LookAt(parameter.target.position);
                }
            }
        }
        //if (manager.distance < 2f && manager.currentState != manager.states[StateType.Attack]&&parameter.target!=null)
        //{
        //    manager.TransitionState(StateType.Walk_Back);
        //}
    }
}
