using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
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

    private void Awake()
    {
        
        SAVE_PATH = Application.dataPath + "/Save";
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
    }
    private void Start()
    {
        LoadFromJson();
        InvokeRepeating("SaveToJson", 1f, 60f);
        InvokeRepeating("EarnMetersPerSec", 0f, 1f);
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
            Debug.Log("[�ε� �׸�]\n" + json);
        }
    }
    public void SaveToJson()
    {
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
        Debug.Log("[���̺� �׸�]\n" + json);
    }
    private void OnApplicationQuit()
    {
        SaveToJson();
    }
}