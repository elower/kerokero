using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Loading_Controller : MonoBehaviour
{
  public AudioMixer audioMixer;
    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicPlayer>().PlayMusic();

        //game-wide settings initialization
        audioMixer.SetFloat("volume", GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data.settingsValues[0]);
    }
}
