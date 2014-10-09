//
// Copyright © 2014 GearShift Studios, All Rights Reserved
//
// THIS SOFTWARE IS PROVIDED BY THE AUTHORS "AS IS" AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//

using UnityEngine;
using System.Collections;

public class GearController : MonoBehaviour
{
	/**********************/
	/** Internal Classes **/
	/**********************/
	// Toolbox = The Gear is located in the Toolbox.
	// Hand = The Player is actively holding the gear. 
	// Board = The Gear is in a valid location on the board.
	public enum GearState {NULL, TOOLBOX, HAND, BOARD};
	
	/**********************/
	/**    Model Data    **/
	/**********************/
	// The state of the Gear.
	public GearState gearState = GearState.NULL;
	
	// Whether or not the Gear can be selected and moved.
	public bool isMoveable;
	
	// Determines how many objects are currently colliding the Gear.
	private int collisions;
	
	// The Last Resting Position of the Gear (free of collision).
	public Vector3 lastPosition;
	
	// Variables used to calculate the drag of a Gear.
	private Vector3 pointWorldScreen;
	private Vector3 mouseOffset;
	
	/**********************/
	/**   Constructors   **/
	/**********************/
	
	/**********************/
	/**   Initializers   **/
	/**********************/
	// Initialization Code
	private void Start()
	{
		if (isMoveable == false)
		{
			gearState = GearState.BOARD;
		}
		else if (gearState == GearState.NULL)
		{
			gearState = GearState.TOOLBOX;
			transform.position = Vector3.zero;
		}
		
		lastPosition = transform.position;
	}
	
	/**********************/
	/**     Updating     **/
	/**********************/
	// Update Code
	private void Update() {}
	
	/**********************/
	/** Accessor Methods **/
	/**********************/
	public GearState getGearState()
	{ return this.gearState; }
	
	public bool getMoveable()
	{ return this.isMoveable; }
	
	public Vector3 getLastPosition()
	{ return this.lastPosition; }
	
	/**********************/
	/** Mutation Methods **/
	/**********************/
	public void setGearState(GearState state)
	{ this.gearState = state; }
	
	public void setMoveable(bool flag)
	{ this.isMoveable = flag; }
	
	public void setLastPosition(Vector3 pos)
	{ this.lastPosition = pos; }
	
	/**********************/
	/** Operator Methods **/
	/**********************/
	private bool isValidLocation()
	{
		if (this.collisions > 0)
		{ return false; }
		else
		{ return true; }
	}
	
	/**********************/
	/** Events / Drivers **/
	/**********************/
	// Called when the gear is selected.
	private void OnMouseDown()
	{
		this.setGearState(GearState.HAND);
		
		this.pointWorldScreen = Camera.main.WorldToScreenPoint(this.transform.position);
		this.mouseOffset = this.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.pointWorldScreen.z));
		this.rigidbody.isKinematic = false;
	}
	
	// Called when the selected gear moves.
	private void OnMouseDrag() 
	{
		// Apply Mouse Position.
		this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.pointWorldScreen.z - 1.0f)) + this.mouseOffset;
		
		// Apply Level Boundaries.
		if (this.transform.position.x < -3.6f)
		{ this.transform.position = new Vector3(-3.6f, this.transform.position.y, this.transform.position.z); }
		
		if (this.transform.position.x > 3.6f)
		{ this.transform.position = new Vector3(3.6f, this.transform.position.y, this.transform.position.z); }
		
		if (this.transform.position.z < -3.6f)
		{ this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -3.6f); }
		
		if (this.transform.position.z > 3.6f)
		{ this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 3.6f); }
		
		// Check if location is valid.
		if (this.isValidLocation())
		{ this.renderer.material.color = Color.green; }
		else
		{ this.renderer.material.color = Color.red; }
	}
	
	// Called when the selected gear is dropped.
	private void OnMouseUp()
	{
		if (this.isValidLocation())
		{
			// Apply Mouse Position.
/*			this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.pointWorldScreen.z)) + this.mouseOffset;
			
			// Apply Level Boundaries.
			if (this.transform.position.x < -3.6f)
			{ this.transform.position = new Vector3(-3.6f, this.transform.position.y, this.transform.position.z); }
			
			if (this.transform.position.x > 3.6f)
			{ this.transform.position = new Vector3(3.6f, this.transform.position.y, this.transform.position.z); }
			
			if (this.transform.position.z < -3.6f)
			{ this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -3.6f); }
			
			if (this.transform.position.z > 3.6f)
			{ this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 3.6f); }
			
			// Lock new Last Position.*/
			this.setLastPosition(this.transform.position);
			
			// Convert Gear State to mark it as being on the Board.
			this.setGearState(GearState.BOARD);
		}
		else
		{
			// Revert to Last Position.
			this.transform.position = this.getLastPosition();
		}
		
		// Change to Deselected Graphics
		this.renderer.material.color = new Color(0.43529411764705882352941176470588f, 0.42352941176470588235294117647059f, 0.55686274509803921568627450980392f, 1.0f);
		this.rigidbody.isKinematic = true;
	}
	
	// Called when the Gear is colliding with another object.
	void OnCollisionEnter()
	{ collisions = collisions + 1; }
	
	// Called when the Gear is no longer colliding with an object.
	void OnCollisionExit()
	{ collisions = collisions - 1; }
	
	/**********************/
	/** Debuggers / Logs **/
	/**********************/
}
