using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class CameraControlledIK : MonoBehaviour
    {
        public Transform spineToOrientate;

        // Update is called once per frame
        void LateUpdate()
        {
            spineToOrientate.rotation = transform.rotation;
        }
    }
}
