using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HookScript : MonoBehaviour
{
    public float grabSpeed = 2.5f;
    //public GameObject stick;

    //private Vector3 stickStartPos;
    private Vector3 startPos;

    private int status = 0;         //0stop 1grab 2return
    private Worker grabbedWorker;   //当前抓到的打工人

    public GameObject falcula01;
    public GameObject falcula02;

    public Material HighlightMT;
    private Material DefaultMT;

    // Start is called before the first frame update
    void Start()
    {
        //stickStartPos = stick.transform.position;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (status == 1) 
        {
            //transform.position += transform.right * Time.deltaTime * grabSpeed;
            transform.position -= transform.up * Time.deltaTime * grabSpeed;
            //stick.transform.position = (transform.position + stickStartPos) / 2;
            //stick.transform.localScale = stick.transform.localScale + Vector3.right * Time.deltaTime * grabSpeed * 2.5f;
        }
        if (status == 2)
        {
            //transform.position -= transform.right * Time.deltaTime * grabSpeed;
            transform.position += transform.up * Time.deltaTime * grabSpeed;
            //stick.transform.position = (transform.position + stickStartPos) / 2;
            //stick.transform.localScale = stick.transform.localScale - Vector3.right * Time.deltaTime * grabSpeed * 2.5f;

            if (grabbedWorker != null)
                grabbedWorker.transform.position = transform.position;

            //if (stick.transform.localScale.x < 0.2f)
            if (Vector3.Dot(transform.position, transform.position - startPos) <= 0) 
            {
                status = 0;
                //钩子完全收回
                falcula01.GetComponent<SpriteRenderer>().material = DefaultMT;
                falcula02.GetComponent<SpriteRenderer>().material = DefaultMT;

                WorkersController.Instance.CheckSeats();
                if (GameInstance.Instance.isfull)
                    Destroy(grabbedWorker.gameObject);
                if (grabbedWorker != null)
                {
                    grabbedWorker.StartWork();
                    grabbedWorker = null;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (status != 1)
            return;
        if (collision.tag == "Worker" )
        {
            status = 2;
            grabbedWorker = collision.GetComponent<Worker>();
        }
        else if(collision.tag == "Border")
            status = 2;
    }

    //void OnMouseDown()
    public void BeClicked()
    {
        WorkersController.Instance.CheckSeats();
        if (status == 0)
        {
            if (!GameInstance.Instance.isfull)
            {
                status = 1;
                //钩子伸出
                DefaultMT = falcula01.GetComponent<SpriteRenderer>().material;
                falcula01.GetComponent<SpriteRenderer>().material = HighlightMT;
                falcula02.GetComponent<SpriteRenderer>().material = HighlightMT;
            }
            else
                Debug.Log("你妈炸了，工位满了");
        }
            
    }
}
