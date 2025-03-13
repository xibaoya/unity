using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    public GameObject plantPrefab;   // 使用一个植物预设
    private GameObject currentPlant; // 当前植物实例
    private GameObject[] plants;     // 存储每块土地的植物实例
    public GameObject[] seeds;
    public Transform[] transLands;   // 地块位置
    public ParticleSystem waterParticles;
    public Hoeing hoe;
    public Seed seed;

    private int currentGrowthStage = 0;  // 当前生长阶段（0:种子, 1:植物）
    private int currentPlot = 0;   // 当前地块索引
    private int waterCount = 0;    // 当前浇水次数

    void Start()
    {
        // 初始化种子和地块
        seeds = seed.cube;
        transLands = hoe.lands;
        // 初始化植物数组
        plants = new GameObject[transLands.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (currentPlot < transLands.Length)
            {
                waterCount++;

                // 每次浇水后，改变植物大小
                if (waterCount <= 3)  // 限制最多浇水3次
                {
                    ChangePlantSize();
                }
                else
                {
                     // 重置浇水次数
                     // 移动到下一个地块
                }
            }
        }
    }

    // 改变植物大小
    private void ChangePlantSize()
    {
        
        if (plants[currentPlot] == null)
        {
            // 如果没有植物实例，并且是第一次浇水，则创建植物并消失种子
            if (waterCount == 1)
            {
                Destroy(seeds[currentPlot]);  // 销毁种子
            }
            InstantiatePlant();
        }

        // 获取当前地块的植物实例
        GameObject currentPlant = plants[currentPlot];

        // 根据浇水次数调整植物的大小
        //Vector3 targetScale = currentPlant.transform.localScale;

        Vector3 targetScale = currentPlant.transform.localScale;

        if (waterCount == 1)  // 第一次浇水
        {
            targetScale = targetScale * 1.3f; // 增大植物到原来的1.3倍
        }
        else if (waterCount == 2)  // 第二次浇水
        {
            targetScale = targetScale * 1.5f;  // 再增大植物到原来的1.5倍
        }
        else if (waterCount == 3)  // 第三次浇水
        {
            targetScale = targetScale * 1.2f;  // 增大植物到原来的1.2倍
            waterCount = 0;
            currentPlot++;
        }

        // 启动缩放动画
        StartCoroutine(ScalePlant(currentPlant, targetScale));
    }

    // 创建植物实例
    private void InstantiatePlant()
    {
        Vector3 plantPosition = transLands[currentPlot].position;

        // 实例化植物对象并设置位置
        GameObject newPlant = Instantiate(plantPrefab, plantPosition, Quaternion.identity);
        newPlant.SetActive(true);  // 确保植物可见

        // 将实例化的植物存储到植物数组中
        plants[currentPlot] = newPlant;

        // 销毁当前地块上的种子
       // Destroy(seeds[currentPlot]);
    }

    // 缩放植物
    private IEnumerator ScalePlant(GameObject plant, Vector3 targetScale)
    {
        Vector3 originalScale = plant.transform.localScale;
        //originalScale = originalScale * 0.1f;
        float duration = 1f;  // 动画持续时间
        float elapsedTime = 0f;

        // 逐步过渡到目标大小
        while (elapsedTime < duration)
        {
            plant.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保最终的大小
        plant.transform.localScale = targetScale;
    }
}
