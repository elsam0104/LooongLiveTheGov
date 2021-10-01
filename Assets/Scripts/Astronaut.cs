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
    [SerializeField]
    private Image buttonImage;
    [SerializeField]
    private  Button astronautButton;
    private float coolTime = 10f;
    private bool coolTimeEnd = false;
    private float original = 0f;
    //public float coolTime = 10.0f;
    //public bool isClicked = false;
    //float leftTime = 10.0f;
    //float speed = 5.0f;

    //private void Update()
    //{
    //    if (isClicked)
    //        if (leftTime > 0)
    //        {
    //            leftTime -= Time.deltaTime * speed;
    //            if (leftTime < 0)
    //            {
    //                leftTime = 0;
    //                if (astronautButton)
    //                   astronautButton.enabled = true;
    //                isClicked = true;
    //            }

    //            float ratio = 1.0f - (leftTime / coolTime);
    //            if (buttonImage)
    //                buttonImage.fillAmount = ratio;
    //        }


    //}
    private void Start()
    {
        astronaut.SetActive(true);
        astronaut.transform.SetParent(GameManager.Instance.showCanvas.transform);
    }
    //private void StartCoolTime()
    //{
    //    leftTime = coolTime;
    //    isClicked = true;
    //    if (astronautButton)
    //       astronautButton.enabled = false; 
    //}

    private void OnMouseEnter()
    {
        soundManager.startSfx();
        InvokeRepeating("CoolTimeCheck", 0f, 1f);
        randomint = Random.Range(0, 51);
        selectEffect();
    }
    private void CoolTimeCheck()
    {
        coolTime -= 1f;
        if(coolTime == 0)
        {
            coolTimeEnd = true;
            return;
        }
    }
    private void selectEffect()
    {
        if(randomint < 15)
        {
            original = GameManager.Instance.ShowShip.mPs;
            if (coolTimeEnd)
            {
                GameManager.Instance.ShowShip.mPs = (long)original;
                return;
            }
            GameManager.Instance.ShowShip.mPs = (long)(GameManager.Instance.ShowShip.mPs * 1.5); 
            setValues("10초 동안 mps 1.5배 증가","안전사업");
            
        }
        else if (randomint < 30)
        {
            int givemeter = (int)(GameManager.Instance.CurrentUser.Meters / 0.15);
            setValues("미터수 " + givemeter + "만큼 획득", "상납");
            return;
        }
        else if (randomint < 20)
        {
            original = GameManager.Instance.CurrentUser.clickPerMeters;
            if (coolTimeEnd)
            {
                GameManager.Instance.CurrentUser.clickPerMeters = (long) original;
                return;
            }
            GameManager.Instance.CurrentUser.clickPerMeters *= 2;
            setValues("클릭 미터 수 10초간 2배 증가", "상부의 감시");
            return;
        }
        else if (randomint < 45)
        {
            original = GameManager.Instance.ShowShip.price;
            if (coolTimeEnd)
            {
                GameManager.Instance.ShowShip.price = (long)original;
                return;
            }
            GameManager.Instance.ShowShip.price = (long)(GameManager.Instance.ShowShip.price / 0.9);
            setValues("10초 동안 생산시설 비용 10% 감소", "무급 로동");
            return;
        }
        else if (randomint < 50)
        {
            original = GameManager.Instance.ShowShip.mPs;
            if (coolTimeEnd)
            {
                GameManager.Instance.ShowShip.mPs = (long)original;
                return;
            }
            GameManager.Instance.ShowShip.mPs = (long) (GameManager.Instance.ShowShip.mPs / 1.5);
            setValues("10초 동안 mps 1.5배 감소", "반동");
            return;
        }
        else if (randomint == 50)
        {
            GameManager.Instance.CurrentUser.Meters *= 2;
            setValues("보유 미터 수 2배", "장부 조작");
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
