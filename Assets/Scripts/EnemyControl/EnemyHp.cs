using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{

    private Image healthHp;
    private float MaxHp = 100f;
    public float currentHp = 10f;
    private void Awake()
    {
        currentHp = MaxHp;

    }
    // Start is called before the first frame update
    void Start()
    {
        healthHp = transform.GetChild(15).GetChild(0).GetChild(0).GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

        healthHp.fillAmount = currentHp / MaxHp;
        if (currentHp <= 0)
        {
            healthHp.fillAmount = 0;
            Destroy(gameObject);//敌人销毁
        }
        //if (GameObject.FindWithTag("Enemy1") == null && GameObject.FindWithTag("Enemy3") == null && i > 0
        //    && GameObject.FindWithTag("Enemy") == null && GameObject.FindWithTag("Enemy2") == null)
        //{
        //    i--;
        //    Debug.Log(i);
        //    EventCenter.Broadcast(EventNum.Win);//敌人全部消失，即玩家胜利
        //}
        // Debug.Log("11" + GameObject.FindWithTag("Enemy1").name);
        //Debug.Log("22"+GameObject.FindWithTag("Enemy3").name);
        //if (GameObject.FindWithTag("Enemy1") == null && GameObject.FindWithTag("Enemy3") == null && i > 0)
        //{
        //    i--;
        //    Debug.Log(i);
        //    EventCenter.Broadcast(EventNum.Win);//敌人全部消失，即玩家胜利
        //}
        //if (GameObject.Find("Enemy") == null && i > 0)
        //{
        //    i--;
        //    Debug.Log(i);
        //    EventCenter.Broadcast(EventNum.Win);//敌人全部消失，即玩家胜利
        //}
    }

    public void GetDamage()
    {
        if (CharacterControl.isAttacking)
        {
            currentHp -= 15f;
            transform.GetChild(16).GetComponent<Damage>().Init("15");
        }
        if (CharacterControl.isAttacking1)
        {
            currentHp -= 10f;
            transform.GetChild(16).GetComponent<Damage>().Init("10");
        }
        if (CharacterControl.isAttacking2)
        {
            currentHp -= 8f;
            transform.GetChild(16).GetComponent<Damage>().Init("8");
        }
        if (CharacterControl.isAttacking3)
        {
            currentHp -= 5f;
            transform.GetChild(16).GetComponent<Damage>().Init("5");
        }

    }
}
