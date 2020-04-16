using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    //public Rigidbody projectile;
    private float speed = 20f;
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

        //give hook velocity
        Vector3 aim = hookDiff;
        aim.Normalize();
        GetComponent<Rigidbody>().velocity = new Vector3(aim.x * speed, 0, aim.z * speed);
        //gameObject.transform.position += new Vector3(aim.x, 0f, aim.z);//(0f, 3f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit obstacle");
            Destroy(gameObject);
        }
    }
}
