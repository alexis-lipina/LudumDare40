using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] List<GameObject> keyslist;
    private bool uiOn;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            uiOn = !uiOn;
            panel.SetActive(uiOn);
        }
    }



    public void UpdateUI(List<bool> list)
    {
        for (int i = 0; i < keyslist.Count; i++)
        {
            keyslist[i].SetActive(list[i]);
        }
    }
}
