using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hand;
    public GameObject Label;
    public Hoeing hoeing;
    public GamePanel gamePanel;
    public int grade;
    void Start()
    {
        grade = 1;
        hoeing.grade = grade;
    }
    void OnEnable()
    {
        Debug.Log("enengrade" + grade);
        hoeing.grade = grade;
       gamePanel.AddGrade(grade);
        if (grade == 1)
        {
            hoeing.jiaodu_grade = -30;
            hoeing.Vertical_angle = 120;
        }
        else if (grade == 2)
        {
            hoeing.jiaodu_grade = -60;
            hoeing.Vertical_angle = 150;
        }
        else if (grade == 3)
        {
            hoeing.jiaodu_grade = -80;
            hoeing.Vertical_angle = 170;
        }
    }
    public void ConsoleResult(int value)
    {
        //这里用 if else if也可，看自己喜欢
        //分别对应：第一项、第二项....以此类推
        switch (value)
        {
            case 0:
                hoeing.grade = 1;
                grade = 1;
                hoeing.jiaodu_grade = -30;
                hoeing.Vertical_angle = 120;
               gamePanel.AddGrade(grade);
                break;
            case 1:

                hoeing.grade = 2;
                grade = 2;
                hoeing.jiaodu_grade = -60;
                hoeing.Vertical_angle = 150;
               gamePanel.AddGrade(grade);
                break;
            case 2:
                hoeing.grade = 3;
                grade = 3;
                hoeing.jiaodu_grade = -80;
                hoeing.Vertical_angle = 170;
               gamePanel.AddGrade(grade);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
