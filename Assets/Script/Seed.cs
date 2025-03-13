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
    private int plantedSeeds = 0;//用来记录土地的索引
    private int currentSeed = 0; // 用来记录当前放置种子的索引
    //状态变量
    private bool hasGrabbed = false;//是否已抓取种子
    private Vector3 inPosition;  //种子袋初始位置
    private Vector3 targetSidePosition;
    void Start()
    {
        inPosition = transform.position; // 保存初始位置
        transLands = hoeing.lands;
        rotatingUP = hoeing.isRotatingForward;
    }

    // Update is called once per frame 
    void Update()
    {
        rotatingUP = hoeing.isRotatingForward;
        if (hoeing.hoeingCount == 0 && rotatingUP && !hasGrabbed)
        {
            // 首次抓取时移动种子袋到手的位置
            seedMove();
        }
        else if ((hoeing.hoeingCount >= 1 || hasGrabbed) && !rotatingUP)
        {
            // 抓取后移动到旁边
            seedMoveToSide();
        }
        else if (hoeing.hoeingCount == 1 && rotatingUP)
        {
            StartCoroutine(PlaceSeedOnLand());
            plantedSeeds++;
            hasGrabbed = false; // 重置抓取状态
        }
        //}
    }
    //种子的移动
    //1、获取手的位置，移动到手的下面，鼠标点击之后，再移回原来的位置。
    //同时袋子上的种子相应的减少，土地上的种子显现
    private void seedMove()
    {

        Vector3[] worldPosition = new Vector3[cube.Length];
        for (int i = 0; i < cube.Length; i++)
        {
            worldPosition[i] = cube[i].transform.position;
        }

        // 计算手的位置并设置当前位置
        Vector3 handLocation = hand.transform.position - new Vector3(-0.1f, 0.3f, -0.1f);
        transform.position = handLocation;
        if (plantedSeeds > 0)
        {
            // 根据 plantedSeeds  设置 cube 的位置
            for (int i = 0; i < Mathf.Min(plantedSeeds, cube.Length); i++)
            {
                cube[i].transform.position = worldPosition[i];
                //放置第二个种子的时候，第一个种子并没有在第一块土地上，而是在最初的位置
            }
        }
        hasGrabbed = true; // 标记已抓取
    }
    private void seedMoveToSide()
    { //设置一个标志进行打开和关闭
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
            // 根据 plantedSeeds  设置 cube 的位置
            for (int i = 0; i < Mathf.Min(plantedSeeds, cube.Length); i++)
            {
                cube[i].transform.position = worldPosition[i];
            }
        }
    }
    private IEnumerator PlaceSeedOnLand()
    {
        // 确保索引在有效范围内
        if (currentSeed >= transLands.Length)
        {
            yield break; // 如果已经放置完所有种子，结束协程
        }

        // 找到当前应该放置种子的土地位置
        if (cube[currentSeed] != null)
        {
            // 选择cube数组中的第currentSeed个种子，并将其放置到对应的土地位置
            Vector3 landPosition = transLands[currentSeed].position;
            landPosition = landPosition + new Vector3(-0.02f, 0.05f, 0f);

            // 先将种子移动到手上，再移动到地面
            cube[currentSeed].transform.position = hand.transform.position;
            Vector3 initialPosition = cube[currentSeed].transform.position;
            float fallDuration = 1.0f; // 下落的持续时间，单位秒
            float timeElapsed = 0f;

            // 平滑下落的过程
            while (timeElapsed < fallDuration)
            {
                // 使用 Lerp 方法平滑过渡
                cube[currentSeed].transform.position = Vector3.Lerp(initialPosition, landPosition, timeElapsed / fallDuration);
                timeElapsed += Time.deltaTime;
                yield return null; // 等待下一帧
            }

            // 确保最终位置为目标位置
            cube[currentSeed].transform.position = landPosition;

            // 增加当前索引，为下次调用准备
            currentSeed++;
            hasGrabbed = false;
        }
    }
}
