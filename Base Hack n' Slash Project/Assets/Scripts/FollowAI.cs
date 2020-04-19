using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{
    public NavMeshAgent agent;
    Transform target;
    //List<GameObject> jellies;
    public GameObject jelly;
    private float spawnDelay = 5f;
    public int enemyLevel;
    public bool isDead = false;
    private float speed;
    private Vector3 size = new Vector3();
    private int enemyType = 0;
    private Renderer mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>();
        if (name == "MeleeJelly")
        {
            enemyLevel = 2;
            enemyType = 0;
            
        }
        else if (name == "RangedJelly")
        {
            enemyLevel = 1;
            enemyType = 3;
        }
        else if (name == "IceJelly1")
        {
            enemyLevel = 1;
            enemyType = 1;
        }
        else if (name == "IceJelly2")
        {
            enemyLevel = 2;
            enemyType = 1;
        }
        else if (name == "IceJelly3")
        {
            enemyLevel = 3;
            enemyType = 1;
        }
        else if (name == "PoisonJelly1")
        {
            enemyLevel = 1;
            enemyType = 2;
        }
        else if (name == "PoisonJelly2")
        {
            enemyLevel = 2;
            enemyType = 2;
        }
        else if (name == "PoisonJelly3")
        {
            enemyLevel = 3;
            enemyType = 2;
        }
        else
        {
            float levelRandom = Random.Range(0, 1);
            if (levelRandom <= .6f) enemyLevel = 1;
            if (levelRandom > .6f && levelRandom <= .9f) enemyLevel = 2;
            if (levelRandom > .9f) enemyLevel = 3;

            float typeRandom = Random.Range(0, 1);
            if (typeRandom <= .49f) enemyType = 1;
            if (typeRandom > .49f) enemyType = 2;
        }


        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        switch(enemyLevel)
        {
            case 1:
                speed = 20;
                size = new Vector3(1, 1, 1);
                transform.localScale = size;
                
                break;
            case 2:
                speed = 15;
                size = new Vector3(2, 2, 2);
                transform.localScale = size;
                break;

            case 3:
                speed = 10;
                size = new Vector3(3, 3, 3);
                transform.localScale = size;
                break;
            default:
                speed = 20;
                size = new Vector3(1, 1, 1);
                transform.localScale = size;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) Destroy(gameObject);
       
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance >= 10f) agent.SetDestination(target.position);
    }
    
}
