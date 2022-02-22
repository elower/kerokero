using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    string furniture;
    string frogspawn;
    bool available;
    Color visible = Color.white;
    Color invisible = Color.white;

    FurnitureInfo furnitureInfo;
    FrogsInfo frogsInfo;
    public PlacementController placementController;

    public void setFurniture(string setFurniture) {
      furniture = setFurniture;
      this.transform.Find("FurniturePlaced").GetComponent<Image>().sprite = furnitureInfo.getSprite(setFurniture);
      this.transform.Find("FurniturePlaced").GetComponent<Image>().color = visible;
    }
    public void setFrogSpawn(string setFrogSpawn) {
      frogspawn = setFrogSpawn;
      this.transform.Find("FrogSpawn").GetComponent<Image> ().sprite = frogsInfo.getSprite(setFrogSpawn);
      this.transform.Find("FrogSpawn").GetComponent<Image> ().color = visible;
      this.transform.Find("FrogSpawn").GetComponent<Button>().interactable = true;
      this.transform.Find("FrogSpawn").GetComponent<Button>().onClick.AddListener(delegate { furnitureInfo.getNewFrog(setFrogSpawn, this.transform.Find("FrogSpawn")); });
    }
    public void setAvailable() {
      available = true;
      this.GetComponent<Image>().sprite = placementController.availSpr;
    }
    public void setUnavailable() {
      available = false;
      this.GetComponent<Image>().sprite = placementController.unavailSpr;
    }

    public void removeFurniture() {
      furniture = null;
      this.transform.Find("FurniturePlaced").GetComponent<Image>().sprite = null;
      this.transform.Find("FurniturePlaced").GetComponent<Image>().color = invisible;
    }
    public void removeFrog() {
      frogspawn = null;
      this.transform.Find("FrogSpawn").GetComponent<Image> ().sprite = null;
      this.transform.Find("FrogSpawn").GetComponent<Image> ().color = invisible;
      this.transform.Find("FrogSpawn").GetComponent<Button>().interactable = false;
      this.transform.Find("FrogSpawn").GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public string getFurniture() {return furniture;}
    public string getFrogSpawn() {return frogspawn;}
    public bool getAvailable() {return available;}

    void Start() {
      furniture = null;
      frogspawn = null;
      available = true;
      visible.a=1.0f;
      invisible.a = 0f;

      furnitureInfo = GameObject.FindGameObjectWithTag("FurnitureInfo").GetComponent<FurnitureInfo>();
      frogsInfo = GameObject.FindGameObjectWithTag("FrogsInfo").GetComponent<FrogsInfo>();
    }
}
