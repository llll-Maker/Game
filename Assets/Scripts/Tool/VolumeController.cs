using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;
    public Toggle toggle;

    private GameObject UIRoot;
    private void Start()
    {
        UIRoot = GameObject.Find("Canvas");
        UIRoot.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
        {
            UIRoot.transform.GetChild(3).gameObject.SetActive(true);
        });
        //按下返回按钮
        UIRoot.transform.GetChild(3).GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            UIRoot.transform.GetChild(3).gameObject.SetActive(false);
        });
    }
    private void Update()
    {
        
    }
    public void ControlVolume()
    {
        
        if (toggle.isOn)
        {
            //激活声音对象自动播放
            audioSource.gameObject.SetActive(true);
        }
        else
        {
            audioSource.gameObject.SetActive(false);
        } 
    }
    //滚动条控制声音大小
    public void Volume()
    {
        audioSource.volume=slider.value;
    }
}
