using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using UnityEngine.EventSystems;
using System.ComponentModel;
using System.Linq;
using System.Text;

public class TCPIP : MonoBehaviour
{
    public Socket newclient;
    IPEndPoint ie;
    private Thread myThread_Read;
    private bool isend = false;

    public Int16 state;
    public double Pos_elbow; 
    public double Pos_wrist;

    public double Torque_elbow;
    public double Torque_wrist;
    public Int16 warningstate;
    public double theta1_L;


    public double hand_rotion;

    public int IWarning;
    public double resetState;
    public double swimmingOrTeaching;
    public double fx;
    public double fy;
    public double fz;

    public double rangle1;
    public double rangle2;
    public double rangle3;
    public double rvelo1;
    public double rvelo2;
    public double rvelo3;
    public double langle1;
    public double langle2;
    public double langle3;

    public int m1;
    public int m2;
    public int m3;
    public int m4;
    public int m5;
    public int m6;
    public int m7;
    public int m8;

    public string Swarning1;
    public string Swarning2;
    public string Swarning3;
    public string Swarning4;
    public string Swarning5;
    public string Swarning6;
    public string Swarning7;
    public string Swarning8;

    public int[] temp_state = new int[8];
    string[] Waningtime = new string[8];
    public string StringWarn;
    string[] BigV = { "�����ٶ�����", "��������" };
    string[] BigT = { "����Ť������", "����Ť���쳣" };
    string[] BigP = { "����λ������", "����λ���쳣" };
    string[] BigM = { "�����ڲ�״̬����", "�����ڲ�����" };
    string[] SmallV = { "С����ٶ�����", "С�������" };
    string[] SmallT = { "С���Ť������", "С���Ť���쳣" };
    string[] SmallP = { "С���λ������", "С���λ���쳣" };
    string[] SmallM = { "С����ڲ�״̬����", "С����ڲ�����" };

    public double LV;//�������ٶ�
    public double RV;//�������ٶ�
    public double Lw;//����ƫ��
    public double Rw;//����ƫ��
    public double Ll;//����ƫ��
    public double Rl;//����ƫ��

    public double F_firc;//ˮ��������ֵ
    public double smartModel;//����ģʽͨ��Big_Rotion
    public double ChangeModel;//���踨��ͨ��
    public GameObject normal;
    public GameObject warm;
    public DateTime datatime;
    public DateTime datatime1;

    public Int16 Big_Rotion;
    public Int16 Small_Rotion;
    public int tcp_grade;  //�ȼ��Ѷ�
    public int tcp_aim_mode;  //��������
    public int tcp_cishu;   //ѵ������
    public GameObject tcp_carl;
    public Int16 frame_head = 23205;    //֡ͷ
    public Int16 frame_end = 19924;     //֡β
    public Int16 send_update_ct = 0;      //���͸��´���
    public int hand_vel = 0;
    public Int16 scene_mod = 0;
    public Int16 my_grade;  //�ȼ��Ѷ�
    public Int16 my_aim_mode;  //��������
    public Int16 Vel_elbow;


