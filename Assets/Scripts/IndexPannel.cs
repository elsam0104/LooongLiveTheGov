using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndexPannel : MonoBehaviour
{
    private Index index = null;
    private Ship ship = null;
    [SerializeField]
    private Text explanationText;
    [SerializeField]
    private Text totalMpsText;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text amountText;
    private UpgradePanel upgradePanel = null;
    private void Start()
    { 
        upgradePanel = FindObjectOfType<UpgradePanel>();
    }
    public void IndexSetValues(Index index)
    {
        upgradePanel.SetValues(ship);
        this.index = index;
        UpdateIndex();   
    }
    public void UpdateIndex()
    {
        explanationText.text = index.explanation;
        totalMpsText.text = string.Format("{0} 개의 우주개발기구가 1 초당 {0}메터만큼 비행사 동무를 전진시킵네다.",ship.amount,index.totalMps);
        nameText.text = ship.shipName;
        amountText.text = string.Format("[현 보유량: {0}]", ship.amount);
    }
}
