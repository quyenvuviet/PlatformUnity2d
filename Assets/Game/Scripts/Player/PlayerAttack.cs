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

        [SerializeField] private GameObject FireballPos;
        [SerializeField] private GameObject[] Fireballhoder;

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

        /// <summary>
        /// Hàm này ???c g?i ? m?i frame
        /// </summary>
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
            Fireballhoder[FindFireball()].transform.position = FireballPos.transform.position;
            Fireballhoder[FindFireball()].GetComponent<Projectile>().SetDiretion(Mathf.Sign(transform.localScale.x));
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