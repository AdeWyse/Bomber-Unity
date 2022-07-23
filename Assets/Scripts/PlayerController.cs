using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController gameController;
    private int xBounds;
    private int zBounds;
    private GameObject player;
    private int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        xBounds = gameController.gridXSize - 1;
        zBounds = gameController.gridZSize - 1;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveHorizontal = player.transform.position;
        Vector3 moveVertical = player.transform.position;
        //Checks to see if there is input(w/s/up/down)
        if (Input.GetAxis("Vertical") != 0 && Input.GetAxis("Horizontal") == 0)
        {
            //Sets the direction of movement (up or down relative to the camera)and speed
            float directionVertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            //Controls bounds by checking position and direction of movement
            if (player.transform.position.z >= 0 && directionVertical < 0 || player.transform.position.z < zBounds && directionVertical > 0)
            {
                //New position
                moveVertical = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + directionVertical); //z
                ControlRotation();
                
            }
            player.transform.position = moveVertical;
        }
        //Checks to see if there is input(a/d/left/right)
        if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") == 0)
        {
            //Sets the direction pf movement (left or right relative to the camera) and speed
            float directionHorizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            //Controls bounds by checking position and direction of movement
            if(player.transform.position.x >=  0 &&  directionHorizontal < 0 || player.transform.position.x < xBounds && directionHorizontal > 0)
            {
                //New position
                moveHorizontal = new Vector3(player.transform.position.x + directionHorizontal, player.transform.position.y, player.transform.position.z); //x
                ControlRotation();

            }
            player.transform.position = moveHorizontal;
        }
    }

    private void ControlRotation()
    {
        if(Input.GetAxis("Vertical") > 0)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(Input.GetAxis("Vertical") < 0)
        {
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            player.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
}

