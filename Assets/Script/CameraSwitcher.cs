using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*����ͷ�л�
public class CameraSwitcher : MonoBehaviour
{
    public Camera cameraMain; // ������ͷ
    public Camera cameraTaskCompleted; // ������ɺ�ʹ�õ�����ͷ

    void Start()
    {
        // ��ʼ����Ϊ������ͷ
        SwitchToMainCamera();
    }

    public void SwitchToMainCamera()
    {
        cameraMain.gameObject.SetActive(true);
        cameraTaskCompleted.gameObject.SetActive(false);
    }

    public void SwitchToTaskCompletedCamera()
    {
        cameraMain.gameObject.SetActive(false);
        cameraTaskCompleted.gameObject.SetActive(true);
    }

    // ��������һ��������ɵĻص�
    public void OnTaskCompleted()
    {
        SwitchToTaskCompletedCamera();
    }
}*/


//�������Ҽ��л�����ͷ
public class CameraSwitcher : MonoBehaviour
{
    public Camera cameraMain; // ������ͷ
    public Camera cameraTaskCompleted; // ������ɺ�ʹ�õ�����ͷ

    void Start()
    {
        // ��ʼ����Ϊ������ͷ
        SwitchToMainCamera();
    }

    void Update()
    {
        // �������Ҽ����
        if (Input.GetMouseButtonDown(1)) // 1�����Ҽ�
        {
            // �л�����ͷ
            if (cameraMain.gameObject.activeInHierarchy)
            {
                SwitchToTaskCompletedCamera();
            }
            else
            {
                SwitchToMainCamera();
            }
        }
    }

    public void SwitchToMainCamera()
    {
        cameraMain.gameObject.SetActive(true);
        cameraTaskCompleted.gameObject.SetActive(false);
        Debug.Log("�л���������ͷ");
    }

    public void SwitchToTaskCompletedCamera()
    {
        cameraMain.gameObject.SetActive(false);
        cameraTaskCompleted.gameObject.SetActive(true);
        Debug.Log("�л��������������ͷ");
    }
}



/*��һ�ֺ�������Ҽ��л�����ͷ
public class CameraSwitcher : MonoBehaviour
{
    public Camera cameraMain; // ������ͷ
    public Camera cameraTaskCompleted; // ������ɺ�ʹ�õ�����ͷ

    private int score = 0; // ��ǰ����
    private bool canSwitchCamera = false; // �Ƿ�����л�����ͷ

    void Start()
    {
        // ��ʼ����Ϊ������ͷ
        SwitchToMainCamera();
    }

    void Update()
    {
        // �������Ҽ����
        if (Input.GetMouseButtonDown(1) && canSwitchCamera) // 1�����Ҽ�
        {
            // �л�����ͷ
            if (cameraMain.gameObject.activeInHierarchy)
            {
                SwitchToTaskCompletedCamera();
            }
            else
            {
                SwitchToMainCamera();
            }
        }
    }

    public void AddScore(int points)
    {
        score += points; // ���ӷ���
        Debug.Log("��ǰ����: " + score);

        // ���÷ִﵽ1��ʱ�������л�����ͷ
        if (score >= 1)
        {
            canSwitchCamera = true;
            Debug.Log("�����л�����ͷ");
        }
    }

    public void SwitchToMainCamera()
    {
        cameraMain.gameObject.SetActive(true);
        cameraTaskCompleted.gameObject.SetActive(false);
        Debug.Log("�л���������ͷ");
    }

    public void SwitchToTaskCompletedCamera()
    {
        cameraMain.gameObject.SetActive(false);
        cameraTaskCompleted.gameObject.SetActive(true);
        Debug.Log("�л��������������ͷ");
    }
}
*/