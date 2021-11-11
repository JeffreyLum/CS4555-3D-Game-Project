using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBin : MonoBehaviour
{

    public PlayerMovement host;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.GetComponent("DragObjectUI"))
        {
            DragObjectUI some = (DragObjectUI)other.GetComponent("DragObjectUI");
            Type a = some.getType();
            if (a == Type.Trash)
            {
                Debug.Log("TrAAAAAAAAAASH");
            }
            else if (a == Type.Plastic)
            {
                Debug.Log("plastic");
                host.addPoint(some.getAmount());
                Destroy(other.gameObject);
            }
            else if (a == Type.Glass)
            {
                Debug.Log("glaasss");
                host.addPoint(some.getAmount());
                Destroy(other.gameObject);
            }
            else if (a == Type.Paper)
            {
                Debug.Log("papper");
                host.addPoint(some.getAmount());
                Destroy(other.gameObject);
            }

        }
    }
}
