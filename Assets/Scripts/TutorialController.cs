using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public GameObject[] popups;
    public GameObject popupBG;
    public GameObject popupButton;
    public GameObject arrow;

    FrogsInfo frogsInfo;
    public int tutorialIdx = 0;

    Button fetchButton;
    Button menuButton;
    Button tutStoreButton;
    Button tutBuyButton;
    Button tutInvButton;
    Button tutPlaceButton;

    UnityAction waitToAdvanceAction;
    UnityAction advanceAction;
    Data data;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
        hideArrow(false, false);
        for(int i = 0; i < popups.Length; i++) {
            popups[i].SetActive(false);
        }

        //set needed vars

        waitToAdvanceAction = () => waitToAdvanceFunc();
        advanceAction = () => advanceTutorial();
        startTutorial();
    }
    public void startTutorial() {
        //check which scene is loaded
        if(SceneManager.GetActiveScene().name == "Garden") {
            tutStoreButton = GameObject.Find("Shop_Button").GetComponent<Button>();
            tutInvButton = GameObject.Find("Inventory_Button").GetComponent<Button>();
            frogsInfo = GameObject.FindGameObjectWithTag("FrogsInfo").GetComponent<FrogsInfo>();
            tutorialIdx = 11; //12-1 for advanceTutorial
        } else if(SceneManager.GetActiveScene().name == "FrogsList") {
            if(data.tutorial == 0) tutorialIdx = -1; //0-1 for advanceTutorial
            else tutorialIdx = 22; //23 - 1 for advanceTutorial
            GameObject pondFrogPanel = GameObject.Find("Pond");
            fetchButton = pondFrogPanel.transform.Find("Fetch").GetComponent<Button>();
            menuButton = GameObject.Find("MenuBtn").GetComponent<Button>();
        }
        advanceTutorial();
    }

    // Update is called once per frame
    public void advanceTutorial() {
        tutorialIdx++;
        for(int i = 0; i < popups.Length; i++) {
            popups[i].SetActive(false);
        }

        switch(tutorialIdx) {
            case 0:
                popups[0].SetActive(true);
                break;
            case 1: // this is your frogs list
                popups[1].SetActive(true);
                break;
            case 2: // point to pond frog
                StartCoroutine(showArrow(700f, 1700f, true, false, 2f, false));
                break;
            case 3: //look at that! you already have a pond frog
                popups[2].SetActive(true);
                break;
            case 4:
                StartCoroutine(showArrow(700f, 1575f, true, false, 2f, false));
                break;
            case 5: //what's this? wants to fetch
                popups[3].SetActive(true);
                break;
            case 6: // close popup, wait for user to press 'Fetch' Button
                StartCoroutine(showArrow(700f, 1575f, true, false, 0f, true));
                //add listener to fetch button to advance tutorial
                fetchButton.onClick.AddListener(waitToAdvanceAction);
                break;
            case 7: //user has pressed the fetch button, which calls 'Wait To Advance'. Cool! Your pond frog went exploring
                fetchButton.onClick.RemoveListener(waitToAdvanceAction);
                popups[4].SetActive(true);
                break;
            case 8: // arrow points to Timer
                StartCoroutine(showArrow(700f, 1650f, true, false, 1.5f, false));
                break;
            case 9: // Did you see this timer?
                popups[5].SetActive(true);
                break;
            case 10: // Let's head to your garden!
                popups[6].SetActive(true);
                break;
            case 11: //arrow points to garden
                menuButton.onClick.AddListener(advanceAction);
                closeAllPopups(true);
                arrow.SetActive(true);
                setArrowPos(575f, 350f, true, true);
                break;
            case 12: //This is your garden 1
                popups[7].SetActive(true);
                break;
            case 13: //This is your garden 2
                popups[8].SetActive(true);
                break;
            case 14: //arrow points to store
                data.coins = 50;
                closeAllPopups(true);
                arrow.SetActive(true);
                setArrowPos(200f, 600f, true, true);
                tutStoreButton.onClick.AddListener(advanceAction);
                break;
            case 15: //arrow points to buy
                arrow.SetActive(true);
                resetArrow(true, true);
                closeAllPopups(true);
                setArrowPos(650f, 950f, false, true);
                tutStoreButton.onClick.RemoveListener(advanceAction);

                GameObject storeWaterCup = GameObject.Find("StoreController").GetComponent<Store>().Water_Cup;
                tutBuyButton = storeWaterCup.transform.Find("ownedOrBuy").GetComponent<Button>();
                tutBuyButton.onClick.AddListener(advanceAction);
                break;
            case 16: //arrow points to inventory
                resetArrow(false, true);
                setArrowPos(600f, 1750f, false, true);
                tutBuyButton.onClick.RemoveListener(advanceAction);
                tutInvButton.onClick.AddListener(advanceAction);

                //enable inv button
                GameObject.Find("Inventory_Button").GetComponent<Button>().enabled = true;

                //disable inv scroll
                GameObject.Find("Inventory").transform.Find("ScrollList").GetComponent<ScrollRect>().enabled = false;
                break;
            case 17://arrow points to place
                arrow.SetActive(true);
                closeAllPopups(true);
                resetArrow(false, true);
                setArrowPos(650f, 950f, true, true);

                GameObject invWaterCup = GameObject.Find("InventoryController").GetComponent<Inventory>().Water_Cup;
                tutPlaceButton = invWaterCup.transform.Find("placeOrNotOwned").GetComponent<Button>();
                tutPlaceButton.onClick.AddListener(advanceAction);
                break;
            case 18: //arrow points to grid place
                resetArrow(true, false);
                setArrowPos(650f, 900f, true, false);
                //set all areas unavail except correct square
                foreach(Transform child in GameObject.Find("Grid").transform) {
                  Color temp = child.GetComponent<Image> ().color;
                  temp.a=1.0f;
                  child.GetComponent<Image> ().color = temp;
                  child.GetComponent<Placement>().enabled = false;
                  child.GetComponent<Image>().sprite = GameObject.Find("PlacementController").GetComponent<PlacementController>().unavailSpr;
                }
                GameObject.Find("PlacementController").GetComponent<PlacementController>().getGridGO(2, 3).GetComponent<Image>().sprite = GameObject.Find("PlacementController").GetComponent<PlacementController>().availSpr;
                GameObject.Find("PlacementController").GetComponent<PlacementController>().getGridGO(2, 3).GetComponent<Placement>().enabled = true;
                GameObject.Find("SaveButton").GetComponent<Button>().onClick.AddListener(advanceAction);
                break;
            case 19: //menu is closed, load popup: Nice! You placed the Water Cup in your garden
                GameObject.Find("GardenMenus").SetActive(false);
                GameObject area = GameObject.Find("PlacementController").GetComponent<PlacementController>().getGridGO(2, 3);

                Transform FrogSpawn = area.transform.Find("FrogSpawn");
                FrogSpawn.gameObject.SetActive(true);
                Button frogButton = FrogSpawn.GetComponent<Button>();
                Sprite frogSprite = frogsInfo.getSprite("Pond");
                FrogSpawn.GetComponent<Image> ().sprite = frogSprite;
                Color colorTemp = FrogSpawn.GetComponent<Image> ().color;
                colorTemp.a = 1.0f;
                FrogSpawn.GetComponent<Image> ().color = colorTemp;

                frogButton.interactable = true;
                frogButton.onClick.AddListener(advanceAction);
                frogButton.onClick.AddListener(delegate { getNewFrog(frogsInfo, "Pond", FrogSpawn, frogButton); });

                hideArrow(true, true);
                openAllPopups();
                popups[9].SetActive(true);
                break;
            case 20: //hide popup to show frog
                arrow.SetActive(true);
                closeAllPopups(true);
                setArrowPos(650f, 1000f, true, false);
                break;
            case 21: //user has clicked on frog, now let's head to your frogs list
                data.tutorial = 1; //second step in tutorial
                hideArrow(true, true);
                openAllPopups();
                popups[10].SetActive(true);
                break;
            case 22:
                SceneManager.LoadScene("FrogsList");
                break;
            case 23:
                popups[11].SetActive(true);
                break;
            case 24:
                popups[12].SetActive(true);
                break;
            case 25:
                data.tutorial = 2;
                data.savePlayerData(false);
                Destroy(GameObject.FindGameObjectWithTag("Tutorial"));
                SceneManager.UnloadSceneAsync("Tutorial");
                break;
        }

    }
    void openAllPopups() {
        popupBG.SetActive(true);
        popupButton.SetActive(true);
        Color temp = popupBG.GetComponent<Image> ().color;
        temp.a=0.3f;
        popupBG.GetComponent<Image> ().color = temp;
    }
    void closeAllPopups(bool deactivateBG) {
        for(int i = 0; i < popups.Length; i++) {
            popups[i].SetActive(false);
        }
        if(deactivateBG) {
            popupBG.SetActive(false);
            switch(tutorialIdx) {
              case 6: //wait for fetch press
                //disable scroll
                GameObject.Find("Frog Scroll").GetComponent<ScrollRect> ().enabled = false;

                //disable popup
                GameObject.Find("Popout_Wrap").GetComponent<Button> ().enabled = false;

                //disable main menu
                GameObject.Find("MenuBtn").GetComponent<Button> ().enabled = false;

                //disable garden button
                GameObject.Find("BottomMenu").GetComponent<Button> ().enabled = false;

                //disable expand hex
                GameObject.Find("Pond").transform.Find("Hex").GetComponent<Animator>().enabled = false;
                break;
              case 11: //wait for garden button press
                //disable fetch
                GameObject.Find("Pond").transform.Find("Fetch").GetComponent<Button>().enabled = false;

                //enable garden button
                GameObject.Find("BottomMenu").GetComponent<Button> ().enabled = true;
                break;
              case 14: //wait for user to press store
                //disable popup
                GameObject.Find("Popout_Wrap").GetComponent<Button> ().enabled = false;

                //disable main menu
                GameObject.Find("MenuBtn").GetComponent<Button> ().enabled = false;

                //disable frogslist button
                GameObject.Find("BottomMenu").GetComponent<Button> ().enabled = false;

                //disable inventory button
                GameObject.Find("Inventory_Button").GetComponent<Button>().enabled = false;

                //disable build mode button
                GameObject.Find("BuildMode_Button").GetComponent<Button>().enabled = false;
                break;
              case 15:
                //disable store button
                GameObject.Find("Shop_Button").GetComponent<Button>().enabled = false;

                //disable shop scroll
                GameObject.Find("Store").transform.Find("ScrollList").GetComponent<ScrollRect>().enabled = false;
                break;
              case 17:
                //disable inventory button
                GameObject.Find("Inventory_Button").GetComponent<Button>().enabled = false;
                break;
            }
        }
        else { //otherwise just lower the opacity
            Color temp = popupBG.GetComponent<Image> ().color;
            temp.a=.001f;
            popupBG.GetComponent<Image> ().color = temp;

        }
        popupButton.SetActive(false);
    }
    IEnumerator showArrow(float posX, float posY, bool flipX, bool flipY, float timeWait, bool deactivateBG) {
        closeAllPopups(deactivateBG);
        arrow.SetActive(true);
        setArrowPos(posX, posY, flipX, flipY);
        if(timeWait != 0) {
            yield return new WaitForSeconds(timeWait);
            hideArrow(flipX, flipY);
            advanceTutorial();
            openAllPopups();
        }
    }
    void hideArrow(bool flipX, bool flipY) {
        arrow.SetActive(false);
        resetArrow(flipX, flipY);

    }
    void setArrowPos(float posX, float posY, bool flipX, bool flipY) {
        Vector3 arrowScale = arrow.transform.localScale;


        arrow.transform.position = new Vector3(posX,posY,0);
        if(flipX) {
            arrowScale.x *= -1;
            arrow.transform.localScale = arrowScale;
        }
        if(flipY) {
            arrow.transform.Rotate(new Vector3(0, 0, 90));
        }
    }
    void resetArrow(bool flipX, bool flipY) {
        arrow.transform.position = new Vector3(0,0,0);

        //if it was previously flipped
        if(flipX) {
            Vector3 arrowScale = arrow.transform.localScale;
            arrowScale.x *= -1;
            arrow.transform.localScale = arrowScale;
        }
        if(flipY) {
            arrow.transform.Rotate(new Vector3(0, 0, 0));
        }
    }

    IEnumerator waitToAdvance(float timeWait) {
        yield return new WaitForSeconds(timeWait);
        advanceTutorial();
        hideArrow(true, false);
        openAllPopups();
    }
    void waitToAdvanceFunc() {
        StartCoroutine(waitToAdvance(2f));
    }

    void getNewFrog(FrogsInfo frogsInfo, string frog, Transform frogSpawn, Button frogButton) {
        frogButton.interactable = false;
        Color temp = frogSpawn.GetComponent<Image> ().color;
        temp.a=0f;
        frogSpawn.GetComponent<Image> ().color = temp;

        frogsInfo.getNewFrog(frog);
    }
}
