using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private Image shipImage = null;
    [SerializeField]
    private Text shipNameText = null;
    [SerializeField]
    private Text amountText = null;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text purchaseButton = null;
    [SerializeField]
    private Sprite[] shipSprite = null;

    private Ship ship = null; 
    private SoundManager soundManager = null;
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    public void SetValues(Ship ship)
    {
        this.ship = ship;
        UpdateValues();
    }
    public void UpdateValues()
    {
        shipImage.sprite = shipSprite[ship.shipNumber];
        shipNameText.text = ship.shipName;
        amountText.text = string.Format("{0}", ship.amount);
        priceText.text = string.Format("{0} ธÞลอ", ship.price);
    }

    public void OnClickPurchase()
    {
        
        if (GameManager.Instance.CurrentUser.Meters < ship.price)
        {
            soundManager.startDisallowance();
            return;
        }
        else
        {
            soundManager.startSfx();
            GameManager.Instance.CurrentUser.Meters -= ship.price;
            ship.amount++;
            ship.price = (long)(ship.price * 1.15f);
            UpdateValues();
            GameManager.Instance.UI.UpdateEnergyPanel();

        }
    }
}
