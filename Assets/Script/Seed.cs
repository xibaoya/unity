using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    // Start is called before the first frame update
    public Hoeing hoeing;
    public Transform[] transLands;
    public GameObject hand;
    public GameObject[] cube;
    private Vector3 seedBagPosition;
    public bool rotatingUP;
    private int plantedSeeds = 0;//������¼���ص�����
    private int currentSeed = 0; // ������¼��ǰ�������ӵ�����
    //״̬����
    private bool hasGrabbed = false;//�Ƿ���ץȡ����
    private bool flag_moveToSide;
    private bool flag_placeSeed;
    private Vector3 inPosition;  //���Ӵ���ʼλ��
    private Vector3 targetSidePosition;
    void Start()
    {
        inPosition = transform.position; // �����ʼλ��
        transLands = hoeing.lands;
        rotatingUP = hoeing.isRotatingForward;
    }

    // Update is called once per frame 
    void Update()
    {
        rotatingUP = hoeing.isRotatingForward;
        // �׶�1���״�ץȡ���Ӵ���δץȡʱ������
        if (hoeing.hoeingCount == 0 && rotatingUP && !hasGrabbed)
        {
            StartCoroutine(DelayedSeedMove());
            hasGrabbed = true; // �������Ϊ��ץȡ
        }
        // �׶�3����ֲ���ӣ�������ץȡ�����������������
        else if (hasGrabbed && hoeing.hoeingCount == 1 && rotatingUP)
        {
            StartCoroutine(PlaceSeedOnLand());
            plantedSeeds++;
            hasGrabbed = false; // ����״̬
        }
        // �׶�2���ƶ����Ӵ������棨ץȡ������ת����ʱ������
        else if (hasGrabbed && hoeing.hoeingCount >= 1 && !rotatingUP)
        {
            seedMoveToSide();
        }
        //}
    }
    private IEnumerator DelayedSeedMove()
    {
        yield return new WaitForSeconds(0.1f); // �ȴ�1��
        seedMove();
    }
    //���ӵ��ƶ�
    //1����ȡ�ֵ�λ�ã��ƶ����ֵ����棬�����֮�����ƻ�ԭ����λ�á�
    //ͬʱ�����ϵ�������Ӧ�ļ��٣������ϵ���������
    private void seedMove()
    {

        Vector3[] worldPosition = new Vector3[cube.Length];
        for (int i = 0; i < cube.Length; i++)
        {
            worldPosition[i] = cube[i].transform.position;
        }

        // �����ֵ�λ�ò����õ�ǰλ��
        Vector3 handLocation = hand.transform.position - new Vector3(-0.1f, 0.3f, -0.1f);
        transform.position = handLocation;
        if (plantedSeeds > 0)
        {
            // ���� plantedSeeds  ���� cube ��λ��
            for (int i = 0; i < Mathf.Min(plantedSeeds, cube.Length); i++)
            {
                cube[i].transform.position = worldPosition[i];
                //���õڶ������ӵ�ʱ�򣬵�һ�����Ӳ�û���ڵ�һ�������ϣ������������λ��
            }
        }
        hasGrabbed = true; // �����ץȡ
        flag_moveToSide = true;
    }
    private void seedMoveToSide()
    { //����һ����־���д򿪺͹ر�
        if (flag_moveToSide)
        {
            Vector3[] worldPosition = new Vector3[cube.Length];
            for (int i = 0; i < cube.Length; i++)
            {
                worldPosition[i] = cube[i].transform.position;
            }
            Vector3 handLocation = hand.transform.position;
            seedBagPosition = (plantedSeeds > 2) ? handLocation - new Vector3(-1f, 0, 0) : handLocation - new Vector3(1f, 0, 0);
            transform.position = seedBagPosition;
            if (plantedSeeds > 0)
            {
                // ���� plantedSeeds  ���� cube ��λ��
                for (int i = 0; i < Mathf.Min(plantedSeeds, cube.Length); i++)
                {
                    cube[i].transform.position = worldPosition[i];
                }
            }
            flag_moveToSide = false;
            flag_placeSeed = true;
        }
    }
    private IEnumerator PlaceSeedOnLand()
    {
        if (flag_placeSeed)
        {
            // ȷ����������Ч��Χ��
            if (currentSeed >= transLands.Length)
            {
                yield break; // ����Ѿ��������������ӣ�����Э��
            }

            // �ҵ���ǰӦ�÷������ӵ�����λ��
            if (cube[currentSeed] != null)
            {
                // ѡ��cube�����еĵ�currentSeed�����ӣ���������õ���Ӧ������λ��
                Vector3 landPosition = transLands[currentSeed].position;
                landPosition = landPosition + new Vector3(-0.02f, 0.05f, 0f);

                // �Ƚ������ƶ������ϣ����ƶ�������
                cube[currentSeed].transform.position = hand.transform.position;
                Vector3 initialPosition = cube[currentSeed].transform.position;
                float fallDuration = 1.0f; // ����ĳ���ʱ�䣬��λ��
                float timeElapsed = 0f;

                // ƽ������Ĺ���
                while (timeElapsed < fallDuration)
                {
                    // ʹ�� Lerp ����ƽ������
                    cube[currentSeed].transform.position = Vector3.Lerp(initialPosition, landPosition, timeElapsed / fallDuration);
                    timeElapsed += Time.deltaTime;
                    yield return null; // �ȴ���һ֡
                }

                // ȷ������λ��ΪĿ��λ��
                cube[currentSeed].transform.position = landPosition;

                // ���ӵ�ǰ������Ϊ�´ε���׼��
                currentSeed++;
                hasGrabbed = false;
                flag_placeSeed = false;
            }
        }
    }
}
