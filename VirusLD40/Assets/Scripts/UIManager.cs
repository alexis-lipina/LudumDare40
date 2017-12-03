using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] List<GameObject> keyslist;



    void Start()
    {
        keyslist = new List<GameObject>();
        
    }

    void UpdateUI(List<bool> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            keyslist[i].SetActive(list[i]);
        }
    }
}
