using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    //public Rigidbody projectile;
    //public float speed = 20;
    private Vector3 hookIntersection;
    private Vector3 hookDiff;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);


        //Rotation
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance = 0;

        if (plane.Raycast(ray, out distance))
        {
            hookIntersection = ray.GetPoint(distance);
            hookDiff = hookIntersection - transform.position;
        }


        Vector3 aim = hookDiff;
        aim.Normalize();
        GetComponent<Rigidbody>().velocity = new Vector3(aim.x * 20f, 0, aim.z * 20f);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(1))
        {
            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        }*/




        Debug.Log(GetComponent<Rigidbody>().velocity);
    }
}
