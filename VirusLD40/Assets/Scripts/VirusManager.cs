using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the creation of new viruses
/// </summary>
public class VirusManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    private UIManager uiManager;

    [SerializeField] private List<Transform> spawners;
    [SerializeField] private GameObject redBloodCellPrefab;
    System.Random rand = new System.Random();

    private int score;

    //a list of all possible virus control schemes
    List<CustomKeyBinding> possibleControlSchemes = new List<CustomKeyBinding>
    {
        new CustomKeyBinding(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, false),
        new CustomKeyBinding(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, false),
        new CustomKeyBinding(KeyCode.Y, KeyCode.H, KeyCode.G, KeyCode.J, false),
        new CustomKeyBinding(KeyCode.P, KeyCode.Semicolon, KeyCode.L, KeyCode.Quote, false),
        new CustomKeyBinding(KeyCode.F, KeyCode.V, KeyCode.C, KeyCode.B, false),
        new CustomKeyBinding(KeyCode.K, KeyCode.Comma, KeyCode.M, KeyCode.Period, false),
        new CustomKeyBinding(KeyCode.Alpha4, KeyCode.R, KeyCode.E, KeyCode.T, false),
        new CustomKeyBinding(KeyCode.Alpha8, KeyCode.I, KeyCode.U, KeyCode.O, false),
        new CustomKeyBinding(KeyCode.Equals, KeyCode.RightBracket, KeyCode.LeftBracket, KeyCode.Backslash, false),
        new CustomKeyBinding(KeyCode.F1, KeyCode.Alpha2, KeyCode.Alpha1, KeyCode.Alpha3, false),
        new CustomKeyBinding(KeyCode.F4, KeyCode.Alpha6, KeyCode.Alpha5, KeyCode.Alpha7, false),
        new CustomKeyBinding(KeyCode.F8, KeyCode.Alpha0, KeyCode.Alpha9, KeyCode.Minus, false),
        new CustomKeyBinding(KeyCode.Home, KeyCode.End, KeyCode.Delete, KeyCode.PageDown, false)
    };

    [System.Serializable]
    private class ListWrapper
    {
        public Sprite sprite;
        public bool inUse;
    }
    [SerializeField] List<ListWrapper> possibleVirusSprites;

    [SerializeField] List<VirusMovement> viruses;

    [SerializeField] GameObject virusPrefab;

    public int Score { get { return score; } }


    private void Start()
    {
        score = 0;

        //sets color to an unused color
        for(int i = 0; i < possibleVirusSprites.Count; i++)
        {
            if(possibleVirusSprites[i].inUse == false)
            {
                viruses[i].GetComponentInChildren<SpriteRenderer>().sprite = possibleVirusSprites[i].sprite;
                possibleVirusSprites[i].inUse = true;
            }
        }

        //assigns starting controls
        for (int i = 0; i < possibleVirusSprites.Count; i++)
        {
            if (possibleVirusSprites[i].inUse == false)
            {
                viruses[i].Controls = possibleControlSchemes[i];
                possibleControlSchemes[i].InUse = true;
            }
        }

        viruses[0].transform.position = Vector3.zero;

        InvokeRepeating("IncrementScore", .0f, 1);

        uiManager = canvas.GetComponent<UIManager>(); 
    }

    private void IncrementScore()
    {
        score += viruses.Count;
    }

    /// <summary>
    /// Creates a new virus
    /// </summary>
    public void CreateVirus(Vector3 position)
    {

        //checks if there are enough viruses to instantly end the game
        int activeViruses = 0;
        for (int i = 0; i < viruses.Count; i++)
        {
            if (viruses[i].gameObject.activeInHierarchy)
            {
                activeViruses++;
            }
        }
        if(activeViruses == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); 
            //*******************************************************************Change to you win screen
            return;
        }

        //creates new virus
        for (int i = 0; i < viruses.Count; i++)
        {
            if (!viruses[i].gameObject.activeInHierarchy)
            {
                viruses[i].gameObject.SetActive(true);
                viruses[i].transform.position = position;
                i = viruses.Count;
            }
        }

        //creates a list of which control schemes are in use
        List<bool> usedControlIndices = new List<bool>();
        for(int i = 0; i < viruses.Count; i++)
        {
            usedControlIndices.Add(viruses[i].gameObject.activeInHierarchy);
        }
        uiManager.UpdateUI(usedControlIndices);
    }

    /// <summary>
    /// Sets the control binding for a destroyed virus to "not in use"
    /// </summary>
    /// <param name="destroyed">The virus that was destroyed</param>
    public void VirusDestroyed(VirusMovement killedCell)
    {
        int index = 0;
        int activeViruses = 0;
        for(int i = 0; i < viruses.Count; i++)
        {
            if(viruses[i] == killedCell)
            {
                index = i;
                //i = viruses.Count;
            }
            if (viruses[i].gameObject.activeInHierarchy)
            {
                activeViruses++;
            }
        }

        score -= score / (activeViruses * 2);

        possibleControlSchemes[index].InUse = false;

        viruses[index].gameObject.SetActive(false);
        //removes destroyed viruses
        //viruses.Remove(killedCell);

        //ends game if player is out of viruses
        int activeVirusCount = 0;
        for (int i = 0; i < viruses.Count; i++)
        {
            if (viruses[i].gameObject.activeInHierarchy)
            {
                activeVirusCount++;
            }
        }
        if (activeVirusCount == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            //*******************************************************************Change to you win screen
            return;
        }

        if (spawners.Count != 0)
        {
            //spawn a new red blood cell
            Instantiate(redBloodCellPrefab, spawners[rand.Next(spawners.Count - 1)]);
        }

        //creates a list of which control schemes are in use
        List<bool> usedControlIndices = new List<bool>();
        for (int i = 0; i < possibleControlSchemes.Count; i++)
        {
            usedControlIndices.Add(viruses[i].gameObject.activeInHierarchy);
        }
        uiManager.UpdateUI(usedControlIndices);
    }
}
