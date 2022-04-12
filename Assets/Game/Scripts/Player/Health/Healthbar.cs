using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Scripts.player
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private Health playerhealth;
        [SerializeField] private Image totalhealthbar;
        [SerializeField] private Image currenthealthBar;
        void Start()
        {
            totalhealthbar.fillAmount = playerhealth.currentHealth / 10;
        }

        // Update is called once per frame
        void Update()
        {
            currenthealthBar.fillAmount = playerhealth.currentHealth / 10;

        }
    }
}

