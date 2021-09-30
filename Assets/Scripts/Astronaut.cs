using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Astronaut : MonoBehaviour
{
    [SerializeField]
    private GameObject astronaut = null;
    [SerializeField]
    private SoundManager soundManager = null;
    private int randomint = 0;
    [SerializeField]
    private Text title = null;
    [SerializeField]
    private Text info = null;
    private void Start()
    {
        astronaut.SetActive(true);
        astronaut.transform.SetParent(GameManager.Instance.showCanvas.transform);
    }

    private void OnMouseEnter()
    {
        soundManager.startSfx();
        randomint = Random.Range(0, 51);
    }
    private void selectEffect()
    {
        if(randomint < 20)
        {
            setValues("10초 동안 mps 1.5배 증가","안전사업");
            return;
        }
        if (randomint < 30)
        {
            int givemeter = (int)(GameManager.Instance.CurrentUser.Meters / 0.15);
            setValues("미터수 " + givemeter + "만큼 획득", "상납");
            return;
        }
        if (randomint < 20)
        {
            setValues("10초 동안 mps 1.5배 증가", "안전사업");
            return;
        }
        if (randomint < 20)
        {
            setValues("10초 동안 mps 1.5배 증가", "안전사업");
            return;
        }
        if (randomint < 20)
        {
            setValues("10초 동안 mps 1.5배 증가", "안전사업");
            return;
        }
        if (randomint == 50)
        {
            setValues("10초 동안 mps 1.5배 증가", "안전사업");
            return;
        }
    }
    private void setValues(string infoStr, string titleStr)
    {
        info.text = infoStr;
        title.text = titleStr;
    }
    //private bool isPressed = false;
    //[SerializeField]
    //private Rigidbody2D rb = null;
    //[SerializeField]
    //Rigidbody2D hook =null ;
    //[SerializeField]
    //private GameObject astronaut = null;
    //[SerializeField]
    //private Transform pool = null;
    //public Transform showPool
    //{
    //    get
    //    {
    //        return pool;
    //    }
    //}
    //private float releaseTime = 0.15f;

    //private float maxDragDistance = 2f;




    //private void Update()
    //{
    //    //CheckPosition();

    //    if (isPressed)
    //    {
    //        Vector2 mousePose = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        if (Vector3.Distance(mousePose, hook.position) > maxDragDistance)
    //        {
    //            rb.position = hook.position + (mousePose - hook.position).normalized * maxDragDistance;
    //        }
    //        else
    //        {
    //            rb.position = mousePose;
    //        }
    //    }

    //}
    ////private void CheckPosition()
    ////{
    ////    if (astronaut.transform.position.x > GameManager.Instance.maxPosition.x+2f)
    ////    {
    ////        Debug.Log("checkPosition");
    ////        SetPosition();
    ////    }
    ////    if (astronaut.transform.position.x < GameManager.Instance.minPosition.x-2f)
    ////    {
    ////        SetPosition();
    ////        Debug.Log("checkPosition");
    ////    }
    ////    if (astronaut.transform.position.y > GameManager.Instance.maxPosition.y+2f)
    ////    {
    ////        SetPosition();
    ////        Debug.Log("checkPosition");
    ////    }
    ////    if (astronaut.transform.position.y < GameManager.Instance.minPosition.y-2f)
    ////    {
    ////        SetPosition();
    ////        Debug.Log("checkPosition");
    ////    }
    ////}
    //private void OnBecameInvisible()
    //{
    //    SetPosition();
    //}
    //private void SetPosition()
    //{
    //    //Destroy(gameObject);
    //    transform.Translate(new Vector3(0f, 0f, -1f));
    //    transform.SetParent(pool, false);
    //    gameObject.SetActive(false);
    //}
    //private void OnMouseDown()
    //{
    //    isPressed = true;
    //    rb.isKinematic = true;

    //}
    //private void OnMouseUp()
    //{
    //    isPressed = false;
    //    rb.isKinematic = false;
    //    StartCoroutine(Release());
    //}

    //private IEnumerator Release()
    //{
    //    yield return new WaitForSeconds(releaseTime);
    //    GetComponent<SpringJoint2D>().enabled = false;
    //    this.enabled = false;
    //}
}
