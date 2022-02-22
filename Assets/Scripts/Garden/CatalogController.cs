using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatalogController : MonoBehaviour
{
    Data data;
    FrogsInfo frogsInfo;
    FurnitureInfo furnitureInfo;

    /* PANEL DECLARATIONS */
    public GameObject pondFrog;
    public GameObject woodFrog;
    public GameObject bullFrog;
    public GameObject tomatoFrog;
    public GameObject flyingFrog;
    public GameObject amazon_milkFrog;
    public GameObject darwinsFrog;
    public GameObject glassFrog;
    public GameObject atelopusFrog;
    public GameObject black_rainFrog;
    public GameObject brownTreeFrog;
    public GameObject whitesTreeFrog;
    public GameObject red_eyedTreeFrog;
    public GameObject grayTreefrog;
    public GameObject white_lippedTreeFrog;
    public GameObject yellow_headedDartFrog;
    public GameObject golden_poisonDartFrog;
    public GameObject blue_jeans_poisonDartFrog;
    public GameObject blue_poisonDartFrog;
    public GameObject strawberry_poisonDartFrog;

    // Start is called before the first frame update
    void Start()
    {
        frogsInfo = GameObject.FindGameObjectWithTag("FrogsInfo").GetComponent<FrogsInfo>();
        furnitureInfo = GameObject.FindGameObjectWithTag("FurnitureInfo").GetComponent<FurnitureInfo>();
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;

        foreach(string frogName in data.frogNames) {
            GameObject frogPanel = getFrog(frogName);
            Transform nameObj = frogPanel.transform.Find("Name");
            TMP_Text name = nameObj.GetComponent<TMP_Text>();
            name.text = frogsInfo.getFrog(frogName).getName();

            Transform descObj = frogPanel.transform.Find("Description");
            TMP_Text desc = descObj.GetComponent<TMP_Text>();
            desc.text = frogsInfo.getFrog(frogName).getDescription();

            Transform spriteObj = frogPanel.transform.Find("Image");
            spriteObj.GetComponent<Image> ().sprite = frogsInfo.getSprite(frogName);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    GameObject getFrog(string frogType) {
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
}
