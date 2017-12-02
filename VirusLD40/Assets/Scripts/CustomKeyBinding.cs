using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds a 4-way keybinding similar to WASD
/// </summary>
public class CustomKeyBinding
{
    private KeyCode up;
    private KeyCode down;
    private KeyCode left;
    private KeyCode right;

    private bool inUse;

    /// <summary>
    /// The key to move up
    /// </summary>
    public KeyCode Up { get { return up; } }
    /// <summary>
    /// The key to move down
    /// </summary>
    public KeyCode Down { get { return down; } }
    /// <summary>
    /// The key to move left
    /// </summary>
    public KeyCode Left { get { return left; } }
    /// <summary>
    /// They key to move right
    /// </summary>
    public KeyCode Right { get { return right; } }

    /// <summary>
    /// Gets and sets if this keybinding is currently being used by a virus
    /// </summary>
    public bool InUse { get { return inUse; } set { inUse = value; } }

    public CustomKeyBinding(KeyCode up, KeyCode down, KeyCode left, KeyCode right, bool inUse)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
        this.inUse = inUse;
    }
}