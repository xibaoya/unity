using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 腕关节旋转运动播种
/*腕关节旋转运动播种
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // 参考腕关节的Transform
    public Transform wristJoint;
    // 播种时的旋转角度
    public float rotationAngle = 90f;
    // 旋转的时间
    public float rotationDuration = 1f;

    private bool isPlanting = false;
    private Quaternion targetRotation;

    void Update()
    {
        // 按下空格键开始播种过程
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartPlanting();
        }

        // 如果正在播种，则逐渐旋转腕关节
        if (isPlanting)
        {
            RotateWrist();
        }
    }

    private void StartPlanting()
    {
        isPlanting = true;
        // 计算目标旋转
        //targetRotation = wristJoint.rotation * Quaternion.Euler(0, rotationAngle, 0);
        targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);
    }

    private void RotateWrist()
    {
        // 逐渐旋转腕关节到目标旋转
        wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

        // 检查是否到达目标旋转
        if (Quaternion.Angle(wristJoint.rotation, targetRotation) < 0.1f)
        {
            isPlanting = false; // 完成播种过程
        }
    }
}
*/



/*自己按照之前锄地修改的   旋转90度再旋转回去
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // 参考腕关节的Transform
    public Transform wristJoint;
    // 播种时的旋转角度
    public float rotationAngle = 90f;
    // 旋转的时间
    //   public float rotationDuration = 1f;

    private bool isPlanting = false; // 当前是否在旋转
    private Quaternion targetRotation; // 目标旋转
                                       //   private float rotationProgress = 0f; // 旋转进度
    public float wristJointSpeed = 1f; // 下臂抬起的速度

    private void Update()
    {
        // 检测空格是否点击
        if (Input.GetKeyDown(KeyCode.Space)) //按下空格键开始播种过程
        {
            // 如果没有正在进行的开垦动作，则开始新动作
            if (isPlanting)
            {
                StartCoroutine(PlantingCoroutine());
            }
        }
    }

    private System.Collections.IEnumerator PlantingCoroutine()
    {
        // 开始开垦的动画
        isPlanting = true; // 标记为正在开垦
        float elapsed = 0f;
        float duration = 1f; // 每次动作持续时间
        Quaternion initialWristJointRotation = wristJoint.rotation;
        Quaternion targetWristJointRotation = initialwristJointRotation * Quaternion.Euler(rotationAngle, 0, 0);

        // 抬起下臂
        elapsed = 0f;
        while (elapsed < duration)
        {
            wristJoint.rotation = Quaternion.Slerp(initialWristJointRotation, targetWristJointRotation, elapsed / duration);
            elapsed += Time.deltaTime * wristJointSpeed;
            yield return null;
        }

        // 还原下臂
        elapsed = 0f;
        while (elapsed < duration)
        {
            lowerArm.rotation = Quaternion.Slerp(targetWristJointRotation, initialWristJointRotation, elapsed / duration);
            elapsed += Time.deltaTime * wristJointSpeed;
            yield return null;
        }

        // 确保下臂回到初始位置
        wristJoint.rotation = initialwristJointRotation;


        isCultivating = false; // 动作完成，标记为未开垦
    }

}

*/






/*
//腕关节旋转运动播种 点击空格一次正向旋转，再点一次逆向旋转
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // 参考腕关节的Transform
    public Transform wristJoint;
    // 播种时的旋转角度
    public float rotationAngle = 90f;
    // 旋转的时间
    public float rotationDuration = 1f;

    private bool isPlanting = false; // 当前是否在旋转
    private Quaternion targetRotation; // 目标旋转
    private bool isRotatingBack = false; // 是否正在逆向旋转

    void Update()
    {
        // 按下空格键开始播种过程
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartPlanting();
        }

        // 如果正在播种，则逐渐旋转腕关节
        if (isPlanting)
        {
            RotateWrist();
        }
    }

    private void StartPlanting()
    {
        isPlanting = true;

        // 根据当前状态决定目标旋转
        if (!isRotatingBack)
        {
            // 计算目标旋转
            targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);
        }
        else
        {
            // 如果已经旋转过90度，设置目标旋转回到原位
            targetRotation = wristJoint.rotation * Quaternion.Euler(-rotationAngle, 0, 0);
        }
    }

    private void RotateWrist()
    {
        // 逐渐旋转腕关节到目标旋转
        wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

        // 检查是否到达目标旋转
        if (Quaternion.Angle(wristJoint.rotation, targetRotation) < 0.1f)
        {
            if (!isRotatingBack)
            {
                // 完成播种过程，开始逆向旋转
                isRotatingBack = true;
                isPlanting = false; // 重置播种状态
            }
            else
            {
                // 完成逆向旋转，重置所有状态
                isRotatingBack = false;
                isPlanting = false; // 完成整个旋转过程
            }
        }
    }
}
*/


