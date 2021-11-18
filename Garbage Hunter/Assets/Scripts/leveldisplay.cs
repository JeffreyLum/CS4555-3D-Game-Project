using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class leveldisplay : MonoBehaviour
{
    public FallingObjs fobj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "Lv " + fobj.getlevel() + " Score";
    }
}
