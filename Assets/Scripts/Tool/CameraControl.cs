using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.LightmapEditorSettings;
using static UnityEngine.GraphicsBuffer;

public class CameraControl : MonoBehaviour
{
    private GameObject target;
    public float smooth = 2f;
    public float xSpeed = 200;  //X轴方向拖动速度
    public float ySpeed = 200;  //Y轴方向拖动速度
    public float mSpeed = 10;   //放大缩小速度
    public float yMinLimit = -50; //在Y轴最小移动范围
    public float yMaxLimit = 50; //在Y轴最大移动范围
    public float distance = 10;  //相机视角距离
    public float minDinstance = 2; //相机视角最小距离
    public float maxDinstance = 30; //相机视角最大距离
    public float x = 0.0f;
    public float y = 0.0f;
    public float damping = 5.0f;
    public bool needDamping = true;
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(target);
        //distance = transform.position-target.transform.position;
        Vector3 angle = transform.eulerAngles;
        x = angle.y;
        y = angle.x+30;
    }
    public void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(target);
    }
    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.Lerp(target.transform.position+distance,transform.position, Time.deltaTime*smooth);
        //transform.LookAt(target.transform.position);//摄像机对准物体
                                                  
        
    }
    void LateUpdate() //处理相机部分
    {
        if (target)
        {
            if (Input.GetMouseButton(1))//鼠标右键
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;//获取鼠标偏移量
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                y = ClamAngle(y, yMinLimit, yMaxLimit);

            }
            distance -= Input.GetAxis("Mouse ScrollWheel") * mSpeed;//鼠标滚轮
            distance = Mathf.Clamp(distance, minDinstance, maxDinstance);
            Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
            Vector3 disVector = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * disVector + target.transform.position;

            if (needDamping)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * damping);
                transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * damping);
            }
            else
            {
                transform.rotation = rotation;
                transform.position = position;
            }
        }
    }
    static float ClamAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}









