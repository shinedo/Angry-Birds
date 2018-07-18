using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {

    public int startsNum = 0;
    private bool isSelect = false;
    public GameObject locks;
    public GameObject stars;

    public GameObject panel;
    public GameObject map;
    public Text starsText;

    public int startNum = 1;
    public int endNum = 3;

    private void Start()
    {
        //删除所有本地数据
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetInt("totalNum", 0) >= startsNum)
        {
            isSelect = true;
        }

        if (isSelect)
        {
            locks.SetActive(false);
            stars.SetActive(true);

            //text星星总数显示
            int count = 0;
            for(int i = startNum; i < endNum; i++)
            {
                count += PlayerPrefs.GetInt("level" + i.ToString(), 0);
            }
            starsText.text = count.ToString()+"/30";
        }
    }

    /// <summary>
    /// 鼠标点击
    /// </summary>
    public void Selected()
    {
        if (isSelect)
        {
            panel.SetActive(true);
            map.SetActive(false);   
        }
    }

    public void panelSelect()
    {
        panel.SetActive(false);
        map.SetActive(true);
    }
}
