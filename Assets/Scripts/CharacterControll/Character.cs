using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    int i = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Enemy1") == null && GameObject.FindWithTag("Enemy3") == null && i > 0
            && GameObject.FindWithTag("Enemy") == null)
        {
            i--;
            Debug.Log(i);
            EventCenter.Broadcast(EventNum.Win);//敌人全部消失，即玩家胜利
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject.FindWithTag("Enemy").GetComponent<EnemyHp>().GetDamage();
            //GameObject.FindWithTag("Enemy").transform.GetChild(16).GetComponent<Damage>().Init("15");
            //Debug.Log(currentHp);
        }
        if (other.CompareTag("Enemy1"))
        {
            GameObject.FindWithTag("Enemy1").GetComponent<EnemyHp>().GetDamage();
            //GameObject.Find("Enemy1").transform.GetChild(16).GetComponent<Damage>().Init("10");
            //Debug.Log(currentHp);
        }
        if (other.CompareTag("Enemy3"))
        {
            GameObject.FindWithTag("Enemy3").GetComponent<EnemyHp>().GetDamage();
            //Debug.Log(currentHp);
        }
    }
}
