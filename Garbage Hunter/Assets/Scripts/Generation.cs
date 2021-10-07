using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public GameObject myPrefab1;
    public GameObject myPrefab2;
    public GameObject myPrefab3;
    public GameObject myPrefab4;
    public GameObject myPrefab5;
    public GameObject myPrefab6;
    public GameObject myPrefab7;
    public GameObject myPrefab8;
    public GameObject myPrefab9; // (42 / -2) (42/-158) (speed test)

    public GameObject tilea;

    private System.Random random;

    private double[,] PrefabSize = new double[9,3];

    private double height = 139.28;
    //Hard Lock T generation
    private double[,] Point =
    {
        { 74.0,-163.5,74.0,305.5},
        { 74.0,-163.5,182.5,-163.5},
        { 182.48,-214.5,-96.4,-214.5},
        { -96.4,-163.5,12.5,-163.5},
        { 12.5,-163.5,12.5,305.5}
    };


void GetSize()
    {
        PrefabSize[0,0] = myPrefab1.GetComponentInChildren<Renderer>().bounds.size.x;
        PrefabSize[0,1] = myPrefab1.GetComponentInChildren<Renderer>().bounds.size.y;
        PrefabSize[0,2] = myPrefab1.GetComponentInChildren<Renderer>().bounds.size.z;

        PrefabSize[1,0] = myPrefab2.GetComponent<Renderer>().bounds.size.x;
        PrefabSize[1,1] = myPrefab2.GetComponent<Renderer>().bounds.size.y;
        PrefabSize[1,2] = myPrefab2.GetComponent<Renderer>().bounds.size.z;

        PrefabSize[2,0] = myPrefab3.GetComponent<Renderer>().bounds.size.x;
        PrefabSize[2,1] = myPrefab3.GetComponent<Renderer>().bounds.size.y;
        PrefabSize[2,2] = myPrefab3.GetComponent<Renderer>().bounds.size.z;

        PrefabSize[3,0] = myPrefab4.GetComponent<Renderer>().bounds.size.x;
        PrefabSize[3,1] = myPrefab4.GetComponent<Renderer>().bounds.size.y;
        PrefabSize[3,2] = myPrefab4.GetComponent<Renderer>().bounds.size.z;

        /*PrefabSize[4,0] = myPrefab5.GetComponent<Renderer>().bounds.size.x;
        PrefabSize[4,1] = myPrefab5.GetComponent<Renderer>().bounds.size.y;
        PrefabSize[4,2] = myPrefab5.GetComponent<Renderer>().bounds.size.z;

        PrefabSize[5,0] = myPrefab6.GetComponent<Renderer>().bounds.size.x;
        PrefabSize[5,1] = myPrefab6.GetComponent<Renderer>().bounds.size.y;
        PrefabSize[5,2] = myPrefab6.GetComponent<Renderer>().bounds.size.z;

        PrefabSize[6,0] = myPrefab7.GetComponent<Renderer>().bounds.size.x;
        PrefabSize[6,1] = myPrefab7.GetComponent<Renderer>().bounds.size.y;
        PrefabSize[6,2] = myPrefab7.GetComponent<Renderer>().bounds.size.z;

        PrefabSize[7,0] = myPrefab8.GetComponent<Renderer>().bounds.size.x;
        PrefabSize[7,1] = myPrefab8.GetComponent<Renderer>().bounds.size.y;
        PrefabSize[7,2] = myPrefab8.GetComponent<Renderer>().bounds.size.z;

        PrefabSize[8,0] = myPrefab9.GetComponent<Renderer>().bounds.size.x;
        PrefabSize[8,1] = myPrefab9.GetComponent<Renderer>().bounds.size.y;
        PrefabSize[8,2] = myPrefab9.GetComponent<Renderer>().bounds.size.z; */

    }

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        GetSize();
        Generate();
    }

    //fuck it hardcode it
    void Generate()
    {
        //line1 gen
        double line1 = Point[0, 3] - Point[0, 2];
        double counter = 0.0;
        while (counter < line1)
        {
            int type = random.Next(0,3);


            double temp = PrefabSize[type, 2];
            counter += temp;
            Vector3 pos = new Vector3((float)tilea.position.x, (float)(height+ (PrefabSize[type, 1]/2)), (float)(Point[0,1]+temp/2));

            switch (type)
            {
                default:

                case 0:
                    Instantiate(myPrefab1, pos, Quaternion.identity);
                    break;

                case 1:
                    Instantiate(myPrefab2, pos, Quaternion.identity);
                    break;

                case 2:
                    Instantiate(myPrefab3, pos, Quaternion.identity);
                    break;

                case 3:
                    Instantiate(myPrefab4, pos, Quaternion.identity);
                    break;

                case 4:
                    Instantiate(myPrefab1, pos, Quaternion.identity);
                    break;

                case 5:
                    Instantiate(myPrefab1, pos, Quaternion.identity);
                    break;

                case 6:
                    Instantiate(myPrefab1, pos, Quaternion.identity);
                    break;

                case 7:
                    Instantiate(myPrefab1, pos, Quaternion.identity);
                    break;

                case 8:
                    Instantiate(myPrefab1, pos, Quaternion.identity);
                    break;

            }
        }

        double line2 = Point[1, 0] - Point[1, 2];
        counter = 0.0;

    }
}
