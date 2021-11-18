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
    private int amount; //object drop scaling
    private System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        LevelStart();
    }

    public void LevelStart()
    {
        amount = level * 25;
        x = (float)(62.5* (double)(Math.Floor( (double)(1+((double)amount /100))) ) / (double)amount);
        random = new System.Random();
        objsFall = new GameObject("Falling");
        objsFall.transform.parent = this.gameObject.transform;

        objsFall.gameObject.tag = "Trash";
        // create falling obj on every x seconds untill the counter = level * 25 
        InvokeRepeating("createType", startT, x);
    }

    public void levelplus()
    {
        level += 1;
    }

    public int getlevel()
    {
        return level;
    }

    public int getgradeavg()
    {
        return (int)Math.Floor(amount * 0.75);
    }

    public int getfull()
    {
        return amount;
    }
    // create ramdon type of trash
    void createType()
    {
        int type = random.Next(1, 5);
        createObjs(type);
        countObjs++;
        // if total number of trash reach the level of difficulty, stop
        if (countObjs == level * 25)
        { CancelInvoke(); }
    }

    // Create trash and set up its properties like gravity, falling speed, collision, and position, parrent class,
    // setting color, Red for disposible, Green for recycle, Balck for Landfill. 
    void createObjs(int type)
    {
        print("creating obj");
        GameObject curTrash;
        GameObject detector;
        int x1;
        int y1;
        int z1;

        if (type == 1) // Blue Cube for Decomposable
        {
            curTrash = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var cubeRenderer = curTrash.GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", Color.blue);
            //curTrash.gameObject.tag = "Glass";
            curTrash.AddComponent<PickUp>();
            curTrash.GetComponent<PickUp>().setType(Type.Glass);
        }
        else if (type == 2) //Green Cylinder for Recyclable
        {
            curTrash = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            var cubeRenderer = curTrash.GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", Color.green);
            curTrash.AddComponent<PickUp>();
            curTrash.GetComponent<PickUp>().setType(Type.Paper);
        }
        else if (type == 3) //Green Cylinder for Recyclable
        {
            curTrash = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            var cubeRenderer = curTrash.GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", Color.white);
            curTrash.AddComponent<PickUp>();
            curTrash.GetComponent<PickUp>().setType(Type.Plastic);
        }
        else    // Black sphere for Harmful Landfill
        {
            curTrash = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            var cubeRenderer = curTrash.GetComponent<Renderer>();
            cubeRenderer.material.SetColor("_Color", Color.black);
            curTrash.AddComponent<PickUp>();
            curTrash.GetComponent<PickUp>().setType(Type.Trash);
        }

        // positioning  (int)groundref.position.y + 
        int T_or_I = random.Next(1, 4);

        if (T_or_I == 1)  // 1 = T shape spawing 
        {
            x1 = random.Next(-110, 195);
            y1 = 400;
            z1 = random.Next(-200, -165);
        }
        else  // i  shape spawing
        {
            x1 = random.Next(20, 65);
            y1 = 400;
            z1 = random.Next(-300, 500);
        }

        curTrash.transform.parent = objsFall.gameObject.transform;
        // groundref.position.
        curTrash.transform.position = new Vector3(x1, y1, z1);

        // parrent
        // set gragity, pickup
        curTrash.AddComponent<Rigidbody>();
        // curTrash.AddComponent<PickUp>();

        //curTrash.gameObject.tag = "Trash";
        Rigidbody rgbd = curTrash.GetComponent<Rigidbody>();
        rgbd.useGravity = true;
        rgbd.drag = 1;

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
