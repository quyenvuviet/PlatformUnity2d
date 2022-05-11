using UnityEngine;

namespace Game.Scripts.player
{
    /// <summary>
    /// Qu?n l� ?�nh c?a ng??i ch?i
    /// </summary>
    public class PlayerAttack : MonoBehaviour
    {
        /// <summary>
        /// T?c ?? ?�nh c?a ng??i ch?i
        /// </summary>
        [SerializeField] private float AttackCoolDown;
        [SerializeField] private Transform FireballPos;
        [SerializeField] private GameObject Projectile;
        [SerializeField] private GameObject[] Fireballhoder;
        //[SerializeField] private GameObject fireball;
        private PoolManager fireBall;
        /// <summary>
        /// Di chuy?n c?a ng??i ch?i
        /// </summary>
        private PlayerMove playerMove;

        /// <summary>
        /// Th?i gian ng?t qu�ng c� th? ?�nh c?a ng??u ch?i
        /// </summary>
        private float cooldwonTime = Mathf.Infinity;

        private void Awake()
        {
           
            playerMove = GetComponent<PlayerMove>();

        }

        /// <summary>
        /// H�m n�y ???c kh?i t?o sau khi bi�n d?ch
        /// </summary>
        private void Start()
        {
        }
        /// <summary>
        /// h�m ???c g?i ? m?i 1 frame
        /// </summary>
        private void Update()
        {
            //ki?m tra xem c� th? b?n kh�ng
            if (Input.GetMouseButton(0) && cooldwonTime > AttackCoolDown && playerMove.canAttack())
            {
                this.Attack();
            }
            cooldwonTime += Time.deltaTime;
        }

        /// <summary>
        /// Vi�n ??n b?n ra ngo�i
        /// </summary>
        private void Attack()
        {
            playerMove.SetAnination("idle");
            cooldwonTime = 0;
            var Poolfireball=Projectile.Spawn().GetComponent<Projectile>();
            Poolfireball.OnStart();
            Poolfireball.transform.position = FireballPos.position;
            var SetDiretion = transform.localScale.x > 0 ? 1 : -1;
            Poolfireball.GetComponent<Rigidbody2D>().AddForce((transform.localScale.x>0?1:-1)*Vector2.right* Poolfireball.speed,ForceMode2D.Impulse);
            Poolfireball.transform.localScale = new Vector3(1 * SetDiretion, 1, 1);
        }
    }
}