using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public Animator transition;
    void Start() {
        DontDestroyOnLoad(transform.gameObject);
    }
    public void startTransition() {
        transition.SetTrigger("Start");
    }
    public void endTransition() {
        transition.SetTrigger("End");
    }
}
