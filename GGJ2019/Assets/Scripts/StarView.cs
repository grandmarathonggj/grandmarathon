using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarView : MonoBehaviour
{
    public Text text;

    // Use this for initialization
    void Start()
    {
    }

    public void SetStars(int count)
    {
        text = GetComponent<Text>();
        text.text = new string('✮', count);

        if (count == 0)
        {
            text.text = "FAILED...";
            
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}