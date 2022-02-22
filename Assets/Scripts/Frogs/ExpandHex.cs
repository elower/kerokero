using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExpandHex : MonoBehaviour
{
    public GameObject[] hexList; //put all your hexs here from the inspector
    public List<Animator> animatorList = new List<Animator>();
    public Vector3 currentPos;
    public GameObject CloseHex;
    public bool Large = false;

    void Start()
    {
        if (hexList.Length >= 1) //make sure list isn't empty
        {
            for (int i = 0; i < hexList.Length; i++)
            {
                animatorList.Add(hexList[i].GetComponent<Animator>()); //fill up your list with animators components from hex gameobjects
                //disable container with info only vis in enlarged view
                hexList[i].transform.parent.transform.Find("Info").gameObject.SetActive(false);
            }
        }
        else return; //if list is empty do nothing
        CloseHex.SetActive(false);
    }
    //return hex associated with hex name
    public Animator findhex(string hexName) {
        return GameObject.Find(hexName).transform.Find("Hex").gameObject.GetComponent<Animator>();
    }

    //expand hex
    public void expandHex()
    {
        string hexName = EventSystem.current.currentSelectedGameObject.transform.parent.name;
        Animator animator = findhex(hexName);
        if(animator.enabled == true && Large == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
          //disable scroll
          GameObject.Find("Frog Scroll").GetComponent<ScrollRect> ().enabled = false;

          animator.SetTrigger("Expand");
          GameObject wrap = GameObject.Find(hexName);
          currentPos = wrap.transform.position;

          CloseHex.transform.SetAsLastSibling();
          wrap.transform.SetAsLastSibling();


          //set elements invisible
          wrap.transform.Find("FrogImage").gameObject.SetActive(false);
          wrap.transform.Find("Fetch").gameObject.SetActive(false);
          wrap.transform.Find("Button").gameObject.SetActive(false);

          Color32 temp = wrap.transform.Find("Timer").GetComponent<Image> ().color;
          temp.a=0;
          wrap.transform.Find("Timer").GetComponent<Image> ().color = temp;

          temp = wrap.transform.Find("Countdown").GetComponent<TMP_Text> ().color;
          temp.a=0;
          wrap.transform.Find("Countdown").GetComponent<TMP_Text> ().color = temp;

          //set elements visible
          StartCoroutine(setLarge(wrap, hexName));

          //move to center
          StartCoroutine(translatePos(wrap, false));

          //disable all other hexes
          for (int i = 0; i < hexList.Length; i++)
          {
            if(animatorList[i] != animator)
            {
              animatorList[i].enabled = false;
            }
          }
          Large = true;
        }
    }

    //close hex
    public void closeHex(string hexName)
    {
        Animator animator = findhex(hexName);
        animator.SetTrigger("Close");
        GameObject wrap = GameObject.Find(hexName);

        //set elements invisible
        wrap.transform.Find("FrogImage").gameObject.SetActive(false);
        wrap.transform.Find("Fetch").gameObject.SetActive(false);
        wrap.transform.Find("Button").gameObject.SetActive(false);

        Color32 temp = wrap.transform.Find("Timer").GetComponent<Image> ().color;
        temp.a=0;
        wrap.transform.Find("Timer").GetComponent<Image> ().color = temp;

        temp = wrap.transform.Find("Countdown").GetComponent<TMP_Text> ().color;
        temp.a=0;
        wrap.transform.Find("Countdown").GetComponent<TMP_Text> ().color = temp;;

        //make normal size
        StartCoroutine(setSmall(wrap, hexName));

        //translate back to original pos
        StartCoroutine(translatePos(wrap, true));

        //disable closeHex object
        CloseHex.SetActive(false);

        Large = false;

    }

    IEnumerator translatePos(GameObject wrap, bool inverse) {
      if(!inverse) {
        float posX = wrap.transform.position.x;
        float posY = wrap.transform.position.y;
        //30 frames, 60 fps, 0.01 second per increment, it should last .05 seconds
        float incX = ((Screen.width / 2)-posX)*.045f;
        float incY = ((Screen.height / 2)-posY)*.045f;

        while(wrap.transform.position != new Vector3(Screen.width / 2, Screen.height / 2, 0)) {
          if((incX > 0 && wrap.transform.position.x+incX < Screen.width / 2) || (incX < 0 && wrap.transform.position.x+incX > Screen.width / 2))
            posX = wrap.transform.position.x+incX;
          else
            posX = Screen.width / 2;

          if((incY > 0 && wrap.transform.position.y+incY < Screen.height / 2) || (incY < 0 && wrap.transform.position.y+incY > Screen.height / 2))
            posY = wrap.transform.position.y+incY;
          else
            posY = Screen.height / 2;
          wrap.transform.position = new Vector3(posX, posY, 0);
          yield return new WaitForSeconds(0.01f);
        }
      } else {
        float posX = Screen.width / 2;
        float posY = Screen.height / 2;
        //30 frames, 60 fps, 0.01 second per increment, it should last .05 seconds
        float incX = (currentPos.x-posX)*.05f;
        float incY = (currentPos.y-posY)*.05f;

        while(wrap.transform.position.x != currentPos.x && wrap.transform.position.y != currentPos.y) {
          if((incX > 0 && wrap.transform.position.x+incX < currentPos.x) || (incX < 0 && wrap.transform.position.x+incX > currentPos.x))
            posX = wrap.transform.position.x+incX;
          else
            posX = currentPos.x;

          if((incY > 0 && wrap.transform.position.y+incY < currentPos.y) || (incY < 0 && wrap.transform.position.y+incY > currentPos.y))
            posY = wrap.transform.position.y+incY;
          else
            posY = currentPos.y;
          wrap.transform.position = new Vector3(posX, posY, 0);
          yield return new WaitForSeconds(0.01f);
        }
      }

    }
    IEnumerator setLarge(GameObject wrap, string hexName) {
      yield return new WaitForSeconds(0.5f);
      //new sizes
      wrap.transform.Find("FrogImage").GetComponent<RectTransform> ().sizeDelta = new Vector2(200, 200);
      wrap.transform.Find("FrogImage").position = new Vector3(wrap.transform.Find("FrogImage").position.x, wrap.transform.Find("FrogImage").position.y+50, 0);

      wrap.transform.Find("Fetch").GetComponent<RectTransform> ().sizeDelta = new Vector2(350, 75);
      wrap.transform.Find("Fetch").transform.Find("FETCH").GetComponent<RectTransform> ().sizeDelta = new Vector2(150, 70);
      wrap.transform.Find("Fetch").position = new Vector3(wrap.transform.Find("Fetch").position.x, wrap.transform.Find("Fetch").position.y-200, 0);

      wrap.transform.Find("Timer").GetComponent<RectTransform> ().sizeDelta = new Vector2(100, 100);
      wrap.transform.Find("Timer").position = new Vector3(wrap.transform.Find("Timer").position.x, wrap.transform.Find("Timer").position.y-15, 0);

      wrap.transform.Find("Countdown").GetComponent<RectTransform> ().sizeDelta = new Vector2(200, 60);
      wrap.transform.Find("Countdown").position = new Vector3(wrap.transform.Find("Countdown").position.x+50, wrap.transform.Find("Countdown").position.y-15, 0);
      wrap.transform.Find("Countdown").GetComponent<TMP_Text> ().fontSize = 46;

      //set active
      wrap.transform.Find("FrogImage").gameObject.SetActive(true);
      wrap.transform.Find("Fetch").gameObject.SetActive(true);
      wrap.transform.Find("Button").gameObject.SetActive(true);

      Color32 temp = wrap.transform.Find("Timer").GetComponent<Image> ().color;
      temp.a=255;
      wrap.transform.Find("Timer").GetComponent<Image> ().color = temp;

      temp = wrap.transform.Find("Countdown").GetComponent<TMP_Text> ().color;
      temp.a=255;
      wrap.transform.Find("Countdown").GetComponent<TMP_Text> ().color = temp;

      //set info container active
      wrap.transform.Find("Info").gameObject.SetActive(true);

      //enable closeHex object
      CloseHex.SetActive(true);
      CloseHex.GetComponent<Button>().onClick.RemoveAllListeners();
      CloseHex.GetComponent<Button>().onClick.AddListener(delegate { closeHex(hexName); });
    }

    IEnumerator setSmall(GameObject wrap, string hexName) {
      //set info container inactive
      wrap.transform.Find("Info").gameObject.SetActive(false);
      yield return new WaitForSeconds(0.5f);
      //enable scroll
      GameObject.Find("Frog Scroll").GetComponent<ScrollRect> ().enabled = true;
      //new sizes
      wrap.transform.Find("FrogImage").GetComponent<RectTransform> ().sizeDelta = new Vector2(150, 150);
      wrap.transform.Find("FrogImage").position = new Vector3(wrap.transform.Find("FrogImage").position.x, wrap.transform.Find("FrogImage").position.y-50, 0);

      wrap.transform.Find("Fetch").GetComponent<RectTransform> ().sizeDelta = new Vector2(194, 48);
      wrap.transform.Find("Fetch").transform.Find("FETCH").GetComponent<RectTransform> ().sizeDelta = new Vector2(100, 40);
      wrap.transform.Find("Fetch").position = new Vector3(wrap.transform.Find("Fetch").position.x, wrap.transform.Find("Fetch").position.y+200, 0);

      wrap.transform.Find("Timer").GetComponent<RectTransform> ().sizeDelta = new Vector2(75, 75);
      wrap.transform.Find("Timer").position = new Vector3(wrap.transform.Find("Timer").position.x, wrap.transform.Find("Timer").position.y+15, 0);

      wrap.transform.Find("Countdown").GetComponent<RectTransform> ().sizeDelta = new Vector2(100, 34);
      wrap.transform.Find("Countdown").position = new Vector3(wrap.transform.Find("Countdown").position.x-50, wrap.transform.Find("Countdown").position.y+15, 0);
      wrap.transform.Find("Countdown").GetComponent<TMP_Text> ().fontSize = 24;

      //set active
      wrap.transform.Find("FrogImage").gameObject.SetActive(true);
      wrap.transform.Find("Fetch").gameObject.SetActive(true);
      wrap.transform.Find("Button").gameObject.SetActive(true);

      Color32 temp = wrap.transform.Find("Timer").GetComponent<Image> ().color;
      temp.a=255;
      wrap.transform.Find("Timer").GetComponent<Image> ().color = temp;

      temp = wrap.transform.Find("Countdown").GetComponent<TMP_Text> ().color;
      temp.a=255;
      wrap.transform.Find("Countdown").GetComponent<TMP_Text> ().color = temp;

      //enable all hexes
      for (int i = 0; i < hexList.Length; i++)
      {
          animatorList[i].enabled = true;
      }
    }
}
