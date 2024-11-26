using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBack : IState
{
    private FSM manager;
    private Parameter parameter;

    public WalkBack(FSM manager)
    {
        this.manager = manager;
        parameter=manager.parameter;
    }

    public void OnEnter()
    {
        parameter.animator.Play("Walk_back");
    }

    public void OnExit()
    {
       
    }

    public void OnUpdate()
    {
        manager.transform.Translate(Vector3.back*parameter.moveBackSpeed*Time.deltaTime);
        if (manager.distance > 1.2f)
        {
            manager.TransitionState(StateType.Attack);
        }

    }
}
