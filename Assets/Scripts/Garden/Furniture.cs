using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furniture
{
    string name;
    string category;
    bool owned;
    bool placed;
    int price;
    string theme;

    public Furniture(string furnitureName) {
        name = furnitureName;
        owned = false;
        placed = false;
        theme = "basic";
        switch(furnitureName) {
            case "Water Cup":
                category = "Water";
                price = 50;
                break;
            case "Puddle":
                category = "Water";
                price = 1000; //1,000
                break;
            case "Pond":
                category = "Water";
                price = 12000; //25,000
                break;
            case "Swimming Pool":
                category = "Water";
                price = 1000000; //300,000
                break;
            case "Creek":
                category = "Water";
                price = 1000000; //1,000,000
                break;
            case "Flower Pot":
                category = "Foliage";
                price = 10000; //10,000
                break;
            case "Stick":
                category = "Foliage";
                price = 120000; //120,000
                break;
            case "Tree":
                category = "Foliage";
                price = 500000; //500,000
                break;
            case "Lily Pad":
                category = "Water Decorations";
                price = 1000; //1,000
                break;
            case "Toadstool":
                category = "Water Decorations";
                price = 2000; //2,000
                break;
            case "Water Spout":
                category = "Water Decorations";
                price = 24000; //24,000
                break;
            case "Pond Stone":
                category = "Water Decorations";
                price = 300000; //300,000
                break;
            case "Fountain":
                category = "Water Decorations";
                price = 1000000; //1,000,000
                break;
            case "Bridge":
                category = "Water Decorations";
                price = 500000; //500,000
                break;
            case "Lantern":
                category = "Foliage Decorations";
                price = 10000; //10,000
                break;
            case "Tree Swing":
                category = "Foliage Decorations";
                price = 150000; //150,000
                break;
            case "Dragonfly Decoration":
                category = "Foliage Decorations";
                price = 1000000; //1,000,000
                break;
            case "Butterfly Decoration":
                category = "Foliage Decorations";
                price = 1000000; //1,000,000
                break;
            case "Diamond Decoration":
                category = "Foliage Decorations";
                price = 1000000; //1,000,000
                break;
            case "Hidey Hole":
                category = "Foliage Decorations";
                price = 10000000; //10,000,000
                break;
            case "Bench":
                category = "Other";
                price = 250000; //250,000
                break;
            case "Side Table":
                category = "Other";
                price = 100000; //100,000
                break;
            case "Long Table":
                category = "Other";
                price = 250000; //250,000
                break;
            case "Path":
                category = "Other";
                price = 100000; //100,000
                break;
            case "Arch":
                category = "Other";
                price = 250000; //250,000
                break;
            case "Humidifier":
                category = "Other";
                price = 2500000; //2,500,000
                break;
            case "Mister":
                category = "Other";
                price = 2500000; //2,500,000
                break;
            default:
                category = "UNDEFINED";
                price = 0;
                break;
        }
    }

    // gets
    public string getCategory() { return category; }
    public int getPrice() { return price; }
    public string getName() { return name; }
    public bool getOwned() { return owned; }
    public bool getPlaced() { return placed; }
    public string getTheme() { return theme; }

    // sets
    public void setOwned() { owned = true; }
    public void setPlaced(bool setPlaced) { placed = setPlaced; }
}
