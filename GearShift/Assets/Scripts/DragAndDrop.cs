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

public class DragAndDrop : MonoBehaviour
{
	/**********************/
	/** Internal Classes **/
	/**********************/
	
	/**********************/
	/**    Model Data    **/
	/**********************/
	public GameObject baseGear;
	private bool isMoveable;
	private Vector3 pointOnScreen;
	private Vector3 offset;
	
	/**********************/
	/**   Constructors   **/
	/**********************/
	
	/**********************/
	/**   Initializers   **/
	/**********************/
	// Use this for initialization
	void Start()
	{
		isMoveable = true;
	}
	
	/**********************/
	/**     Updating     **/
	/**********************/
	// Update is called once per frame
	void Update() {}
	
	/**********************/
	/** Accessor Methods **/
	/**********************/
	bool getMoveable()
	{ return this.isMoveable; }
	
	float getXPosition()
	{ return this.transform.position.x; }
	
	float getYPosition()
	{ return this.transform.position.y; }
	
	float getZPosition()
	{ return this.transform.position.z; }
	
	/**********************/
	/** Mutation Methods **/
	/**********************/
	// Needed for later "finalizing" gear placement
	void setMoveable(bool flag)
	{ this.isMoveable = flag; }
	
	void setXPosition(float value)
	{ this.transform.position = new Vector3(value, this.transform.position.y, this.transform.position.z); }
	
	void setYPosition(float value)
	{ this.transform.position = new Vector3(this.transform.position.x, value, this.transform.position.z); }
	
	void setZPosition(float value)
	{ this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, value); }
	
	/**********************/
	/** Operator Methods **/
	/**********************/
	
	/**********************/
	/** Events / Drivers **/
	/**********************/
	// Called when the gear is selected.
	void OnMouseDown()
	{ 
		pointOnScreen = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointOnScreen.z));
	}
	
	// Called when the selected gear is translated on the screen.
	void OnMouseDrag() 
	{  
		if (this.getMoveable())
		{
			// Apply Mouse Coordinates of the Gear.
			this.setXPosition(Input.mousePosition.x);
			this.setYPosition(Input.mousePosition.y);
			this.setZPosition(pointOnScreen.z);
			
			// Translate Mouse to Screen Coordinates.
			transform.position = Camera.main.ScreenToWorldPoint(transform.position) + offset;
			
			// Apply Level Boundaries
			if (this.getXPosition() < -4.0f)
			{ this.setXPosition(-4.0f); }
			
			if (this.getXPosition() > 4.0f)
			{ this.setXPosition(4.0f); }
			
			if (this.getZPosition() < -4.0f)
			{ this.setZPosition(-4.0f); }
			
			if (this.getZPosition() > 4.0f)
			{ this.setZPosition(4.0f); }
			
			// Apply Snapping
		}
	}
	
	/**********************/
	/** Debuggers / Logs **/
	/**********************/
}
