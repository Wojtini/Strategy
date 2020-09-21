using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources instance;
    [Header("Player resources")]
    public int GoldAmount = 0;
    public int WoodAmount = 0;
    [Header("UI references")]
    public Text UIgoldText;
    public Text UItreeText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

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
