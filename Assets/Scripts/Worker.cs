using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum WorkerClass { 
    Rare = 0,
    Epic = 1,
    Legendary = 2
};

public class Worker : MonoBehaviour
{
    public string       wname;                      // 名称
    public WorkerClass  wclass = WorkerClass.Rare;  // 级别
    public float        HP = 100;                   // 生命值
    public int          productivity = 1;           // 生产力
    public int          ability = 0;                // 特殊能力
    public float        moveSpeed = 1f;
    public Material DissolveMT;                     //目标材质
    private Animator MyAnimator;                     //动画机

    public bool         isWorking = false;          // 是否被雇佣工作
    private bool        isGrabbed = false;          // 是否被抓走
    private bool        live = true;
    private float       currentHP;                  // 当前HP
    private Vector3     moveDir = Vector3.zero;
    private float       workTimer;                  // 当前
    public int          seat;                       // 当前座位

    private Material DefaultMT;                     //当前材质

    private Route       route;                      // 行走路线
    private int         routePoint;                 // 当前路线点
    private Vector3     nextPointPos;               // 下一个路线点坐标
    private Vector3     section1pos;
    private Vector3     section2pos;                //三个工作区块的坐标
    private Vector3     section3pos;

    public Image FillHealthbar; 
    public Image BorderHealthbar;
    public Image HighlightHealthbar;
    public Text MoneyText;

    private GameObject coinPrefab;
    private GameObject ghostPrefab;

    // Start is called before the first frame update
    void Start()
    {
        MyAnimator = this.GetComponent<Animator>();
        int rnum = Random.Range(0, 2);
        if (Mathf.Abs(transform.position.x) < WorkersController.Instance.feasibleRegion.x &&
            Mathf.Abs(transform.position.y) > WorkersController.Instance.feasibleRegion.y)
        {
            if (rnum == 0)
                moveDir = Vector3.left;
            else
                moveDir = Vector3.right;
        }
        else if (Mathf.Abs(transform.position.x) > WorkersController.Instance.feasibleRegion.x &&
            Mathf.Abs(transform.position.y) < WorkersController.Instance.feasibleRegion.y)
        {
            if (rnum == 0)
                moveDir = Vector3.up;
            else
                moveDir = Vector3.down;
        }
        
        else if (transform.position.x > 0 && transform.position.y > 0)
        {
            if (rnum == 0)
                moveDir = Vector3.down;
            else
                moveDir = Vector3.left;
        }
        else if (transform.position.x > 0 && transform.position.y < 0)
        {
            if (rnum == 0)
                moveDir = Vector3.up;
            else
                moveDir = Vector3.left;
        }
        else if (transform.position.x < 0 && transform.position.y > 0)
        {
            if (rnum == 0)
                moveDir = Vector3.down;
            else
                moveDir = Vector3.right;
        }
        else
        {
            if (rnum == 0)
                moveDir = Vector3.up;
            else
                moveDir = Vector3.right;
        }

        currentHP = HP;

        FillHealthbar.fillAmount = 0.0f;
        BorderHealthbar.fillAmount = 0.0f;
        HighlightHealthbar.fillAmount = 0.0f;

        section1pos = new Vector3(-3.4f, 0.9f, -2f);
        section2pos = new Vector3(-1.0f, -2.2f, -2f);
        section3pos = new Vector3(2.7f, 0.7f, -2f);

        coinPrefab = Resources.Load("Prefabs/Coin") as GameObject;
        ghostPrefab = Resources.Load("Prefabs/ghost") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (this != null)
        {
            if (!isWorking && !isGrabbed)
                UpdateMovement();
        }
    }

