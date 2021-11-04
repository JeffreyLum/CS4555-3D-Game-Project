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

    private System.Random random;
    // Start is called before the first frame update

    public Sprite trashsp;
    public Sprite plasticsp;
    public Sprite glasssp;
    public Sprite papersp;

    void Start()
    {
        random = new System.Random();
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

                    while (trash_p > 0)
                    {
                        if (trash_p >= divisor)
                        {
                            fillTable(Type.Trash, (int)divisor);
                            trash_p -= divisor;
                            playermove.removeTrash((int) divisor);
                        } else
                        {
                            fillTable(Type.Trash, (int)trash_p);
                            playermove.removeTrash((int)trash_p);
                            trash_p = 0;
                        }
                    }

                    while (paper_p > 0)
                    {
                        if (paper_p >= divisor)
                        {
                            fillTable(Type.Paper, (int)divisor);
                            playermove.removePaper((int)divisor);
                            paper_p -= divisor;
                        }
                        else
                        {
                            fillTable(Type.Paper, (int)paper_p);
                            playermove.removePaper((int)paper_p);
                            paper_p = 0;
                        }
                    }

                    while (plastic_p > 0)
                    {
                        if (plastic_p >= divisor)
                        {
                            fillTable(Type.Plastic, (int)divisor);
                            playermove.removePlastic((int)divisor);
                            plastic_p -= divisor;
                        }
                        else
                        {
                            fillTable(Type.Plastic, (int)plastic_p);
                            playermove.removePlastic((int)plastic_p);
                            plastic_p = 0;
                        }
                    }

                    while (glass_p > 0)
                    {
                        if (glass_p >= divisor)
                        {
                            fillTable(Type.Glass, (int)divisor);
                            playermove.removeGlass((int)divisor);
                            glass_p -= divisor;
                        }
                        else
                        {
                            fillTable(Type.Glass, (int)glass_p);
                            playermove.removeGlass((int)glass_p);
                            glass_p = 0;
                        }
                    }



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
        newOb.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        int sizex=0;
        int sizey=0;

        int hitx=0;
        int hity=0;

        int x1 = random.Next(-500,300);
        int y1 = random.Next(-200,200);

        if (input == Type.Trash)
        {

            newOb.GetComponent<Image>().sprite = trashsp;
            newOb.GetComponent<DragObjectUI>().setType(Type.Trash);
            sizex = 310;
            sizey = 256;

            hitx = 310;
            hity = 172;

            

        } else if (input == Type.Plastic){
            newOb.GetComponent<Image>().sprite = plasticsp;
            newOb.GetComponent<DragObjectUI>().setType(Type.Plastic);

            sizex = 308;
            sizey = 272;

            hitx = 116;
            hity = 272;
        }
        else if (input == Type.Paper)
        {
            newOb.GetComponent<Image>().sprite = papersp;
            newOb.GetComponent<DragObjectUI>().setType(Type.Paper);

            sizex = 291;
            sizey = 258;

            hitx = 291;
            hity = 258;
        }
        else if (input == Type.Glass)
        {
            newOb.GetComponent<Image>().sprite = glasssp;
            newOb.GetComponent<DragObjectUI>().setType(Type.Glass);

            sizex = 195;
            sizey = 266;

            hitx = 195;
            hity = 266;
        }

        newOb.GetComponent<RectTransform>().sizeDelta = new Vector2(sizex, sizey);
        newOb.GetComponent<BoxCollider2D>().size = new Vector2(hitx,hity);
        newOb.GetComponent<RectTransform>().position = new Vector2(x1, y1);
        newOb.GetComponent<RectTransform>().SetParent(sortui.transform);
        newOb.SetActive(true);
    }




}
