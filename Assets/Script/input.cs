using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class input : MonoBehaviour
{
    // Start is called before the first frame update
    public int value1;
    private void EndValue(string value)
    {
        int.TryParse(value, out value1);
    }

    // Use this for initialization
    void Start()
    {
       
        transform.GetComponent<InputField>().onEndEdit.AddListener(EndValue);//文本输入结束时会调用
    }
}
