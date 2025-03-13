using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*袋子与腕关节同步代码
public class BagFollower : MonoBehaviour
{
    public Transform wristJoint; // 腕关节的Transform

    void Update()
    {
        if (wristJoint != null)
        {
            // 使袋子的旋转与腕关节的旋转保持一致
            transform.rotation = wristJoint.rotation;
        }
    }
}
*/


/*
using UnityEngine;

public class BagFollower : MonoBehaviour
{
    // 袋子的旋转角度
    public float rotationAngle = 90f;
    // 旋转的持续时间
    public float rotationDuration = 1f;
    // 等待的时间
    public float waitDuration = 5f;

    void Start()
    {
        // 开始旋转过程
        StartCoroutine(RotateBag());
    }

    private IEnumerator RotateBag()
    {
        // 旋转袋子 90 度
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, rotationAngle, 0);
        float timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            // 插值旋转
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));
            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保袋子确实到达目标旋转
        transform.rotation = targetRotation;

        // 等待 5 秒
        yield return new WaitForSeconds(waitDuration);

        // 逆向旋转袋子回到原位置
        targetRotation = transform.rotation * Quaternion.Euler(0, -rotationAngle, 0);
        timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            // 插值旋转
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));
            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保袋子回到原位
        transform.rotation = targetRotation;
    }
}
*/




using UnityEngine;

public class BagFollower : MonoBehaviour
{
    // 袋子的旋转角度
    public float rotationAngle = 90f;
    // 旋转的持续时间
    public float rotationDuration = 1f;
    // 等待的时间
    public float waitDuration = 5f;

    private bool isRotating = false; // 标记是否正在旋转

    void Update()
    {
        // 检测空格键按下
        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            isRotating = true; // 设置为正在旋转状态
            StartCoroutine(RotateBag());
        }
    }

    private IEnumerator RotateBag()
    {
        // 旋转袋子 90 度
        //Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, rotationAngle, 0);
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(rotationAngle, 0, 0);

        float timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            // 插值旋转
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));
            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保袋子确实到达目标旋转
        transform.rotation = targetRotation;

        // 等待 5 秒
        yield return new WaitForSeconds(waitDuration);

        // 逆向旋转袋子回到原位置
 //       targetRotation = transform.rotation * Quaternion.Euler(0, -rotationAngle, 0);
        targetRotation = transform.rotation * Quaternion.Euler(-rotationAngle, 0, 0);

        timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            // 插值旋转
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));
            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保袋子回到原位
        transform.rotation = targetRotation;

        isRotating = false; // 重置旋转状态
    }
}
