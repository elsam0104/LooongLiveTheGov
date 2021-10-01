using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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
    private Image QTbuttonGameImage = null;
    [SerializeField]
    private GameObject QTshadow = null;
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
        //CreateIndex();
        UpdateOtherUpgradePanel();

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
        
        if(GameManager.Instance.CurrentUser.Meters < GameManager.Instance.CurrentUser.CUprice)
        {
            soundManager.startDisallowance();
            return;
        }
        soundManager.startSfx();
        GameManager.Instance.CurrentUser.Meters -= GameManager.Instance.CurrentUser.CUprice;
        GameManager.Instance.CurrentUser.clickPerMeters += (long)Mathf.Pow(10, GameManager.Instance.CurrentUser.CUupgradeNumber);
        GameManager.Instance.CurrentUser.CUupgradeNumber++;
        GameManager.Instance.CurrentUser.CUprice = (long)Mathf.Pow(GameManager.Instance.CurrentUser.CUprice, 2);
        UpdateOtherUpgradePanel();
    }
    public void QuickTimeUpgrade()
    {
        
        if (GameManager.Instance.CurrentUser.Meters < GameManager.Instance.CurrentUser.QTprice)
        {
            soundManager.startDisallowance();
            return;
        }
        soundManager.startSfx();
        GameManager.Instance.CurrentUser.Meters -= GameManager.Instance.CurrentUser.QTprice;
        float coolTime = 1;
        
        while(coolTime > 100)
        {
            coolTime = coolTime * Time.deltaTime;
            Time.timeScale = 1.2f;
            QTbutton.interactable = false;
            UpdateOtherUpgradePanel();
        }
        Time.timeScale = 1f;
        GameManager.Instance.CurrentUser.QTupgradeNumber++;
        GameManager.Instance.CurrentUser.QTprice = (long)Mathf.Pow(GameManager.Instance.CurrentUser.QTprice, 1.5f);
        UpdateOtherUpgradePanel();
        QTshadow.SetActive(true);
        //spriteRenderer.material.SetColor("_Color", new Color(0.8f, 0.8f, 0.8f, 1f));
        //QTbuttonGameImage.color = Color.gray;
        QTbutton.interactable = true;
    }
    public void OnClickShip()
    {
        soundManager.startSfx();
        Debug.Log("클릭");
        GameManager.Instance.CurrentUser.Meters += GameManager.Instance.CurrentUser.clickPerMeters;
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
        GameManager.Instance.LengthUnitText(GameManager.Instance.CurrentUser.CUprice);

        CUpriceText.text = string.Format("{0} ", GameManager.Instance.CurrentUser.CUprice / GameManager.Instance.ShowUnitOrder) + (string)GameManager.Instance.LengthUnitText(GameManager.Instance.CurrentUser.CUprice);
        GameManager.Instance.LengthUnitText(GameManager.Instance.CurrentUser.QTprice);
        QTpriceText.text = string.Format("{0} ", GameManager.Instance.CurrentUser.QTprice / GameManager.Instance.ShowUnitOrder) + (string)GameManager.Instance.LengthUnitText(GameManager.Instance.CurrentUser.QTprice);
        CUbuttonText.text = string.Format("{0}기 채용", GameManager.Instance.CurrentUser.CUupgradeNumber);
        QTbuttonText.text = string.Format("{0}화 시청", GameManager.Instance.CurrentUser.QTupgradeNumber);
    }
    public void UpdateEnergyPanel()
    {
        metersText.text = string.Format("{0} ", GameManager.Instance.CurrentUser.Meters / GameManager.Instance.ShowUnitOrder) + (string)GameManager.Instance.LengthUnitText(GameManager.Instance.CurrentUser.Meters);
    }


}
