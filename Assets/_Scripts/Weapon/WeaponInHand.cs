using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace akistd
{
    public class WeaponInHand : MonoBehaviour
    {
        private bool isWeaponInHand;
        private akistd.FirstPerson.PlayerInput input;

        [SerializeField]
        private WeaponData weaponData;

        private Camera cam;

        private Vector3 centerScreenPoint;
        private GameObject currentWeapon;

        [SerializeField]
        private float throwForce;

        [SerializeField]
        private float upForce;

        [SerializeField]
        private float callbackTime;

        [SerializeField]
        private Texture2D centerpoint;

        private void Awake()
        {
            isWeaponInHand = true;
            input = GameObject.Find("Player").GetComponent<akistd.FirstPerson.PlayerInput>(); 
        }

        private void Start()
        {
            cam = Camera.main;
            input.InputActions.Player.Interact.performed += ThrowWeapon;

            foreach (Transform item in transform)
            {
                currentWeapon = item.gameObject;
            }
            
        }


        private void OnDisable()
        {
            input.InputActions.Player.Interact.performed -= ThrowWeapon;
        }
        
        
        
       

        void OnGUI()
        {
            Vector3 point = new Vector3();
            Event currentEvent = Event.current;
            Vector2 mousePos = new Vector2();

            // Get the mouse position from Event.
            // Note that the y position from Event is inverted.
            mousePos.x = currentEvent.mousePosition.x;
            mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

            point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
            centerScreenPoint = point;
            
            GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 2, 5f, 5f), centerpoint);
        }

        private void Update()
        {
            if (!isWeaponInHand)
            {
                input.InputActions.Player.Attack.Disable();
            }
            else
            {
                
                input.InputActions.Player.Attack.Enable();
                if (currentWeapon != null)
                {
                    currentWeapon.transform.localPosition = Vector3.zero;
                    currentWeapon.transform.localRotation = Quaternion.Euler(71.2f, -72.3f, 26.2f);
                }
            }
            
        }


        private void ThrowWeapon(InputAction.CallbackContext context)
        {

            if (isWeaponInHand)
            {

                //Debug.Log("im throwing");
                foreach (Transform child in transform)
                {

                    //Vector3 forceDir = cam.transform.forward;
                    float x = Screen.width / 2;
                    float y = Screen.height / 2;
                    var ray = cam.ScreenPointToRay(new Vector3(x, y, 0));
                    Vector3 forceDir = ray.direction;
                    //RaycastHit hit;
                    /*if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 200f))
                    {
                        forceDir = (hit.point - cam.transform.position).normalized;
                    }*/
                    //Vector3 vectorToAdd = forceDir * throwForce + cam.transform.up * upForce;

                    Vector3 vectorToAdd = forceDir * throwForce + cam.transform.up * upForce;
                    child.localRotation = Quaternion.Euler(180f, 0f, 0f);
    
                    child.gameObject.GetComponent<Rigidbody>().AddForce(vectorToAdd, ForceMode.Impulse);
                    child.gameObject.GetComponent<Rigidbody>().useGravity = true;


                    AudioManager.Instance.Play(weaponData.swordSFX.audioList[1]);
                    child.parent = null;
                    currentWeapon = child.gameObject;
                }
                isWeaponInHand = false;
            }
            else {
                //Debug.Log("im calling");
                input.InputActions.Player.Interact.Disable();
                currentWeapon.transform.parent = gameObject.transform;
                foreach (Transform child in transform)
                {
   
                    StartCoroutine(LerpPosition(gameObject.transform.position, callbackTime));
                    child.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    child.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    child.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                }


            }
        }
        IEnumerator LerpPosition(Vector3 targetPosition, float duration)
        {
            
            float time = 0;
            Vector3 startPosition = currentWeapon.transform.position;
            //AudioManager.Instance.StopSfx();
            AudioManager.Instance.Play(weaponData.swordSFX.audioList[0]);
            GameObject trail = GameObject.FindWithTag("Weapon").transform.Find("Trail").gameObject;
            trail.SetActive(true);
            while (time < duration)
            {
                currentWeapon.transform.Rotate(180 * Time.deltaTime * 10f, 180 * Time.deltaTime * 10f, 180 * Time.deltaTime * 10f);
                currentWeapon.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            trail.SetActive(false);
            currentWeapon.transform.position = targetPosition;
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localRotation = Quaternion.Euler(71.2f, -72.3f, 26.2f);
            isWeaponInHand = true;
            AudioManager.Instance.Play(weaponData.swordSFX.audioList[2]);
            input.InputActions.Player.Interact.Enable();
        }


    }
}
