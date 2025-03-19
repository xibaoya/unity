using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 进行三次开垦，每次开垦完土地都变原来的三倍并产生粒子特效三次 开垦六块地
public class Hoeing : MonoBehaviour
{
    public GameObject hand;
    public float Vertical_angle;
    public bool flag_up;
    public bool flag_up_initialize;
    public bool flag_up_end;
    public bool flag_down;
    public bool flag_fishrod;
    public float bigtheta_x;
    public float bigtheta_v;
    public float bigtheta_x_init;
    public float bigtheta_v_init;
    public float bigtheta_x_end;
    public float bigtheta_end;
    public float bigtheta_v_end;
    public float jiaodu_grade;
    public int grade;
    public float temp;
    public GamePanel gamePanel;
    public GameManagement gameManagement;
    private TCPIP tcpipVar;//通讯

    public float rotationSpeed = 25f; // 旋转速度，单位是度每秒
    public Transform rotationCenter; // 旋转中心，改为 Transform 类型
    public Transform[] lands; // 多块土地的 Transform 数组
    public GameObject particleEffectPrefab; // 粒子特效预制件
    public bool isRotatingForward = true; // 标记当前是否在正向旋转
    private bool isRotating = false; // 标记当前是否在旋转中
    private float totalRotation; // 当前已旋转的角度
    private float targetRotation; // 每次旋转目标角度，正向和反向都为 45°
    public int hoeingCount = 0; // 已进行的开垦次数
    public int currentLandIndex = 0; // 当前正在开垦的土地索引
    private Vector3 initialLandScale; // 记录土地初始大小
    private int particleEffectCount = 0; // 记录粒子特效的生成次数
    private int score = 0; // 得分

    public ParticleSystem waterParticles;//浇水粒子特效，控制出现与消失


    private void Start()
    {
        flag_up_initialize = false;
        flag_up_end = false;
        flag_up = false;
        flag_down = false;
        flag_fishrod = false;
        Application.targetFrameRate = 30;//调整帧率

        hand = GameObject.Find("GleechiRig/GleechiHandRig/Neck/Clavicle_R");
        tcpipVar = hand.GetComponent<TCPIP>();
        // 记录土地的初始大小（假设所有土地大小相同）
        initialLandScale = lands[0].localScale;
        Debug.Log("第一块土地的位置" + lands[1].position);

        if (gameManagement.aim_mode == 0)
        {
            targetRotation = 65f;
            totalRotation = 20f;
        }
        else if (gameManagement.aim_mode == 1)
        {

        }

    }
    public void Pause()
    {
        flag_up_initialize = false;
    }

    public void Initializing()//晋级调用
    {
        gamePanel.AddGrade(grade);
        this.GetComponent<GameManagement>().iniTimes = 0;
        bigtheta_x = 0f;
        gamePanel.ResetScore();
        //设置每一个等级的初始化项
        if (grade == 1)
        {
            hand.transform.position = new Vector3(495.5f, 7.5f, 307.4f);

            if (gameManagement.aim_mode == 0)
            {
                targetRotation = 65f;
                totalRotation = 20f;
            }
        }
        bigtheta_v = 0.1f;
    }

