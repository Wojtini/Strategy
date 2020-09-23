using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{
    [Header("Player resources")]
    public int GoldAmount = 500;
    public int WoodAmount = 500;
    [Header("UI references")]
    public Text UIgoldText;
    public Text UItreeText;

    public void addGold(int amount)
    {
        GoldAmount += amount;
        UIgoldText.text = GoldAmount.ToString();
    }

    public void addWood(int amount)
    {
        WoodAmount += amount;
        UItreeText.text = WoodAmount.ToString();
    }
}
