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
        //����������������ʱ�����Ƕ���
        time = 0;
        sportTime = GameObject.Find("inputField_trainTime").GetComponent<inputTime>().value1;
        //Ĭ�ϵ���ʱ������60
        if (sportTime == 0)
        {
            sportTime = 300;
        }
        this.text_time.text = sportTime.ToString();
    }
    // Update is called once per frame
    //���ڵ���ʱ����ʱ��ĸ���
    void Update()
    {
        time += Time.deltaTime;
        timeint = (int)(sportTime - time);
        text_time.text = timeint + "";
        if (timeint == 0 && flag == 0)
        {
            //Debug.Log("�����������ʧ�����");
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
        //Debug.Log("�ѵ���"+sportTime);
    }
}
