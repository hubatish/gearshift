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
using GearShift;

namespace GearShift
{
	/// <summary>
	/// 
	/// </summary>
	public class EndGearScript : MonoBehaviour
	{
		/**********************/
		/**  Externals Data  **/
		/**********************/
		// Rotation Controller From the Gear linked to this obstacle
		public Rotation rotationController;
		
		// Splash Image to be displayed when victory is achieved
		public GameObject victorySplash;

		/**********************/
		/**   Initializers   **/
		/**********************/
		protected void Start() {}

		/**********************/
		/**     Updating     **/
		/**********************/
        protected void Update()
        {
			if (rotationController.getRotationStatus() != Rotation.GearRotation.NoRotation)
			{
				victorySplash.SetActive(true);
			}
        }
	}
}