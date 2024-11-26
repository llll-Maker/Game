using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState :ISceneState
{
    public AnimationState(SceneStateController controller) : base("Animation", controller)
    {
    }
    private Transform Camera;

    public float speedx = 2f;
    public float speedZ = 5f;
    private float endZ = -100;
    private float endx = 16;
    private GameObject UIRoot;
    private bool isAnyKeyDown = false;//表示有任意按键摁下

    // Start is called before the first frame update
    public override void StateStart()
    {
        base.StateStart();
        Camera = GameObject.Find("Camera").transform;
        UIRoot = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    public override void StateUpdate()
    {
        base.StateUpdate();
        if (Camera.transform.position.z > endZ)
        {
            Camera.transform.Translate(Vector3.forward * speedZ * Time.deltaTime);
        }
        if (Camera.transform.position.x > endx)
        {
            Camera.transform.Translate(Vector3.down * speedx * Time.deltaTime);
        }
        if (Camera.transform.position.x <= endx)
        {
            UIRoot.transform.GetChild(0).gameObject.SetActive(true);
        }
        InAnyKeyDown();
        if (isAnyKeyDown)
        {
            //切换状态
            controller.SetState(new Game1State(controller));
        }
    }

    private void InAnyKeyDown()
    {
        if (isAnyKeyDown == false)
        {
            if (Input.anyKey)
            {
                isAnyKeyDown = true;
            }
        }
    }

}
