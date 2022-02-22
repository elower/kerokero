using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GardenController : MonoBehaviour
{
    Data data;
    FrogsInfo frogsInfo;
    FurnitureInfo furnitureInfo;
    public Upgrades Upgrades;
    public PlacementController placementController;

    /* SPRITE DECLARATIONS */
    public Sprite owned;
    public Sprite buy;
    public Sprite place;
    public Sprite remove;
    public Sprite notOwned;

    /* SPAWNED LIST */
    public List<string> areasSpawned = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
        Upgrades = new Upgrades(data);

        frogsInfo = GameObject.FindGameObjectWithTag("FrogsInfo").GetComponent<FrogsInfo>();
        furnitureInfo = GameObject.FindGameObjectWithTag("FurnitureInfo").GetComponent<FurnitureInfo>();
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicPlayer>().PlayMusic();
        if(data.tutorial != 2) {
            SceneManager.LoadSceneAsync("Tutorial", LoadSceneMode.Additive);
        } else {
            //set owned if owned
            foreach(string ownedFurniture in data.ownedFurniture) {
                furnitureInfo.getFurniture(ownedFurniture).setOwned();
            }
            //set placed if placed
            foreach(string placedFurniture in data.furniturePlaced) {
                furnitureInfo.getFurniture(placedFurniture).setPlaced(true);
            }

            //set furniture sprites for all placed
            int count = 0;
            foreach(int grid in data.gridPlaced) {
              int gridX = grid/10;
              int gridY = grid % 10;

              placementController.getGridGO(gridX, gridY).GetComponent<GridSquare>().setFurniture(data.furniturePlaced[count]);
              count++;
            }

            //try to spawn on every placed furniture item
            foreach(string furnitureName in data.furniturePlaced) {
                Furniture FurnitureObj = furnitureInfo.getFurniture(furnitureName);
                FurnitureObj.setPlaced(true);

                //check if spawn time is up
                int gridSpace = data.gridPlaced[data.furniturePlaced.IndexOf(furnitureName)];
                int gridX = gridSpace/10;
                int gridY = gridSpace % 10;
                if(data.nextSpawn[Array.IndexOf(data.furnitureNames, furnitureName)] <= DateTime.UtcNow)
                    furnitureInfo.spawnFrog(FurnitureObj, this, placementController.getGridGO(gridX, gridY));
            }
        }
    }

    public void openBuildMode() {
      Inventory inventoryController = GameObject.Find("InventoryController").GetComponent<Inventory>();
      placementController.buildMode = true;
      placementController.selectingSpace = true;
      placementController.movingMode = false;
      placementController.swapping = false;

      //show grid
      foreach(Transform child in GameObject.Find("Grid").transform) {
        Color temp = child.GetComponent<Image> ().color;
        temp.a=1.0f;
        child.GetComponent<Image> ().color = temp;
      }

      //hide popup menu
      inventoryController.PopupWrap.SetActive(false);

      //if garden menu open, close to see grid
      if(GameObject.Find("MenuController").GetComponent<MenuController>().gardenMenuOpened != null)
        GameObject.Find("MenuController").GetComponent<MenuController>().openGardenMenu(GameObject.Find("MenuController").GetComponent<MenuController>().gardenMenuOpened);

      //enable save pos button
      Button SavePos = GameObject.Find("SaveButton").GetComponent<Button>();
      SavePos.onClick.RemoveAllListeners();

      SavePos.onClick.AddListener(delegate { saveAllGridPos(); });
    }
    public void saveAllGridPos() {
      placementController.buildMode = false;
      data.furniturePlaced.Clear();
      data.gridPlaced.Clear();
      //clear furniture placed
      foreach(string furniture in data.furnitureNames)
        furnitureInfo.getFurniture(furniture).setPlaced(false);

      foreach(Transform child in GameObject.Find("Grid").transform) {
        Color temp = child.GetComponent<Image> ().color;
        temp.a=0f;
        child.GetComponent<Image> ().color = temp;
        if(child.GetComponent<GridSquare>().getFurniture() != null) {
          //add to data furniture placed and grid placed
          data.furniturePlaced.Add(child.GetComponent<GridSquare>().getFurniture());

          int gridPlaced = placementController.getGridCoords(child.gameObject);
          data.gridPlaced.Add(gridPlaced);

          //set unavail spr
          placementController.getGridGO(placementController.placement.currentGridX, placementController.placement.currentGridY).GetComponent<GridSquare>().setUnavailable();

          //place furniture
          GameObject.Find("InventoryController").GetComponent<Inventory>().place(furnitureInfo.getFurniture(child.GetComponent<GridSquare>().getFurniture()));
        }
        child.GetComponent<Placement>().enabled = false;
      }
      GameObject.Find("InventoryController").GetComponent<Inventory>().PopupWrap.SetActive(true);

      data.savePlayerData(true);
    }
}
