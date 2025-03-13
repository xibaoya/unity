using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public Hoeing hoeing;
    public Transform[] trans_lands;
    public GameObject hoe;
    public GameObject seed;
    public GameObject hand;
    public GameObject cube;
    public GameObject spinklingCan;
    public GameObject plant;
    public GameObject inputTraiTimes;

    public int hoeingCount;
    public int currentLandIndex;
    public Vector3 initialPosition;//记录手的初始位置
    public int grade;
    public int aim_mode;
    public int trainingTimes;// 训练次数
    public int iniTimes;//初始训练次数

    void Start()
    {
        trans_lands = hoeing.lands;
        initialPosition = hoe.transform.position;
        trainingTimes = 0;
        iniTimes = inputTraiTimes.GetComponent<input>().value1;
    }
    public void Initializing()
    {
        iniTimes = inputTraiTimes.GetComponent<input>().value1;
        if (iniTimes == 0)
        {
            iniTimes = 18;
        }
    }
    // Update is called once per frame
    void Update()
    {
        hoeingCount = hoeing.hoeingCount;
        currentLandIndex = hoeing.currentLandIndex;
        if (currentLandIndex > 5 && currentLandIndex <= 11 )
           // if (currentLandIndex > 5 && currentLandIndex <=11 && hoeingCount == 0) //trans_lands.Length - 1  5
        {
            hoe.SetActive(false);
            seed.SetActive(true);
        }
        else if (currentLandIndex > 11)
        // else if (currentLandIndex > 11 && hoeingCount == 0)
        {
            cube.SetActive(false);
            spinklingCan.SetActive(true);
            plant.SetActive(true);
        }
    }
}
