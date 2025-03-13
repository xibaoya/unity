using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text_score;//我在那里面已经给他赋值了，就不用再刻意得到他了
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
    #region 界面更新
    public void AddTask(int number, int number1)
    {
        //Debug.Log("inter里面的number" + number);
        this.text_task.text = number + "/" + number1;
    }
    //进度条进度更新
    public void AddImage(int number, int number1)
    {
        float num = (float)number / number1;
        this.TaskImage.GetComponent<Image>().fillAmount = num;
    }
    public void AddScore()
    {
        Debug.Log("加分了吗");
        currentScore += 2;
        UpdateScore(currentScore);
    }
    //重置分数//获得那一个文本框，获得那一个信息
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScore(currentScore);
    }
    public void UpdateScore(double score)
    {
        this.text_score.text = score.ToString();
    }
    //更新倒计时时间（好像没用上？）
    public void UpdateTime(int countdown)
    {
        this.text_time.text = countdown.ToString();
    }
    //更新等级
    public void AddGrade(float grade)
    {
        int grade_int = (int)grade;
        this.text_level.text = grade_int.ToString();
        Debug.Log("inter传进来的等级是" + grade);
    }
    //更新得分
    public void AddScore(int score, float scoreTime, int sportTime)
    {
        Debug.Log("加分了吗");
        scoreTime_int = (int)scoreTime;
    }
    #endregion
    public void OnClickset()//先实现一个简单的弹出按钮：会显示两个按钮：重新开始、回到游戏
    {
        setPanel.SetActive(false);
        setPanel.SetActive(true);
        btn_set.SetActive(true);
        btn_pause.SetActive(false);
    }
    //暂停按钮，设置界面弹出
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
    //返回主菜单按钮
    public void OnClickretutn()//先实现一个简单的弹出按钮：会显示两个按钮：重新开始、回到游戏
    {
        setPanel.SetActive(false);
        //carl.GetComponent<AutoAim>().Pause();
        time.SetActive(false);
        //slider.GetComponent<TimeControl>().time = 0;
        ResetScore();
    }

}
