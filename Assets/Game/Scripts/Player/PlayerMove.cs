using UnityEngine;

namespace Game.Scripts.player
{
    public class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D body;

        [SerializeField]
        private float Speed;

        [SerializeField]
        private float JumpPower;

        private float walljumpCoolider;
        private Animator animator;
        private float horizontal;
        private BoxCollider2D boxcollider;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private LayerMask groundwall;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            boxcollider = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            if (horizontal > 0.01)
            {
                body.transform.localScale = Vector3.one;
            }
            if (horizontal < -0.01)
            {
                body.transform.localScale = new Vector3(-1, 1, 1);
            }
            if (walljumpCoolider > 0.02)
            {
                body.velocity = new Vector2(horizontal * Speed, body.velocity.y);
                if (this.onWall() && !isGround())
                {
                    body.gravityScale = 0;
                    body.velocity = Vector2.zero;
                }
                else
                {
                    body.gravityScale = 3;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    this.Jump();
                }
            }
            else
            {
                walljumpCoolider += Time.deltaTime;
            }

            animator.SetBool("run", horizontal != 0);
            animator.SetBool("grounded", this.isGround());
        }

        private void Jump()
        {
            Debug.Log(isGround());
            if (this.isGround())
            {
                body.velocity = new Vector2(body.velocity.x, JumpPower);
                animator.SetTrigger("jump");
            }
            else if (this.onWall() && !this.isGround())
            {
                if (horizontal == 0)
                {
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                    transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
                }
                walljumpCoolider = 0;
            }
        }

        private bool isGround()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
            return raycastHit2D.collider != null;
        }

        private bool onWall()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, groundwall);
            return raycastHit2D.collider != null;
        }

        public bool canAttack()
        {
            return horizontal == 0 && isGround() && !onWall();
        }
    }
}