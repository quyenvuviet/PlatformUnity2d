using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform PreviousRoom;
    [SerializeField] Transform NextRoom;
    private CameraController cam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MovetoNextRoom(NextRoom);
            }
            else
                cam.MovetoNextRoom(PreviousRoom);
        }
    }
}
