using UnityEngine;
using System.Collections;
using GearShift;

/// <summary>
/// The EndGear is always auto placed on the board; it never moves or changes states
/// So it's really more like an obstacle than a gear
///     This lets us detect when it starts rotating
///     
/// Obviously handles winning the game when it's powered on, though the specifics may go elsewhere later
/// </summary>
public class EndGearScript : Obstacle {

    public override void PowerOn()
    {
        Win();
        base.PowerOn();
    }

    public GameObject winObject;
    
    public void Win()
    {
        winObject.SetActive(true);
    }

    //temp code to try to restart the game
    public void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}