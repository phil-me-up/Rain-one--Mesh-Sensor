using Ontology;
using System;
using UnityEngine;
using RAIN.Sensors;

	[AddComponentMenu("RAIN/Sensors/MeshSensor"), RequireComponent(typeof(MeshCollider)), RequireComponent(typeof(Rigidbody))]
	public class MeshSensor : RAIN.Sensors.RaycastSensor
    {
		private MeshCollider _collider;
		
		public void Start()
		{
            pStart();
		}
	
	
        private void pStart()
        {
            _collider = (MeshCollider)gameObject.collider;
			//this sets the collider mesh to equal the objects mesh filter.
			//May not be required if you aren't using 2D Toolkit
			_collider.sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        }

        /// <summary>
        /// CreateEvidence tells the RAIN Belief system to add new evidence
        /// regarding the existence of the GameObject. This is usually called in
        /// response to Enter or Stay collider events
        /// </summary>
        /// <param name="evidence">The GameObject to add evidence for</param>
        /// <returns>Returns true if a belief was created or updated, false otherwise</returns>
        protected virtual bool CreateEvidence(GameObject evidence)
        {
            return pCreateEvidence(evidence);
        }
        private bool pCreateEvidence(GameObject evidence)
        {
            Entity entity = evidence.GetComponent<Entity>();
            if ((entity != null) && CanSee(evidence))
            {
                return AddEvidence(entity, persistenceTime);
            }
            return false;
        }
		
        protected override void Enter(GameObject evidence)
        {
			CreateEvidence(evidence);
        }
		
		protected override void Stay(GameObject evidence)
		{
			CreateEvidence(evidence);
		}

        protected override void Exit(GameObject evidence)
        {
			//Do nothing.  Objects will expire on their own
		}
	}