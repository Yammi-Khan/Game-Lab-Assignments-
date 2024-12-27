using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    Camera cam;
    [SerializeField]
    InputAction fire;
    bool isFired;
    [SerializeField]
    float forceMagnitude = 1000f;

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
        if (fire.WasPressedThisFrame())
        {
            isFired = true;
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        GameObject hitGO = Target(out hit);
        if (isFired && hitGO)
        {
            //Start in a seperate thread
            StartCoroutine(Fire(hit, hitGO));
        }
    }


    private IEnumerator Fire(RaycastHit hit, GameObject hitGO)
    {
        isFired = false;

        if (hitGO.CompareTag("Enemy"))
        {
            Enemy enemy = hitGO.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.Damage(hit.point, cam.transform.position, forceMagnitude);
            }
        }
        yield return null;
    }

    private GameObject Target(out RaycastHit hit)
    {
        bool isHit;
        Vector2 screenPos = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = cam.ScreenPointToRay(screenPos);
        isHit = Physics.Raycast(ray, out hit, Mathf.Infinity);
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 1000f, Color.red);
        if (isHit)
        {
            return hit.transform.root.gameObject;
        }
        else
        {
            return null;
        }
    }
}
