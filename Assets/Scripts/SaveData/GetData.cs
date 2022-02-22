using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GetData : MonoBehaviour
{
    public Data data;
    // Start is called before the first frame update
    void Awake()
    {
        data = new Data();
        DontDestroyOnLoad(transform.gameObject);
    }
}
