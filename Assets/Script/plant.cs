using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    public GameObject plantPrefab;   // ʹ��һ��ֲ��Ԥ��
    private GameObject currentPlant; // ��ǰֲ��ʵ��
    private GameObject[] plants;     // �洢ÿ�����ص�ֲ��ʵ��
    public GameObject[] seeds;
    public Transform[] transLands;   // �ؿ�λ��
    public ParticleSystem waterParticles;
    public Hoeing hoe;
    public Seed seed;

    private int currentGrowthStage = 0;  // ��ǰ�����׶Σ�0:����, 1:ֲ�
    private int currentPlot = 0;   // ��ǰ�ؿ�����
    private int waterCount = 0;    // ��ǰ��ˮ����

    void Start()
    {
        // ��ʼ�����Ӻ͵ؿ�
        seeds = seed.cube;
        transLands = hoe.lands;
        // ��ʼ��ֲ������
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

                // ÿ�ν�ˮ�󣬸ı�ֲ���С
                if (waterCount <= 3)  // ������ཽˮ3��
                {
                    ChangePlantSize();
                }
                else
                {
                     // ���ý�ˮ����
                     // �ƶ�����һ���ؿ�
                }
            }
        }
    }

    // �ı�ֲ���С
    private void ChangePlantSize()
    {
        
        if (plants[currentPlot] == null)
        {
            // ���û��ֲ��ʵ���������ǵ�һ�ν�ˮ���򴴽�ֲ�ﲢ��ʧ����
            if (waterCount == 1)
            {
                Destroy(seeds[currentPlot]);  // ��������
            }
            InstantiatePlant();
        }

        // ��ȡ��ǰ�ؿ��ֲ��ʵ��
        GameObject currentPlant = plants[currentPlot];

        // ���ݽ�ˮ��������ֲ��Ĵ�С
        //Vector3 targetScale = currentPlant.transform.localScale;

        Vector3 targetScale = currentPlant.transform.localScale;

        if (waterCount == 1)  // ��һ�ν�ˮ
        {
            targetScale = targetScale * 1.3f; // ����ֲ�ﵽԭ����1.3��
        }
        else if (waterCount == 2)  // �ڶ��ν�ˮ
        {
            targetScale = targetScale * 1.5f;  // ������ֲ�ﵽԭ����1.5��
        }
        else if (waterCount == 3)  // �����ν�ˮ
        {
            targetScale = targetScale * 1.2f;  // ����ֲ�ﵽԭ����1.2��
            waterCount = 0;
            currentPlot++;
        }

        // �������Ŷ���
        StartCoroutine(ScalePlant(currentPlant, targetScale));
    }

    // ����ֲ��ʵ��
    private void InstantiatePlant()
    {
        Vector3 plantPosition = transLands[currentPlot].position;

        // ʵ����ֲ���������λ��
        GameObject newPlant = Instantiate(plantPrefab, plantPosition, Quaternion.identity);
        newPlant.SetActive(true);  // ȷ��ֲ��ɼ�

        // ��ʵ������ֲ��洢��ֲ��������
        plants[currentPlot] = newPlant;

        // ���ٵ�ǰ�ؿ��ϵ�����
       // Destroy(seeds[currentPlot]);
    }

    // ����ֲ��
    private IEnumerator ScalePlant(GameObject plant, Vector3 targetScale)
    {
        Vector3 originalScale = plant.transform.localScale;
        //originalScale = originalScale * 0.1f;
        float duration = 1f;  // ��������ʱ��
        float elapsedTime = 0f;

        // �𲽹��ɵ�Ŀ���С
        while (elapsedTime < duration)
        {
            plant.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ȷ�����յĴ�С
        plant.transform.localScale = targetScale;
    }
}
