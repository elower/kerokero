using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using System;

public class Upgrades
{
    int waterAreas;
    int foliageAreas;
    int otherAreas;

    bool modern;
    bool cute;
    bool interstellar;

    DateTime lastNewsUpdate;
    string lastNewsUpgrade;

    public Upgrades(Data data) {
      waterAreas = int.Parse(data.upgrades["WaterAreas"]);
      foliageAreas = int.Parse(data.upgrades["FoliageAreas"]);
      otherAreas = int.Parse(data.upgrades["OtherAreas"]);

      if(data.upgrades["Modern"] == "owned") modern = true; else modern = false;
      if(data.upgrades["Cute"] == "owned") cute = true; else cute = false;
      if(data.upgrades["Interstellar"] == "owned") interstellar = true; else interstellar = false;

      lastNewsUpdate = DateTime.Parse(data.upgrades["LastNewsUpdate"]);
      lastNewsUpgrade = data.upgrades["LastNewsUpgrade"];
    }
    public int getWaterAreas() { return waterAreas; }
    public int getFoliageAreas() { return foliageAreas; }
    public int getOtherAreas() { return otherAreas; }
    public bool getThemeOwned(string theme) {
      switch(theme) {
        case "Modern":
          return modern;
        case "Cute":
          return cute;
        case "Interstellar":
          return interstellar;
        default: return false;
      }
    }
    public int getGardenArea(string area) {
      switch(area) {
        case "Water":
          return waterAreas;
        case "Foliage":
          return foliageAreas;
        case "Other":
          return otherAreas;
        default:
          return 1;
      }
    }

    public DateTime getLastNewsUpdate() { return lastNewsUpdate; }
    public string getLastNewsUpgrade() { return lastNewsUpgrade; }

    public void addGardenArea(string area, Data data) {
      int currentCount = int.Parse(data.upgrades[area+"Areas"]);
      data.upgrades[area+"Areas"] = (currentCount+1).ToString();
      switch(area) {
        case "Water":
          waterAreas++;
          break;
        case "Foliage":
          foliageAreas++;
          break;
        case "Other":
          otherAreas++;
          break;
      }
      data.savePlayerData(true);
     }
    public void setThemeOwned(string theme, Data data) {
      data.upgrades[theme] = "owned";
      data.savePlayerData(true);
      switch(theme) {
        case "Modern":
          modern = true;
          break;
        case "Cute":
          cute = true;
          break;
        case "Interstellar":
          interstellar = true;
          break;
      }
    }

    public void setNewsUpgrades(DateTime update, string upgrade, Data data) {
      data.upgrades["LastNewsUpdate"] = update.ToString();
      data.upgrades["LastNewsUpgrade"] = upgrade;

      data.savePlayerData(true);

      lastNewsUpdate = update;
      lastNewsUpgrade = upgrade;
    }
}
