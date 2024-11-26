using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Drawing;
using UnityEngine.SocialPlatforms;

public enum StateType
{
    Idle,Attack,Run,Walk,Walk_Back
}

[Serializable]//使其序列化
public class Parameter
{
    //敌人攻击玩家扣血
    public static float characterhp=10.0f;  //玩家血量
    public static float MaxHP = 100f;  //玩家血量
    //设置敌人的各种参数
    public GameObject UIRoot;
    public float moveSpeed; //移动速度
    public float chaseSpeed; //追击速度
    public float moveBackSpeed; //追击速度
    public float idleTime; //停止时间
    public Transform[] patrolPoints;//巡逻范围
    public Transform[] chasePoints; //追击范围
    public Transform target=null;
    public float attackCd;//开枪冷却时间
    //public float attackCd1=3f;//开枪冷却时间
    //public LayerMask targetLayer;
    //public Transform attackPoint;
   // public float attackArea;
    public Animator animator;//获取动画机
}


public class FSM : MonoBehaviour
{
    private IState currentState;
    private Dictionary<StateType,IState> states=new Dictionary<StateType, IState>();
    public Parameter parameter = new Parameter();
    public ParticleSystem explosion;//发射子弹的粒子特效
    public static Image healthHp;//敌人血条
    private bool gameEnd = false;
    public float distance;
    int i = 1;
    private void Awake()
    {
        Parameter.characterhp = Parameter.MaxHP;//任务血条
       
    }
    // Start is called before the first frame update
    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Walk, new LookState(this));
        states.Add(StateType.Run, new ChaseState(this));
        states.Add(StateType.Attack, new AttackState(this));
        states.Add(StateType.Walk_Back, new WalkBack(this));
        //states.Add(StateType.Die, new DieState(this));
        //states.Add(StateType.Win, new WinState(this));
        TransitionState(StateType.Idle);//一开始先处于Idle状态
        parameter.animator = transform.GetComponent<Animator>();
        parameter.UIRoot = GameObject.Find("Canvas");
        explosion = transform.Find("Explosion").GetComponent<ParticleSystem>();
        //玩家第一关失败，敌人播放胜利的动画
        EventCenter.AddListerner(EventNum.Defeat, BroadWinning);
        //玩家第一关成功，敌人播放失败的动画
        EventCenter.AddListerner(EventNum.Win, BroadDie);
        //玩家第二关失败，敌人播放胜利的动画
        //EventCenter.AddListerner(EventNum.Defeat2, BroadWinning);
        //玩家第二关成功，敌人播放失败的动画
        //EventCenter.AddListerner(EventNum.Win2, BroadDie);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd) return;
        if(parameter.target!=null)
        {
           distance = Vector3.Distance(transform.position, parameter.target.position);
        }
        currentState.OnUpdate();
        //玩家血条
        parameter.UIRoot.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = Parameter.characterhp/ Parameter.MaxHP;//更新主角的血条
        parameter.UIRoot.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>().text = PlayerPrefs.GetString("name");
        if (Parameter.characterhp<= 0 && i>0)
        {
            i--;
            Debug.Log(i);
            explosion.gameObject.SetActive(false);
            parameter.animator.Play("Win");
            EventCenter.Broadcast(EventNum.Defeat);//玩家血条为0，则广播失败
            //explosion.gameObject.SetActive(false);
            //parameter.animator.Play("Die");
            //Destroy(gameObject);
            gameEnd = true;
        }
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(EventNum.Win, BroadWinning);
        EventCenter.RemoveListener(EventNum.Defeat, BroadDie);
    }
    public void TransitionState(StateType type)//进行转换状态
    {
        if (currentState != null)
            currentState.OnExit();//在转移状态时，要执行前一个状态的退出
        currentState = states[type];
        currentState.OnEnter();//执行新状态的进入
    }
    public void FlipTo(Transform target)//让人物改变朝向
    {
        if (target != null)
        {
            if ((transform.position.x > target.transform.position.x && transform.tag == "Enemy1")||
                (transform.position.x > target.transform.position.x && transform.tag == "Enemy3"))
            {
                //transform.localEulerAngles = new Vector3(1, 180, 1);
                //transform.localScale = new Vector3(1, 1, 1);
                transform.localEulerAngles=new Vector3(0,-90,0);

            }
            if ((transform.position.x > target.transform.position.x&&transform.tag=="Enemy")||
                    (transform.position.x > target.transform.position.x && transform.tag == "Enemy2"))
            {
               
                //transform.localScale = new Vector3(1, 1, 1);
                transform.localEulerAngles = new Vector3(0, 0, 0);

            }
            if ((transform.position.x < target.transform.position.x && transform.tag == "Enemy1")||
                (transform.position.x < target.transform.position.x && transform.tag == "Enemy3"))
            {
                //transform.localScale = new Vector3(1, 1, -1);
                transform.localEulerAngles=new Vector3(0,90,0);
            }
            if ((transform.position.x < target.transform.position.x&&transform.tag=="Enemy")||
                (transform.position.x < target.transform.position.x && transform.tag == "Enemy2"))
            {
                //transform.localScale = new Vector3(1, 1, -1);
                transform.localEulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            parameter.target=other.transform;
            //parameter.characterhp -= 10;
            //Debug.Log(parameter.characterhp);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = other.transform;
            //parameter.characterhp -= 10;
            //Debug.Log(parameter.characterhp);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = null;
        }
    }

    private void BroadDie()
    {
        //GetComponent<AudioSource>().Stop();
        //explosion.gameObject.SetActive(false);
        //parameter.animator.Play("Die");
        //Destroy(gameObject);
        gameEnd=true;
    }
    private void BroadWinning()
    {
        //GetComponent<AudioSource>().Stop();
       // explosion.gameObject.SetActive(false);
        //parameter.animator.Play("Win");
        gameEnd = true;
    }
}
