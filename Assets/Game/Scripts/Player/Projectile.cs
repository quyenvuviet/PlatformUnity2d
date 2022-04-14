using UnityEngine;

namespace Game.Scripts.player
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        private float direction;
        private bool hit;
        private Animator animator;
        private float lifetime;
        private BoxCollider2D boxCollider;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (hit) return;
            float movementSpeed = speed * Time.deltaTime * direction;
            transform.Translate(movementSpeed, 0, 0);
            lifetime += Time.deltaTime;
            if (lifetime > 1)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            hit = true;
            boxCollider.enabled = false;
            animator.SetTrigger("explode");
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