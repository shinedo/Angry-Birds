using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour {

    private Animator anim;
    public GameObject btn;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Replay()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void Pause()
    {
        //播放pause动画
        anim.SetBool("isPause", true);
        btn.SetActive(false);

        //使暂停时无法对未飞出的小鸟做出操作
        if (GameManager._instance.birds.Count > 0)
        {
            //没有飞出
            if (GameManager._instance.birds[0].isRealased = false)
            {
                GameManager._instance.birds[0].canMove = false;
            }
        }
    }

    public void Home()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        //播放resume动画
        Time.timeScale = 1;
        anim.SetBool("isPause", false);

        //使不暂停时恢复对未飞出的小鸟做出操作
        if (GameManager._instance.birds.Count > 0)
        {
            //没有飞出
            if (GameManager._instance.birds[0].isRealased = false)
            {
                GameManager._instance.birds[0].canMove = true;
            }
        }
    }

    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    //动画播放完
    public void ResumeAnimEnd()
    {
        btn.SetActive(true);
    }
	
}
