using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{
    public double coins;
    public int tutorial;
    public float[] settingsValues;
    public DateTime[] nextSpawn = new DateTime[27]; //nextspawn is the array of times for each furniture where the next frog will spawn
    public DateTime lastLoggedTime;

    public string[] ownedFrogs;
    public string[] mates = new string[frogCount]; //array of string-converted ints for mates
    public string[] timersProgress = new string[frogCount];
    public string[] newMates = new string[frogCount]; //newmates is array of time when last gotten frog

    public string[] ownedFurniture;
    public int[] gridPlaced;
    public string[] furniturePlaced;

    public static int frogCount = 20; //the amount of frogs I have added to the game currently
    public static int furnitureCount = 27; //amount of furniture I have added to the game currently
    public static int upgradesCount = 13; //current amount of upgrades added

    public string[] upgrades = new string[upgradesCount];

    public PlayerData(Data data) {
        coins = data.coins;
        ownedFrogs = data.ownedFrogs.ToArray();

        data.mates.CopyTo(mates, 0);
        data.timersProgress.CopyTo(timersProgress, 0);
        data.newMates.CopyTo(newMates, 0);
        nextSpawn = data.nextSpawn;

        ownedFurniture = data.ownedFurniture.ToArray();
        gridPlaced = data.gridPlaced.ToArray();
        furniturePlaced = data.furniturePlaced.ToArray();

        lastLoggedTime = DateTime.UtcNow;
        tutorial = data.tutorial;

        //upgrades
        data.upgrades.CopyTo(upgrades, 0);

        settingsValues = data.settingsValues;
    }

}
