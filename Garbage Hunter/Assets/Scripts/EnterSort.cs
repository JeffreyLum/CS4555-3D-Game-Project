using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using System.Diagnostics;
using UnityEngine;
 

public class EnterSort : MonoBehaviour
{
    bool debounce = false;
    float stored_speed = 16;
    float divisor = 4;
    private Collider hostage;
    public RectTransform sortui;
    // Start is called before the first frame update

    public Sprite trashsp;
    public Sprite plasticsp;
    public Sprite glasssp;
    public Sprite papersp;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updatebool(bool input)
    {
        debounce = input;
    }

    public void free()
    {
        if (hostage.tag == "Player") // If the collision area touches the player, the object gets destroyed.
        {

            if (hostage.GetComponent<PlayerMovement>())
            {
                var playermove = hostage.GetComponent<PlayerMovement>();
                playermove.speed = stored_speed;
                Cursor.lockState = CursorLockMode.Locked;
                sortui.gameObject.SetActive(false);
                debounce = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("touched_EnterSort");
        if (debounce == false)
        {
            if (other.tag == "Player") // If the collision area touches the player, the object gets destroyed.
            {

                if (other.GetComponent<PlayerMovement>())
                {

                    var playermove = other.GetComponent<PlayerMovement>();
                    Cursor.lockState = CursorLockMode.Confined;
                    stored_speed = playermove.speed;
                    hostage = other;
                    playermove.speed = 0;
                    debounce = true;

                    float inv_p = playermove.getInvSpace();

                    float trash_p = playermove.getTrash();
                    float paper_p = playermove.getPaper();
                    float glass_p = playermove.getGlass();
                    float plastic_p = playermove.getPlastic();





                    sortui.gameObject.SetActive(true);


                }
            }
        }

    }

    private void fillTable(Type input, int amount)
    {
        GameObject newOb = new GameObject();
        newOb.AddComponent<Image>();
        newOb.AddComponent<DragObjectUI>();
        newOb.GetComponent<DragObjectUI>().setAmount(amount);
        newOb.AddComponent<Rigidbody2D>();
        newOb.GetComponent<Rigidbody2D>().gravityScale = 0;
        newOb.AddComponent<BoxCollider2D>();
        newOb.GetComponent<BoxCollider2D>().isTrigger = true;

        if (input == Type.Trash)
        {

            newOb.GetComponent<Image>().sprite = trashsp;
            newOb.GetComponent<DragObjectUI>().setType(Type.Trash);

        } else if (input == Type.Plastic){
            newOb.GetComponent<Image>().sprite = plasticsp;
            newOb.GetComponent<DragObjectUI>().setType(Type.Plastic);
        }
        else if (input == Type.Paper)
        {
            newOb.GetComponent<Image>().sprite = papersp;
            newOb.GetComponent<DragObjectUI>().setType(Type.Paper);
        }
        else if (input == Type.Glass)
        {
            newOb.GetComponent<Image>().sprite = glasssp;
            newOb.GetComponent<DragObjectUI>().setType(Type.Glass   );
        }

        newOb.GetComponent<RectTransform>().SetParent(sortui.transform);
        newOb.SetActive(true);
    }




}
