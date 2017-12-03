using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Sprite imageThatAppearsWhenYouMouseOver;
    public GameObject panelToPutTheImageOn;


    void OnMouseOver()
    {
        Debug.Log("MouseOver called");
        panelToPutTheImageOn.GetComponent<Image>().sprite = imageThatAppearsWhenYouMouseOver;
    }
	
}
