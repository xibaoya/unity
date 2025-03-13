using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdown : MonoBehaviour
{
    public Text text_time;
    public float time;
    public int timeint;
    public int sportTime;
    public float flag;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        flag = 0;
        //从设置面板读出倒计时设置是多少
        time = 0;
        sportTime = GameObject.Find("inputField_trainTime").GetComponent<inputTime>().value1;
        //默认倒计时设置是60
        if (sportTime == 0)
        {
            sportTime = 300;
        }
        this.text_time.text = sportTime.ToString();
    }
    // Update is called once per frame
    //关于倒计时框内时间的更新
    void Update()
    {
        time += Time.deltaTime;
        timeint = (int)(sportTime - time);
        text_time.text = timeint + "";
        if (timeint == 0 && flag == 0)
        {
            //Debug.Log("调用了这里的失败面板");
            //FailPanel.SetActive(false);
            //FailPanel.SetActive(true);
            //count.SetActive(false);

            //carl.GetComponent<AutoAim>().Pause();

            //slider.SetActive(false);
            //Destroy(GameObject.Find("balloon 1(C  lone)"));

        }
    }
    public void updateTime()
    {
        this.text_time.text = sportTime.ToString();
        time = 0;
        //Debug.Log("已调用"+sportTime);
    }
}
