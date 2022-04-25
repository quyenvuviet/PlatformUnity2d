using UnityEngine;

namespace Game.Scripts.player
{
    /// <summary>
    /// Qu?n lý ?ánh c?a ng??i ch?i
    /// </summary>
    public class PlayerAttack : MonoBehaviour
    {
        /// <summary>
        /// T?c ?? ?ánh c?a ng??i ch?i
        /// </summary>
        [SerializeField] private float AttackCoolDown;
        [SerializeField] private Transform FireballPos;
        [SerializeField] private GameObject Projectile;
        [SerializeField] private GameObject[] Fireballhoder;
        //[SerializeField] private GameObject fireball;
        private PoolManager fireBall;

        /// <summary>
        /// Hành ??ng c?a ng??i ch?i
        /// </summary>
        private Animator animator;

        /// <summary>
        /// Di chuy?n c?a ng??i ch?i
        /// </summary>
        private PlayerMove playerMove;

        /// <summary>
        /// Th?i gian ng?t quãng có th? ?ánh c?a ng??u ch?i
        /// </summary>
        private float cooldwonTime = Mathf.Infinity;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            playerMove = GetComponent<PlayerMove>();

        }

        /// <summary>
        /// Hàm này ???c kh?i t?o sau khi biên d?ch
        /// </summary>
        private void Start()
        {
        }
        private void Update()
        {
            if (Input.GetMouseButton(0) && cooldwonTime > AttackCoolDown && playerMove.canAttack())
            {
                this.Attack();
            }
            cooldwonTime += Time.deltaTime;
        }

        private void Attack()
        {
            animator.SetTrigger("attack");
            cooldwonTime = 0;
            var Poolfireball=Projectile.Spawn().GetComponent<Projectile>();
            Poolfireball.OnStart();

            Poolfireball.transform.position = FireballPos.position;
            var b = transform.localScale.x > 0 ? 1 : -1;
            Poolfireball.GetComponent<Rigidbody2D>().AddForce((transform.localScale.x>0?1:-1)*Vector2.right* Poolfireball.speed,ForceMode2D.Impulse);
            Poolfireball.transform.localScale = new Vector3(1 * b, 1, 1);

        
        }

        private int FindFireball()
        {
            for (int i = 0; i < Fireballhoder.Length; i++)
            {
                if (!Fireballhoder[i].activeInHierarchy)
                {
                    return i;
                }
            }
            return 0;
        }
    }
}