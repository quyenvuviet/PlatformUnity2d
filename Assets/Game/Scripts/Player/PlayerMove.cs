using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

namespace Game.Scripts.player
{
    public partial class PlayerMove : MonoBehaviour
    {
        public enum PlayerState
        {
            NONE,
            DIE,
            IDLE,
            RUN,
            JUMP,
            SWIM,
        }
        private Rigidbody2D body;

        [Header("=====core Player=======")]
        [SerializeField]
        private float speed;

        [SerializeField]
        private float jumpPower;

        [SerializeField]
        private float houderInput;

        [SerializeField]
        private float ckeckHouderInputAttack;

        private float walljumpCoolider;
        private float horizontal;
        private BoxCollider2D boxcollider;
        [SerializeField] SkeletonAnimation AnimationData;
        private string currentAnimation = "";
        /// <summary>
        ///  bi?n h�nh
        /// </summary>
        private bool transfigure;
        [Header("=======LayerMask=========")]
        [SerializeField] private LayerMask groundLayer;

        [SerializeField] private LayerMask groundwall;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            boxcollider = GetComponent<BoxCollider2D>();
          
        }

        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            RaycastHit2D ray = CommonDebug.RayCast(transform.position, Vector2.right, 1f,groundLayer, Color.red);
            horizontal = Input.GetAxis("Horizontal");
            if (isGround())
            {
                if (horizontal != 0)
                    this.SetAnination("run");
                else if (horizontal == 0)
                {
                    this.SetAnination("idle");
                } else
                {
                    this.SetAnination("jump");
                }
            }
            if (horizontal > 0.01)
            {
                body.transform.localScale = Vector3.one;
            }
            else if (horizontal < -0.01)
            {
                body.transform.localScale = new Vector3(-1, 1, 1);
            }
             
            
            if (walljumpCoolider > 0.02)
            {
                body.velocity = new Vector2(horizontal * speed, body.velocity.y);
      
               
                if (this.onWall() && !isGround())
                {
                    body.gravityScale = 0;
                    body.velocity = Vector2.zero;
                }
                else
                {
                    body.gravityScale = 3;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    this.SetAnination("jump",false);
                    this.Jump();
                    
                }
            }
            else
            {
                walljumpCoolider += Time.deltaTime;
            }

        }

        /// <summary>
        ///  S�t h??ng quay cho nh�n v?t
        /// </summary>
        private void IsDircetion()
        {
        }

        /// <summary>
        /// nh�n v?t nh?y l�n
        /// </summary>
        private void Jump()
        {
            Debug.Log(isGround());
            if (this.isGround())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                
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

        /// <summary>
        /// ckeck xem c� ch?m ??t vs m?t ??t hay kh�ng
        /// </summary>
        /// <returns></returns>
        private bool isGround()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
            return raycastHit2D.collider != null;
        }

        /// <summary>
        /// Ki?m tra xem nh�n v?t c� ch?m v?i t??ng hay kh�ng
        /// </summary>
        /// <returns></returns>
        private bool onWall()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, groundwall);
            return raycastHit2D.collider != null;
        }

        /// <summary>
        ///  c� th? b?n khi
        /// </summary>
        /// <returns></returns>
        public bool canAttack()
        {
            return horizontal == 0 && isGround() && !onWall();
        }
        public void SetAnination(string name, bool loop = true)
        {
            if (name == this.currentAnimation)
            {
                return;
            }
            AnimationData.state.SetAnimation(0, name, loop);
            currentAnimation = name;
        }
        /// <summary>
        /// ckeck va ch?m vs vi�n g?ch
        /// </summary>
        private void ckeck()
        {

        }

        /// <summary>
        /// n?u d? ph�m th� s? ch?y nhanh
        /// </summary>
        private void inputSpeedRun()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                houderInput += Time.deltaTime;
                if (houderInput < ckeckHouderInputAttack)
                {
                    canAttack();
                }
                else
                {
                    this.speed = speed * 1.5f;
                }
            }
        }
       
    }
}