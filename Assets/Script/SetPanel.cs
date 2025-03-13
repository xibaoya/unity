using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Animation ani;
    public Animator anim;
    public GameObject btn_return;
    public GameObject btn_pause;
    public GameObject btn_set;
    public GameObject time;
    public GameObject hand;
    public Hoeing hoeing;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        ani = this.GetComponent<Animation>();
        btn_return = GameObject.Find("UICanvas/GamePanel/btn_return");
        btn_pause = GameObject.Find("UICanvas/GamePanel/btn_pause");
    }

    // Update is called once per frame
    public void OnClickButton_start()
    {
        time.SetActive(true);
        ani.Play("downn");
        btn_return.SetActive(false);
        btn_pause.SetActive(true);
        hoeing.flag_up_initialize = true;
    }
    public void OnClickX()
    {
        ani.Play("down");
        btn_pause.SetActive(false);
        btn_return.SetActive(true);
        btn_set.SetActive(true);
    }
}
