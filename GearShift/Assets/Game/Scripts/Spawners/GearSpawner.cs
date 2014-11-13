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
using System.Collections.Generic;
using GearShift;

namespace GearShift
{
	/// <summary>
	///
	/// </summary>
	public class GearSpawner : MonoBehaviour
	{
		/**********************/
		/** Custom Datatypes **/
		/**********************/
		[System.Serializable]
		public class RandomGear
		{
			public GameObject Gear;
			public float Probability;
		}
		
		[System.Serializable]
		public class PresetGear
		{
			public GameObject Gear;
		}
		
		[System.Serializable]
		public class SpawnGear
		{
			public GameObject Gear;
			public Vector3 Location;
			
			public void setGear(GameObject gear)
			{ this.Gear = gear; }
		}
		
		/**********************/
		/**  Reference Data  **/
		/**********************/
		private LayerController layerController;
		private GameObject toolbarGearContainer;
		
		/**********************/
		/**  Internals Data  **/
		/**********************/
		// The combined probabilities of all Random Gears
		private float totalProbability;
		
		/**********************/
		/**  Externals Data  **/
		/**********************/
		// List of all possible Random Gears, along with their probabilities.
		public List<RandomGear> randomGearList;
		
		// Ordered List of Pre-defined Gears for Spawn Control.
		public List<PresetGear> presetGearList;
		
		// List of Gears that are spawned and in the Toolbox for pickup.
		public List<SpawnGear> spawnGearList;
		
		// The maximum number of gears that will be spawned.
		// 0 - Infinite Gears may be Spawned
		public int maxGearSpawnCount;
		
		// The current number of gears spawned in the puzzle's lifetime
		private int curGearSpawnCount;
		
		/**********************/
		/**   Initializers   **/
		/**********************/
		protected void Start()
		{
			// Locate References
			layerController = GameObject.FindWithTag("LayerController").GetComponent<LayerController>();
			toolbarGearContainer = GameObject.FindWithTag("ToolbarGearContainer");
			
			// Calculate the Total Probability for all Random Gears.
			totalProbability = 0.0f;
			foreach (RandomGear rg in randomGearList)
			{ totalProbability = totalProbability + rg.Probability; }
			
			// Set the number of Gears Used to zero.
			curGearSpawnCount = 0;
		}
		
		/**********************/
		/**     Updating     **/
		/**********************/
        protected void Update()
		{
			if ((maxGearSpawnCount == 0) || (curGearSpawnCount < maxGearSpawnCount))
			{
				for (int i = 0; i < spawnGearList.Count; i++)
				{
					if ((spawnGearList[i].Gear == null) || (spawnGearList[i].Gear.GetComponent<DragDropDestroy>().getGearLocation() != DragDropDestroy.GearLocation.Toolbox))
					{
						GameObject newGear = spawnNewGear(spawnGearList[i].Location);
						newGear.transform.parent = toolbarGearContainer.transform;
						spawnGearList[i].setGear(newGear);
						curGearSpawnCount = curGearSpawnCount + 1;
					}
				}
			}
		}
		
		/**********************/
        /** Operator Methods **/
        /**********************/
		public GameObject spawnNewGear(Vector3 location)
		{
			if (presetGearList.Count > 0)
			{ return spawnPresetGear(location); }
			else if (randomGearList.Count > 0)
			{ return spawnRandomGear(location); }
			else
			{ return null; }
		}
		
		// Pulls a gear at the front of the Preset Gears Queue and spawns it.
		public GameObject spawnPresetGear(Vector3 location)
		{
			float layerFactor = -layerController.layerList[layerController.getCurrentLayer()].LayerCoordinates.position.y;
			Vector3 updatedLocation = location + (Vector3.up * layerFactor);
			
			GameObject newGear = Instantiate(presetGearList[0].Gear, updatedLocation, Quaternion.identity) as GameObject;
			presetGearList.RemoveAt(0);
			return newGear;
		}
		
		// Chooses a random gear from the Random Gears List and spawns it.
		public GameObject spawnRandomGear(Vector3 location)
		{
			float randomProbability = Random.value * totalProbability;
			float sumProbability = 0.0f;
			
			foreach (RandomGear rg in randomGearList)
			{
				sumProbability = sumProbability + rg.Probability;
				if (randomProbability < sumProbability)
				{
					float layerFactor = layerController.layerList[layerController.getCurrentLayer()].LayerCoordinates.position.y;
					Vector3 updatedLocation = location + (Vector3.up * layerFactor);
					
					return Instantiate(rg.Gear, updatedLocation, Quaternion.identity) as GameObject;
				}
			}
			return null;
		}
	}
}