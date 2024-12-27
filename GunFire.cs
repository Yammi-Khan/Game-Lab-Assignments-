using UnityEngine;
using UnityEngine.InputSystem;

public class GunFire : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    InputAction fire;
    bool isFired;

    private void OnEnable()
    {
        fire.Enable();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (fire.IsPressed())
        {
            isFired = true;
        }
    }
    private void FixedUpdate()
    {
        Vector2 screenPos = new Vector2 (Screen.width/2, Screen.height/2);
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit, Mathf.Infinity);
        if(isHit)
        {
            if(isFired)
            {
                Rigidbody rb = hit.rigidbody;
                if(rb)
                {
                    //Debug.Log("hit: " + hit.collider.gameObject.name);
                    rb.gameObject.GetComponentInParent<Animator>().enabled = false;
                    rb.AddForceAtPosition(-rb.transform.forward * 1000f, hit.point, ForceMode.Impulse);


                }
                isFired = false;
            }
            
        }
    }
}
