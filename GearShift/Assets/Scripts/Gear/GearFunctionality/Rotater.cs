using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using GearShift;

/// <summary>
/// Causes the gear to rotate
/// Also detects collisions with other gears and decides whether to rotate based on whether they're rotating or not
/// Probably needs some abstraction to account for different gear teeth, though that could be as simple as an enum
/// </summary>
public class Rotater : MonoBehaviour
{
    //How fast do the gears rotate when turning?
    public float rotationSpeed = 20f;

    //Are we attached to other rotating gears?
    public bool isRotating
    {
        get
        {
            return _isRotating;
        }
        set
        {
            //The root gear can never not rotate
            if (rootGear)
            {
                _isRotating = true;
            }
            else
            {
                _isRotating = value;
            }
        }
    }
    protected bool _isRotating;

    //Is this the root gear?
    public bool rootGear = false;

    //Attached obstacle to tell whether we're attached or not
    protected Obstacle obstacle;

    protected void Start()
    {
        if(rootGear)
        {
            isRotating = true;
        }
        if(obstacle==null)
        {
            obstacle = gameObject.GetComponent<Obstacle>();
        }
    }

    //which direciton are we rotating?
    public bool clockwise = true;

    //What gears are we attached to?
    protected List<Rotater> attachedGears = new List<Rotater>();

    protected void Update()
    {
        if(isRotating)
        {
            Rotate();
        }
    }

    /// <summary>
    /// Spin a clockwise or counterclockwise fashion
    /// </summary>
    protected void Rotate()
    {
        float toRotate = rotationSpeed;
        if (!clockwise)
        {
            //clockwise is opposite direction of counter-clockwise
            toRotate = -toRotate;
        }
        //Rotate around the y axis
        transform.Rotate(Vector3.up * Time.deltaTime * toRotate);
    }

    protected void OnTriggerEnter(Collider col)
    {
        if(!enabled)
        {
            return;
        }
        //Attach the next gear
        Rotater other = col.GetComponent<Rotater>();
		if(other!=null)
		{
			bool newGear = (attachedGears.Count == 0);
			//become connected
			attachedGears.Add(other);
			if(newGear)
			{
				//Only check connection when new gears are added
				CheckConnectionToRoot();
			}
		}
    }

    protected void OnTriggerExit(Collider col)
    {
        if (!enabled)
        {
            return;
        }
        //Unattach the gear
        Rotater other = col.GetComponent<Rotater>();
        if(other!=null && attachedGears.Contains(other))
        {
            //We were connected, break that connection
            attachedGears.Remove(other);

            CheckConnectionToRoot();
        }
    }

    /// <summary>
    /// Recursively walk the tree of connected gears until one is found that is the root gear or all gears in the chain are walked
    /// With side effects of starting to rotate if connected, or stopping if not
    /// </summary>
    /// <param name="gears">Rotater's in this list have already been checked - don't check again</param>
    /// <returns>true if the chain is connected to the root gear</returns>
    public bool CheckConnectionToRoot(List<Rotater> checkedGears = null)
    {
        if (checkedGears == null)
        {
            //Initialize default arguments (ie, checking starting with me)
            checkedGears = new List<Rotater>();
        }
        if (rootGear)
        {
            //I am the root, whatever called this is connected
            return true;
        }
        //Get all attached gears that haven't been checked yet
        IEnumerable<Rotater> unCheckedGears = attachedGears.Where(gear => !checkedGears.Contains(gear));

        //Add myself to checked gears and recurse
        checkedGears.Add(this);
        foreach (Rotater gear in unCheckedGears)
        {
            if (gear.CheckConnectionToRoot(checkedGears))
            {
                //We are connected!
                isRotating = true;
                clockwise = !gear.clockwise;
                if(obstacle!=null)
                {
                    obstacle.PowerOn();
                }
                return true;
            }
            else
            {
                //Probably doing unnecessary work when 3 gears are connected to each other but...
                //Add the gear we just checked to the list and try the other gear
                checkedGears.Add(gear);
            }
        }
        //We aren't connected to the root gear
        isRotating = false;
        if(obstacle!=null)
        {
            obstacle.PowerOff();
        }
        return false;
    }

}