    public byte[] recvData = new byte[11005];//29*8=208���ջ�����
    public int offset = 0;//���ݴ洢ƫ����
    public int recvSt = 0;//���ݽ�����ʼλ��
    public int send_state;
    public int tempElbow;
    public int tempWrist;
    // Start is called before the first frame update
    void Start()
    {
       tcp_carl = GameObject.Find("GleechiRig/GleechiHandRig/Neck/Clavicle_R");
        send_state = 0;
        DateTime datatime;
        DateTime datatime1;
        newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        ie = new IPEndPoint(IPAddress.Parse("10.10.100.254"), 8899);    //���ػ�IP��ַ
        //ie = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);    //���ػ�IP��ַ
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        int temp_ct = 1000000000;
        int my_flag = 0;
        Big_Rotion = 30;
        Small_Rotion = 30;
        switch (send_state)
        {

            case 1:                                                                                 //�豸ά������Write
                Big_Rotion = (Int16)tempElbow;
                Small_Rotion = (Int16)tempWrist;
                WriteMsg((Int16)tcp_aim_mode, (Int16)tcp_grade, 0, (Int16)scene_mod);
                //WriteMsg(tcp_aim_mode, tcp_grade, 0, scene_mod);
                send_state = 0;
                break;
            case 2:                                                                                 //����ѡ��Write
                my_aim_mode = (Int16)this.transform.GetComponent<GameManagement>().aim_mode;
                my_grade = (Int16)tcp_carl.GetComponent<Hoeing>().grade;
                hand_vel = (Int16)tcp_carl.GetComponent<Hoeing>().bigtheta_v;
                WriteMsg(my_aim_mode, 15, 0, scene_mod);//��λ������λ��д����Ϣ
                while (temp_ct != 0) temp_ct--;
                while (temp_ct != 0) temp_ct--;
                while (temp_ct != 0) temp_ct--;
                //WriteMsg(my_aim_mode, 15, 0, scene_mod);//��λ������λ��д����Ϣ
                //WriteMsg(my_aim_mode, 15, 0, scene_mod);//��λ������λ��д����Ϣ

                send_state = 0;
                break;
            case 3:                                                                                 //�ֱ�����
                my_aim_mode = (Int16)this.transform.GetComponent<GameManagement>().aim_mode;
                my_grade = (Int16)tcp_carl.GetComponent<Hoeing>().grade;
                hand_vel = (Int16)tcp_carl.GetComponent<Hoeing>().bigtheta_v;
                if (my_grade == 1)
                {
                    Big_Rotion = 400;
                    Small_Rotion = 400;
                }
                else if (my_grade == 2)
                {
                    Big_Rotion = 800;
                    Small_Rotion = 800;
                }
                else if (my_grade == 3)
                {
                    Big_Rotion = 1200;
                    Small_Rotion = 1100;
                }
                WriteMsg((Int16)my_aim_mode, 15, (Int16)hand_vel, (Int16)scene_mod);//��λ������λ��д����Ϣ
                while (temp_ct != 0) temp_ct--;
                while (temp_ct != 0) temp_ct--;
                while (temp_ct != 0) temp_ct--;
                //WriteMsg( (Int16)my_aim_mode, 15, (Int16)hand_vel, (Int16)scene_mod);//��λ������λ��д����Ϣ

                //WriteMsg(my_aim_mode, 15, hand_vel, scene_mod);//��λ������λ��д����Ϣ
                //if (Vel_elbow >= -10 && Vel_elbow <= 10)
                //    send_state = send_state;
                //else
                //
                send_state = 0;
                break;
            case 4:                                                                                 //�ֱ��½�
                my_aim_mode = (Int16)this.transform.GetComponent<GameManagement>().aim_mode;
                my_grade = (Int16)tcp_carl.GetComponent<Hoeing>().grade;
                hand_vel = (Int16)tcp_carl.GetComponent<Hoeing>().bigtheta_v;
                Big_Rotion = 30;
                Small_Rotion = 30;
                WriteMsg((Int16)my_aim_mode, 15, (Int16)hand_vel, (Int16)scene_mod);//��λ������λ��д����Ϣ
                while (temp_ct != 0) temp_ct--;
                while (temp_ct != 0) temp_ct--;
                while (temp_ct != 0) temp_ct--;
                //WriteMsg((Int16)my_aim_mode, 15, (Int16)hand_vel, (Int16)scene_mod);//��λ������λ��д����Ϣ

                //WriteMsg(my_aim_mode, 15, hand_vel, scene_mod);//��λ������λ��д����Ϣ
                //if (Vel_elbow >= -10 && Vel_elbow <= 10)
                //    send_state = send_state;
                //else

                send_state = 0;
                break;

        }

        warningMessage();


    }

    public void warningMessage()//������Ϣ��ʾ
    {


        StringWarn = Convert.ToString(warningstate + 2048, 2);
        StringWarn = StringWarn.Substring(4, 8);
        for (int i = 0; i < StringWarn.Length; i++)
        {
            temp_state[8 - StringWarn.Length + i] = StringWarn[i] - 48;
            if (temp_state[8 - StringWarn.Length + i] == 1 && Waningtime[8 - StringWarn.Length + i] == "")
                Waningtime[8 - StringWarn.Length + i] = System.DateTime.Now.ToString("T");
            else if (temp_state[8 - StringWarn.Length + i] == 0)
                Waningtime[8 - StringWarn.Length + i] = "";
        }
        for (int i = 0; i < 8; i++)
            Debug.Log(i + "recvoffset\n" + temp_state[i]);
        Swarning1 = SmallM[temp_state[0]] + "\t" + Waningtime[0] + "\n";
        Swarning2 = BigM[temp_state[1]] + "\t" + Waningtime[1] + "\n";
        Swarning3 = SmallP[temp_state[2]] + "\t" + Waningtime[2] + "\n";
        Swarning4 = BigP[temp_state[3]] + "\t" + Waningtime[3] + "\n";
        Swarning5 = SmallT[temp_state[4]] + "\t" + Waningtime[4] + "\n";
        Swarning6 = BigT[temp_state[5]] + "\t" + Waningtime[5] + "\n";
        Swarning7 = SmallV[temp_state[6]] + "\t" + Waningtime[6] + "\n";
        Swarning8 = BigV[temp_state[7]] + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + "\t" + Waningtime[7] + "\n";
    }


    private void Connect()
    {
        // ϵͳ����
        try
        {
            newclient.Connect(ie);
            //MonoBehaviour.print("���ӳɹ�");
        }
        catch (SocketException e)
        {
            //MonoBehaviour.print("����ʧ��" + e);
            return;
        }
        // �������ݶ�ȡ�߳�
        ThreadStart myThreadDelegate_R = new ThreadStart(ReceiveMsg);
        myThread_Read = new Thread(myThreadDelegate_R);
        myThread_Read.Start();
    }

