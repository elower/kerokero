using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    Transition transitionController;
    public Animator mainMenuAnimation;
    public Animator popoutMenuAnimation;
    public Animator gardenMenuAnimation;
    public GameObject gardenMenuClose;
    public bool PopoutIsOpened = false;
    public string gardenMenuOpened = null;
    public bool popupOpened;

    // Garden Menu first panel headers
    public Sprite waterSpr;
    public Sprite basicSpr;

    void Start() {
        transitionController = GameObject.FindGameObjectWithTag("TransitionController").GetComponent<Transition>();
    }
    void Update() {
      if(SceneManager.GetSceneByName("Popup").isLoaded) popupOpened = true;
      else popupOpened = false;
    }
    public void goToScene(string sceneName) {
        //play end
        StartCoroutine(loadNewScene(sceneName));
    }
    IEnumerator loadNewScene(string sceneName) {
        //if menu is open, make it invisible to show transition
        if(SceneManager.GetSceneByName("Menu").isLoaded) {
            CanvasGroup menuCanvas = GameObject.Find("MenuCanvas").GetComponent<CanvasGroup>();
            menuCanvas.alpha = 0f;
        }

        transitionController.startTransition();

        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
        //play start
        transitionController.endTransition();
    }
    public void playButtonSound() {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicPlayer>().buttonSound.Play();
    }

    /* POPUP MENUS */
    public void openPopup(string popupType) {
      SceneManager.LoadSceneAsync("Popup", LoadSceneMode.Additive);
      StartCoroutine(waitForPopup(popupType));
    }
    IEnumerator waitForPopup(string popupType) {
      yield return new WaitUntil(() => popupOpened == true);
      foreach(Transform child in GameObject.Find("Popups").transform) {
        if(child.name != popupType) child.gameObject.SetActive(false);
        else child.gameObject.SetActive(true);
      }
      if(popupType == "SwapFurniture") {
        //make popup, asking if user wants to swap with this furniture piece
        Data data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
        FurnitureInfo furnitureInfo = GameObject.Find("FurnitureInfo").GetComponent<FurnitureInfo>();
        PlacementController placementController = GameObject.Find("PlacementController").GetComponent<PlacementController>();
        Inventory inventory = GameObject.Find("InventoryController").GetComponent<Inventory>();
        Upgrades upgrades = new Upgrades(data);


        int areasCount = upgrades.getGardenArea(furnitureInfo.getFurniture(placementController.furnitureSwapping).getCategory());
        string areaType = furnitureInfo.getFurniture(placementController.furnitureSwapping).getCategory();

        GameObject.Find("Verify").GetComponent<TMP_Text>().text = "You currently only have " + areasCount + " " + areaType + " areas unlocked. \nAre you sure you want to swap " + placementController.furnitureSwapping + " with " + placementController.furniturePlacing.getName() + "?";
        GameObject.Find("Yes").GetComponent<Button>().onClick.AddListener(delegate { placementController.enableSwap(); });
        GameObject.Find("Yes").GetComponent<Button>().onClick.AddListener(delegate { inventory.savePos(false); });
        GameObject.Find("Yes").GetComponent<Button>().onClick.AddListener(delegate { inventory.refreshItems(); });

        GameObject.Find("No").GetComponent<Button>().onClick.AddListener(delegate { placementController.cancelSwap(); });

        GameObject.Find("Yes").GetComponent<Button>().onClick.AddListener(delegate { placementController.setPopupOpened(); });
        GameObject.Find("No").GetComponent<Button>().onClick.AddListener(delegate { placementController.setPopupOpened(); });

        GameObject.Find("Popup_Close").GetComponent<Button>().onClick.AddListener(delegate { placementController.cancelSwap(); });
      }
      GameObject.Find("PopupCover").SetActive(false);
    }
    public void closePopup() {
      SceneManager.UnloadSceneAsync("Popup");
    }

    /* MAIN SIDE SLIDE-IN MENU*/
    public void openMainMenu() {
      mainMenuAnimation.SetFloat("Speed", 1f);
      mainMenuAnimation.SetTrigger("OpenMenu");
    }
    public void closeMainMenu() {
      mainMenuAnimation.SetFloat("Speed", -1f);
      mainMenuAnimation.SetTrigger("OpenMenu");
    }
    public void ghostDesignsLink() {
      Application.OpenURL("http://ghostdesigns.me");
    }

    /* BOTTOM CIRCLE POPOUT MENU */
    public void openPopoutMenu() {
      if(PopoutIsOpened == false)
        popoutMenuAnimation.SetFloat("Speed", 1f);
      else
        popoutMenuAnimation.SetFloat("Speed", -1f);
      PopoutIsOpened = !PopoutIsOpened;
      popoutMenuAnimation.SetTrigger("OpenMenu");
    }

    /* GARDEN STORE/INV MENUS */
    public void openGardenMenu(string menuType) {
      gardenMenuClose.SetActive(true);
      if(gardenMenuOpened == menuType) {
        gardenMenuAnimation.SetFloat("Speed", -1f);
        gardenMenuOpened = null;
        gardenMenuAnimation.SetTrigger("Close");
      }
      else {
        GameObject.Find("Store").transform.SetAsLastSibling();
        GameObject.Find("Inventory").transform.SetAsLastSibling();
        GameObject.Find("MenuBG").transform.SetAsLastSibling();
        GameObject.Find("GardenMenus").transform.Find("Header").transform.SetAsLastSibling();
        switch(menuType) {
          case "Store":
            GameObject.Find("Store").transform.SetAsLastSibling();
            GameObject.Find("GardenMenus").transform.Find("Header").GetComponent<Image>().sprite = waterSpr;
            GameObject.Find("GardenMenus").transform.Find("Header").GetComponent<Image>().rectTransform.sizeDelta = new Vector2(300, 100);
            break;
          case "Inventory":
            GameObject.Find("Inventory").transform.SetAsLastSibling();
            GameObject.Find("GardenMenus").transform.Find("Header").GetComponent<Image>().sprite = waterSpr;
            GameObject.Find("GardenMenus").transform.Find("Header").GetComponent<Image>().rectTransform.sizeDelta = new Vector2(300, 100);
            break;
        }
        gardenMenuAnimation.SetFloat("Speed", 1f);
        gardenMenuOpened = menuType;
        gardenMenuAnimation.SetTrigger("Expand");
      }
    }
    public void closeGardenMenu() {
      gardenMenuClose.SetActive(false);
      gardenMenuAnimation.SetFloat("Speed", -1f);
      gardenMenuAnimation.SetTrigger("Expand");
      gardenMenuOpened = null;
      gardenMenuAnimation.SetTrigger("Close");
    }
}