    private void FixedUpdate()
    {
        if (flag_up_initialize == true)
        {
            if (gameManagement.aim_mode == 0)
            {
                //被动模式
                if (isRotatingForward)
                {
                    if (totalRotation < targetRotation) //正向旋转45°
                    {
                        float rotationThisFrame = rotationSpeed * Time.deltaTime;
                        rotationThisFrame = Mathf.Min(rotationThisFrame, targetRotation - totalRotation); // 确保不会超过目标旋转角度
                        transform.RotateAround(rotationCenter.position, Vector3.right, rotationThisFrame);
                        totalRotation += rotationThisFrame;
                        waterParticles.Play();
                    }
                    else
                    {
                        // 达到 45 度后，停止并切换到反向旋转
                        isRotatingForward = false;
                        totalRotation = 0f; // 重置旋转角度
                        waterParticles.Stop();

                        // 如果是第一次、第二次或第三次开垦，扩大土地
                        if (hoeingCount < 3)
                        {
                            ExpandLand();
                        }

                        // 每次土地扩展时触发粒子特效（与扩展次数同步）
                        if (particleEffectCount < hoeingCount && particleEffectPrefab != null)
                        {
                            SpawnParticleEffect();
                            particleEffectCount++; // 增加粒子特效生成计数
                        }
                    }
                }
                else
                {
                    // 反向旋转回原位置
                    if (totalRotation < targetRotation)
                    {
                        // 逐帧旋转，使用 Time.deltaTime 来平滑控制
                        float rotationThisFrame = rotationSpeed * Time.deltaTime;
                        rotationThisFrame = Mathf.Min(rotationThisFrame, targetRotation - totalRotation); // 确保不会超过目标旋转角度
                        transform.RotateAround(rotationCenter.position, Vector3.right, -rotationThisFrame);
                        totalRotation += rotationThisFrame;
                    }
                    else
                    {
                        // 达到原始位置后，停止旋转
                        isRotating = false; // 停止旋转
                        isRotatingForward = true; // 重置为正向旋转
                        totalRotation = 0f; // 重置旋转角度

                        // 如果当前土地已经完成开垦3次，移动到下一块土地
                        if ((hoeingCount == 3 && (currentLandIndex <= 5 || currentLandIndex >= 12)) || (hoeingCount == 2 && currentLandIndex >= 6 && currentLandIndex <= 11))
                        {
                            MoveToNextLand();
                        }
                    }
                }
            }
        }
        else if (gameManagement.aim_mode == 1)
        {

        }
    }
    private void ExpandLand()
    {
        hoeingCount++; // 增加开垦次数

        if (hoeingCount <= 3)
        {
            // 每次开垦后，土地扩大为原来的三倍
            lands[currentLandIndex].localScale = initialLandScale * Mathf.Pow(3.75f, hoeingCount);

            if (hoeingCount == 3)
            {
                // 达到第三次开垦后，停止扩大土地
                Debug.Log("土地 " + (currentLandIndex + 1) + " 已完成三次开垦，后续不会再变化.");

                // 三次开垦后增加一分
                AddScore();
            }
        }
    }

    // 在土地上方0.1米的位置生成粒子特效
    private void SpawnParticleEffect()
    {
        Vector3 spawnPosition = lands[currentLandIndex].position + Vector3.up * 0.0001f; // 在土地上方0.1米
        Instantiate(particleEffectPrefab, spawnPosition, Quaternion.identity); // 实例化粒子系统
    }

    // 增加分数
    private void AddScore()
    {
        score++; // 增加得分
        Debug.Log("获得了一分! 当前得分: " + score);
    }

    // 移动到下一块土地
    public void MoveToNextLand()
    {

        // 增加当前土地索引
        currentLandIndex++;

        // 如果还有更多土地进行开垦
        if (currentLandIndex < 18)//六块土地，3个轮巡
        {
            // 重置开垦次数
            hoeingCount = 0;
            particleEffectCount = 0;

            // 根据当前土地索引进行不同的位移
            if (currentLandIndex == 1 || currentLandIndex == 2 || currentLandIndex == 7 || currentLandIndex == 8)
            {
                // 沿X轴正方向移动1米
                MoveHoeAndRotationCenter(Vector3.right * 2.4f);
            }
            else if (currentLandIndex == 13 || currentLandIndex == 14)
            {
                // 沿X轴正方向移动1米
                MoveHoeAndRotationCenter(Vector3.right * 2.4f);
            }
            else if (currentLandIndex == 3 || currentLandIndex == 9 || currentLandIndex == 15)
            {
                // 沿Z轴负方向移动1米
                MoveHoeAndRotationCenter(Vector3.back * 2.4f);
            }
            else if (currentLandIndex == 4 || currentLandIndex == 5 || currentLandIndex == 10 || currentLandIndex == 11 || currentLandIndex == 16 || currentLandIndex == 17)
            {
                // 沿X轴负方向移动1米
                MoveHoeAndRotationCenter(Vector3.left * 2.4f);
            }
            else if (currentLandIndex == 6)
            {
                MoveHoeAndRotationCenter(Vector3.right * 0.07f);
                MoveHoeAndRotationCenter(Vector3.down * 0.2f);
                MoveHoeAndRotationCenter(Vector3.forward * 3.2f);
                Debug.Log("现在的位置" + transform.position);
            }
            else if (currentLandIndex == 12)
            {
                MoveHoeAndRotationCenter(Vector3.right * 0.07f);
                MoveHoeAndRotationCenter(Vector3.up * 0.3f);
                MoveHoeAndRotationCenter(Vector3.forward * 3.2f);
                Debug.Log("现在的位置1" + transform.position);
            }
        }
        else
        {
            Debug.Log("所有土地已完成开垦!");
        }
    }

    // 移动锄头、手和旋转中心点
    private void MoveHoeAndRotationCenter(Vector3 direction)
    {
        transform.position += direction;
        rotationCenter.position += direction;
    }
}



















