using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �������ο��ѣ�ÿ�ο��������ض���ԭ��������������������Ч���� ���������
public class Hoeing : MonoBehaviour
{
    public GameObject hand;
    public float Vertical_angle;
    public bool flag_up;
    public bool flag_up_initialize;
    public bool flag_up_end;
    public bool flag_down;
    public bool flag_fishrod;
    public float bigtheta_x;
    public float bigtheta_v;
    public float bigtheta_x_init;
    public float bigtheta_v_init;
    public float bigtheta_x_end;
    public float bigtheta_end;
    public float bigtheta_v_end;
    public float jiaodu_grade;
    public int grade;
    public float temp;
    public GamePanel gamePanel;
    public GameManagement gameManagement;
    private TCPIP tcpipVar;//ͨѶ

    public float rotationSpeed = 25f; // ��ת�ٶȣ���λ�Ƕ�ÿ��
    public Transform rotationCenter; // ��ת���ģ���Ϊ Transform ����
    public Transform[] lands; // ������ص� Transform ����
    public GameObject particleEffectPrefab; // ������ЧԤ�Ƽ�
    public bool isRotatingForward = true; // ��ǵ�ǰ�Ƿ���������ת
    private bool isRotating = false; // ��ǵ�ǰ�Ƿ�����ת��
    private float totalRotation; // ��ǰ����ת�ĽǶ�
    private float targetRotation; // ÿ����תĿ��Ƕȣ�����ͷ���Ϊ 45��
    public int hoeingCount = 0; // �ѽ��еĿ��Ѵ���
    public int currentLandIndex = 0; // ��ǰ���ڿ��ѵ���������
    private Vector3 initialLandScale; // ��¼���س�ʼ��С
    private int particleEffectCount = 0; // ��¼������Ч�����ɴ���
    private int score = 0; // �÷�

    public ParticleSystem waterParticles;//��ˮ������Ч�����Ƴ�������ʧ


    private void Start()
    {
        flag_up_initialize = false;
        flag_up_end = false;
        flag_up = false;
        flag_down = false;
        flag_fishrod = false;
        Application.targetFrameRate = 30;//����֡��

        hand = GameObject.Find("GleechiRig/GleechiHandRig/Neck/Clavicle_R");
        tcpipVar = hand.GetComponent<TCPIP>();
        // ��¼���صĳ�ʼ��С�������������ش�С��ͬ��
        initialLandScale = lands[0].localScale;
        Debug.Log("��һ�����ص�λ��" + lands[1].position);

        if (gameManagement.aim_mode == 0)
        {
            targetRotation = 65f;
            totalRotation = 20f;
        }
        else if (gameManagement.aim_mode == 1)
        {

        }

    }
    public void Pause()
    {
        flag_up_initialize = false;
    }

    public void Initializing()//��������
    {
        gamePanel.AddGrade(grade);
        this.GetComponent<GameManagement>().iniTimes = 0;
        bigtheta_x = 0f;
        gamePanel.ResetScore();
        //����ÿһ���ȼ��ĳ�ʼ����
        if (grade == 1)
        {
            hand.transform.position = new Vector3(495.5f, 7.5f, 307.4f);

            if (gameManagement.aim_mode == 0)
            {
                targetRotation = 65f;
                totalRotation = 20f;
            }
        }
        bigtheta_v = 0.1f;
    }

