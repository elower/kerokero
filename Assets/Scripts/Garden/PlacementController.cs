using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlacementController : MonoBehaviour
{

  public GameObject[] xRow1;
  public GameObject[] xRow2;
  public GameObject[] xRow3;
  public GameObject[] xRow4;
  public GameObject[] xRow5;

  public GameObject Grid;

  public Sprite availSpr;
  public Sprite unavailSpr;
  public Sprite placingSpr;

  FurnitureInfo furnitureInfo;
  Data data;
  public Placement placement;
  public Furniture furniturePlacing;
  public MenuController menuController;

  public bool buildMode; //build mode
  public bool selectingSpace; //selecting mode
  public bool movingMode; //moving mode
  public bool swapping; //swapping mode
  public bool swap; //enable swap
  bool popupOpened;
  public string furnitureSwapping;

    void Start()
    {
      furnitureInfo = GameObject.FindGameObjectWithTag("FurnitureInfo").GetComponent<FurnitureInfo>();
      data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
      //set unavail spr to all spaces with placed furniture
      foreach(int grid in data.gridPlaced) {
        int gridX = grid/10;
        int gridY = grid % 10;
        getGridGO(gridX, gridY).GetComponent<GridSquare>().setUnavailable();
      }
    }
    public void enableSwap() {swap = true;}
    public void setPopupOpened() {popupOpened = false;}
    public void cancelSwap() {
      getGridGO(placement.currentGridX, placement.currentGridY).GetComponent<GridSquare>().setAvailable();
      getGridGO(placement.currentGridX, placement.currentGridY).GetComponent<GridSquare>().setFurniture(furnitureSwapping);
    }
    void Update()
    {
      foreach(Transform child in Grid.transform) {
        if(child.GetComponent<Placement>().isBeingHeld == true) {
          placement = child.GetComponent<Placement>();
          break;
        }
      }
      if(buildMode) {
        foreach(Transform child in Grid.transform) {
          if(child.GetComponent<GridSquare>().getFurniture() != null)
            child.GetComponent<Placement>().thisFurniturePlacing = furnitureInfo.getFurniture(child.GetComponent<GridSquare>().getFurniture());
          else child.GetComponent<Placement>().thisFurniturePlacing = null;
          if(selectingSpace) {
            if(child.GetComponent<GridSquare>().getFurniture() != null) {
              child.GetComponent<Image>().sprite = placingSpr;
              child.GetComponent<Placement>().enabled = true;
            }
            else {
              child.GetComponent<GridSquare>().setAvailable();
              child.GetComponent<Placement>().enabled = false;
            }
          } else if(movingMode) {
            if(child.GetComponent<GridSquare>().getFurniture() != null) {
              if(child.GetComponent<GridSquare>().getFurniture() != furniturePlacing.getName())
                child.GetComponent<GridSquare>().setUnavailable();
              else if(child == getGridGO(placement.currentGridX, placement.currentGridY))
                child.GetComponent<Image>().sprite = placingSpr;
              else {
                child.GetComponent<GridSquare>().setAvailable();
              }
            }
            else
              child.GetComponent<GridSquare>().setAvailable();
          }
        }
      } else if (swapping) {
        if(placement && placement.isBeingHeld) {
          placement.mousePos = Input.mousePosition;
          placement.translateMouseToGrid();
          int gridIdx = (placement.currentGridX*10)+placement.currentGridY;
          furnitureSwapping = data.furniturePlaced[data.gridPlaced.IndexOf(gridIdx)];
          if(!popupOpened) {
            menuController.openPopup("SwapFurniture");
            popupOpened = true;
          }
        }
      }

      if(!buildMode || (buildMode && movingMode) || (swapping && swap)) {
        if(placement && placement.isBeingHeld) {
          placement.mousePos = Input.mousePosition;
          placement.translateMouseToGrid();
          Color temp = xRow1[0].transform.Find("FurniturePlaced").GetComponent<Image> ().color;
          temp.a=0f;

          foreach(Transform child in Grid.transform) {
            if(!swapping && child.GetComponent<GridSquare>().getAvailable()) {
              child.GetComponent<GridSquare>().removeFurniture();
              child.GetComponent<GridSquare>().setAvailable();
            }
          }
          GameObject selected = getGridGO(placement.currentGridX, placement.currentGridY);
          if(selected.GetComponent<Image>().sprite == availSpr) {
            selected.GetComponent<Image>().sprite = placingSpr;
            selected.GetComponent<GridSquare>().setFurniture(furniturePlacing.getName());
          }
        }
      }
    }

    public GameObject getGridGO(int x, int y) {
      x -= 1;
      switch(y) {
        case 1:
          return xRow5[x];
        case 2:
          return xRow4[x];
        case 3:
          return xRow3[x];
        case 4:
          return xRow2[x];
        case 5:
          return xRow1[x];
        default:
          return null;
      }
    }
    public int getGridCoords(GameObject go) {
      int x = 1;
      int y = 1;
      if(Array.IndexOf(xRow5, go) != -1) {
        y = 1;
        x = Array.IndexOf(xRow5, go);
      }
      else if(Array.IndexOf(xRow4, go) != -1) {
        y = 2;
        x = Array.IndexOf(xRow4, go);
      }
      else if(Array.IndexOf(xRow3, go) != -1) {
        y = 3;
        x = Array.IndexOf(xRow3, go);
      }
      else if(Array.IndexOf(xRow2, go) != -1) {
        y = 4;
        x = Array.IndexOf(xRow2, go);
      }
      else if(Array.IndexOf(xRow1, go) != -1) {
        y = 5;
        x = Array.IndexOf(xRow1, go);
      }
      x += 1;
      return (10*x)+y;
    }
    public bool getGridAvail(int gridX, int gridY) {
      if(gridX < 1) gridX = 1;
      if(gridX > 5) gridX = 5;

      if(gridY < 1) gridY = 1;
      if(gridY > 5) gridY = 5;
      return getGridGO(gridX, gridY).GetComponent<GridSquare>().getAvailable();
    }
}
