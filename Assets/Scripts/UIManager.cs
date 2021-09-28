using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private long clickPerMeters = 1;
    public long showClickPerMeters
    {
        get
        {
            return clickPerMeters;
        }
    }
    private int QTupgradeNumber = 1;
    private int CUupgradeNumber = 1;
    [SerializeField]
    private Text metersText = null;
    [SerializeField]
    private EarnText earnTextTemplate = null;
    [SerializeField]
    private Animator ShipAnimator = null;

    [SerializeField]
    private Text CUbuttonText = null;
    [SerializeField]
    private Text QTbuttonText = null;
    [SerializeField]
    private Button QTbutton = null;
    [SerializeField]
    private Image QTbuttonGameSR = null;
    [SerializeField]
    private Text CUpriceText = null;
    [SerializeField]
    private Text QTpriceText = null;
    [SerializeField]
    private GameObject upgradePanelTemplate = null;
    private List<UpgradePanel> upgradePanelList = new List<UpgradePanel>();
    private List<IndexPannel> indexPannelList = new List<IndexPannel>();
    private SoundManager soundManager = null;
    private void Start()
    {
        soundManager = FindObjectOfType < SoundManager >();
        UpdateEnergyPanel();
        CreatePanel();
        CreateIndex();
    }
    private void CreatePanel()
    {
        GameObject newPanel = null;
        UpgradePanel newPanelComponent = null;

        foreach (Ship ship in GameManager.Instance.CurrentUser.shipList)
        {
            newPanel = Instantiate(upgradePanelTemplate, upgradePanelTemplate.transform.parent);
            newPanelComponent = newPanel.GetComponent<UpgradePanel>();
            newPanelComponent.SetValues(ship);
            newPanel.SetActive(true);
            upgradePanelList.Add(newPanelComponent);
        }
    }

    private void CreateIndex()
    {
        GameObject newIndex = null;
        IndexPannel newIndexComponent = null;

        foreach(Index index in GameManager.Instance.CurrentUser.indexList)
        {
            newIndex = Instantiate(upgradePanelTemplate, upgradePanelTemplate.transform.parent);
            newIndexComponent = newIndex.GetComponent<IndexPannel>();
            newIndexComponent.IndexSetValues(index);
            newIndex.SetActive(true);
            indexPannelList.Add(newIndexComponent);
        }
    }
    public void ClickUpgrade()
    {
        soundManager.startSfx();
        if(GameManager.Instance.CurrentUser.Meters < GameManager.Instance.CurrentUser.CUprice)
        {
            return;
        }
        GameManager.Instance.CurrentUser.Meters -= GameManager.Instance.CurrentUser.CUprice;
        clickPerMeters += (long)Mathf.Pow(10, CUupgradeNumber);
        CUupgradeNumber++;
        UpdateOtherUpgradePanel();
    }
    public void QuickTimeUpgrade()
    {
        soundManager.startSfx();
        if (GameManager.Instance.CurrentUser.Meters < GameManager.Instance.CurrentUser.QTprice)
        {
            return;
        }
        GameManager.Instance.CurrentUser.Meters -= GameManager.Instance.CurrentUser.QTprice;
        float coolTime= 1;
        
        while(coolTime > 100)
        {
            coolTime = coolTime * Time.deltaTime;
            Time.timeScale = 1.2f;
            QTbutton.interactable = false;
            UpdateOtherUpgradePanel();
        }
        Time.timeScale = 1f;
        //spriteRenderer.material.SetColor("_Color", new Color(0.8f, 0.8f, 0.8f, 1f));
        QTbuttonGameSR.material.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f, 1));
        QTbutton.interactable = true;
    }
    public void OnClickShip()
    {
        soundManager.startSfx();
        Debug.Log("클릭");
        GameManager.Instance.CurrentUser.Meters += clickPerMeters;
        ShipAnimator.Play("spaceShip");
        EarnText newText = null;
        if (GameManager.Instance.Pool.childCount > 0)  //풀메니저에 들어있는 차일드(에너지 텍스트)가 없으면 만들고 있으면 거기서 가져온다.
        {
            newText = GameManager.Instance.Pool.GetChild(0).GetComponent<EarnText>(); // .은 ~의 띄어쓰기는 다른거 .이 많으면 복잡해짐, 최적화에도 별로

        }
        else
        {
            newText = Instantiate(earnTextTemplate, GameManager.Instance.showCanvas.transform).GetComponent<EarnText>();
        }
        newText.Show(Input.mousePosition);
        UpdateEnergyPanel();
    }
    public void UpdateOtherUpgradePanel()
    {
        CUpriceText.text = string.Format("{0} ", GameManager.Instance.CurrentUser.CUprice / GameManager.Instance.ShowUnitOrder) + (string)GameManager.Instance.LengthUnitText(GameManager.Instance.CurrentUser.CUprice);
        QTpriceText.text = string.Format("{0} ", GameManager.Instance.CurrentUser.QTprice / GameManager.Instance.ShowUnitOrder) + (string)GameManager.Instance.LengthUnitText(GameManager.Instance.CurrentUser.QTprice);
        CUbuttonText.text = string.Format("{0}기 채용",1);
        QTbuttonText.text = string.Format("{0}화 시청",1);
    }
    public void UpdateEnergyPanel()
    {
        metersText.text = string.Format("{0} ", GameManager.Instance.CurrentUser.Meters / GameManager.Instance.ShowUnitOrder) + (string)GameManager.Instance.LengthUnitText(GameManager.Instance.CurrentUser.Meters);
    }


}
