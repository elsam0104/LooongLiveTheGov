using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject endingSet = null;
    [SerializeField]
    private GameObject continueButton = null;
    [SerializeField]
    private Image poison = null;
    [SerializeField]
    private GameObject endingStartButton = null;
    private SoundManager soundManager;

    private bool isEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.CurrentUser.Meters >= 20000000000000)
        {
            if (isEnd)
                return;
            endingStartButton.SetActive(true);
            soundManager.Stop();
            Poison();
            isEnd = true;
        }
    }
    public void OnClickStartEnding()
    {
        endingSet.SetActive(true);

    }
    public void OnClickContinueButton()
    {
        endingSet.SetActive(false);
        soundManager.ReplayThema();
    }
    private void Poison()
    {
        poison.DOFade(1f, 20f).OnComplete( () => ShowContinueButton());
    }
    private void ShowContinueButton()
    {
        endingStartButton.SetActive(false);
        continueButton.SetActive(true);
        
    }
    /*RectTransform rectTransform = GetComponent<RectTransform>();
        float targetPositionY = rectTransform.anchoredPosition.y + 100f;

        energyText.DOFade(0f, 0.5f).OnComplete(() => Despawn());
        rectTransform.DOAnchorPosY(targetPositionY, 0.5f);
    }
    private void Despawn()
    {
        energyText.DOFade(1f, 0f);
        transform.SetParent(GameManager.Instance.Pool);
        gameObject.SetActive(false);
    }
}*/
}
