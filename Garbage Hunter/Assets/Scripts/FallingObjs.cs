using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FallingObjs : MonoBehaviour
{
    private GameObject objsFall;
    private float startT = 2.0f;
    private float x = 2.5f;
    private int countObjs = 0;
    // Level of difficulty. Falling speed and total number of trash varies depends on this level. TODO: make it be set at the start of game
    public int level = 1;
    private System.Random random;

    public Transform groundref;



    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        objsFall = new GameObject("Falling");
        objsFall.transform.parent = this.gameObject.transform;

        objsFall.gameObject.tag = "Trash";
        // create falling obj on every x seconds untill the counter = level * 25 
        InvokeRepeating("createType", startT, x);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // create ramdon type of trash
    void createType()
    {

        int type = random.Next(1, 4);
        createObjs(type);
        countObjs++;
        // if total number of trash reach the level of difficulty, stop
        if (countObjs == level * 25)
        {
            CancelInvoke();
        }
    }

    // Create trash and set up its properties like gravity, falling speed, collision, and position, parrent class,
    // setting color, Red for disposible, Green for recycle, Balck for Landfill. 
    void createObjs(int type)
    {
        print("creating obj");
        GameObject curTrash;
        GameObject detector;

        if (type == 1) // Blue Cube for Decomposable

        {
            curTrash = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var cubeRenderer = curTrash.GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", Color.blue);
        }
        else if (type == 2) //Green Cylinder for Recyclable

        {
            curTrash = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            var cubeRenderer = curTrash.GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", Color.green);
        }
        else    // Black sphere for Harmful Landfill

        {
            curTrash = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            var cubeRenderer = curTrash.GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", Color.black);
        }

        // positioning  (int)groundref.position.y + 
        int x1 = random.Next(12, 55);
        int y1 = 400;
        int z1 = random.Next(-80, 250);

        curTrash.transform.parent = objsFall.gameObject.transform;

        curTrash.transform.position = new Vector3(x1, groundref.position.y + y1, z1);

        // parrent
        // set gragity, pickup
        curTrash.AddComponent<Rigidbody>();
        curTrash.AddComponent<PickUp>();
        int tagrand = random.Next(1, 4);
        switch (tagrand)
        {
            default:
                break;
            case 1:
                curTrash.GetComponent<PickUp>().setType(Type.Trash);
                break;
            case 2:
                curTrash.GetComponent<PickUp>().setType(Type.Trash);
                break;
            case 3:
                curTrash.GetComponent<PickUp>().setType(Type.Trash);
                break;
            case 4:
                curTrash.GetComponent<PickUp>().setType(Type.Trash);
                break;
        }
        curTrash.gameObject.tag = "Trash";
        Rigidbody rgbd = curTrash.GetComponent<Rigidbody>();
        rgbd.useGravity = true;
        rgbd.drag = 2;


        // set Collision
        detector = new GameObject("Collision Detector");
        detector.transform.parent = curTrash.gameObject.transform;
        detector.AddComponent<BoxCollider>();
        BoxCollider col = detector.GetComponent<BoxCollider>();
        // So it spawns at the same place as the parent
        col.transform.position = new Vector3(curTrash.transform.position.x, curTrash.transform.position.y, curTrash.transform.position.z);
        //  sets how big the detection box should be
        col.size = new Vector3(5, 5, 5);
        col.isTrigger = true;

    }

}
