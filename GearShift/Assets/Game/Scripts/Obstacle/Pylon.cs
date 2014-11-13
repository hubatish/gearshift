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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GearShift
{
    /// <summary>
    /// Transfer energy to another pylon
    /// </summary>
    public class Pylon : MonoBehaviour
    {
		/**********************/
		/**  Internals Data  **/
		/**********************/
		// Value between 0.0 and 1.0 marking fully inactive and fully active
		private float progress;
		
		private float baseXVal;
		private float baseYVal;
		private float baseZVal;
		
		/**********************/
		/**  Externals Data  **/
		/**********************/
		// Pylons that this Pylon is linked to. Note: One way connections can exist.
		public List<Rotation> connectedPylons;

		/**********************/
		/**   Initializers   **/
		/**********************/
		protected void Start()
		{
			foreach (Rotation rot in connectedPylons)
			{ this.GetComponent<Rotation>().rotationsList.Add(rot); }
		}

		/**********************/
		/**     Updating     **/
		/**********************/
        protected void Update()
        {}
    }
}
