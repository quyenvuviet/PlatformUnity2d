using UnityEngine;
using System;
using System.Collections;
namespace Game.Scripts.player
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] public float speed;
        [SerializeField] private float direction;
        [SerializeField] private bool hit;
        [SerializeField] private Animator animator;
        [SerializeField] private float lifetime;
        [SerializeField] private BoxCollider2D boxCollider;


        private void Awake()
        {
            animator = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
        }
        public void OnStart()
        {
            boxCollider.enabled = true;
            StartCoroutine(IeDespawn(lifetime));
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (hit)
            {
                return;
            }
            hit = true;
            boxCollider.enabled = false;
            
            animator.SetTrigger("explode");
            if(collision.tag == "enemy")
            {
                collision.GetComponent<Health>().TakeDame(1);
            }
            if (collision.CompareTag("enemy"))
            {
                var Pos = boxCollider.bounds.size.x / 2;
                transform.position = new Vector2(Pos, transform.position.y);
            }
            GetComponent<Rigidbody2D>().velocity =Vector2.zero;
            StartCoroutine(IeDespawn(1));
        }
        IEnumerator IeDespawn(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.Recycle();
        }
        public void SetDiretion(float _direction)
        {
            lifetime = 0;
            direction = _direction;
            gameObject.SetActive(true);
            hit = false;
            boxCollider.enabled = true;

            float locaSacleX = transform.localScale.x;

            if (Mathf.Sign(locaSacleX) != _direction)
            {
                locaSacleX = -locaSacleX;
            }
            transform.localScale = new Vector3(locaSacleX, transform.localScale.y, transform.localScale.z);
        }

        private void Deactive()
        {
            gameObject.SetActive(false);
        }
    }
}