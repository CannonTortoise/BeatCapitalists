using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HookScript : MonoBehaviour
{
    public float grabSpeed = 2.5f;
    public GameObject stick;
    
    private Vector3 stickStartPos;

    private int status = 0; //0stop 1grab 2return

    // Start is called before the first frame update
    void Start()
    {
        stickStartPos = stick.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(status == 1)
        {
            transform.position += transform.right * Time.deltaTime * grabSpeed;
            stick.transform.position = (transform.position + stickStartPos) / 2;
            stick.transform.localScale = stick.transform.localScale + Vector3.right * Time.deltaTime * grabSpeed * 2.5f;
        }
        if (status == 2)
        {
            transform.position -= transform.right * Time.deltaTime * grabSpeed;
            stick.transform.position = (transform.position + stickStartPos) / 2;
            stick.transform.localScale = stick.transform.localScale - Vector3.right * Time.deltaTime * grabSpeed * 2.5f;
            if (stick.transform.localScale.x < 0.2f)
            {
                status = 0;
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.tag == "Worker" || collision.tag == "Border")
        {
            status = 2;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnMouseDown()
    {
        status = 1;
    }
}
