using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarView : MonoBehaviour {
	public TextMeshProUGUI text;

	// Use this for initialization
	void Start ()
	{
		 text = GetComponent<TextMeshProUGUI>();
	}

	public void SetStars(int count)
	{
		text.text = new string('✮', count);
	}
	// Update is called once per frame
	void Update () {
		
		
	}
}
