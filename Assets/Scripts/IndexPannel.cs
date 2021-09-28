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
        totalMpsText.text = string.Format("{0} ���� ���ְ��߱ⱸ�� 1 �ʴ� {0}���͸�ŭ ����� ������ ������ŵ�״�.",ship.amount,index.totalMps);
        nameText.text = ship.shipName;
        amountText.text = string.Format("[�� ������: {0}]", ship.amount);
    }
}
