using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Placement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
  public bool isBeingHeld = false;

  public Vector3 mousePos;
  public int currentGridX;
  public int currentGridY;
  public PlacementController placementController;
  public Furniture thisFurniturePlacing;

    public void OnPointerDown(PointerEventData eventData) {
      mousePos = Input.mousePosition;
      isBeingHeld = true;
      if(placementController.buildMode) {
        placementController.selectingSpace = false;
        placementController.movingMode = true;
        placementController.furniturePlacing = thisFurniturePlacing;
      }
    }
    public void OnPointerUp(PointerEventData eventData) {
      isBeingHeld = false;
      if(placementController.movingMode && placementController.buildMode) {
        placementController.movingMode = false;
        placementController.selectingSpace = true;
      }
    }

    public void translateMouseToGrid() {
      int heightPercentage = Convert.ToInt32(Screen.height/1732.267);
      int tempX = Convert.ToInt32(Math.Ceiling(mousePos.x/(Screen.width/5)));
      int tempY = Convert.ToInt32(Math.Ceiling((mousePos.y-(277.43*heightPercentage))/(Screen.width/5)));
      if(placementController.getGridAvail(tempX, tempY) == true) {
        currentGridX = tempX;
        currentGridY = tempY;
        if(currentGridX < 1) currentGridX = 1;
        if(currentGridX > 5) currentGridX = 5;

        if(currentGridY < 1) currentGridY = 1;
        if(currentGridY > 5) currentGridY = 5;
      }
    }
}
