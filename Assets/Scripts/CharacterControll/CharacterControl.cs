using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    //public GameObject damagePre;
    public static CharacterControl instace;
    public static float runspeed = 3f;
    private GameObject Player;
   // private GameObject gameObject;
    private bool gameEnd = false;
    private Animator animator;
    private Rigidbody rigidbody;
    private float jumpForce = 15f;
    private bool isGround;//防止玩家二连跳
    private ParticleSystem circle, slash,aoe,red;
    public static bool isAttacking=false;
    public static bool isAttacking1=false;
    public static bool isAttacking2=false;
    public static bool isAttacking3=false;
    private bool isParticle = true;
    //各技能冷却时间
    public static float attackcd = 14f;
    public static float attack = 14f;
    private float attack1cd = 10f;
    private float attack1 = 10f;
    private float attack2cd = 8f;
    private float attack2 = 8f;
    private float rectcd = 6f;
    private float rect = 6f;
    private bool isStartTimer = false;//计时器开关
    private bool isStartTimer1 = false;//计时器开关
    private bool isStartTimer2 = false;//计时器开关
    private bool isStartTimer3 = false;//计时器开关
    private float timer;//计时器
    private float timer1;//计时器
    private float timer2;//计时器
    private float timer3;//计时器
    private Image filledImage;//技能填充图片
    private Image filledImage1;//技能填充图片
    private Image filledImage2;//技能填充图片
    private Image filledImage3;//技能填充图片
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //gameObject = GameObject.Find("GameObject");
        Physics.gravity = new Vector3(0, -98, 0);
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        circle=transform.GetChild(4).GetComponent<ParticleSystem>();
        slash=transform.GetChild(5).GetComponent<ParticleSystem>();
        aoe=transform.GetChild(6).GetComponent<ParticleSystem>();
        red=transform.GetChild(7).GetComponent<ParticleSystem>();
        filledImage = GameObject.Find("Canvas/skillImage/skillImage/filledImage").GetComponent<Image>();
        filledImage1 = GameObject.Find("Canvas/skillImage1/shillImage1/filledImage").GetComponent<Image>();
        filledImage2 = GameObject.Find("Canvas/skillImage2/shillImage2/filledImage").GetComponent<Image>();
        filledImage3 = GameObject.Find("Canvas/skillImage3/shillImage3/filledImage").GetComponent<Image>();
        EventCenter.AddListerner(EventNum.Defeat, BroadDeath);//监听到死亡，则播放死亡的动画
        EventCenter.AddListerner(EventNum.Win, BroadWin);
        //EventCenter.AddListerner(EventNum.Defeat2, BroadDeath);//监听到死亡，则播放死亡的动画
        //EventCenter.AddListerner(EventNum.Win2, BroadWin);
    }

    private void Update()
    {
        if (gameEnd) return;
        float h = Input.GetAxis("Horizontal");//获取水平轴(A D）
        float v = Input.GetAxis("Vertical");//获取垂直轴（W S）
        Vector3 direction = new Vector3(h, 0, v); //创建方向向量
        if (direction != Vector3.zero)  //如果按下了方向键
        {
            transform.rotation = Quaternion.LookRotation(direction); //朝向direction方向
            transform.Translate(Vector3.forward * runspeed * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.S)||
            Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D))
        {
            gameObject.GetComponent<AudioSource>().clip = GameFacade.Instance.LoadAudio("run",3);
            gameObject.GetComponent<AudioSource>().Play();
            animator.SetBool("speed", true);
            isParticle=false;
        }
        if (Input.GetKeyUp(KeyCode.W)|| Input.GetKeyUp(KeyCode.S)|| 
            Input.GetKeyUp(KeyCode.A)|| Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("speed", false);
            gameObject.GetComponent<AudioSource>().Stop();
            isParticle = true;
        } 
        //transform.localPosition += rigidbody.velocity * Time.fixedDeltaTime;
        //transform.Rotate(0, h * 2f, 0);//让人物进行旋转改变方向
        //animator.SetFloat("Direction",h);
        if (Input.GetKeyDown(KeyCode.Space)&&isGround)
        {
            rigidbody.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            animator.SetBool("Jump", true);
            gameObject.GetComponent<AudioSource>().clip = GameFacade.Instance.LoadAudio("Jump",3);
            gameObject.GetComponent<AudioSource>().Play();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Jump", false);
            gameObject.GetComponent<AudioSource>().Stop();
        }
        Attack();
        Attack1();
        Attack2();
        Rect();
        //EventCenter.AddListerner(EventNum.Defeat1, BroadDeath);//监听到死亡，则播放死亡的动画
        //EventCenter.AddListerner(EventNum.Win1,BroadWin);
        //EventCenter.AddListerner(EventNum.Defeat2, BroadDeath);//监听到死亡，则播放死亡的动画
        //EventCenter.AddListerner(EventNum.Win2, BroadWin);
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(EventNum.Win, BroadWin);
        EventCenter.RemoveListener(EventNum.Defeat, BroadDeath);
    }
    public void BroadDeath()
    {
        Debug.Log("death");
        //animator.SetBool("Death",true);
        CloseParticle();
        gameObject.GetComponent<AudioSource>().clip = GameFacade.Instance.LoadAudio("Defeat", 3);
        gameObject.GetComponent<AudioSource>().Play();
        animator.Play("Death_01");
        GameObject.Find("Canvas").transform.GetChild(12).gameObject.SetActive(true);
        Debug.Log("Defeat");
        gameEnd = true;
    }
    public void BroadWin()
    {
        
        //animator.SetBool("Win", true);
       //Debug.Log("death");
        CloseParticle();
        gameObject.GetComponent<AudioSource>().clip=GameFacade.Instance.LoadAudio("Win", 3);
        gameObject.GetComponent<AudioSource>().Play();
        animator.Play("Win");
        GameObject.Find("Canvas").transform.GetChild(11).gameObject.SetActive(true);
        Debug.Log("Win");
        gameEnd=true;
    }
    private void CloseParticle()
    {
        circle.gameObject.SetActive(false);
        slash.gameObject.SetActive(false);
        aoe.gameObject.SetActive(false);    
        red.gameObject.SetActive(false);
    }
    private void Attack()
    {

        attackcd += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.J) && isStartTimer == false)
        {
            if (attackcd > attack)
            {
                isStartTimer = true;
                isAttacking = true;
                Player.transform.GetChild(3).gameObject.SetActive(true);
                gameObject.GetComponent<AudioSource>().clip = GameFacade.Instance.LoadAudio("J", 3);
                gameObject.GetComponent<AudioSource>().Play();
                animator.SetBool("Attack", true);
                if (isParticle)
                {
                    circle.gameObject.SetActive(true);
                    circle.Play();
                }
                attackcd = 0;
            }
        }
        if (isStartTimer)
        {
            timer += Time.deltaTime;
            filledImage.fillAmount = (attack - timer) / attack;
            if (timer > attack)
            {
                filledImage.fillAmount = 1;
                timer = 0;
                isStartTimer = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            isAttacking = false;
            Player.transform.GetChild(3).gameObject.SetActive(false);
            animator.SetBool("Attack", false);
            gameObject.GetComponent<AudioSource>().Stop();
            circle.gameObject.SetActive(false);
            circle.Stop();
        }
    }
    
    private void Attack1()
    {
        attack1cd+=Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.K)&&isStartTimer1==false)
        {
            if (attack1cd > attack1)
            {
                isStartTimer1 = true;
                isAttacking1 = true;
                Player.transform.GetChild(3).gameObject.SetActive(true);
                animator.SetBool("Attack1", true);
                gameObject.GetComponent<AudioSource>().clip = GameFacade.Instance.LoadAudio("K", PlayerPrefs.GetInt("Player"));
                gameObject.GetComponent<AudioSource>().Play();
                if (isParticle)
                {
                    aoe.gameObject.SetActive(true);
                    aoe.Play();
                }
                attack1cd = 0;
            }
        }
        if (isStartTimer1)
        {
            timer1 += Time.deltaTime;
            filledImage3.fillAmount = (attack1 - timer1) / attack1;
            if (timer1 > attack1)
            {
                filledImage3.fillAmount = 1;
                timer1 = 0;
                isStartTimer1 = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.K))
        {

            isAttacking1 = false;
            Player.transform.GetChild(3).gameObject.SetActive(false);
            animator.SetBool("Attack1", false);
            gameObject.GetComponent<AudioSource>().Stop();
            aoe.gameObject.SetActive(false);
            aoe.Stop();
        }
    }

    private void Attack2()
    {
        attack2cd+=Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.L)&&isStartTimer2==false)
        {
            if(attack2cd > attack2)
            {
                isStartTimer2 = true;
                isAttacking2 = true;
                Player.transform.GetChild(3).gameObject.SetActive(true);
                animator.SetBool("Attack2", true);
                gameObject.GetComponent<AudioSource>().clip = GameFacade.Instance.LoadAudio("L", PlayerPrefs.GetInt("Player"));
                gameObject.GetComponent<AudioSource>().Play();
                if (isParticle)
                {
                    red.gameObject.SetActive(true);
                    red.Play();
                }
                attack2cd = 0;
            }
        }
          if(isStartTimer2)
        {
           timer2 += Time.deltaTime;
           filledImage1.fillAmount = (attack2 - timer2) / attack2;
            if (timer2 > attack2)
            {
                filledImage1.fillAmount = 1;
                timer2 = 0;
                isStartTimer2 = false;
            }
        }   
        if (Input.GetKeyUp(KeyCode.L))
        {
            isAttacking2 = false;
            Player.transform.GetChild(3).gameObject.SetActive(false);
            animator.SetBool("Attack2", false);
            gameObject.GetComponent<AudioSource>().Stop();
            red.gameObject.SetActive(false);
            red.Stop();
        }
    }

    private void Rect()
    {
        rectcd += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.I)&&isStartTimer3==false)
        {
            if(rectcd>rect)
            {
                isStartTimer3 = true;
                isAttacking3 = true;
                Player.transform.GetChild(3).gameObject.SetActive(true);
                animator.SetBool("Rect",true);
                gameObject.GetComponent<AudioSource>().clip = GameFacade.Instance.LoadAudio("I",PlayerPrefs.GetInt("Player"));
                gameObject.GetComponent<AudioSource>().Play();
                if (isParticle)
                {
                    slash.gameObject.SetActive(true);
                    slash.Play();
                }
                rectcd = 0;
            }
        }
        if(isStartTimer3)
        {
            timer3 += Time.deltaTime;
            filledImage2.fillAmount = (rect - timer3) / rect;
            if (timer3 > rect)
            {
                filledImage2.fillAmount = 1;
                timer3 = 0;
                isStartTimer3=false;
            }
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            isAttacking3 = false;
            animator.SetBool("Rect", false);
            gameObject.GetComponent<AudioSource>().Stop();
            Player.transform.GetChild(3).gameObject.SetActive(false);
            slash.gameObject.SetActive(false);
            slash.Stop();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