/*腕关节旋转运动播种 点击空格一次正向旋转，再逆向旋转
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // 参考腕关节的Transform
    public Transform wristJoint;
    // 播种时的旋转角度
    public float rotationAngle = 90f;
    // 旋转的时间
    public float rotationDuration = 1f;
    // 逆向旋转前的等待时间
    public float delayBeforeRotatingBack = 2f;

    private bool isPlanting = false; // 当前是否在旋转

    void Update()
    {
        // 按下空格键开始播种过程
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartCoroutine(PlantSeeds());
        }
    }

    private IEnumerator PlantSeeds()
    {
        isPlanting = true;

        // 计算目标旋转（腕关节旋转90度）
        Quaternion targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);

        // 旋转腕关节到目标旋转
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));
            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保腕关节确实到达目标旋转
        wristJoint.rotation = targetRotation;

        // 等待5秒钟再进行逆向旋转
        yield return new WaitForSeconds(delayBeforeRotatingBack);

        // 计算逆向旋转的目标（旋转回原位）
        targetRotation = wristJoint.rotation * Quaternion.Euler(-rotationAngle, 0, 0);

        // 旋转腕关节回到原位
        timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));
            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保腕关节确实回到原位
        wristJoint.rotation = targetRotation;

        // 重置播种状态
        isPlanting = false;
    }
}
*/



/*using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // 参考腕关节的Transform
    public Transform wristJoint;
    // 袋子的Transform
    public Transform bagTransform;
    // 播种时的旋转角度
    public float rotationAngle = 90f;
    // 旋转的时间
    public float rotationDuration = 1f;
    // 逆向旋转前的等待时间
    public float delayBeforeRotatingBack = 5f;

    private bool isPlanting = false; // 当前是否在旋转

    void Update()
    {
        // 按下空格键开始播种过程
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartCoroutine(PlantSeeds());
        }
    }

    private IEnumerator PlantSeeds()
    {
        isPlanting = true;

        // 计算目标旋转（腕关节旋转90度）
        Quaternion targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);

        // 旋转腕关节到目标旋转
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保腕关节确实到达目标旋转
        wristJoint.rotation = targetRotation;

        // 等待5秒钟再进行逆向旋转
        yield return new WaitForSeconds(delayBeforeRotatingBack);

        // 计算逆向旋转的目标（旋转回原位）
        targetRotation = wristJoint.rotation * Quaternion.Euler(-rotationAngle, 0, 0);

        // 旋转腕关节回到原位
        timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

  
            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保腕关节确实回到原位
        wristJoint.rotation = targetRotation;


      
        isPlanting = false;
    }
}
*/



/*//实现手带着手中的袋子旋转运动90度后又返回到原来位置
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // 参考腕关节的Transform
    public Transform wristJoint;
    // 袋子的Transform
    public Transform bagTransform;
    // 播种时的旋转角度
    public float rotationAngle = 90f;
    // 旋转的时间
    public float rotationDuration = 1f;
    // 逆向旋转前的等待时间
    public float delayBeforeRotatingBack = 1f;

    private bool isPlanting = false; // 当前是否在旋转

    void Update()
    {
        // 按下空格键开始播种过程
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartCoroutine(PlantSeeds());
        }
    }

    private IEnumerator PlantSeeds()
    {
        isPlanting = true;

        // 计算目标旋转（腕关节旋转90度）
        Quaternion targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);

        // 旋转腕关节到目标旋转
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            // 同步更新袋子的旋转
            bagTransform.rotation = wristJoint.rotation;

            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保腕关节确实到达目标旋转
        wristJoint.rotation = targetRotation;
        bagTransform.rotation = wristJoint.rotation; // 确保袋子同步

        // 等待5秒钟再进行逆向旋转
        yield return new WaitForSeconds(delayBeforeRotatingBack);

        // 计算逆向旋转的目标（旋转回原位）
        targetRotation = wristJoint.rotation * Quaternion.Euler(-rotationAngle, 0, 0);

        // 旋转腕关节回到原位
        timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            // 同步更新袋子的旋转
            bagTransform.rotation = wristJoint.rotation;

            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保腕关节确实回到原位
        wristJoint.rotation = targetRotation;
        bagTransform.rotation = wristJoint.rotation; // 确保袋子同步

        isPlanting = false;
    }
}
*/