    private void ReceiveMsg()
    {

        Debug.Log("recvoffset\n" + offset++);
        // �˴���8������Ϊһ��ѭ����ϵͳ�����򲻶ϱ����õ����µ�����
        while (!isend)
        {
            if (offset > 10005)
            {
                recvSt = 0;
                offset = 0;
                recvData = new byte[11005];
            }
            if (recvData != null)
            {
                int recvLen = newclient.Receive(recvData, offset, 20, SocketFlags.None);
                offset += recvLen;
            }
            for (int ii = recvSt; (ii < offset - 19) && (ii < recvSt + 18); ii++)
            {
                if (BitConverter.ToInt16(recvData, ii) == 23205 && BitConverter.ToInt16(recvData, ii + 18) == 19924)
                {
                    Debug.Log("recvST\n" + recvSt);
                    tempElbow = BitConverter.ToInt16(recvData, ii + 2);
                    tempWrist = BitConverter.ToInt16(recvData, ii + 4);
                    Vel_elbow = BitConverter.ToInt16(recvData, ii + 6);
                    Debug.Log("Vel_elbow\n" + Vel_elbow);
                    theta1_L = 88.5 + (tempElbow / 1100.0) * 70.0;
                    Pos_elbow = BitConverter.ToInt16(recvData, ii + 2) * 360.0 / 2048.0;
                    Pos_wrist = BitConverter.ToInt16(recvData, ii + 4) * 360.0 / 2048.0;
                    Torque_elbow = BitConverter.ToInt16(recvData, ii + 10) / 10.0;
                    Torque_wrist = BitConverter.ToInt16(recvData, ii + 12) / 10.0;
                    warningstate = BitConverter.ToInt16(recvData, ii + 16);
                    recvSt = ii + 20;
                    break;
                }
            }
        }
    }

    public Int16 GetState()
    {
        return state;
    }

    private void WriteMsg(Int16 _aim_mode, Int16 _grade, Int16 _hand_vel, Int16 _scene_mod)
    {

        Int16 figg = 28;

        byte[] send2cmd = new byte[18];

        byte[] byteframe_head = BitConverter.GetBytes(frame_head);      //֡ͷ0
        byte[] byteBig_rotion = BitConverter.GetBytes(Big_Rotion);    //�����Ƕ�2
        byte[] byteSmall_rotion = BitConverter.GetBytes(Small_Rotion);    //С����Ƕ�6

        byte[] byteAim_mode = BitConverter.GetBytes(_aim_mode);         //ģʽ10
        byte[] byteGrade = BitConverter.GetBytes(_grade);               //�Ѷȵȼ�14
        byte[] byteHandvel = BitConverter.GetBytes(_hand_vel);          //�ٶ�18
        byte[] byteSend_update_ct = BitConverter.GetBytes(send_update_ct); //���·��ʹ���22
        byte[] byteScenemod = BitConverter.GetBytes(_scene_mod);         //����24
        byte[] byteframe_end = BitConverter.GetBytes(frame_end);        //֡β26

        send_update_ct++;
        figg = (Int16)(send_update_ct / (Int16)2);
        Array.Reverse(byteframe_head);
        Array.Reverse(byteframe_end);


        Debug.Log("byteAim_mode      " + tcp_aim_mode);
        Debug.Log("byte_hand_vel      " + _grade);
        //Debug.Log("byteCishu      " + _cishu);

        Array.Copy(byteframe_head, 0, send2cmd, 0, byteframe_head.Length);
        Array.Copy(byteBig_rotion, 0, send2cmd, 2, byteBig_rotion.Length);
        Array.Copy(byteSmall_rotion, 0, send2cmd, 4, byteSmall_rotion.Length);
        Array.Copy(byteAim_mode, 0, send2cmd, 6, byteAim_mode.Length);
        Array.Copy(byteGrade, 0, send2cmd, 8, byteGrade.Length);
        Array.Copy(byteHandvel, 0, send2cmd, 10, byteHandvel.Length);
        Array.Copy(byteSend_update_ct, 0, send2cmd, 12, byteSend_update_ct.Length);
        Array.Copy(byteScenemod, 0, send2cmd, 14, byteScenemod.Length);
        Array.Copy(byteframe_end, 0, send2cmd, 16, byteframe_end.Length);

        try
        {
            newclient.Send(send2cmd, send2cmd.Length, 0);
        }
        catch (Exception e)
        {
            //   MonoBehaviour.print("����" + e.ToString() + "\r\n");
        }
    }



    private void OnDestroy()
    {
        //����ر�ʱ�ر�ͨѶ�߳�
        isend = true;
    }
}
