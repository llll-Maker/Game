using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BAgController : MonoBehaviour
{
    private GameObject Bag;
    private bool isOpen=false;
    // Start is called before the first frame update
    void Start()
    {
        Bag = GameObject.Find("Canvas/bag");
    }

    // Update is called once per frame
    void Update()
    {
        OpenMyBag();
    }
    void OpenMyBag()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            isOpen = !isOpen;
            Bag.transform.GetChild (0).gameObject.SetActive(isOpen);
        }
        Bag.GetComponent<Button>().onClick.AddListener(() =>
        {
            isOpen = !isOpen;
            Bag.transform.GetChild(0).gameObject.SetActive(isOpen);
        });
    }
}
