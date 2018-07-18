using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<Bird> birds;
    public List<Pig> pig;

    public static GameManager _instance;

    public GameObject win;
    public GameObject lose;

    public GameObject[] starts;

    private int starsNum = 0;

    private int totalNum = 10;

    //初始化位置
    private Vector3 origionPos;

    private void Awake()
    {
        _instance = this;
        if (birds.Count > 0)
        {
            origionPos = birds[0].transform.position;
        }
    }

    private void Start()
    {
        Initialized();
    }

    //初始化小鸟
    private void Initialized()
    {
        for (int i = 0; i < birds.Count; i++)
        {
            //第一只小鸟
            if (i == 0)
            {
                birds[i].transform.position = origionPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
                birds[i].canMove = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

    //游戏逻辑
    public void NextBird()
    {
        if (pig.Count > 0)
        {
            if (birds.Count > 0)
            {
                //下一只飞
                Initialized();
            }
            else
            {
                //输了
                lose.SetActive(true);
            }
        }
        else
        {
            //赢了
            win.SetActive(true);
        }
    }

    public void ShowStarts()
    {
        StartCoroutine("show");
        //或StartCoroutine(show());
    }

    IEnumerator show()
    {
        for (; starsNum < birds.Count + 1; starsNum++)
        {
            if (starsNum >= starts.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            starts[starsNum].SetActive(true);
        }
    }

    //重玩
    public void Replay()
    {
        SavaData();
        SceneManager.LoadScene(2);
    }

    //返回主菜单
    public void Home()
    {
        SavaData();
        SceneManager.LoadScene(1);
    }

    public void SavaData()
    {
        if (starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }

        //存储所有星星的个数
        int sum = 0;
        for (int i = 0; i < totalNum; i++)
        {
            sum += PlayerPrefs.GetInt("level"+i.ToString());
        }
        PlayerPrefs.SetInt("totalNum", sum);
    }
}
