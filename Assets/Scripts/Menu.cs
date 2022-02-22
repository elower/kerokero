using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject FrogsMenuBtn;
    public GameObject GardenMenuBtn;
    public GameObject UpgradesMenuBtn;
    public GameObject CreditsMenuBtn;
    public GameObject NewspaperMenuBtn;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetSceneByName("FrogsList").isLoaded)
            FrogsMenuBtn.GetComponent<Button>().interactable = false;
        if(SceneManager.GetSceneByName("Garden").isLoaded)
            GardenMenuBtn.GetComponent<Button>().interactable = false;
        if(SceneManager.GetSceneByName("Upgrades").isLoaded)
            UpgradesMenuBtn.GetComponent<Button>().interactable = false;
        if(SceneManager.GetSceneByName("Credits").isLoaded)
            CreditsMenuBtn.GetComponent<Button>().interactable = false;
        if(SceneManager.GetSceneByName("Newspaper").isLoaded)
            NewspaperMenuBtn.GetComponent<Button>().interactable = false;
    }
}
