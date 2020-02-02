using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text goldOreText;
    public Text goldBarText;
    public Text moneyText;
    public GameObject brokenBuildingText;

    public GameObject menuPanel;
    public GameObject resourcePanel;

    private GameObject[] buildings;

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
        buildings = GameObject.FindGameObjectsWithTag("Building");
        brokenBuildingText.SetActive(false);

        for (int i = 0; i < buildings.Length; i++)
        {
            if(buildings[i].GetComponent<BuildingController>().isFixed == false)
            {
                brokenBuildingText.SetActive(true);
            }
        }

        goldOreText.text = "Gold Ore: " + goldOreAmount;
        goldBarText.text = "Gold Bar: " + goldBarAmount;
        moneyText.text = "Money: " + moneyAmount;
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
        menuPanel.SetActive(false);
        resourcePanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
