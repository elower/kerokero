using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrogsInfo : MonoBehaviour
{
    Data data;
    /* SPRITE DECLARATIONS */
    public Sprite pondSpr;
    public Sprite woodSpr;
    public Sprite bullfrogSpr;
    public Sprite tomatoSpr;
    public Sprite flyingSpr;
    public Sprite amazon_milkSpr;
    public Sprite darwinsSpr;
    public Sprite glassSpr;
    public Sprite atelopusSpr;
    public Sprite black_rainSpr;
    public Sprite brownSpr;
    public Sprite whitesSpr;
    public Sprite red_eyedSpr;
    public Sprite graySpr;
    public Sprite white_lippedSpr;
    public Sprite yellow_headedSpr;
    public Sprite golden_poisonSpr;
    public Sprite blue_jeans_poisonSpr;
    public Sprite blue_poisonSpr;
    public Sprite strawberry_poisonSpr;

    /* FROG DECLARATIONS */
    Frog pondFrog = new Frog("Pond");
    Frog woodFrog = new Frog("Wood");
    Frog bullFrog = new Frog("Bullfrog");
    Frog tomatoFrog = new Frog("Tomato");
    Frog flyingFrog = new Frog("Flying");
    Frog amazon_milkFrog = new Frog("Amazon_Milk");
    Frog darwinsFrog = new Frog("Darwins");
    Frog glassFrog = new Frog("Glass");
    Frog atelopusFrog = new Frog("Atelopus");
    Frog black_rainFrog = new Frog("Black_Rain");
    Frog brownTreeFrog = new Frog("Brown");
    Frog whitesTreeFrog = new Frog("Whites");
    Frog red_eyedTreeFrog = new Frog("Red_Eyed");
    Frog grayTreefrog = new Frog("Gray");
    Frog white_lippedTreeFrog = new Frog("White_Lipped");
    Frog yellow_headedDartFrog = new Frog("Yellow_Headed");
    Frog golden_poisonDartFrog = new Frog("Golden_Poison");
    Frog blue_jeans_poisonDartFrog = new Frog("Blue_Jeans_Poison");
    Frog blue_poisonDartFrog = new Frog("Blue_Poison");
    Frog strawberry_poisonDartFrog = new Frog("Strawberry_Poison");

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
    }

    public Sprite getSprite(string frogType) {
        switch(frogType){
            case "Pond":
                return pondSpr;
            case "Wood":
                return woodSpr;
            case "Bullfrog":
                return bullfrogSpr;
            case "Tomato":
                return tomatoSpr;
            case "Flying":
                return flyingSpr;
            case "Amazon_Milk":
                return amazon_milkSpr;
            case "Darwins":
                return darwinsSpr;
            case "Glass":
                return glassSpr;
            case "Atelopus":
                return atelopusSpr;
            case "Black_Rain":
                return black_rainSpr;
            case "Brown":
                return brownSpr;
            case "Whites":
                return whitesSpr;
            case "Red_Eyed":
                return red_eyedSpr;
            case "Gray":
                return graySpr;
            case "White_Lipped":
                return white_lippedSpr;
            case "Yellow_Headed":
                return yellow_headedSpr;
            case "Golden_Poison":
                return golden_poisonSpr;
            case "Blue_Jeans_Poison":
                return blue_jeans_poisonSpr;
            case "Blue_Poison":
                return blue_poisonSpr;
            case "Strawberry_Poison":
                return strawberry_poisonSpr;
            default:
                return pondSpr;
        }
    }
    public Frog getFrog(string frogType) {
        switch(frogType){
            case "Pond":
                return pondFrog;
            case "Wood":
                return woodFrog;
            case "Bullfrog":
                return bullFrog;
            case "Tomato":
                return tomatoFrog;
            case "Flying":
                return flyingFrog;
            case "Amazon_Milk":
                return amazon_milkFrog;
            case "Darwins":
                return darwinsFrog;
            case "Glass":
                return glassFrog;
            case "Atelopus":
                return atelopusFrog;
            case "Black_Rain":
                return black_rainFrog;
            case "Brown":
                return brownTreeFrog;
            case "Whites":
                return whitesTreeFrog;
            case "Red_Eyed":
                return red_eyedTreeFrog;
            case "Gray":
                return grayTreefrog;
            case "White_Lipped":
                return white_lippedTreeFrog;
            case "Yellow_Headed":
                return yellow_headedDartFrog;
            case "Golden_Poison":
                return golden_poisonDartFrog;
            case "Blue_Jeans_Poison":
                return blue_jeans_poisonDartFrog;
            case "Blue_Poison":
                return blue_poisonDartFrog;
            case "Strawberry_Poison":
                return strawberry_poisonDartFrog;
            default:
                return pondFrog;
        }
    }
    public void getNewFrog(string frog) {
        //if not already owned, add to owned list
        Frog frogObj = getFrog(frog);

        if(frogObj.getOwned() == false) {
            data.ownedFrogs.Add(frog);
            frogObj.setOwned();
        } else { //add mate to frog
            if(frogObj.getMates() == 0) data.newMates[frog] = DateTime.UtcNow.ToString();
            frogObj.setMates(frogObj.getMates()+1);
            data.mates[frog] = frogObj.getMates().ToString();

            if(frogObj.getMates() >= frogObj.getRequiredMates()) {
                if((frogObj.getTime() - (frogObj.getMates() / frogObj.getRequiredMates()) > 1)  && frogObj.getMates() % frogObj.getRequiredMates() == 0)
                    frogObj.setTime(frogObj.getTime() - (frogObj.getMates()/frogObj.getRequiredMates()));
                else if(frogObj.getTime() - (frogObj.getMates() / frogObj.getRequiredMates()) <= 1)
                    frogObj.setTime(1);
            }
        }
        data.savePlayerData(true);
    }
}
