using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text_score;//�����������Ѿ�������ֵ�ˣ��Ͳ����ٿ���õ�����
    public Text text_task;
    public Text text_time;
    public Text text_level;
    public GameObject TaskImage;
    public GameObject setPanel;
    public GameObject btn_set;
    public GameObject btn_pause;
    public GameObject time;
    public GameObject hand;
    public int scoreTime_int;
    public int index;
    public int grade;
    public double currentScore;

    void Start()
    {
        int a = 1;
        text_level.text = a.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    #region �������
    public void AddTask(int number, int number1)
    {
        //Debug.Log("inter�����number" + number);
        this.text_task.text = number + "/" + number1;
    }
    //���������ȸ���
    public void AddImage(int number, int number1)
    {
        float num = (float)number / number1;
        this.TaskImage.GetComponent<Image>().fillAmount = num;
    }
    public void AddScore()
    {
        Debug.Log("�ӷ�����");
        currentScore += 2;
        UpdateScore(currentScore);
    }
    //���÷���//�����һ���ı��򣬻����һ����Ϣ
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScore(currentScore);
    }
    public void UpdateScore(double score)
    {
        this.text_score.text = score.ToString();
    }
    //���µ���ʱʱ�䣨����û���ϣ���
    public void UpdateTime(int countdown)
    {
        this.text_time.text = countdown.ToString();
    }
    //���µȼ�
    public void AddGrade(float grade)
    {
        int grade_int = (int)grade;
        this.text_level.text = grade_int.ToString();
        Debug.Log("inter�������ĵȼ���" + grade);
    }
    //���µ÷�
    public void AddScore(int score, float scoreTime, int sportTime)
    {
        Debug.Log("�ӷ�����");
        scoreTime_int = (int)scoreTime;
    }
    #endregion
    public void OnClickset()//��ʵ��һ���򵥵ĵ�����ť������ʾ������ť�����¿�ʼ���ص���Ϸ
    {
        setPanel.SetActive(false);
        setPanel.SetActive(true);
        btn_set.SetActive(true);
        btn_pause.SetActive(false);
    }
    //��ͣ��ť�����ý��浯��
    public void OnClickpause()
    {

        //GameObject.Find("GamePanel/btn_return").SetActive(false);
        setPanel.SetActive(false);
        setPanel.SetActive(true);
        btn_set.SetActive(true);
        btn_pause.SetActive(false);
        ResetScore();
        time.SetActive(false);
        hand.GetComponent<Hoeing>().Pause();

    }
    //�������˵���ť
    public void OnClickretutn()//��ʵ��һ���򵥵ĵ�����ť������ʾ������ť�����¿�ʼ���ص���Ϸ
    {
        setPanel.SetActive(false);
        //carl.GetComponent<AutoAim>().Pause();
        time.SetActive(false);
        //slider.GetComponent<TimeControl>().time = 0;
        ResetScore();
    }

}
