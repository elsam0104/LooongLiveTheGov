using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject optionSet = null;
    [SerializeField]
    private GameObject nameInputSet = null;
    [SerializeField]
    private GameObject recommandSet = null;
    [SerializeField]
    private GameObject indexSet = null;
    [SerializeField]
    private Text nameChangeButtonText = null;
    [SerializeField]
    private Text inputText = null;

    private User user = null;
    private SoundManager soundManager = null;
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        SetValues(user);
    }
    private void SetValues(User user)
    {
        this.user = GameManager.Instance.CurrentUser;
        SetUserName();
    }
    public void SetUserName()
    {
        soundManager.startSfx();
        nameChangeButtonText.text = user.UserName;
    }
    public void GoOptionSet()
    {
        soundManager.startSfx();
        Time.timeScale = 0;
        optionSet.SetActive(true);
        nameInputSet.SetActive(false);
        recommandSet.SetActive(false);
        indexSet.SetActive(false);
    }
    public void OnClickReturnToGame()
    {
        soundManager.startSfx();
        Time.timeScale = 1;
        optionSet.SetActive(false);
    }
    public void OnClickExitButton()
    {
        soundManager.startSfx();
        recommandSet.SetActive(true);
    }
    public void OnClickChangeName()
    {
        soundManager.startSfx();
        nameInputSet.SetActive(true);
    }
    public void OnClickIndex()
    {   soundManager.startSfx();
        indexSet.SetActive(true);
    }
    public void OnClickReallyQuit()
    {
        soundManager.startSfx();
        Time.timeScale = 1;
        Application.Quit();
    }

    public void OnAcceptChangeName()
    {
        soundManager.startSfx();
        GameManager.Instance.CurrentUser.UserName = inputText.text;
        GameManager.Instance.SaveToJson();
        SetUserName();
        GoOptionSet();
    }
}
