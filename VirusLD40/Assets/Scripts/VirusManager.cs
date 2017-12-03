using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the creation of new viruses
/// </summary>
public class VirusManager : MonoBehaviour
{
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

    List<VirusMovement> viruses;

    [SerializeField] GameObject virusPrefab;

    public int Score { get { return score; } }


    private void Start()
    {
        score = 0;

        //creates starting virus
        viruses = new List<VirusMovement>();
        viruses.Add(Instantiate(virusPrefab).GetComponent<VirusMovement>());

        //sets color to an unused color
        Sprite sprite = null;
        for(int i = 0; i < possibleVirusSprites.Count; i++)
        {
            if(possibleVirusSprites[i].inUse == false)
            {
                sprite = possibleVirusSprites[i].sprite;
                possibleVirusSprites[i].inUse = true;
                i = possibleVirusSprites.Count;
            }
        }
        viruses[0].GetComponentInChildren<SpriteRenderer>().sprite = sprite;

        //assigns starting controls
        viruses[0].Init(possibleControlSchemes[0]);
        possibleControlSchemes[0].InUse = true;

        InvokeRepeating("IncrementScore", .0f, 1);
    }

    private void IncrementScore()
    {
        score += viruses.Count;
    }

    //private void Update()
    //{
    //    score += viruses.Count;
    //}

    /// <summary>
    /// Creates a new virus
    /// </summary>
    public void CreateVirus(Vector3 position)
    {
        //checks if there are enough viruses to instantly end the game
        if (viruses.Count == possibleControlSchemes.Count)
        {
            viruses.Add(Instantiate(virusPrefab, position, Quaternion.identity).GetComponent<VirusMovement>());
            viruses[viruses.Count - 1].GetComponentInChildren<SpriteRenderer>().sprite = possibleVirusSprites[possibleVirusSprites.Count - 1].sprite;
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); //*******************************************************************Change to you win screen
            return;
        }

        //creates new virus
        viruses.Add(Instantiate(virusPrefab, position, Quaternion.identity).GetComponent<VirusMovement>());

        //sets control scheme to first unused control scheme
        int controlIndex = - 1;
        for(int i = 0; i < possibleControlSchemes.Count; i++)
        {
            if(possibleControlSchemes[i].InUse == false)
            {
                controlIndex = i;
                i = possibleControlSchemes.Count;
            }
        }
        viruses[viruses.Count - 1].Init(possibleControlSchemes[controlIndex]);
        possibleControlSchemes[controlIndex].InUse = true;

        //sets color to an unused color
        Sprite sprite = null;
        for (int i = 0; i < possibleVirusSprites.Count; i++)
        {
            if (possibleVirusSprites[i].inUse == false)
            {
                sprite = possibleVirusSprites[i].sprite;
                possibleVirusSprites[i].inUse = true;
                i = possibleVirusSprites.Count;
            }
        }
        viruses[viruses.Count - 1].GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }

    /// <summary>
    /// Sets the control binding for a destroyed virus to "not in use"
    /// </summary>
    /// <param name="destroyed">The virus that was destroyed</param>
    public void VirusDestroyed(VirusMovement killedCell)
    {
        //removes destroyed viruses
        viruses.Remove(killedCell);

        //ends game if player is out of viruses
        if(viruses.Count <= 0)
        {
            Destroy(killedCell.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); //************************************Change to game over screen
            return;
        }

        //lose points when viruses are destroyed
        score -= score / ((viruses.Count + 1) * 2);


        //at this point the player has won so don't mess with it
        if (viruses.Count > possibleControlSchemes.Count)
        {
            return;
        }

        //finds the binding that is no longer in use and marks it as such
        for (int i = 0; i < possibleControlSchemes.Count; i++)
        {
            if (possibleControlSchemes[i] == killedCell.Controls)
            {
                possibleControlSchemes[i].InUse = false;
                i = possibleControlSchemes.Count;
            }
        }

        //finds the sprite that is no longer in use and marks it as such
        for(int i = 0; i < possibleVirusSprites.Count; i++)
        {
            if(possibleVirusSprites[i].sprite == killedCell.GetComponentInChildren<SpriteRenderer>().sprite)
            {
                possibleVirusSprites[i].inUse = false;
                i = possibleVirusSprites.Count;
            }
        }

        Destroy(killedCell.gameObject);
    }
}
