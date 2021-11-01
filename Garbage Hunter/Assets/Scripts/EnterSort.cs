using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class EnterSort : MonoBehaviour
{
    bool debounce = false;
    float stored_speed = 16;
    private Collider hostage;
    public RectTransform sortui;
    // Start is called before the first frame update

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

                    sortui.gameObject.SetActive(true);

                    float inv_p = playermove.getInvSpace();

                    float trash_p = playermove.getTrash();
                    float paper_p = playermove.getPaper();
                    float glass_p = playermove.getGlass();
                    float plastic_p = playermove.getPlastic();


                }
            }
        }
    }






}
