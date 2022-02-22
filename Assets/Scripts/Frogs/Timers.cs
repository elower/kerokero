 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;
 using TMPro;

 public class Timers : MonoBehaviour
 {
     public GameObject[] timersList; //put all your timers here from the inspector
     public List<Animator> animatorList = new List<Animator>();

     void Start()
     {
         if (timersList.Length >= 1) //make sure list isn't empty
         {
             for (int i = 0; i < timersList.Length; i++)
             {
                 animatorList.Add(timersList[i].GetComponent<Animator>()); //fill up your list with animators components from timer gameobjects
                 animatorList[i].enabled = false; //turn off each animator component at the start
             }
         }
         else return; //if list is empty do nothing
     }
     void Update() {

     }
     //return timer associated with timer name
     public Animator findTimer(string timerName) {
         return GameObject.Find(timerName).transform.Find("Timer").gameObject.GetComponent<Animator>();
     }

     //check if timer is running
     public bool running(string frogName) {
         Animator animator = findTimer(frogName);
         if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime != 0) return true;
         else return false;
     }

     //play a timer once
     public void startTimer(string timerName, float animSpeed, float late)
     {
         Animator animator = findTimer(timerName);
         animator.enabled = true;
         animator.SetFloat("speed", animSpeed);
         if(late == 0f) animator.SetTrigger("loop");
         else animator.Play("timer", 0, late);

     }
     //add coins once
     public IEnumerator fetchCoins(Data data, int coins, int time, Button button, bool auto) {
         yield return new WaitForSecondsRealtime(time);
         data.coins += coins;
         if(auto == false) {
             button.GetComponent<Button>().interactable = true;//turn on button
         }
     }

     //start countdown timer once
     public IEnumerator startCountdown(TMP_Text countdown, int currentTime, bool repeat) {
         if(repeat)
            countdown.text = currentTime + " Sec";
         while(currentTime > 0) {
             yield return new WaitForSeconds(1f);
             currentTime--;
             if(repeat && currentTime != 0)
                 countdown.text = currentTime + " Sec";
             else if(repeat == false)
                 countdown.text = currentTime + " Sec";
         }
     }

     //run automatic fetch
     public IEnumerator autoFetchCoins(Data data, int coins, int time, TMP_Text countdown, Button button, int whereToStart, Animator animator) {
         while(true) {
             StartCoroutine(startCountdown(countdown, time, true));
             StartCoroutine(fetchCoins(data, coins, time, button, true));
             StartCoroutine(loopTimer(animator, time));
             yield return new WaitForSecondsRealtime(time);
         }
     }
     public IEnumerator loopTimer(Animator animator, int time) {
         animator.SetTrigger("loop");
         yield return new WaitForSeconds(time);
     }

     public IEnumerator startTimerLate(TMP_Text countdown, int time, int whereToStart, Data data, int coins, string frogName, Button button, bool repeatTimer, float animSpeed) {
         //fps = 30/time
         // current frame = normalized time * 30
         // current frame / x seconds = 30 frames / time seconds, x = (30/time)/frame
         Animator animator = findTimer(frogName);


          // 30 frames in time seconds
          // 30 frames/time seconds = frames per second
          // time is 10 seconds, if I have 5 seconds left, I want frame 15
          // 30/10 = 3, 30 - 5*3 = 15
          // time is 3 seconds, if I have 2 seconds left, i want frame 10
          // 30/3 = 10, 30 - 2*10 = 10

          // 30 - whereToStart*fps

          //whereToStart is the amount of seconds you are into the animation
          //extraTime is the amount of time remaining before animation is over
         int extraTime = time-whereToStart;
         float startingFrame = 30f-(extraTime*(30f/time));
         float animationPoint = ((1f/30f)*startingFrame);
         countdown.text = extraTime + " Sec";
         StartCoroutine(startCountdown(countdown, extraTime, repeatTimer));
         StartCoroutine(fetchCoins(data, coins, extraTime, button, repeatTimer));
         startTimer(frogName, animSpeed, animationPoint);
         if(repeatTimer == true) {
             //wait for this to complete, and then start the auto fetch
             yield return new WaitForSecondsRealtime(extraTime);
             StartCoroutine(autoFetchCoins(data, coins, time, countdown, button, whereToStart, animator));
         } else yield return new WaitForSecondsRealtime(0);
     }
 }
