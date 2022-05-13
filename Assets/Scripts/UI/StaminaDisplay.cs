using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PossessionGame
{
    public class StaminaDisplay : MonoBehaviour
    {
        //[SerializeField] private EntityStats entityStats;

        public Slider slider;
        public int currentStamina;

        

        // Start is called before the first frame update
        void Start()
        {
           // currentStamina = entityStats.bodyHealth;
        }

        // Update is called once per frame
        void Update()
        {
            slider.value = currentStamina;
        }
    }
}
