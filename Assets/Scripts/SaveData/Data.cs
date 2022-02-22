using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using System;
public class Data
{
    public double coins;
    public DateTime lastLoggedTime;
    public DateTime[] nextSpawn = new DateTime[furnitureCount];
    public int tutorial;
    public float[] settingsValues = new float[1];
    public NameValueCollection upgrades = new NameValueCollection();

    //=== Frogs List ==/
    public List<string> ownedFrogs = new List<string>();
    public NameValueCollection mates = new NameValueCollection();
    public NameValueCollection newMates = new NameValueCollection();//newmates is array of frogs that should not be counted towards timer when gone
    public NameValueCollection timersProgress = new NameValueCollection();

    //=== Garden ==/
    public List<string> ownedFurniture = new List<string>();
    public List<int> gridPlaced = new List<int>();
    public List<string> furniturePlaced = new List<string>();

    //=== Static Sizes ==//
    public static int upgradesCount = 13; //current amount of upgrades added
    public static int frogCount = 20;
    public static int furnitureCount = 27;
    public static int furnitureAreaCount = 25; //number of grid spaces

    //=== Keys ==/
    public string[] upgradeKeys = {
        "WaterAreas",
        "FoliageAreas",
        "OtherAreas",
        "Modern",
        "Cute",
        "Interstellar",
        "Spooky",
        "CherryBlossom",
        "Minimalist",
        "LastNewsUpdate",
        "LastNewsUpgrade",
        "CurrentNewsUpgrade",
        "AchievementsMultiplier"
    };
    public string[] frogNames = {
        "Pond",
        "Wood",
        "Bullfrog",
        "Tomato",
        "Flying",
        "Amazon_Milk",
        "Darwins",
        "Glass",
        "Atelopus",
        "Black_Rain",
        "Brown",
        "Whites",
        "Red_Eyed",
        "Gray",
        "White_Lipped",
        "Yellow_Headed",
        "Golden_Poison",
        "Blue_Jeans_Poison",
        "Blue_Poison",
        "Strawberry_Poison"
    };
    public string[] furnitureNames = {
        "Water Cup",
        "Puddle",
        "Pond",
        "Swimming Pool",
        "Creek",
        "Flower Pot",
        "Stick",
        "Tree",
        "Lily Pad",
        "Toadstool",
        "Water Spout",
        "Pond Stone",
        "Fountain",
        "Bridge",
        "Lantern",
        "Tree Swing",
        "Dragonfly Decoration",
        "Butterfly Decoration",
        "Diamond Decoration",
        "Hidey Hole",
        "Bench",
        "Side Table",
        "Long Table",
        "Path",
        "Arch",
        "Humidifier",
        "Mister"
    };
    public Data() {
        reset();
        //loadSerializedData();
    }
    void loadSerializedData() {
      PlayerData playerData = SaveSystem.load();
      if(playerData != null) {
          coins = playerData.coins;
          lastLoggedTime = playerData.lastLoggedTime;
          settingsValues = playerData.settingsValues;
          nextSpawn = playerData.nextSpawn;
          tutorial = playerData.tutorial;

          foreach (string i in playerData.ownedFrogs)
              ownedFrogs.Add(i);

          int counter = 0;
          foreach (string i in playerData.mates) {
              mates.Add(frogNames[counter], i);
              counter++;
          }

          counter = 0;
          foreach (string i in playerData.timersProgress) {
              timersProgress.Add(frogNames[counter], i);
              counter++;
          }

          counter = 0;
          foreach (string i in playerData.newMates) {
              newMates.Add(frogNames[counter], i);
              counter++;
          }

          counter = 0;
          foreach(string i in playerData.upgrades) {
              upgrades.Add(upgradeKeys[counter], i);
              counter++;
          }

          foreach (string i in playerData.ownedFurniture)
            ownedFurniture.Add(i);
          foreach (int i in playerData.gridPlaced)
              gridPlaced.Add(i);
          foreach (string i in playerData.furniturePlaced)
              furniturePlaced.Add(i);

      } else { //save file not found
          reset();
      }
    }
    public void savePlayerData(bool saveWithoutTime) {
        if(saveWithoutTime)
            SaveSystem.saveWithoutTime(this);
        else
            SaveSystem.save(this);
    }

    public void reset() {
      Debug.LogError("Resetting...");
        coins = 12000;
        ownedFurniture.Clear();
        lastLoggedTime = DateTime.UtcNow;
        tutorial = 2; //set to 0 to begin tutorial
        settingsValues[0] = 0.1f;
        ownedFrogs.Clear();
        ownedFrogs.Add("Pond");
        // ownedFrogs.Add("Bullfrog");
        // ownedFrogs.Add("Amazon_Milk");
        // ownedFrogs.Add("Wood");
        // ownedFrogs.Add("Tomato");
        // ownedFrogs.Add("Flying");
        // ownedFrogs.Add("Darwins");
        // ownedFrogs.Add("Glass");
        // ownedFrogs.Add("Atelopus");
        // ownedFrogs.Add("Black_Rain");
        // ownedFrogs.Add("Brown");
        // ownedFrogs.Add("Whites");
        // ownedFrogs.Add("Red_Eyed");
        // ownedFrogs.Add("Gray");
        // ownedFrogs.Add("White_Lipped");
        // ownedFrogs.Add("Yellow_Headed");
        // ownedFrogs.Add("Golden_Poison");
        // ownedFrogs.Add("Blue_Jeans_Poison");
        // ownedFrogs.Add("Blue_Poison");
        // ownedFrogs.Add("Strawberry_Poison");

        mates.Clear();
        for(int i = 0; i < frogCount; i++) {
            mates[frogNames[i]] = "0";
        }

        timersProgress.Clear();
        for(int i = 0; i < frogCount; i++) {
            timersProgress[frogNames[i]] = "0";
        }
        for(int i = 0; i < frogCount; i++) {
            newMates[frogNames[i]] = null;
        }
        for(int i = 0; i < furnitureCount; i++) {
            nextSpawn[i] = DateTime.UtcNow;
        }
        for(int i = 0; i < upgradesCount; i++) {
            upgrades[upgradeKeys[i]] = null;
        }

        upgrades["WaterAreas"] = "1";
        upgrades["FoliageAreas"] = "1";
        upgrades["OtherAreas"] = "1";
        upgrades["LastNewsUpdate"] = DateTime.UtcNow.AddMinutes(-20).ToString();

        savePlayerData(false);
    }

    public void consoleDebug(string debugStr) {
        Debug.LogError(debugStr);
    }

    public string coinsFormatter(double price) {
        if(price < 1000000) { //less than 1 mil
            return price.ToString("N0");
        } else if(price >= 1000000 && price < 1000000000) { //millions
            decimal num = (decimal)price/1000000;
            return Math.Round(num, 4) + " Million";
        } else if(price >= 1000000000 && price < 1000000000000) { //billions
            decimal num = (decimal)price/1000000000;
            return Math.Round(num, 4) + " Billion";
        } else if(price >= 1000000000000 && price < 1000000000000000) { //trillions
            decimal num = (decimal)price/1000000000000;
            return Math.Round(num, 4) + " Trillion";
        } else return null;
    }
}
