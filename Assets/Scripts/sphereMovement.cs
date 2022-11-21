using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class sphereMovement : MonoBehaviour
{
    public GameObject[] gos;
    public List <GameObject> herdList = new List <GameObject>();
    public Vector3 avgPos= Vector3.zero;

    public Rigidbody rb;
    public float thrust = 100f;
    public float sample;

    public void BuildList()
    {
        gos = GameObject.FindGameObjectsWithTag("Sphere");
        foreach (GameObject go in gos)
        {
            herdList.Add(go);

        }

    }



    public void AverageHerdPos()
    {
        Vector3 pos = Vector3.zero;

        foreach (GameObject go in herdList) 
        {
            pos += go.transform.position;
            avgPos = pos / herdList.Count;
            print(avgPos);
        
        }


    }

    public void MoveTowardAvg()
    {
        float distance = Vector3.Distance(gameObject.transform.position, avgPos);
        if (distance > 20)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, avgPos, .2f);


        }

        if (distance < 10)
        {
            var randCoords = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
            //rb.AddRelativeForce(Vector3.forward * thrust);
            
            
            
            sample =  Mathf.PerlinNoise(randCoords.x, randCoords.y);
            rb.AddForce(randCoords, ForceMode.Impulse);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        BuildList();
    }

    // Update is called once per frame
    void Update()
    {
        AverageHerdPos();
        MoveTowardAvg();

    }
}
