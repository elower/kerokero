using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradesController : MonoBehaviour
{
    Data data;
    GardenController gardenController;
    public GameObject[] upgrades;
    Upgrades Upgrades;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
        Upgrades = new Upgrades(data);
        updateUpgrades();
    }

    public void getNewArea(string area) {
        Upgrades.addGardenArea(area, data);
        if(SceneManager.GetSceneByName("Garden").isLoaded) { //if garden is loaded, update areas NOW TODO
            gardenController = GameObject.FindGameObjectWithTag("GardenController").GetComponent<GardenController>();
            // update area arrays to only contain the amount visible to user

        }
    }
    public void getTheme(string theme) {
        Upgrades.setThemeOwned(theme, data);
    }
    public void purchaseUpgrade(int price) {
        data.coins -= price;
        data.savePlayerData(true);
        updateUpgrades();
    }

    void updateUpgrades() {
        foreach(GameObject upgrade in upgrades) {
            Button buyBtn = upgrade.transform.Find("Buy").GetComponent<Button>();
            double price = double.Parse(upgrade.transform.Find("Price").GetComponent<TMP_Text>().text);
            if(data.coins < price)
                buyBtn.interactable = false;


            //upgrade-specific code
            if(upgrade.name == "Water" || upgrade.name == "Foliage" || upgrade.name == "Other") {
                //garden area upgrade
                switch(Upgrades.getGardenArea(upgrade.name)){
                    case 1:
                        price = 10000; //10,000
                        break;
                    case 2:
                        price = 100000; //100,000
                        break;
                    case 3:
                        price = 500000; //500,000
                        break;
                    case 4:
                        price = 1000000; //1,000,000
                        break;
                    default:
                        buyBtn.interactable = false;
                        break;
                }
            }
            if(upgrade.name == "Modern" || upgrade.name == "Cute" || upgrade.name == "Interstellar") {
                //theme specific upgrade
                if(Upgrades.getThemeOwned(upgrade.name))
                    buyBtn.interactable = false;
            }
            upgrade.transform.Find("Price_Format").GetComponent<TMP_Text>().text = data.coinsFormatter(price);
        }
    }
}
