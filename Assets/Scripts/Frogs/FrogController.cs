using UnityEngine;
﻿using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class FrogController : MonoBehaviour
{
    Data data;
    public Timers timers;
    FrogsInfo frogsInfo;
    public GameObject[] frogPanels;

    public void Start() {
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
        frogsInfo = GameObject.FindGameObjectWithTag("FrogsInfo").GetComponent<FrogsInfo>();
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicPlayer>().PlayMusic();

        //set frogs info if owned
        foreach (string i in data.ownedFrogs) {
            Frog frog = frogsInfo.getFrog(i);
            //set speed corresponding to how many mates you have
            if(frog.getMates() >= frog.getRequiredMates()) {
                if((frog.getTime() - (frog.getMates() / frog.getRequiredMates()) > 1)  && frog.getMates() % frog.getRequiredMates() == 0)
                    frog.setTime(frog.getTime() - (frog.getMates()/frog.getRequiredMates()));
                else if(frog.getTime() - (frog.getMates() / frog.getRequiredMates()) <= 1)
                    frog.setTime(1);
            }

            frog.setOwned();
            //make visible if owned
            setFrogVisibility(i);

            //set frog info
            setFrogInfo(i);
        }

        //disable all other hexes
        int activeCounter = 0;
        foreach(GameObject i in frogPanels) {
          if(i.name == "Frog") i.SetActive(false);
          else activeCounter++;
        }
        if(activeCounter % 2 == 0) //even
          GameObject.Find("Content").GetComponent<RectTransform> ().sizeDelta = new Vector2(0.0f, (400*activeCounter/2));
        else //odd
          GameObject.Find("Content").GetComponent<RectTransform> ().sizeDelta = new Vector2(0.0f, (400*activeCounter/2)+200);
        if(data.tutorial != 2) {
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Additive);
            if(data.tutorial == 1) {
                startTimerLate("Pond", 0, true);
            }
        } else {
            //check for mates and repeat timer if mates
            int coinsEarned = 0; //coins earned while gone
            int extraTime = 0;
            // get time passed
            double timePassed = (DateTime.UtcNow - data.lastLoggedTime).TotalSeconds;

            int frogCoins = 0;
            int frogTime = 0;
            int frogMates = 0;
            foreach(string frogName in data.frogNames) {
                if(data.ownedFrogs.Contains(frogName)) { //make sure frog is owned first
                    Frog thisFrog = frogsInfo.getFrog(frogName);
                    frogCoins = thisFrog.getCoins();
                    frogTime = thisFrog.getTime();
                    frogMates = thisFrog.getMates();
                    if(frogMates >= thisFrog.getRequiredMates()) { //frog has enough mates to auto fetch - calculate how many times timer has gone around
                        if(data.newMates[frogName] != null) {
                            timePassed = (DateTime.UtcNow - DateTime.Parse(data.newMates[frogName])).TotalSeconds;
                            data.newMates[frogName] = null;
                            //add coins earned while gone
                            if(timePassed < frogTime)
                                extraTime = frogTime - Convert.ToInt32(timePassed);
                            else
                                extraTime = Convert.ToInt32(timePassed % frogTime);

                            if((Convert.ToInt32(((timePassed - extraTime) / frogTime) * frogCoins)) > 0)
                                coinsEarned += (Convert.ToInt32(((timePassed - extraTime) / frogTime) * frogCoins));
                            //set auto fetch for this frog since it has mates
                            startTimerLate(frogName, extraTime, true);
                        }
                        else { //not a new mate, so you can add coins
                            //add coins earned while gone
                            if(timePassed < frogTime)
                                extraTime = frogTime - Convert.ToInt32(timePassed);
                            else
                                extraTime = Convert.ToInt32(timePassed % frogTime);

                            if((Convert.ToInt32(((timePassed - extraTime) / frogTime) * frogCoins)) > 0)
                                coinsEarned += (Convert.ToInt32(((timePassed - extraTime) / frogTime) * frogCoins));

                            // check if the timer is in progress and the progress of it the last time it was started
                            if(data.timersProgress[frogName] != "0") {
                                float progress = float.Parse(data.timersProgress[frogName]);
                                //get the progress of the timer in seconds
                                int progressSeconds = Convert.ToInt32(progress*frogTime);
                                if(progressSeconds + extraTime >= frogTime) { //if this progress plus extra time is more than one rotation
                                    int timesAround = Convert.ToInt32((progressSeconds + extraTime) / frogTime);
                                    extraTime = Convert.ToInt32((timePassed+progressSeconds) % frogTime);
                                    coinsEarned += (frogCoins * timesAround);
                                } else { //add progress seconds to extra time to account for progress before leaving
                                    extraTime += progressSeconds;
                                }
                            }
                            //set auto fetch for this frog since it has mates
                            startTimerLate(frogName, extraTime, true);
                        }
                    } else if(data.timersProgress[frogName] != "0"){ // no mates - so only calculate for one rotation
                        //if timer already had progress before leaving
                        //add coins earned while gone
                        float progress = float.Parse(data.timersProgress[frogName]);
                        int progressSeconds = Convert.ToInt32(progress*frogTime);
                        if(timePassed < frogTime) { //if the timer isn't finished, set extratime to time left
                            extraTime = Convert.ToInt32(timePassed+progressSeconds);
                            if(frogTime - extraTime > 0) {
                                //coinsEarned will be 0, since it hasn't completed a rotation
                                //start the timer late
                                startTimerLate(frogName, extraTime, false);
                            } else {//in this case, timer has completed once.
                                coinsEarned += frogCoins;
                                data.timersProgress[frogName] = "0";
                            }
                        } else coinsEarned += frogCoins; //timer has completed once
                    }
                }
            }
            if(coinsEarned > 0 && data.tutorial == 2) {
                SceneManager.LoadSceneAsync("Popup", LoadSceneMode.Additive);
                StartCoroutine(waitForScene(coinsEarned));
            }
            data.coins += coinsEarned;

            data.lastLoggedTime = DateTime.UtcNow;
        }
    }
    private void Update() {
        //get running timers progress
        for (int i = 0; i < timers.timersList.Length; i++)
        {
          if(timers.timersList[i].transform.parent.name != "Frog") {
            if(timers.animatorList[i].GetCurrentAnimatorStateInfo(0).IsName("timer") && timers.animatorList[i].GetCurrentAnimatorStateInfo(0).normalizedTime != 0 && timers.animatorList[i].GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                data.timersProgress[timers.timersList[i].transform.parent.name] = timers.animatorList[i].GetCurrentAnimatorStateInfo(0).normalizedTime.ToString();
            else
                data.timersProgress[timers.timersList[i].transform.parent.name] = "0";
          }
        }
        data.savePlayerData(false);
    }
    IEnumerator waitForScene(int coinsEarned) {
        yield return new WaitForSeconds(0.01f);
        GameObject.Find("Returning").transform.SetAsLastSibling();
        GameObject.Find("Popup_Close").transform.SetAsLastSibling();
        GameObject.Find("ReturningCoins").GetComponent<TMP_Text>().text = data.coinsFormatter(coinsEarned);
    }
    public void setFrogVisibility(string frogType) {
        Frog frog = frogsInfo.getFrog(frogType);
        if(frog.getOwned() == true) {
            //set name for correct hex
            for(int i = 0; i < frogPanels.Length; i++) {
              if(frogPanels[i].name == "Frog") {
                frogPanels[i].name = frogType;
                break;
              }
            }
            //set frog image
            GameObject panel = GameObject.Find(frogType);
            Transform sprite = panel.transform.Find("FrogImage");
            sprite.GetComponent<Image> ().sprite = frogsInfo.getSprite(frogType);
            //set fetch button
            Button fetchButton = panel.transform.Find("Fetch").GetComponent<Button>();
            fetchButton.onClick.AddListener(delegate { addCoins(frogType); });
        }
    }
    public void setFrogInfo(string frogType) {
        Frog frog = frogsInfo.getFrog(frogType);
        frog.setMates(int.Parse(data.mates[frogType]));
        Transform info = GameObject.Find(frogType).transform.Find("Info");
        TMP_Text mates = info.transform.Find("Mates").gameObject.GetComponent<TMP_Text>();
        TMP_Text coinsPerSecond = info.transform.Find("CoinsInSeconds").gameObject.GetComponent<TMP_Text>();
        TMP_Text name = info.transform.Find("Name").gameObject.GetComponent<TMP_Text>();

        if(data.mates[frogType] == "1") mates.text = data.mates[frogType] + "1 Mate";
        else mates.text = data.mates[frogType] + " Mates";

        int coins = frog.getCoins();
        int time = frog.getTime();
        if(coins == 1 && time == 1)
          coinsPerSecond.text = "Fetches 1 Coin every second";
        else if(time == 1)
          coinsPerSecond.text = "Fetches " + coins + " Coins every second";
        else
          coinsPerSecond.text = "Fetches " + coins + " Coins every " + time + " Seconds";

        name.text = frog.getName();
    }
    public void startTimerLate(string frogName, int extraTime, bool autoFetch) {
        Frog frog = frogsInfo.getFrog(frogName);

        //start auto fetch coroutines

        //disable button
        GameObject panel = GameObject.Find(frogName);
        Transform fetch = panel.transform.Find("Fetch");
        Button button = fetch.GetComponent<Button>();
        button.interactable = false;

        TMP_Text countdown = panel.transform.Find("Countdown").gameObject.GetComponent<TMP_Text>();

        //start timer late
        StartCoroutine(timers.startTimerLate(countdown, frog.getTime(), extraTime, data, frog.getCoins(), frogName, button, autoFetch, frog.getAnimationSpeed()));
    }

    public void addCoins(string frogType) {
        Frog frog = frogsInfo.getFrog(frogType);

        // start timer
        timers.startTimer(frogType, frog.getAnimationSpeed(), 0f);

        //disable button
        GameObject panel = GameObject.Find(frogType);
        Transform fetch = panel.transform.Find("Fetch");
        Button button = fetch.GetComponent<Button>();
        button.interactable = false;

        //start countdown
        TMP_Text countdown = panel.transform.Find("Countdown").gameObject.GetComponent<TMP_Text>();
        countdown.text = frog.getTime() + " Sec";

        //update as timer animation plays
        StartCoroutine(timers.startCountdown(countdown, frog.getTime(), false));
        StartCoroutine(timers.fetchCoins(data, frog.getCoins(), frog.getTime(), button, false));
    }
}