/*//实现手带着手中的袋子旋转运动90度后种下一颗种子，又返回到原来位置
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // 参考腕关节的Transform
    public Transform wristJoint;
    // 袋子的Transform
    public Transform bagTransform;
    // 播种时的旋转角度
    public float rotationAngle = 90f;
    // 旋转的时间
    public float rotationDuration = 1f;
    // 逆向旋转前的等待时间
    public float delayBeforeRotatingBack = 1f;

    // 种子的Prefab
    public GameObject seedPrefab; // 用于指定种子的Prefab
    // 种子下落的高度
    public float fallDistance = 1f; // 下落高度

    private bool isPlanting = false; // 当前是否在旋转

    void Update()
    {
        // 按下空格键开始播种过程
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartCoroutine(PlantSeeds());
        }
    }

    private IEnumerator PlantSeeds()
    {
        isPlanting = true;

        // 计算目标旋转（腕关节旋转90度）
        Quaternion targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);

        // 旋转腕关节到目标旋转
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            // 同步更新袋子的旋转
            bagTransform.rotation = wristJoint.rotation;

            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保腕关节确实到达目标旋转
        wristJoint.rotation = targetRotation;
        bagTransform.rotation = wristJoint.rotation; // 确保袋子同步

        // 下落种子
        StartCoroutine(DropSeed());

        // 等待指定时间再进行逆向旋转
        yield return new WaitForSeconds(delayBeforeRotatingBack);

        // 计算逆向旋转的目标（旋转回原位）
        targetRotation = wristJoint.rotation * Quaternion.Euler(-rotationAngle, 0, 0);

        // 旋转腕关节回到原位
        timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            // 同步更新袋子的旋转
            bagTransform.rotation = wristJoint.rotation;

            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保腕关节确实回到原位
        wristJoint.rotation = targetRotation;
        bagTransform.rotation = wristJoint.rotation; // 确保袋子同步

        isPlanting = false;
    }

    private IEnumerator DropSeed()
    {
        // 实例化种子
        GameObject seed = Instantiate(seedPrefab, bagTransform.position, Quaternion.identity);

        // 获取种子的初始位置
        Vector3 initialPosition = seed.transform.position;

        // 计算下落的目标位置（当前高度减去下落距离）
        Vector3 targetPosition = initialPosition - new Vector3(0, fallDistance, 0);

        float timeElapsed = 0f;
        float fallDuration = fallDistance / 2f; // 可以调整下落的持续时间

        // 平滑下落的过程
        while (timeElapsed < fallDuration)
        {
            seed.transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / fallDuration);
            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保种子到达目标位置
        seed.transform.position = targetPosition;
    }
}
*/



#endregion




using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    // 参考腕关节的Transform
    public Transform wristJoint;
    // 袋子的Transform
    public Transform bagTransform;
    // 播种时的旋转角度
    public float rotationAngle = 90f;
    // 旋转的时间
    public float rotationDuration = 1f;
    // 逆向旋转前的等待时间
    public float delayBeforeRotatingBack = 1f;

    // 种子的Prefab
    public GameObject seedPrefab; // 用于指定种子的Prefab
    // 种子下落的高度
    public float fallDistance = 1f; // 下落高度

    private bool isPlanting = false; // 当前是否在旋转
    private int seedsPlanted = 0; // 已经落地的种子数
    private int score = 0; // 当前分数

    void Update()
    {
        // 按下空格键开始播种过程
        if (Input.GetKeyDown(KeyCode.Space) && !isPlanting)
        {
            StartCoroutine(PlantSeeds());
        }
    }

    private IEnumerator PlantSeeds()
    {
        isPlanting = true;

        // 计算目标旋转（腕关节旋转90度）
        Quaternion targetRotation = wristJoint.rotation * Quaternion.Euler(rotationAngle, 0, 0);

        // 旋转腕关节到目标旋转
        float timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            // 同步更新袋子的旋转
            bagTransform.rotation = wristJoint.rotation;

            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保腕关节确实到达目标旋转
        wristJoint.rotation = targetRotation;
        bagTransform.rotation = wristJoint.rotation; // 确保袋子同步

        // 下落种子
        StartCoroutine(DropSeed());

        // 等待指定时间再进行逆向旋转
        yield return new WaitForSeconds(delayBeforeRotatingBack);

        // 计算逆向旋转的目标（旋转回原位）
        targetRotation = wristJoint.rotation * Quaternion.Euler(-rotationAngle, 0, 0);

        // 旋转腕关节回到原位
        timeElapsed = 0f;
        while (timeElapsed < rotationDuration)
        {
            wristJoint.rotation = Quaternion.RotateTowards(wristJoint.rotation, targetRotation, Time.deltaTime * (rotationAngle / rotationDuration));

            // 同步更新袋子的旋转
            bagTransform.rotation = wristJoint.rotation;

            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保腕关节确实回到原位
        wristJoint.rotation = targetRotation;
        bagTransform.rotation = wristJoint.rotation; // 确保袋子同步

        isPlanting = false;
    }

    private IEnumerator DropSeed()
    {
        // 实例化种子
        GameObject seed = Instantiate(seedPrefab, bagTransform.position, Quaternion.identity);

        // 获取种子的初始位置
        Vector3 initialPosition = seed.transform.position;

        // 计算下落的目标位置（当前高度减去下落距离）
        Vector3 targetPosition = initialPosition - new Vector3(0, fallDistance, 0);

        float timeElapsed = 0f;
        float fallDuration = fallDistance / 2f; // 可以调整下落的持续时间

        // 平滑下落的过程
        while (timeElapsed < fallDuration)
        {
            seed.transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / fallDuration);
            timeElapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保种子到达目标位置
        seed.transform.position = targetPosition;

        // 每次种子落地，增加已落地种子的计数
        seedsPlanted++;

        // 每三次种子落地增加一分
        if (seedsPlanted >= 3)
        {
            score++; // 增加分数
            seedsPlanted = 0; // 重置种子计数
            Debug.Log("Score: " + score); // 输出当前分数
        }
    }
}




