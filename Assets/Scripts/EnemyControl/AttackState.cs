using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;
    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        //parameter.animator.Play("Attack");

    }

    public void OnExit()
    {
        manager.explosion.gameObject.SetActive(false);//让特效关闭
    }
    bool temp = false;
    private void OnAttack()
    {
        parameter.attackCd += Time.deltaTime;
        if (parameter.attackCd > 3f&&temp ==false)
        {
            Debug.Log("5555");
            temp = true;
            parameter.animator.Play("Attack");
            manager.explosion.gameObject.SetActive(true);//让特效先显示出来
            manager.explosion.Play();
            //manager.GetComponent<AudioSource>().Play();
            manager.GetComponent<AudioSource>().PlayOneShot(GameFacade.Instance.LoadAudio("Blast",3));
            //manager.GetComponent<AudioSource>().Play();
            Parameter.characterhp -= 10;
            Debug.Log(Parameter.characterhp);
            //parameter.attackCd = 0;
        }
        if (parameter.attackCd > 3.8f)
        {
            manager.explosion.gameObject.SetActive(false);//让特效关闭
            manager.explosion.Stop();
            //manager.GetComponent<AudioSource>().Stop();
            parameter.animator.Play("Idle");
            parameter.attackCd = 0;
            temp = false;
        }
    }
    public void OnUpdate()
    {
        OnAttack();
        if (parameter.target != null)
        {
            manager.transform.LookAt(parameter.target.position);
        }
        //manager.FlipTo(parameter.target);//朝向目标
        if (parameter.target==null||parameter.target.position.x < parameter.chasePoints[0].position.x||
            parameter.target.position.x > parameter.chasePoints[1].position.x)
        {
            manager.TransitionState(StateType.Idle);
        }
        if(parameter.target!=null&&manager.distance<1.2f)
        {
            manager.TransitionState(StateType.Walk_Back);
        }
    }
}
