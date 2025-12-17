using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd.FirstPerson
{
    public class CapsuleColliderData 
    {
        public CapsuleCollider Collider { get; private set; }

        public Vector3 ColliderCenterInLocalSpace { get; private set; }

        public void Initialize (GameObject gameObject)
        {
            if (Collider !=  null || gameObject.GetComponent<CapsuleCollider>() != null)
            {
                Collider = gameObject.GetComponent<CapsuleCollider>();
                return;
            }

            Collider = gameObject.AddComponent<CapsuleCollider>();

           
            UpdateColliderData();
        }

        public void UpdateColliderData()
        {
            ColliderCenterInLocalSpace = Collider.center;
        }
    }
}
