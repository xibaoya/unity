using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*摄像头切换
public class CameraSwitcher : MonoBehaviour
{
    public Camera cameraMain; // 主摄像头
    public Camera cameraTaskCompleted; // 任务完成后使用的摄像头

    void Start()
    {
        // 初始设置为主摄像头
        SwitchToMainCamera();
    }

    public void SwitchToMainCamera()
    {
        cameraMain.gameObject.SetActive(true);
        cameraTaskCompleted.gameObject.SetActive(false);
    }

    public void SwitchToTaskCompletedCamera()
    {
        cameraMain.gameObject.SetActive(false);
        cameraTaskCompleted.gameObject.SetActive(true);
    }

    // 假设这是一个任务完成的回调
    public void OnTaskCompleted()
    {
        SwitchToTaskCompletedCamera();
    }
}*/


//点击鼠标右键切换摄像头
public class CameraSwitcher : MonoBehaviour
{
    public Camera cameraMain; // 主摄像头
    public Camera cameraTaskCompleted; // 任务完成后使用的摄像头

    void Start()
    {
        // 初始设置为主摄像头
        SwitchToMainCamera();
    }

    void Update()
    {
        // 检测鼠标右键点击
        if (Input.GetMouseButtonDown(1)) // 1代表右键
        {
            // 切换摄像头
            if (cameraMain.gameObject.activeInHierarchy)
            {
                SwitchToTaskCompletedCamera();
            }
            else
            {
                SwitchToMainCamera();
            }
        }
    }

    public void SwitchToMainCamera()
    {
        cameraMain.gameObject.SetActive(true);
        cameraTaskCompleted.gameObject.SetActive(false);
        Debug.Log("切换到主摄像头");
    }

    public void SwitchToTaskCompletedCamera()
    {
        cameraMain.gameObject.SetActive(false);
        cameraTaskCompleted.gameObject.SetActive(true);
        Debug.Log("切换到任务完成摄像头");
    }
}



/*得一分后点击鼠标右键切换摄像头
public class CameraSwitcher : MonoBehaviour
{
    public Camera cameraMain; // 主摄像头
    public Camera cameraTaskCompleted; // 任务完成后使用的摄像头

    private int score = 0; // 当前分数
    private bool canSwitchCamera = false; // 是否可以切换摄像头

    void Start()
    {
        // 初始设置为主摄像头
        SwitchToMainCamera();
    }

    void Update()
    {
        // 检测鼠标右键点击
        if (Input.GetMouseButtonDown(1) && canSwitchCamera) // 1代表右键
        {
            // 切换摄像头
            if (cameraMain.gameObject.activeInHierarchy)
            {
                SwitchToTaskCompletedCamera();
            }
            else
            {
                SwitchToMainCamera();
            }
        }
    }

    public void AddScore(int points)
    {
        score += points; // 增加分数
        Debug.Log("当前分数: " + score);

        // 当得分达到1分时，允许切换摄像头
        if (score >= 1)
        {
            canSwitchCamera = true;
            Debug.Log("可以切换摄像头");
        }
    }

    public void SwitchToMainCamera()
    {
        cameraMain.gameObject.SetActive(true);
        cameraTaskCompleted.gameObject.SetActive(false);
        Debug.Log("切换到主摄像头");
    }

    public void SwitchToTaskCompletedCamera()
    {
        cameraMain.gameObject.SetActive(false);
        cameraTaskCompleted.gameObject.SetActive(true);
        Debug.Log("切换到任务完成摄像头");
    }
}
*/