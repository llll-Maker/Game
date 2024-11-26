using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.VersionControl.Asset;

public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float timer;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.animator.Play("Idle");//进入时播放动画
    }

    public void OnExit()
    {
        timer=0;//清空计时器
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;
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
        if (timer > parameter.idleTime)
        {
            manager.TransitionState(StateType.Walk);//巡逻状态
        }
        //if (manager.distance < 2f&&manager.currentState!= manager.states[StateType.Attack] && parameter.target != null)
        //{
        //    manager.TransitionState(StateType.Walk_Back);
        //}
    }
}
