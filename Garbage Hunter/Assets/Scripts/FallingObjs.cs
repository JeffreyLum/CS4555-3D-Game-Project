using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FallingObjs : MonoBehaviour
{
    private GameObject objsFall;
    private  float startT = 2.0f;
    private float x = 2.5f;
    private int countObjs = 0;
    // Level of difficulty. Falling speed and total number of trash varies depends on this level. TODO: make it be set at the start of game
    public int level = 1;
    private System.Random random;



    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        objsFall = new GameObject("Falling");
        objsFall.transform.position = new Vector3(50,40,-42);
        objsFall.gameObject.tag = "Trash";
        // create falling obj on every x seconds untill the counter is level * 20 
        InvokeRepeating("createType", startT, x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // create ramdon type of trash
    void createType()
    {
        print("creatingtypes");
        int type = random.Next(1, 4);
        createObjs(type);
        countObjs++;
        // if total number of trash reach the level of difficulty, stop
        if (countObjs == level * 12)
        {
            CancelInvoke();
        }
    }

    // Create trash and set up its properties like gravity, falling speed, collision, and position, parrent class. 
    void createObjs(int type)
    {
        print("creating obj");
        GameObject curObj;
        GameObject detector;

        if ( type == 1)
        {
            curObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        else if (type == 2)
        {
            curObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        }
        else
        {
            curObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        // positioning
        int x1 = random.Next(25, 75);
        int y1 = random.Next(30, 50);
        int z1 = random.Next(-60, -20);
        curObj.transform.position = new Vector3(x1, y1, z1);
        // parrent
        curObj.transform.parent = objsFall.gameObject.transform;
        // set gragity, pickup
        curObj.AddComponent<Rigidbody>();
        curObj.AddComponent<PickUp>();
        curObj.gameObject.tag = "Trash";
        Rigidbody rgbd = curObj.GetComponent<Rigidbody>();
        rgbd.useGravity = true;
        rgbd.drag = 2;
        // set Collision
        detector = new GameObject("Collision Detector");
        detector.transform.parent = curObj.gameObject.transform;
        detector.AddComponent<BoxCollider>();
        BoxCollider col = detector.GetComponent<BoxCollider>();
        // So it spawns at the same place as the parent
        col.transform.position = new Vector3(curObj.transform.position.x, curObj.transform.position.y, curObj.transform.position.z);
        //  sets how big the detection box should be
        col.size = new Vector3(3, 3, 3);
        col.isTrigger = true;
        
    }

}
