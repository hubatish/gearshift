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
    public Obstacle obstacle;

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
    public List<Rotater> attachedGears = new List<Rotater>();

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

	/// When colliding with another gear, record that gear in a list
	/// Each Gear calls this...
    protected void OnTriggerEnter(Collider col)
    {
        //Attach the next gear
        Rotater other = col.GetComponent<Rotater>();
		if(other!=null)
		{
			//record that we're connected
			attachedGears.Add(other);

            //has this gear been placed - don't do stuff for HeldGear
            if(enabled && other.enabled)
            {
                //I'm not rotating but connected gear is
                if(other.isRotating && !isRotating)
                {
                    PowerOn(other);
                }
                //rotate the other guy if we are and he isn't
                else if(isRotating && !other.isRotating)
                {
                    other.PowerOn(this);
                }
                //we're both rotating - this might be a fail case
                else if(isRotating && other.isRotating)
                {
                    //are we rotating the same direction?
                    if(clockwise == other.clockwise)
                    {
                        //FAIL CASE!!  Probably break one of the gears
                        GameObject.Destroy(gameObject);
                    }
                }
            }
		}
    }

	/// When another gear leaves, remove that gear from our list
    protected void OnTriggerExit(Collider col)
    {
        //Unattach the gear
        Rotater other = col.GetComponent<Rotater>();
        if(other!=null)
		{
			DetachFrom(other);
		}
    }
	
	/// Actually remove the other collider from our list
	/// 	And check to see if we should rotate
	///		Separated from OnTriggerExit so it can be called by other things
	public void DetachFrom(Rotater other)
	{
		if (attachedGears.Contains(other))
        {
            //We were connected, break that connection
            attachedGears.Remove(other);

            //We need to check to see if we're still connected, or if the gear that was removed was what connecting us
            CheckMyConnection();
        }
	}
	
	protected bool appIsQuitting = false;
	//Don't call OnDestroy when application is quitting, so record that bool here
	protected void OnApplicationQuit()
	{
		appIsQuitting = true;
	}
	
	//Remove myself from all the other gears
	protected void OnDestroy()
	{
		//Let's not get errors about other objects already being destroyed
		if(appIsQuitting)
			return;
		foreach(Rotater gear in attachedGears)
		{
			//Just in case two gears are destroyed at the same time
			if(gear!=null)
			{
				gear.DetachFrom(this);
			}
		}
	}


    //An invokable public method to call CheckConnectionToRoot
    //Called by InPlaceGearState when a gear is placed
    protected void BePlaced()
    {
        CheckMyConnection();
    }

    public void CheckMyConnection()
    {
        Rotater connectingGear = CheckConnectionToRoot();
        
        if(connectingGear!=null)
        {
            PowerOn(connectingGear);
        }
        else
        {
            PowerOff();
        }
    }

    /// <summary>
    /// Recursively walk the graph of connected gears until one is found that is the root gear or all gears in the chain are walked
    /// With side effects of starting to rotate if connected, or stopping if not
    /// </summary>
    /// <param name="gears">Rotater's in this list have already been checked - don't check again</param>
    /// <returns>true if the chain is connected to the root gear</returns>
    public Rotater CheckConnectionToRoot(List<Rotater> checkedGears = null)
    {
        if (rootGear)
        {
            //I am the root, whatever called this is connected
            //Debug.Log("the great root is found");
            return this;
        }

        if (checkedGears == null)
        {
            //Initialize default arguments (ie, checking starting with me)
            checkedGears = new List<Rotater>();
        }
        //Get all attached gears that haven't been checked yet
        List<Rotater> unCheckedGears = attachedGears.Where(gear => !checkedGears.Contains(gear)).ToList();

        //Add myself to checked gears and recurse
        checkedGears.Add(this);

        foreach (Rotater gear in unCheckedGears)
        {
            if (gear.CheckConnectionToRoot(checkedGears)!=null)
            {
                //PowerOn(gear);
                return gear;
            }
            else
            {
                //Probably doing unnecessary work when 3 gears are connected to each other but...
                //Add the gear we just checked to the list and try the other gear
                checkedGears.Add(gear);
            }
        }
        return null;            
    }

    //Power myself up, tell any obstacles attached
    //  And power up any attached gears that aren't powered yet
    public void PowerOn(Rotater gear)
	{
		isRotating = true;
		clockwise = !gear.clockwise;
		if(obstacle!=null)
		{
			obstacle.PowerOn();
		}

        //Power on unpowered attached gears
        foreach (Rotater attached in attachedGears)
        {
            if(!attached.isRotating)
            {
                attached.PowerOn(this);
            }
        }
	}

    //Just power off and tell the obstacle we did so
	public void PowerOff()
	{
		isRotating = false;
		if(obstacle!=null)
		{
			obstacle.PowerOff();
		}

        //Power off nearby powered attached gears
        foreach (Rotater attached in attachedGears)
        {
            if (attached.isRotating)
            {
                attached.PowerOff();
            }
        }

	}
	
}