    void UpdateMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPointPos, Time.deltaTime * moveSpeed);
    }

    public void Move()
    {
        //transform.position += Time.deltaTime * moveDir * moveSpeed;

        routePoint++;

        if (routePoint < route.points.Length)
        {
            //transform.position = route.points[routePoint].position;
            nextPointPos = route.points[routePoint].position;
        }
        
    }

    public void CheckRemovement()
    {
        if (routePoint >= route.points.Length - 1)
        {
            WorkersController.Instance.FreeWorkers.Remove(this);
            if (this != null)
                Destroy(this.gameObject);
        }
    }

    void BeGrabbed()
    {
        isGrabbed = true;
    }

    public void StartWork()
    {
        isGrabbed = false;
        isWorking = true;

        seat = WorkersController.Instance.FindSeat();
        if (seat != -1)
        {
            int section = seat / 6;
            int order = seat % 6;
            if (section == 0) {
                int x = order / 3, y = order % 3;
                transform.position = section1pos + new Vector3(1.1f * x, -1.2f * y, 0f);
            }
            else if (section == 1) {
                int x = order / 2, y = order % 2;
                transform.position = section2pos + new Vector3(1.0f * x, -1.2f * y, -2f);
            }
            if(section == 2)
            {
                int x = order / 3, y = order % 3;
                transform.position = section3pos + new Vector3(1.1f * x, -1.2f * y, -2f);
            }
            WorkersController.Instance.WorkingWorkers.Add(this);
            WorkersController.Instance.FreeWorkers.Remove(this);
        }
        else
            Debug.Log("没工位了，你妈炸了");

        FillHealthbar.fillAmount = 1.0f;
        BorderHealthbar.fillAmount = 1.0f;
        HighlightHealthbar.fillAmount = 1.0f;
    }

    public void Work(bool click, float damage)
        //bool是判定是否是拍一拍，金钱和damage挂钩
    {
        int addingmoney = Mathf.RoundToInt
                ((productivity * (1 - (WorkersController.Instance.WorkStrength * 0.2f))) * damage);
        if(currentHP < damage){
            addingmoney = Mathf.RoundToInt
                ((productivity * (1 - (WorkersController.Instance.WorkStrength * 0.2f))) * currentHP);
            SoundEffectManager.playSound(1);
        }

        if (!click)
        {
            currentHP -= damage;
            checkclickdead(false);
            GameInstance.Instance.Money += addingmoney;
        }
        else
        {
            addingmoney = Mathf.RoundToInt(0.9f * addingmoney);
            currentHP -= damage;
            checkclickdead(true);
            GameInstance.Instance.Money += addingmoney;
        }//拍一拍只有0.9的收入

        FillHealthbar.fillAmount = (float)(currentHP / HP); 
        HighlightHealthbar.fillAmount = (float)(currentHP / HP);

        GameObject coin = Instantiate(coinPrefab, transform.position + new Vector3(0.08f, 0.5f,0), new Quaternion(),transform);
        MoneyText.text = "+" + addingmoney;

        StartCoroutine(DestroyCoin(coin));
        SoundEffectManager.playSound(4);

        Debug.Log("+" + addingmoney + "=" + GameInstance.Instance.Money);
    }

    void BeClicked()
    {
        Work(true, WorkersController.Instance.FinalDamage);
        //老板拍了拍你
    }

    void checkclickdead(bool click)
    {

        if (currentHP <= 0)
        {
            live = false;
            #region
            GameObject ghost = Instantiate(ghostPrefab, transform.position + new Vector3(0.08f, 0.5f, 0), new Quaternion(), transform);
            //DefaultMT = this.GetComponent<SpriteRenderer>().material;
            this.GetComponent<SpriteRenderer>().material = DissolveMT;
            MyAnimator.SetBool("IfDissolve", true);
            StartCoroutine(DestroyBhost(ghost));
            #endregion
            GameInstance.Instance.DeadEmployee++;
            if (click)
            {
                GameInstance.Instance.DeadEmployeebyPYP++;
                GameInstance.Instance.Money -= (5 * productivity);
            }
            WorkersController.Instance.WorkingWorkers.Remove(this);
            WorkersController.Instance.seats.SetValue(false, seat);
            Destroy(this.gameObject, 1f);

        }
    }

    void OnMouseDown()
    {
        if(isWorking & live)
            BeClicked();
    }

    public void SetRoute(Route i_route)
    {
        route = i_route;
        routePoint = 0;
        nextPointPos = i_route.points[0].position;
    }

    IEnumerator DestroyCoin(GameObject coin)
    {
        yield return new WaitForSeconds(0.2f);
        MoneyText.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.8f);
        Destroy(coin);
        MoneyText.gameObject.SetActive(false);
    }

    IEnumerator DestroyBhost(GameObject ghost)
    {
        yield return new WaitForSeconds(1f);
        Destroy(ghost);
    }
}
