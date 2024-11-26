using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Damage : MonoBehaviour
{
    public static Damage instance;
    public TMP_Text text;
    float alpha = 1;
    private void FixedUpdate()
    {
        ChangeAlpha();
    }
    public void Init(string damage)
    {
        text.text = damage;
        alpha = 1;
    }
    void ChangeAlpha()
    {
        if(alpha<=0)
        {
            return;
        }
        alpha -= 0.01f;
        text.color=new Color(text.color.r, text.color.g, text.color.b,alpha);
    }
}
