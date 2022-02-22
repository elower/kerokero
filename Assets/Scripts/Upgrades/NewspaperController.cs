using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class NewspaperController : MonoBehaviour
{
    Data data;
    Upgrades Upgrades;
    static double refreshTime = 20;
    public TMP_Text nextUpdate;
    public TMP_Text upgrade;

    DateTime lastNewsUpdate;
    string lastNewsUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
        Upgrades = new Upgrades(data);
        lastNewsUpdate = Upgrades.getLastNewsUpdate();
        lastNewsUpgrade = Upgrades.getLastNewsUpgrade();


        if(lastNewsUpdate.AddMinutes(refreshTime) <= DateTime.UtcNow) {
            lastNewsUpdate = DateTime.UtcNow;
            lastNewsUpgrade = getRandomUpgrade(data.coins);
            Upgrades.setNewsUpgrades(lastNewsUpdate, lastNewsUpgrade, data);
        }
    }

    // Update is called once per frame
    void Update()
    {
        formatArticle();
    }
    string getRandomUpgrade(double coins) {
        string randomUpgrade = "";
        int randomIdx = 0;
        if(coins >= 0 && coins < 100000) { //lowest tier, 0 to 100,000
            randomIdx = UnityEngine.Random.Range(1, 5);
        } else if(coins >= 100000 && coins < 1000000) { //low-mid tier, 100,000 to 1,000,000
            randomIdx = UnityEngine.Random.Range(6, 10);
        } else if(coins >= 1000000 && coins < 5000000) { //middle tier, 1,000,000 to 5,000,000
            randomIdx = UnityEngine.Random.Range(11, 15);
        } else if(coins >= 5000000 && coins < 100000000) { //high-mid tier, 5,000,000 to 100,000,000
            randomIdx = UnityEngine.Random.Range(16, 20);
        } else if(coins >= 100000000 && coins < 1000000000) { //highest tier, 100,000,000 to 1,000,000,000
            randomIdx = UnityEngine.Random.Range(21, 30);
        }

        switch(randomIdx) {
            case 1:
                randomUpgrade = "Coins:1.1";
                break;
            case 2:
                randomUpgrade = "Coins:1.3";
                break;
            case 3:
                randomUpgrade = "Coins:1.4";
                break;
            case 4:
                randomUpgrade = "Coins:1.5";
                break;
            case 5:
                randomUpgrade = "Speed:1.1";
                break;
            case 6:
                randomUpgrade = "Coins:1.75";
                break;
            case 7:
                randomUpgrade = "Coins:2.0";
                break;
            case 8:
                randomUpgrade = "Coins:2.2";
                break;
            case 9:
                randomUpgrade = "InstantCoins:1000000"; //1,000,000
                break;
            case 10:
                randomUpgrade = "Speed:1.25";
                break;
            case 11:
                randomUpgrade = "Speed:1.5";
                break;
            case 12:
                randomUpgrade = "Coins:2.5";
                break;
            case 13:
                randomUpgrade = "Coins:2.75";
                break;
            case 14:
                randomUpgrade = "InstantCoins:100000000"; //100,000,000
                break;
            case 15:
                randomUpgrade = "InstantCoins:500000000"; //500,000,000
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                break;
            case 21:
                break;
            case 22:
                break;
            case 23:
                break;
            case 24:
                break;
            case 25:
                break;
            case 26:
                break;
            case 27:
                break;
            case 28:
                break;
            case 29:
                break;
            case 30:
                break;
        }
        //special upgrade regardless of coins count
        if(UnityEngine.Random.Range(1, 200000) == 100000) randomUpgrade = "Special";

        return randomUpgrade;
    }
    void formatArticle() {
        DateTime nextNewsUpdate = lastNewsUpdate.AddMinutes(refreshTime);
        int timeBetween = Convert.ToInt32((nextNewsUpdate - DateTime.UtcNow).TotalMinutes);
        nextUpdate.text = timeBetween + " Minutes Until Refresh";

        string upgradeFormat = "";
        string[] upgradeArr = lastNewsUpgrade.Split(':');

        string upgradeType = upgradeArr[0];
        string upgradeAmount = upgradeArr[1];

        switch(upgradeType) {
            case "Coins":
                upgradeFormat = "Reset your progress and each frog will gain " + upgradeAmount + "x their base amount of coins each time they fetch!";
                break;
            case "Speed":
                upgradeFormat = "Reset your progress and each frog's fetch timer will be " + upgradeAmount + "x faster!";
                break;
            case "InstantCoins":
                upgradeFormat = "Reset your progress and instantly gain " + upgradeAmount + " Coins to start off with!";
                break;
            case "Special":
                upgradeFormat = "You're a winner!";
                break;
        }

        upgrade.text = upgradeFormat;
    }
}
