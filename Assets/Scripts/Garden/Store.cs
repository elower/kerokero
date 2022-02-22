using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Store : MonoBehaviour
{
    public GardenController gardenController;
    Data data;
    FurnitureInfo furnitureInfo;

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
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
        furnitureInfo = GameObject.FindGameObjectWithTag("FurnitureInfo").GetComponent<FurnitureInfo>();
        GameObject storeWrap = GameObject.Find("Store");
        setItems();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void setItems() {
      foreach(string furnitureName in data.furnitureNames) {
          GameObject storeFurniturePanel = getFurniturePanel(furnitureName);
          Transform ownedOrBuy = storeFurniturePanel.transform.Find("ownedOrBuy");
          Button ownedBuyBtn = ownedOrBuy.GetComponent<Button>();
          Sprite ownedBuySpr;
          Transform textObj = storeFurniturePanel.transform.Find("Price");
          Transform nameObj = storeFurniturePanel.transform.Find("Name");
          TMP_Text price = textObj.GetComponent<TMP_Text>();
          TMP_Text name = nameObj.GetComponent<TMP_Text>();
          Image furnitureImage = storeFurniturePanel.transform.Find("Image").GetComponent<Image>();
          furnitureImage.sprite = furnitureInfo.getSprite(furnitureName);
          price.text = data.coinsFormatter(furnitureInfo.getFurniture(furnitureName).getPrice()) + " Coins";
          name.text = furnitureName;
          if(furnitureInfo.getFurniture(furnitureName).getOwned() == true)
              ownedBuySpr = gardenController.owned;
          else {
              ownedBuySpr = gardenController.buy;
              //set button if have enough coins
              ownedBuyBtn.onClick.RemoveAllListeners();
              if(furnitureInfo.getFurniture(furnitureName).getPrice() <= data.coins) {
                  ownedBuyBtn.interactable = true;
                  ownedBuyBtn.onClick.AddListener(delegate { purchase(furnitureInfo.getFurniture(furnitureName), ownedOrBuy, gardenController.owned); });
              } else {
                ownedBuyBtn.interactable = false;
              }
          }
          ownedOrBuy.GetComponent<Image> ().sprite = ownedBuySpr;
          Color temp = ownedOrBuy.GetComponent<Image> ().color;
          temp.a=1.0f;
          ownedOrBuy.GetComponent<Image> ().color = temp;
      }
    }

    GameObject getFurniturePanel(string furnitureName) {
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
    void purchase(Furniture furniture, Transform ownedOrBuy, Sprite ownedSpr) {
        furniture.setOwned();
        data.coins -= furniture.getPrice();
        data.ownedFurniture.Add(furniture.getName());
        ownedOrBuy.GetComponent<Image> ().sprite = ownedSpr;

        ownedOrBuy.GetComponent<Button>().interactable = false;

        //refresh items in inventory
        GameObject.Find("InventoryController").GetComponent<Inventory>().refreshItems();
        data.savePlayerData(true);
        //refresh all items for new money amount
        setItems();
    }
}
