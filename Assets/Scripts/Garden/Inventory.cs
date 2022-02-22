using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class Inventory : MonoBehaviour
{
    Data data;
    FurnitureInfo furnitureInfo;

    public GardenController gardenController;
    public GameObject Grid;
    public GameObject PopupWrap;
    public PlacementController placementController;

    /* FURNITURE PANELS */
    public GameObject Water_Cup;
    public GameObject Puddle;
    public GameObject Pond;
    public GameObject Swimming_Pool;
    public GameObject Creek;
    public GameObject Flower_Pot;
    public GameObject Stick;
    public GameObject Tree;
    public GameObject Lily_Pad;
    public GameObject Toadstool;
    public GameObject Water_Spout;
    public GameObject Pond_Stone;
    public GameObject Fountain;
    public GameObject Bridge;
    public GameObject Lantern;
    public GameObject Tree_Swing;
    public GameObject Dragonfly_Decoration;
    public GameObject Butterfly_Decoration;
    public GameObject Diamond_Decoration;
    public GameObject Hidey_Hole;
    public GameObject Bench;
    public GameObject Side_Table;
    public GameObject Long_Table;
    public GameObject Path;
    public GameObject Arch;
    public GameObject Humidifier;
    public GameObject Mister;


    // Start is called before the first frame update
    void Start()
    {
        furnitureInfo = GameObject.FindGameObjectWithTag("FurnitureInfo").GetComponent<FurnitureInfo>();
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
        refreshItems();
        //hide grid sprites and disable all placement controllers
        foreach(Transform child in Grid.transform) {
          Color temp = child.GetComponent<Image> ().color;
          temp.a=0f;
          child.GetComponent<Image> ().color = temp;
          child.GetComponent<Placement>().enabled = false;
        }
    }
    public void refreshItems() {
      GameObject inventoryWrap = GameObject.Find("Inventory");
      foreach(string furnitureName in data.furnitureNames) {
          GameObject furniturePanel = getFurniturePanel(furnitureName);
          Transform removeOrNotOwned = furniturePanel.transform.Find("placeOrNotOwned");
          Button placeBtn = removeOrNotOwned.GetComponent<Button>();
          Sprite removeNotOwnedSpr;
          Transform nameObj = furniturePanel.transform.Find("Name");
          TMP_Text name = nameObj.GetComponent<TMP_Text>();
          name.text = furnitureName;
          Image furnitureImage = furniturePanel.transform.Find("Image").GetComponent<Image>();
          furnitureImage.sprite = furnitureInfo.getSprite(furnitureName);
          if(furnitureInfo.getFurniture(furnitureName).getOwned() == false)
              removeNotOwnedSpr = gardenController.notOwned;
          else {
              placeBtn.interactable = true;
              placeBtn.onClick.RemoveAllListeners();
              if(furnitureInfo.getFurniture(furnitureName).getPlaced() == true) {
                  removeNotOwnedSpr = gardenController.remove;
                  int gridSpace = data.gridPlaced[data.furniturePlaced.IndexOf(furnitureName)];
                  int gridX = gridSpace/10;
                  int gridY = gridSpace % 10;
                  placeBtn.onClick.AddListener(delegate { remove(furnitureInfo.getFurniture(furnitureName), false, placementController.getGridGO(gridX, gridY)); });
              } else {
                  removeNotOwnedSpr = gardenController.place;
                  placeBtn.onClick.AddListener(delegate { chooseSpot(furnitureInfo.getFurniture(furnitureName)); });
              }
          }
          removeOrNotOwned.GetComponent<Image> ().sprite = removeNotOwnedSpr;
          Color temp = removeOrNotOwned.GetComponent<Image> ().color;
          temp.a=1.0f;
          removeOrNotOwned.GetComponent<Image> ().color = temp;
      }
    }
    public GameObject getFurniturePanel(string furnitureName) {
        switch(furnitureName) {
            case "Water Cup":
                return Water_Cup;
            case "Puddle":
                return Puddle;
            case "Pond":
                return Pond;
            case "Swimming Pool":
                return Swimming_Pool;
            case "Creek":
                return Creek;
            case "Flower Pot":
                return Flower_Pot;
            case "Stick":
                return Stick;
            case "Tree":
                return Tree;
            case "Lily Pad":
                return Lily_Pad;
            case "Toadstool":
                return Toadstool;
            case "Water Spout":
                return Water_Spout;
            case "Pond Stone":
                return Pond_Stone;
            case "Fountain":
                return Fountain;
            case "Bridge":
                return Bridge;
            case "Lantern":
                return Lantern;
            case "Tree Swing":
                return Tree_Swing;
            case "Dragonfly Decoration":
                return Dragonfly_Decoration;
            case "Butterfly Decoration":
                return Butterfly_Decoration;
            case "Diamond Decoration":
                return Diamond_Decoration;
            case "Hidey Hole":
                return Hidey_Hole;
            case "Bench":
                return Bench;
            case "Side Table":
                return Side_Table;
            case "Long Table":
                return Long_Table;
            case "Path":
                return Path;
            case "Arch":
                return Arch;
            case "Humidifier":
                return Humidifier;
            case "Mister":
                return Mister;
            default:
                return Water_Cup;
        }
    }
    void chooseSpot(Furniture furniture) {
        string furnitureArea = furniture.getCategory();

        //check number of furniture placed that belong to this category and compare to unlocked items
        int count = 0;
        foreach(string thisFurniture in data.furniturePlaced) {
          if(furnitureInfo.getFurniture(thisFurniture).getCategory() == furnitureArea)
            count++;
        }

        placementController.furniturePlacing = furniture;

        //enable save pos button
        Button SavePos = GameObject.Find("SaveButton").GetComponent<Button>();
        SavePos.onClick.RemoveAllListeners();
        SavePos.onClick.AddListener(delegate { savePos(true); });

        if(count < gardenController.Upgrades.getGardenArea(furnitureArea)) { //all avail spaces are where no furniture is placed
          placementController.swapping = false;
          //show grid sprites and enable placement controllers for all avail spaces
          foreach(Transform child in Grid.transform) {
            Color temp = child.GetComponent<Image> ().color;
            temp.a=1.0f;
            child.GetComponent<Image> ().color = temp;
            if(child.GetComponent<GridSquare>().getFurniture() == null) {
              child.GetComponent<Placement>().enabled = true;
              child.GetComponent<GridSquare>().setAvailable();
            } else {
              child.GetComponent<GridSquare>().setUnavailable();
            }
          }
        } else { //all avail spaces are the ones with the same type of furniture
          placementController.swapping = true;
          //Do not let user swap if frog is spawned on area
          foreach(Transform child in Grid.transform) {
            Color temp = child.GetComponent<Image> ().color;
            temp.a=1.0f;
            child.GetComponent<Image> ().color = temp;
            if(child.GetComponent<GridSquare>().getFurniture() != null && furnitureInfo.getFurniture(child.GetComponent<GridSquare>().getFurniture()).getCategory() == furnitureArea && child.GetComponent<GridSquare>().getFrogSpawn() == null) {
              child.GetComponent<Placement>().enabled = true;
              child.GetComponent<GridSquare>().setAvailable();
            } else {
              child.GetComponent<GridSquare>().setUnavailable();
            }
          }
        }

        //hide popup menu
        PopupWrap.SetActive(false);

        //close garden menu to see grid
        GameObject.Find("MenuController").GetComponent<MenuController>().openGardenMenu("Inventory");
    }
    public void savePos(bool reopenInventory) {
      //reopen inventory if from place
      if(reopenInventory)
        GameObject.Find("MenuController").GetComponent<MenuController>().openGardenMenu("Inventory");

      //disable place button
      PopupWrap.SetActive(true);

      //save grid position
      gardenController.saveAllGridPos();


      //hide grid sprites and disable all placement controllers
      foreach(Transform child in Grid.transform) {
        Color temp = child.GetComponent<Image> ().color;
        temp.a=0f;
        child.GetComponent<Image> ().color = temp;
        child.GetComponent<Placement>().enabled = false;
      }
    }
    public void place(Furniture furniture) {
        furniture.setPlaced(true);

        int gridSpace = data.gridPlaced[data.furniturePlaced.IndexOf(furniture.getName())];
        int gridX = gridSpace/10;
        int gridY = gridSpace % 10;
        getFurniturePanel(furniture.getName()).transform.Find("placeOrNotOwned").GetComponent<Button>().onClick.RemoveAllListeners();
        getFurniturePanel(furniture.getName()).transform.Find("placeOrNotOwned").GetComponent<Button>().onClick.AddListener(delegate { remove(furniture, false, placementController.getGridGO(gridX, gridY)); });
        getFurniturePanel(furniture.getName()).transform.Find("placeOrNotOwned").GetComponent<Image>().sprite = gardenController.remove;
    }
    void remove(Furniture furniture, bool checkSpot, GameObject gridSpot) {

        string toRemove = "";
        if(checkSpot) {
            //find furniture in that spot
            foreach(string furniturePlaced in data.furniturePlaced) {
              //TODO: check data vals instead of getareaplaced
                // if(furnitureInfo.getFurniture(furniturePlaced).getAreaPlaced() == areaToPlace)
                //     toRemove = furnitureInfo.getFurniture(furniturePlaced).getName();
            }
        } else
            toRemove = furniture.getName();
        if(toRemove != "") {

            furnitureInfo.getFurniture(toRemove).setPlaced(false);

            int gridSpace = data.gridPlaced[data.furniturePlaced.IndexOf(furniture.getName())];
            data.gridPlaced.Remove(gridSpace);
            data.furniturePlaced.Remove(toRemove);

            gridSpot.GetComponent<GridSquare>().setAvailable();
            gridSpot.GetComponent<GridSquare>().removeFurniture();

            Button placeBtn = getFurniturePanel(furniture.getName()).transform.Find("placeOrNotOwned").GetComponent<Button> ();
            placeBtn.GetComponent<Image>().sprite = gardenController.place;
            placeBtn.onClick.RemoveAllListeners();
            placeBtn.onClick.AddListener(delegate { chooseSpot(furnitureInfo.getFurniture(toRemove)); });
        }
    }
}
