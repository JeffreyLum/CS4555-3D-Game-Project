using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FallingObjs : MonoBehaviour
{
    private GameObject objsFall;
    private  float startT = 2.0f;
    private float interval = 2.5f;
    private int countObjs = 0;
    public int level = 1;
    private System.Random random;



    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        objsFall = new GameObject("Falling");
        objsFall.transform.position = new Vector3(50,40,-42);
        objsFall.gameObject.tag = "Trash";
        // create falling obj on every interval seconds untill the counter is level * 20 
        InvokeRepeating("createType", startT, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createType()
    {
        print("creatingtypes");
        int type = random.Next(1, 4);
        createObjs(type);
        countObjs++;
        if (countObjs == level * 12)
        {
            CancelInvoke();
        }
    }
    void createObjs(int type)
    {
        print("creating obj");
        GameObject cur;
        GameObject detector;

        if ( type == 1)
        {
            cur = GameObject.CreatePrimitive(PrimitiveType.Cube);
            detector = new GameObject("Collision Detector");
        }
        else if (type == 2)
        {
            cur = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            detector = new GameObject("Collision Detector");
        }
        else
        {
            cur = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            detector = new GameObject("Collision Detector");
        }
        int x1 = random.Next(25, 75);
        int y1 = random.Next(30, 50);
        int z1 = random.Next(-60, -20);
        cur.transform.position = new Vector3(x1, y1, z1);
        cur.transform.parent = objsFall.gameObject.transform;
        cur.AddComponent<Rigidbody>();
        cur.AddComponent<PickUp>();
        cur.gameObject.tag = "Trash";
        Rigidbody rgbd = cur.GetComponent<Rigidbody>();
        rgbd.useGravity = true;
        rgbd.drag = 2;

        detector.transform.parent = cur.gameObject.transform;
        detector.AddComponent<BoxCollider>();
        BoxCollider col = detector.GetComponent<BoxCollider>();
        col.transform.position = new Vector3(cur.transform.position.x, cur.transform.position.y, cur.transform.position.z); // So it spawns at the same place as the parent
        col.size = new Vector3(3, 3, 3); // This sets how big the detection box should be
        col.isTrigger = true;
        
    }

}
