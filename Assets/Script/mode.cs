using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mode : MonoBehaviour
{
    public GameObject Label;
    public GameObject hand;
    public int aim_mode;
    // Start is called before the first frame update
    void Start()
    {
        aim_mode = 0;
        hand.GetComponent<GameManagement>().aim_mode = aim_mode;
    }
    void OnEnable()
    {
        hand.GetComponent<GameManagement>().aim_mode = aim_mode;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ConsoleResult(int value)
    {
        switch (value)
        {
            case 0:
                hand.GetComponent<GameManagement>().aim_mode = 0;
                aim_mode = 0;
                break;
            case 1:
                hand.GetComponent<GameManagement>().aim_mode = 1;
                aim_mode = 1;
                break;

        }
    }
}
