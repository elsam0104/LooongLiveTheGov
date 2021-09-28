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
    private GameObject upgradePanelTemplate = null;
    private List<UpgradePanel> upgradePanelList = new List<UpgradePanel>();
    private List<IndexPannel> indexPannelList = new List<IndexPannel>();
    private void Start()
    {
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

    }
    public IEnumerator QuickTimeUpgrade()
    {
        Time.timeScale = 1.2f;
        yield return new WaitForSeconds(10f);
    }
    public void OnClickShip()
    {
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
        CUbuttonText.text = string.Format("{0}기 채용",1);
        QTbuttonText.text = string.Format("{0}화 시청",1);
    }
    public void UpdateEnergyPanel()
    {
        metersText.text = string.Format("{0} 메터", GameManager.Instance.CurrentUser.Meters);
    }


}
