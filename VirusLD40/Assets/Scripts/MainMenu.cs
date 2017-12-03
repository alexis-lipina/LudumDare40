using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button button1;
    [SerializeField] Button button2;

    private void Start()
    {
        button1.onClick.AddListener(LoadLevel1);
        button2.onClick.AddListener(LoadLevel2);
    }

    private void LoadLevel1() { UnityEngine.SceneManagement.SceneManager.LoadScene("DavisTestScene"); }
    private void LoadLevel2() { UnityEngine.SceneManagement.SceneManager.LoadScene("DaneTest"); }

}
