using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] VirusManager manager;

	// Update is called once per frame
	void Update ()
    {
        text.text = "Score: " + manager.Score;
	}
}
