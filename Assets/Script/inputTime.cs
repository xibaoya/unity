using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputTime : MonoBehaviour     // ����ʱ�ļ�
{
    public int value1;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<InputField>().onEndEdit.AddListener(EndValue);//�ı��������ʱ�����
    }

    private void EndValue(string value)
    {
        int.TryParse(value, out value1);

    }
}