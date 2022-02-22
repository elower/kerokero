using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FurnitureInfo : MonoBehaviour
{
    Data data;
    FrogsInfo frogsInfo;


    /* SPRITE DECLARATIONS */
    public Sprite Water_CupSpr;
    public Sprite PuddleSpr;
    // public Sprite PondSpr;
    // public Sprite Swimming_PoolSpr;
    // public Sprite CreekSpr;
    // public Sprite Flower_PotSpr;
    // public Sprite StickSpr;
    // public Sprite TreeSpr;
    // public Sprite Lily_PadSpr;
    // public Sprite ToadstoolSpr;
    // public Sprite Water_SpoutSpr;
    // public Sprite Pond_StoneSpr;
    // public Sprite FountainSpr;
    // public Sprite BridgeSpr;
    // public Sprite LanternSpr;
    // public Sprite Tree_SwingSpr;
    // public Sprite Dragonfly_DecorationSpr;
    // public Sprite Butterfly_DecorationSpr;
    // public Sprite Diamond_DecorationSpr;
    // public Sprite Hidey_HoleSpr;
    // public Sprite BenchSpr;
    // public Sprite Side_TableSpr;
    // public Sprite Long_TableSpr;
    // public Sprite PathSpr;
    // public Sprite ArchSpr;
    // public Sprite HumidifierSpr;
    // public Sprite MisterSpr;


    /*Furniture Declarations*/
    Furniture Water_Cup = new Furniture("Water Cup");
    Furniture Puddle = new Furniture("Puddle");
    Furniture Pond = new Furniture("Pond");
    Furniture Swimming_Pool = new Furniture("Swimming Pool");
    Furniture Creek = new Furniture("Creek");
    Furniture Flower_Pot = new Furniture("Flower Pot");
    Furniture Stick = new Furniture("Stick");
    Furniture Tree = new Furniture("Tree");
    Furniture Lily_Pad = new Furniture("Lily Pad");
    Furniture Toadstool = new Furniture("Toadstool");
    Furniture Water_Spout = new Furniture("Water Spout");
    Furniture Pond_Stone = new Furniture("Pond Stone");
    Furniture Fountain = new Furniture("Fountain");
    Furniture Bridge = new Furniture("Bridge");
    Furniture Lantern = new Furniture("Lantern");
    Furniture Tree_Swing = new Furniture("Tree Swing");
    Furniture Dragonfly_Decoration = new Furniture("Dragonfly Decoration");
    Furniture Butterfly_Decoration = new Furniture("Butterfly Decoration");
    Furniture Diamond_Decoration = new Furniture("Diamond Decoration");
    Furniture Hidey_Hole = new Furniture("Hidey Hole");
    Furniture Bench = new Furniture("Bench");
    Furniture Side_Table = new Furniture("Side Table");
    Furniture Long_Table = new Furniture("Long Table");
    Furniture Path = new Furniture("Path");
    Furniture Arch = new Furniture("Arch");
    Furniture Humidifier = new Furniture("Humidifier");
    Furniture Mister = new Furniture("Mister");


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
        frogsInfo = GameObject.FindGameObjectWithTag("FrogsInfo").GetComponent<FrogsInfo>();
    }
    public Furniture getFurniture(string furnitureName) {
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
    public Sprite getSprite(string furnitureName) {
        switch(furnitureName) {
            case "Water Cup":
                return Water_CupSpr;
            case "Puddle":
                return PuddleSpr;
            // case "Pond":
            //     return PondSpr;
            // case "Swimming Pool":
            //     return Swimming_PoolSpr;
            // case "Creek":
            //     return CreekSpr;
            // case "Flower Pot":
            //     return Flower_PotSpr;
            // case "Stick":
            //     return StickSpr;
            // case "Tree":
            //     return TreeSpr;
            // case "Lily Pad":
            //     return Lily_PadSpr;
            // case "Toadstool":
            //     return ToadstoolSpr;
            // case "Water Spout":
            //     return Water_SpoutSpr;
            // case "Pond Stone":
            //     return Pond_StoneSpr;
            // case "Fountain":
            //     return FountainSpr;
            // case "Bridge":
            //     return BridgeSpr;
            // case "Lantern":
            //     return LanternSpr;
            // case "Tree Swing":
            //     return Tree_SwingSpr;
            // case "Dragonfly Decoration":
            //     return Dragonfly_DecorationSpr;
            // case "Butterfly DecorationSpr":
            //     return Butterfly_DecorationSpr;
            // case "Diamond Decoration":
            //     return Diamond_DecorationSpr;
            // case "Hidey Hole":
            //     return Hidey_HoleSpr;
            // case "Bench":
            //     return BenchSpr;
            // case "Side Table":
            //     return Side_TableSpr;
            // case "Long Table":
            //     return Long_TableSpr;
            // case "Path":
            //     return PathSpr;
            // case "Arch":
            //     return ArchSpr;
            // case "Humidifier":
            //     return HumidifierSpr;
            // case "Mister":
            //     return MisterSpr;
            default:
                return Water_CupSpr;
        }
    }
    public bool gardenContains(string furnitureName) {
        return data.furniturePlaced.Contains(furnitureName);
    }

    public List<string> getPossibleSpawns(string furnitureName) {
        List<string> frogsList = new List<string>();

        switch(furnitureName) {
            case "Water Cup":
                frogsList.Add("Pond");
                frogsList.Add("Wood");
                frogsList.Add("Tomato");
                break;
            case "Puddle":
                frogsList.Add("Wood");
                frogsList.Add("Tomato");
                break;
            case "Pond":
                frogsList.Add("Pond");
                frogsList.Add("Bullfrog");
                frogsList.Add("Tomato");
                break;
            case "Swimming Pool":
                frogsList.Add("Amazon_Milk");
                frogsList.Add("Atelopus");
                break;
            case "Creek":
                frogsList.Add("Pond");
                frogsList.Add("Bullfrog");
                break;
            case "Flower Pot":
                frogsList.Add("Darwins");
                frogsList.Add("Brown");
                frogsList.Add("Whites");
                frogsList.Add("White_Lipped");
                break;
            case "Stick":
                frogsList.Add("Darwins");
                frogsList.Add("Brown");
                frogsList.Add("Whites");
                frogsList.Add("Gray");
                frogsList.Add("White_Lipped");
                break;
            case "Tree":
                frogsList.Add("Flying");
                frogsList.Add("Amazon_Milk");
                frogsList.Add("Atelopus");
                frogsList.Add("Brown");
                frogsList.Add("Whites");
                frogsList.Add("Red_Eyed");
                frogsList.Add("Gray");
                frogsList.Add("White_Lipped");
                break;
            case "Lily Pad":
                frogsList.Add("Pond");
                if(gardenContains("Humidifier") && gardenContains("Lily Pad"))
                    frogsList.Add("Glass");
                break;
            case "Toadstool":
                frogsList.Add("Pond");
                break;
            case "Water Spout":
                if(gardenContains("Humidifier") && gardenContains("Lily Pad"))
                    frogsList.Add("Glass");
                frogsList.Add("Black_Rain");
                break;
            case "Pond Stone":
                frogsList.Add("Pond");
                break;
            case "Fountain":
                if(gardenContains("Humidifier") && gardenContains("Lily Pad"))
                    frogsList.Add("Glass");
                frogsList.Add("Black_Rain");
                break;
            case "Bridge":
                frogsList.Add("Wood");
                frogsList.Add("Tomato");
                if(gardenContains("Dragonfly_Decoration") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Golden_Poison");
                break;
            case "Lantern":
                frogsList.Add("Brown");
                if(gardenContains("Lantern") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Yellow_Headed");
                break;
            case "Tree Swing":
                frogsList.Add("Flying");
                if(gardenContains("Tree_Swing") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Blue_Jeans_Poison");
                break;
            case "Dragonfly Decoration":
                frogsList.Add("Whites");
                frogsList.Add("White_Lipped");
                if(gardenContains("Dragonfly_Decoration") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Golden_Poison");
                break;
            case "Butterfly Decoration":
                if(gardenContains("Tree_Swing") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Blue_Jeans_Poison");
                if(gardenContains("Butterfly_Decoration") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Strawberry_Poison");
                break;
            case "Diamond Decoration":
                frogsList.Add("Wood");
                frogsList.Add("Atelopus");
                if(gardenContains("Diamond_Decoration") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Blue_Poison");
                break;
            case "Hidey Hole":
                frogsList.Add("Atelopus");
                frogsList.Add("Brown");
                frogsList.Add("Whites");
                frogsList.Add("Red_Eyed");
                frogsList.Add("Gray");
                frogsList.Add("White_Lipped");
                if(gardenContains("Diamond_Decoration") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Blue_Poison");
                break;
            case "Bench":
                frogsList.Add("Pond");
                frogsList.Add("Bullfrog");
                frogsList.Add("Tomato");
                frogsList.Add("Amazon_Milk");
                if(frogsInfo.getFrog("Darwins").getOwned() == true)
                    frogsList.Add("Darwins");
                break;
            case "Side Table":
                if(frogsInfo.getFrog("Darwins").getOwned() == true)
                    frogsList.Add("Darwins");
                frogsList.Add("Black_Rain");
                break;
            case "Long Table":
                frogsList.Add("Bullfrog");
                frogsList.Add("Amazon_Milk");
                break;
            case "Path":
                frogsList.Add("Bullfrog");
                frogsList.Add("Black_Rain");
                break;
            case "Arch":
                frogsList.Add("Wood");
                frogsList.Add("Bullfrog");
                frogsList.Add("Brown");
                frogsList.Add("Whites");
                frogsList.Add("Red_Eyed");
                frogsList.Add("Gray");
                frogsList.Add("White_Lipped");
                if(gardenContains("Tree_Swing") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Blue_Jeans_Poison");
                break;
            case "Humidifier":
                frogsList.Add("Tomato");
                if(gardenContains("Humidifier") && gardenContains("Lily Pad"))
                    frogsList.Add("Glass");
                frogsList.Add("Atelopus");
                if(gardenContains("Lantern") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Yellow_Headed");
                if(gardenContains("Dragonfly_Decoration") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Golden_Poison");
                if(gardenContains("Tree_Swing") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Blue_Jeans_Poison");
                if(gardenContains("Diamond_Decoration") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Blue_Poison");
                if(gardenContains("Butterfly_Decoration") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Strawberry_Poison");
                break;
            case "Mister":
                if(gardenContains("Lantern") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Yellow_Headed");
                if(gardenContains("Dragonfly_Decoration") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Golden_Poison");
                if(gardenContains("Tree_Swing") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Blue_Jeans_Poison");
                if(gardenContains("Diamond_Decoration") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Blue_Poison");
                if(gardenContains("Butterfly_Decoration") && gardenContains("Humidifier") && gardenContains("Mister"))
                    frogsList.Add("Strawberry_Poison");
                break;
            default:
                break;
        }
        return frogsList;
    }

    //spawn frog on top of a piece of furniture
    public void spawnFrog(Furniture furnitureObj, GardenController gardenController, GameObject area) {
        List<string> frogsList = getPossibleSpawns(furnitureObj.getName());
        int rarity;
        int spawnChance; //percentage
        bool spawned = false;
        foreach(string frog in frogsList) {
            if(spawned == false) {
                Frog frogObj = frogsInfo.getFrog(frog);
                rarity = frogObj.getRarity();
                switch(rarity){
                    case 1:
                        spawnChance = 50;
                        break;
                    case 2:
                        spawnChance = 45;
                        break;
                    case 3:
                        spawnChance = 30;
                        break;
                    case 4:
                        spawnChance = 25;
                        break;
                    case 5:
                        spawnChance = 10;
                        break;
                    default:
                        spawnChance = 0;
                        break;
                }
                int spawn = UnityEngine.Random.Range(0, 99);
                if(spawn < spawnChance) {
                    //spawn frog on furniture

                    area.GetComponent<GridSquare>().setFrogSpawn(frog);
                    spawned = true;

                    //make corresponding furniture remove button uninteractable
                    GameObject.Find("InventoryController").GetComponent<Inventory>().getFurniturePanel(furnitureObj.getName()).transform.Find("placeOrNotOwned").GetComponent<Button>().interactable = false;
                }
            } else break;
        }
    }
    public void getNewFrog(string frog, Transform frogSpawn) {
        frogSpawn.transform.parent.GetComponent<GridSquare>().removeFrog();

        frogsInfo.getNewFrog(frog);
        //got new frog, set next spawn to random datetime between 5 and 20 minutes from now

        float minutesToWait = UnityEngine.Random.Range(5, 20);
        //float minutesToWait = 0;
        DateTime nextSpawn = DateTime.UtcNow.AddMinutes(minutesToWait);

        data.nextSpawn[Array.IndexOf(data.furnitureNames, frogSpawn.transform.parent.GetComponent<GridSquare>().getFurniture())] = nextSpawn;
    }
}
