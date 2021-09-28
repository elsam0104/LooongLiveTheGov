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
    private void Start()
    {
        SetValues(user);
    }
    private void SetValues(User user)
    {
        this.user = GameManager.Instance.CurrentUser;
        SetUserName();
    }
    public void SetUserName()
    {
        nameChangeButtonText.text = user.UserName;
    }
    public void GoOptionSet()
    {
        Time.timeScale = 0;
        optionSet.SetActive(true);
        nameInputSet.SetActive(false);
        recommandSet.SetActive(false);
        indexSet.SetActive(false);
    }
    public void OnClickReturnToGame()
    {
        Time.timeScale = 1;
        optionSet.SetActive(false);
    }
    public void OnClickExitButton()
    {
        recommandSet.SetActive(true);
    }
    public void OnClickChangeName()
    {
        nameInputSet.SetActive(true);
    }
    public void OnClickIndex()
    {
        indexSet.SetActive(true);
    }
    public void OnClickReallyQuit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void OnAcceptChangeName()
    {
        GameManager.Instance.CurrentUser.UserName = inputText.text;
        GameManager.Instance.SaveToJson();
        SetUserName();
        GoOptionSet();
    }
}
