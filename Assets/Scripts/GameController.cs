using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text GoldOre;
    public Text GoldBar;
    public Text Money;

    public GameObject Menu;
    public GameObject ResourcePanel;

    public int goldOreAmount;

    public int GoldOreAmount
    {
        get { return goldOreAmount; }
        set { goldOreAmount = value; }
    }

    public int goldBarAmount;

    public int GoldBarAmount
    {
        get { return goldBarAmount; }
        set { goldBarAmount = value; }
    }

    public int moneyAmount;

    public int MoneyAmount
    {
        get { return moneyAmount; }
        set { moneyAmount = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GoldOre.text = "Gold Ore: " + goldOreAmount;
        GoldBar.text = "Gold Bar: " + goldBarAmount;
        Money.text = "Money: " + moneyAmount;
    }

    public void GoldOreGain()
    {
        goldOreAmount++;
    }

    public void GoldBarGain()
    {
        if (GoldOreAmount > 0)
        {
            goldOreAmount--;
            goldBarAmount++;
        }
    }

    public void MoneyGained()
    {
        if (GoldBarAmount > 0)
        {
            if (GoldBarAmount > 2)
            {
                goldBarAmount -= 3;
                moneyAmount += 30;
            }
            else
            {
                goldBarAmount--;
                moneyAmount += 10;
            }
        }
    }

    public void CloseMenu()
    {
        Menu.SetActive(false);
        ResourcePanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
