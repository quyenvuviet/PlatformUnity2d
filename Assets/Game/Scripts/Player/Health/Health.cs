using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Scripts.player
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float StartingHealth;
        public float currentHealth { get; set; }
        private Animator animator;
        private bool dead;
        private void Awake()
        {
            currentHealth = StartingHealth;
        }

        public void TakeDame(float _dame)
        {
            currentHealth = Mathf.Clamp(currentHealth - _dame, 0, StartingHealth);
            if (currentHealth > 0)
            {
                animator.SetTrigger("hurt");
            }
            else
            {
                if (!dead)
                {
                    animator.SetTrigger("die");
                    GetComponent<PlayerMove>().enabled = false;
                    dead = true;
                }
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    } 
}
