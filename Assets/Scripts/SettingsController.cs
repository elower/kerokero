using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    float volumeVal;
    Data data;
    // Start is called before the first frame update
    void Start()
    {
      data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
      //set volumeval to save data
      volumeVal = data.settingsValues[0];

      //set slider to volume val
      volumeSlider.value = volumeVal;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeVolume(float value) {
      audioMixer.SetFloat("volume", value);
      volumeVal = value;
    }
    public void saveSettings() {
      data.settingsValues[0] = volumeVal;
      data.savePlayerData(true);
    }
}
