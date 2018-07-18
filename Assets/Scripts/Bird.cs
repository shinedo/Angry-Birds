using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour {

    private bool isClick = false;
    public float maxDis = 1.5f;
    [HideInInspector]
    public SpringJoint2D sp;
    protected Rigidbody2D rg;

    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;

    public GameObject boom;

    protected TestMyTrail myTrail;

    [HideInInspector]
    public bool canMove = false;
    public float smooth = 3;

    public AudioClip select;
    public AudioClip fly;

    private bool isFly = false;

    public bool isRealased = false;

    public Sprite hurt;
    protected SpriteRenderer render;

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()//鼠标按下
    {
        if (canMove)
        {
            AudioPlay(select);

            isClick = true;
            rg.isKinematic = true;
        }

    }

    private void OnMouseUp()
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);

            //关闭画线
            right.enabled = false;
            left.enabled = false;

            canMove = false;
        }
    }

    private void Update()
    {
        //判断是否点击了UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //鼠标一直按下，进行位置跟随
        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //两种方法解决不在同一Z轴
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);
            //进行位置限定
            if (Vector3.Distance(transform.position,rightPos.position)>maxDis)
            {
                //单位化向量
                Vector3 pos = (transform.position - rightPos.position).normalized;
                pos *= maxDis;//最大长度的向量
                transform.position = pos + rightPos.position;
            }

            Line();
        }

        //相机跟随
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posX, 0, 15), Camera.main.transform.position.y, Camera.main.transform.position.z), smooth * Time.deltaTime);

        if (isFly)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    void Fly()
    {
        isRealased = true;
        isFly = true;
        //音效
        AudioPlay(fly);

        //开始拖尾
        myTrail.StartTrail();

        sp.enabled = false;
        Invoke("Next", 5f);
    }

    //画线
    void Line()
    {
        //启动画线
        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    //下一只小鸟的飞出
    protected virtual void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
        myTrail.ClearTrail();
    }

    //音乐的播放
    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    //技能
    public virtual void ShowSkill()
    {
        isFly = false;
    }

    public void Hurt()
    {
        render.sprite = hurt;
    }
}
