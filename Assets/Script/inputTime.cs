using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputTime : MonoBehaviour     // 倒计时文件
{
    public int value1;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<InputField>().onEndEdit.AddListener(EndValue);//文本输入结束时会调用
    }

    private void EndValue(string value)
    {
        int.TryParse(value, out value1);

    }
}