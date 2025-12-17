using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace akistd
{
    public class TutorialText : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            transform.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            transform.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            transform.gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        }
    }
}
