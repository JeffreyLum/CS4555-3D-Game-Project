using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    public Type type;
    private bool debounce = false;

    void Start()
    {
        var outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (debounce == false)
        {
            if (other.tag == "Player") // If the collision area touches the player, the object gets destroyed.
            {

                if (other.GetComponent<PlayerMovement>())
                {

                    var playermove = other.GetComponent<PlayerMovement>();

                    float inv_p = playermove.getInvSpace();

                    float trash_p = playermove.getTrash();
                    float paper_p = playermove.getPaper();
                    float glass_p = playermove.getGlass();
                    float plastic_p = playermove.getPlastic();

                    if (trash_p + paper_p + glass_p + plastic_p < inv_p)
                    {

                        switch (type)
                        {
                            default:
                            case Type.Trash:
                                playermove.addTrash();
                                break;
                            case Type.Paper:
                                playermove.addPaper();
                                break;
                            case Type.Glass:
                                playermove.addGlass();
                                break;
                            case Type.Plastic:
                                playermove.addPlastic();
                                break;
                        }
                        debounce = true;
                        Debug.Log("touched_OnTrigger");
                        Destroy(gameObject);
                    }

                }
            }
        }
    }

    // Input case OnTriggerEnter doesn't work
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Trash")
        {
            Debug.Log("touched_CollisionEnter");
            Destroy(collision.gameObject);
        }
    }

    public void setType(Type input)
    {
        type = input;
    }

    public Type getType()
    {
        return this.type;
    }



}

public enum Type
{
    Trash,
    Paper,
    Glass,
    Plastic
}
