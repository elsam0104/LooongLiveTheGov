using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class TitleText : MonoBehaviour
{
    [SerializeField]
    private Text titleText = null;
    [SerializeField]
    private GameObject startButton = null;
    [SerializeField]
    private GameObject startSet = null;
    // Start is called before the first frame update
    void Start()
    {
        TitleTypingText();
    }
    public void TitleTypingText()
    {
        Tweener tweener = titleText.DOText("Loong\nLive the\nGov!", 3f);
        tweener.OnComplete(() => ShowButton());
    }
    private void ShowButton()
    {
        startButton.SetActive(true);
    }
    public void OnClickStartButton()
    {
        startSet.transform.DOLocalMoveY(400f, 2f).OnComplete(() => startSet.SetActive(false));
    }
}
