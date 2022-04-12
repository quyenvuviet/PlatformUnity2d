using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Scripts.player;

public class Raw : MonoBehaviour
{
    [SerializeField] private float dame;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDame(dame);
        }
    }

}
