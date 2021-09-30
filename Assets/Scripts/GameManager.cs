using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private string[] unitArr;
    public string[] ShowUnit
    {
        get
        {
            return unitArr;
        }
    }
    private int unitOrder = 1;
    public int ShowUnitOrder
    {
        get
        {
            return unitOrder;
        }
    }
    [SerializeField]
    private User user = null;

    public User CurrentUser
    {
        get
        {
            return user;
        }
    }

    private Index index = null;
    public Index ShowIndex
    {
        get
        {
            return index;
        }
    }
    private Ship ship = null;
    public Ship ShowShip
    {
        get
        {
            return ship;
        }
    }
    [SerializeField]
    private Canvas canvas = null;

    public Canvas showCanvas { get { return canvas; } }
    private UIManager uiManager;
    public UIManager UI
    {
        get
        {
            if (uiManager == null)
            {
                uiManager = GetComponent<UIManager>();
            }
            return uiManager;
        }
    }
    [SerializeField]
    private Transform poolManager;
    public Transform Pool { get { return poolManager; } }


 
    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.txt";
    public Vector2 maxPosition { get; private set; }
    public Vector2 minPosition { get; private set; }
    [SerializeField]
    private GameObject astronautGameObject = null;
    private Astronaut astronaut;
    [SerializeField]
    private GameObject astronautPosition = null;
    public GameObject showAstronautPosition
    {
        get
        {
            return astronautPosition;
        }
    }
    private void Awake()
    {
        LengthUnitText(CurrentUser.Meters);
        SAVE_PATH = Application.persistentDataPath + "/Save";
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
    }
    private void Start()
    {
        maxPosition = new Vector3(9f, 3.2f,-1f);
        minPosition = new Vector3(-9f, -3.2f);
        astronaut = FindObjectOfType<Astronaut>();
        LoadFromJson();
        //StartCoroutine(ShowAstronaut());
        InvokeRepeating("SaveToJson", 1f, 60f);
        InvokeRepeating("EarnMetersPerSec", 0f, 1f);
    }

    public string LengthUnitText(long meters)
    {
        string currentUnit = null;
        int order = 0;
        int i = 0;
        currentUnit = unitArr[order];
        for (i = 3; meters / Mathf.Pow(10,i) < 0; i+=3)
        {
            currentUnit = unitArr[order];
            order++;
        }
        if(order == 0)
        {
            unitOrder = 1;
        }
        else
        {
            unitOrder = order;
        }
        //Debug.Log(currentUnit);
        return currentUnit;
    }

    private IEnumerator ShowAstronaut()
    {
        float randomDelay = 0f;
        yield return new WaitForSeconds(0f);

        while (true)
        {
            randomDelay = Random.Range(3f, 5f);
            

            Astronaut newAstronaut = null;
            if (Pool.childCount > 0) 
            {
                newAstronaut = Pool.GetChild(0).GetComponent<Astronaut>(); 
            }
            else
            {
                newAstronaut = Instantiate(astronautGameObject,  canvas.transform).GetComponent<Astronaut>();
            }
            //if (GameManager.Instance.Pool.childCount > 0)  //풀메니저에 들어있는 차일드(에너지 텍스트)가 없으면 만들고 있으면 거기서 가져온다.
            //{
            //    newText = GameManager.Instance.Pool.GetChild(0).GetComponent<EarnText>(); // .은 ~의 띄어쓰기는 다른거 .이 많으면 복잡해짐, 최적화에도 별로

            //}
            //else
            //{
            //    newText = Instantiate(earnTextTemplate, GameManager.Instance.showCanvas.transform).GetComponent<EarnText>();
            //}
            Debug.Log("spawned");
            yield return new WaitForSeconds(randomDelay);
        }
    }
    private void EarnMetersPerSec()
    {
        foreach(Ship ship in user.shipList)
        {
            user.Meters += ship.mPs * ship.amount;
        }
        UI.UpdateEnergyPanel();
    }
    private void LoadFromJson()
    {
        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
            Debug.Log("[로드 항목]\n" + json);
            Debug.Log("로드 경로\n" + SAVE_PATH + SAVE_FILENAME);
        }
    }
    public void SaveToJson()
    {
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
        Debug.Log("[세이브 항목]\n" + json);
        Debug.Log("세이브 경로\n" + SAVE_PATH + SAVE_FILENAME);
    }
    private void OnApplicationQuit()
    {
        SaveToJson();
    }

}
