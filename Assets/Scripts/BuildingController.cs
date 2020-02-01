using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private GameController GC;

    public GameObject brokenSprite;

    public string buildingType;

    public int resourceCollectRate;
    public bool isFixed = true;

    public int oreTimer = 0;
    public int barTimer = 0;
    public int moneyTimer = 0;
    public int aliveTime;
    public int breakTimer = 0;

    private void Start()
    {
        GC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        aliveTime = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFixed)
        {
            brokenSprite.SetActive(false);
        }
        else
        {
            brokenSprite.SetActive(true);
        }

        if (buildingType == "Mine")
        {
            Mine();
        }
        else if (buildingType == "Factory")
        {
            Factory();
        }
        else if (buildingType == "Market")
        {
            Market();
        }

        BreakChance();
    }

    public void Mine()
    {
        if (isFixed)
        {
            if (oreTimer == 20)
            {
                GC.GoldOreGain();

                oreTimer = 0;
            }
            else
            {
                oreTimer++;
            }
        }
    }

    public void Factory()
    {
        if (isFixed)
        {
            if (barTimer == 60)
            {
                GC.GoldBarGain();

                barTimer = 0;
            }
            else
            {
                barTimer++;
            }
        }
    }

    public void Market()
    {
        if (isFixed)
        {
            if (moneyTimer == 200)
            {
                GC.MoneyGained();

                moneyTimer = 0;
            }
            else
            {
                moneyTimer++;
            }
        }
    }

    public void BreakChance()
    {
        if (breakTimer == aliveTime * 500)
        {
            isFixed = false;
        }
        else
        {
            breakTimer++;
        }
    }

    public void Repair()
    {
        breakTimer = 0;
        isFixed = true;
        aliveTime = Random.Range(1, 6);
    }
}
