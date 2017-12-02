using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the creation of new viruses
/// </summary>
public class VirusManager : MonoBehaviour
{
    //a list of all possible virus control schemes
    List<CustomKeyBinding> possibleControlSchemes = new List<CustomKeyBinding>
    {
        new CustomKeyBinding(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, false),
        new CustomKeyBinding(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, false),
        new CustomKeyBinding(KeyCode.Y, KeyCode.H, KeyCode.G, KeyCode.J, false),
        new CustomKeyBinding(KeyCode.P, KeyCode.Semicolon, KeyCode.L, KeyCode.DoubleQuote, false),
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
    
    List<VirusMovement> viruses;

    [SerializeField] GameObject virusPrefab;


    private void Start()
    {
        viruses = new List<VirusMovement>();
        viruses.Add(GameObject.FindGameObjectWithTag("InitialVirus").GetComponent<VirusMovement>());
        viruses[0].Init(possibleControlSchemes[0]);
        possibleControlSchemes[0].InUse = true;
    }

    /// <summary>
    /// Creates a new virus
    /// </summary>
    public void CreateVirus()
    {
        viruses.Add(Instantiate(virusPrefab).GetComponent<VirusMovement>());

        int controlIndex = -1; //**********************************************************************************************This will break if we run out of control schemes!!
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
    }

    /// <summary>
    /// Sets the control binding for a destroyed virus to "not in use"
    /// </summary>
    /// <param name="destroyed">The virus that was destroyed</param>
    public void VirusDestroyed(VirusMovement destroyed)
    {
        CustomKeyBinding lostBinding = destroyed.Controls;

        int controlIndex = -1; //**********************************************************************************************This will break if we run out of control schemes!!
        for (int i = 0; i < possibleControlSchemes.Count; i++)
        {
            if (possibleControlSchemes[i] == lostBinding)
            {
                controlIndex = i;
                i = possibleControlSchemes.Count;
            }
        }

        possibleControlSchemes[controlIndex].InUse = false;
    }
}
