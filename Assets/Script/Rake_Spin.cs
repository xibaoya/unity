using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic; // 添加此引用以使用 List





/*
// 三次开垦，得一分，在开垦土地不会变化，大小三次后刚好合适，产生粒子特效
// 之后人物与锄头六次中第一、第二块地移动 X 正方向，第三块地移动 Z 反方向，第四、五块地移动 X 负方向

public class Rake_Spin : MonoBehaviour
{
    public Camera mainCamera;  // 用来引用主相机

    public Transform lowerArm; // 下臂的 Transform
    public Transform elbow;     // 肘关节的 Transform
    public List<Transform> lands; // 土地的 Transform 列表（假设有6块土地）
    public GameObject cultivationEffect; // 粒子特效的 Prefab
    public float raiseAngle = 30f; // 下臂抬起的角度
    public float lowerArmSpeed = 1f; // 下臂抬起的速度
    public float initialScaleIncrement = 0.1f; // 初始开垦增加的土地大小
    public float scaleDuration = 0.05f; // 增大土地的时间

    private bool isCultivating = false; // 用于控制是否正在开垦
    private int cultivateCount = 0; // 开垦次数
    private int score = 0; // 玩家得分
    private bool hasScored = false; // 用于标记是否已经得分
    private float currentScaleIncrement; // 当前开垦增加的土地大小

    private int currentLandIndex = 0; // 当前操作的土地索引

    private void Start()
    {
        // 初始化当前增量为初始值
        currentScaleIncrement = 2*initialScaleIncrement;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isCultivating && currentLandIndex < lands.Count)
            {
                StartCoroutine(CultivateCoroutine());
            }
        }
    }

    private System.Collections.IEnumerator CultivateCoroutine()
    {
        isCultivating = true;
        float elapsed = 0f;
        float duration = 1f;
        Quaternion initialLowerArmRotation = lowerArm.rotation;
        Quaternion targetLowerArmRotation = initialLowerArmRotation * Quaternion.Euler(0, 0, raiseAngle);

        // 锄地动作开始
        elapsed = 0f;
        while (elapsed < duration)
        {
            lowerArm.rotation = Quaternion.Slerp(initialLowerArmRotation, targetLowerArmRotation, elapsed / duration);
            elapsed += Time.deltaTime * lowerArmSpeed;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < duration)
        {
            lowerArm.rotation = Quaternion.Slerp(targetLowerArmRotation, initialLowerArmRotation, elapsed / duration);
            elapsed += Time.deltaTime * lowerArmSpeed;
            yield return null;
        }

        lowerArm.rotation = initialLowerArmRotation;

        // 每块土地的锄地逻辑
        if (!hasScored)
        {
            Transform currentLand = lands[currentLandIndex];

            // 锄地特效
            Vector3 initialScale = currentLand.localScale;
            Vector3 targetScale = initialScale + new Vector3(currentScaleIncrement, currentScaleIncrement, currentScaleIncrement);

            if (cultivationEffect != null)
            {
                Vector3 effectPosition = currentLand.position + Vector3.up * -0.4f;
                Instantiate(cultivationEffect, effectPosition, Quaternion.identity);
            }

            // 开始锄地的过程（增大土地的尺寸）
            elapsed = 0f;
            while (elapsed < scaleDuration)
            {
                currentLand.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / scaleDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            currentLand.localScale = targetScale;

            cultivateCount++;

            // 如果已完成三次锄地
            if (cultivateCount >= 3)
            {
                score++;
                Debug.Log("得分: " + score);
                cultivateCount = 0;
                hasScored = true;

                // 根据土地的顺序平移，分别在 X 和 Z 轴上移动
                Vector3 targetPosition = transform.position;

                // 按顺序平移：第一、第二块地移动 X 正方向，第三块地移动 Z 反方向，第四、五块地移动 X 负方向
                if (currentLandIndex == 0 || currentLandIndex == 1)
                {
                    targetPosition += new Vector3(1.5f, 0f, 0f); // X 正方向
                }
                else if (currentLandIndex == 2)
                {
                    targetPosition += new Vector3(0f, 0f, -1.5f); // Z 负方向
                }
                else if (currentLandIndex == 3 || currentLandIndex == 4)
                {
                    targetPosition += new Vector3(-1.5f, 0f, 0f); // X 负方向
                }

                // 移动时间
                float moveDuration = 1f;
                float moveElapsed = 0f;

                // 移动角色和耙子
                while (moveElapsed < moveDuration)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPosition, moveElapsed / moveDuration);
                    moveElapsed += Time.deltaTime;
                    yield return null;
                }
                transform.position = targetPosition; // 确保达到目标位置

                // 锄地操作完成后进入下一块土地
                currentLandIndex++;
                if (currentLandIndex < lands.Count)
                {
                    // 继续锄地下一块土地
                    hasScored = false; // 重置得分标记，准备对下一块土地进行操作
                }
            }
        }

        isCultivating = false;
    }
}
*/





