using UnityEngine;
using System.Collections;

namespace Game.Scripts.player
{
    public class Health : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private float StartingHealth;
        public float currentHealth { get; set; }
        private Animator animator;
        private bool dead;
        [Header("iFrames")]
        [SerializeField] private float iFramesDuration;
        [SerializeField] private float numberOffLlashes;
        private SpriteRenderer spriteRenderer;
      

        private void Awake()
        {
            animator = GetComponent<Animator>();
            currentHealth = StartingHealth;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            
        }
        public void TakeDame(float _dame)
        {
            currentHealth = Mathf.Clamp(currentHealth - _dame, 0, StartingHealth);
            if (currentHealth > 0)
            {
                animator.SetTrigger("hurt");
                StartCoroutine(Invunerabilyty());
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
        public void AddHeath(float _values)
        {
            currentHealth = Mathf.Clamp(currentHealth + _values, 0, StartingHealth);

        }
       private IEnumerator Invunerabilyty()
        {
            
            Physics2D.IgnoreLayerCollision(8, 9, true);
            for(int i = 0; i < numberOffLlashes; i++)
            {
                spriteRenderer.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(iFramesDuration/(numberOffLlashes*2));
                spriteRenderer.color = Color.white;
            }
            Physics2D.IgnoreLayerCollision(8, 9, false);
            yield return null;
        }

    }
}