    private void FixedUpdate()
    {
        if (flag_up_initialize == true)
        {
            if (gameManagement.aim_mode == 0)
            {
                //����ģʽ
                if (isRotatingForward)
                {
                    if (totalRotation < targetRotation) //������ת45��
                    {
                        float rotationThisFrame = rotationSpeed * Time.deltaTime;
                        rotationThisFrame = Mathf.Min(rotationThisFrame, targetRotation - totalRotation); // ȷ�����ᳬ��Ŀ����ת�Ƕ�
                        transform.RotateAround(rotationCenter.position, Vector3.right, rotationThisFrame);
                        totalRotation += rotationThisFrame;
                        waterParticles.Play();
                    }
                    else
                    {
                        // �ﵽ 45 �Ⱥ�ֹͣ���л���������ת
                        isRotatingForward = false;
                        totalRotation = 0f; // ������ת�Ƕ�
                        waterParticles.Stop();

                        // ����ǵ�һ�Ρ��ڶ��λ�����ο��ѣ���������
                        if (hoeingCount < 3)
                        {
                            ExpandLand();
                        }

                        // ÿ��������չʱ����������Ч������չ����ͬ����
                        if (particleEffectCount < hoeingCount && particleEffectPrefab != null)
                        {
                            SpawnParticleEffect();
                            particleEffectCount++; // ����������Ч���ɼ���
                        }
                    }
                }
                else
                {
                    // ������ת��ԭλ��
                    if (totalRotation < targetRotation)
                    {
                        // ��֡��ת��ʹ�� Time.deltaTime ��ƽ������
                        float rotationThisFrame = rotationSpeed * Time.deltaTime;
                        rotationThisFrame = Mathf.Min(rotationThisFrame, targetRotation - totalRotation); // ȷ�����ᳬ��Ŀ����ת�Ƕ�
                        transform.RotateAround(rotationCenter.position, Vector3.right, -rotationThisFrame);
                        totalRotation += rotationThisFrame;
                    }
                    else
                    {
                        // �ﵽԭʼλ�ú�ֹͣ��ת
                        isRotating = false; // ֹͣ��ת
                        isRotatingForward = true; // ����Ϊ������ת
                        totalRotation = 0f; // ������ת�Ƕ�

                        // �����ǰ�����Ѿ���ɿ���3�Σ��ƶ�����һ������
                        if ((hoeingCount == 3 && (currentLandIndex <= 5 || currentLandIndex >= 12)) || (hoeingCount == 2 && currentLandIndex >= 6 && currentLandIndex <= 11))
                        {
                            MoveToNextLand();
                        }
                    }
                }
            }
        }
        else if (gameManagement.aim_mode == 1)
        {

        }
    }
    private void ExpandLand()
    {
        hoeingCount++; // ���ӿ��Ѵ���

        if (hoeingCount <= 3)
        {
            // ÿ�ο��Ѻ���������Ϊԭ��������
            lands[currentLandIndex].localScale = initialLandScale * Mathf.Pow(3.75f, hoeingCount);

            if (hoeingCount == 3)
            {
                // �ﵽ�����ο��Ѻ�ֹͣ��������
                Debug.Log("���� " + (currentLandIndex + 1) + " ��������ο��ѣ����������ٱ仯.");

                // ���ο��Ѻ�����һ��
                AddScore();
            }
        }
    }

    // �������Ϸ�0.1�׵�λ������������Ч
    private void SpawnParticleEffect()
    {
        Vector3 spawnPosition = lands[currentLandIndex].position + Vector3.up * 0.0001f; // �������Ϸ�0.1��
        Instantiate(particleEffectPrefab, spawnPosition, Quaternion.identity); // ʵ��������ϵͳ
    }

    // ���ӷ���
    private void AddScore()
    {
        score++; // ���ӵ÷�
        Debug.Log("�����һ��! ��ǰ�÷�: " + score);
    }

    // �ƶ�����һ������
    public void MoveToNextLand()
    {

        // ���ӵ�ǰ��������
        currentLandIndex++;

        // ������и������ؽ��п���
        if (currentLandIndex < 18)//�������أ�3����Ѳ
        {
            // ���ÿ��Ѵ���
            hoeingCount = 0;
            particleEffectCount = 0;

            // ���ݵ�ǰ�����������в�ͬ��λ��
            if (currentLandIndex == 1 || currentLandIndex == 2 || currentLandIndex == 7 || currentLandIndex == 8)
            {
                // ��X���������ƶ�1��
                MoveHoeAndRotationCenter(Vector3.right * 2.4f);
            }
            else if (currentLandIndex == 13 || currentLandIndex == 14)
            {
                // ��X���������ƶ�1��
                MoveHoeAndRotationCenter(Vector3.right * 2.4f);
            }
            else if (currentLandIndex == 3 || currentLandIndex == 9 || currentLandIndex == 15)
            {
                // ��Z�Ḻ�����ƶ�1��
                MoveHoeAndRotationCenter(Vector3.back * 2.4f);
            }
            else if (currentLandIndex == 4 || currentLandIndex == 5 || currentLandIndex == 10 || currentLandIndex == 11 || currentLandIndex == 16 || currentLandIndex == 17)
            {
                // ��X�Ḻ�����ƶ�1��
                MoveHoeAndRotationCenter(Vector3.left * 2.4f);
            }
            else if (currentLandIndex == 6)
            {
                MoveHoeAndRotationCenter(Vector3.right * 0.07f);
                MoveHoeAndRotationCenter(Vector3.down * 0.2f);
                MoveHoeAndRotationCenter(Vector3.forward * 3.2f);
                Debug.Log("���ڵ�λ��" + transform.position);
            }
            else if (currentLandIndex == 12)
            {
                MoveHoeAndRotationCenter(Vector3.right * 0.07f);
                MoveHoeAndRotationCenter(Vector3.up * 0.3f);
                MoveHoeAndRotationCenter(Vector3.forward * 3.2f);
                Debug.Log("���ڵ�λ��1" + transform.position);
            }
        }
        else
        {
            Debug.Log("������������ɿ���!");
        }
    }

    // �ƶ���ͷ���ֺ���ת���ĵ�
    private void MoveHoeAndRotationCenter(Vector3 direction)
    {
        transform.position += direction;
        rotationCenter.position += direction;
    }
}



















