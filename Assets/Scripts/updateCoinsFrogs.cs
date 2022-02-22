using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class updateCoinsFrogs : MonoBehaviour
{
    public TMP_Text coinsCounter;
    Data data;
    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindGameObjectWithTag("Data").GetComponent<GetData>().data;
    }

    // Update is called once per frame
    void Update()
    {
        coinsCounter.text = data.coinsFormatter(data.coins) + " coins";
    }
}
