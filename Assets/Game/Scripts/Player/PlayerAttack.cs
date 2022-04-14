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

        [SerializeField] private GameObject FireballPos;
        [SerializeField] private GameObject[] Fireballhoder;

        /// <summary>
        /// H�nh ??ng c?a ng??i ch?i
        /// </summary>
        private Animator animator;

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
            animator = GetComponent<Animator>();
            playerMove = GetComponent<PlayerMove>();
        }

        /// <summary>
        /// H�m n�y ???c kh?i t?o sau khi bi�n d?ch
        /// </summary>
        private void Start()
        {
        }

        /// <summary>
        /// H�m n�y ???c g?i ? m?i frame
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