// 三次开垦，得一分，在开垦土地不会变化，大小三次后刚好合适，产生粒子特效
// 之后人物与锄头六次中第一、第二块地移动 X 正方向，第三块地移动 Z 反方向，第四、五块地移动 X 负方向
public class Rake_Spin : MonoBehaviour
{
    public Camera mainCamera;  // 用来引用主相机

    public Transform lowerArm; // 下臂的 Transform
    public Transform elbow;     // 肘关节的 Transform
    public List<Transform> lands; // 土地的 Transform 列表（假设有6块土地）
    public GameObject cultivationEffect; // 粒子特效的 Prefab
    public float raiseAngle = 30f; // 下臂抬起的角度
    public float lowerArmSpeed = 1f; // 下臂抬起的速度
    public float initialScaleIncrement = 0.1f; // 初始开垦增加的土地大小
    public float scaleDuration = 0.05f; // 增大土地的时间

    private bool isCultivating = false; // 用于控制是否正在开垦
    private int cultivateCount = 0; // 开垦次数
    private int score = 0; // 玩家得分
    private bool hasScored = false; // 用于标记是否已经得分
    private float currentScaleIncrement; // 当前开垦增加的土地大小

    private int currentLandIndex = 0; // 当前操作的土地索引

    private void Start()
    {
        // 初始化当前增量为初始值
        currentScaleIncrement = 2 * initialScaleIncrement;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isCultivating && currentLandIndex < lands.Count)
            {
                StartCoroutine(CultivateCoroutine());
            }
        }
    }

    private System.Collections.IEnumerator CultivateCoroutine()
    {
        isCultivating = true;
        float elapsed = 0f;
        float duration = 1f;
        Quaternion initialLowerArmRotation = lowerArm.rotation;
        Quaternion targetLowerArmRotation = initialLowerArmRotation * Quaternion.Euler(0, 0, raiseAngle);

        // 锄地动作开始
        elapsed = 0f;
        while (elapsed < duration)
        {
            lowerArm.rotation = Quaternion.Slerp(initialLowerArmRotation, targetLowerArmRotation, elapsed / duration);
            elapsed += Time.deltaTime * lowerArmSpeed;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < duration)
        {
            lowerArm.rotation = Quaternion.Slerp(targetLowerArmRotation, initialLowerArmRotation, elapsed / duration);
            elapsed += Time.deltaTime * lowerArmSpeed;
            yield return null;
        }

        lowerArm.rotation = initialLowerArmRotation;

        // 每块土地的锄地逻辑
        if (!hasScored)
        {
            Transform currentLand = lands[currentLandIndex];

            // 锄地特效
            Vector3 initialScale = currentLand.localScale;
            Vector3 targetScale = initialScale + new Vector3(currentScaleIncrement, currentScaleIncrement, currentScaleIncrement);

            if (cultivationEffect != null)
            {
                Vector3 effectPosition = currentLand.position + Vector3.up * -0.4f;
                Instantiate(cultivationEffect, effectPosition, Quaternion.identity);
            }

            // 开始锄地的过程（增大土地的尺寸）
            elapsed = 0f;
            while (elapsed < scaleDuration)
            {
                currentLand.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / scaleDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            currentLand.localScale = targetScale;

            cultivateCount++;

            // 如果已完成三次锄地
            if (cultivateCount >= 3)
            {
                score++;
                Debug.Log("得分: " + score);
                cultivateCount = 0;
                hasScored = true;

                // 根据土地的顺序平移，分别在 X 和 Z 轴上移动
                Vector3 targetPosition = transform.position;

                // 按顺序平移：第一、第二块地移动 X 正方向，第三块地移动 Z 反方向，第四、五块地移动 X 负方向
                if (currentLandIndex == 0 || currentLandIndex == 1)
                {
                    targetPosition += new Vector3(1.5f, 0f, 0f); // X 正方向
                }
                else if (currentLandIndex == 2)
                {
                    targetPosition += new Vector3(0f, 0f, -1.5f); // Z 负方向
                }
                else if (currentLandIndex == 3 || currentLandIndex == 4)
                {
                    targetPosition += new Vector3(-1.5f, 0f, 0f); // X 负方向
                }

                // 移动时间
                float moveDuration = 2f;
                float moveElapsed = 0f;

                // 移动角色和耙子
                while (moveElapsed < moveDuration)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPosition, moveElapsed / moveDuration);

                    // 同步相机位置，使其与角色一起移动
                    if (mainCamera != null)
                    {
                        mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, transform.position.z);
                    }

                    moveElapsed += Time.deltaTime;
                    yield return null;
                }
                transform.position = targetPosition; // 确保达到目标位置

                // 锄地操作完成后进入下一块土地
                currentLandIndex++;
                if (currentLandIndex < lands.Count)
                {
                    // 继续锄地下一块土地
                    hasScored = false; // 重置得分标记，准备对下一块土地进行操作
                }
            }
        }

        isCultivating = false;
    }

}

