using CM.Events;
using PossessionAbility.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float leaveBodyDelay;
    [HideInInspector] public float stamina;
    [HideInInspector] public float maxStamina = 100;
    [HideInInspector] public bool isGhost;

    private UpdateSlider staminaDisplayer;
    private Player player;
    private BodyStamina bodyStamina;
    void Awake()
    {
        
        player = GameObject.Find("Player Manager").GetComponent<Player>();
        EventManager.AddListener<PossessionSwapEvent>(OnPossessionSwap);
    }
    public void Start()
    {
        staminaDisplayer = GameObject.Find("Stamina Bar").GetComponent<UpdateSlider>();
        BodySwitch();
    }
    private void OnPossessionSwap(object eventData)
    {
        PossessionSwapEvent possessionSwapEvent = eventData as PossessionSwapEvent;

        BodySwitch();
    }
    public void OnDestroy()
    {
        EventManager.RemoveListener<PossessionSwapEvent>(OnPossessionSwap);
    }
    public void Update()
    {
        if (stamina <= 0 && !isGhost) KillBody();
    }
    public void BodySwitch()
    {
        if (player.currentPossessedBody.tag == "Ghost")
        {
            isGhost = true;

            staminaDisplayer.SetMax(1);
            stamina = 0;
            staminaDisplayer.UpdateDisplay(stamina);
        }
        else
        {
            isGhost = false;

            bodyStamina = player.currentPossessedBody.GetComponent<BodyStamina>();
            maxStamina = bodyStamina.maxBodyStamina;
            staminaDisplayer.SetMax(maxStamina);
            stamina = bodyStamina.bodyStamina;
            staminaDisplayer.UpdateDisplay(stamina);
        }
    }

    public void TakeDamage(float damage)
    {
        if (stamina > 0)
        {
            stamina -= damage;
            if (stamina < 0) stamina = 0;
            staminaDisplayer.UpdateDisplay(stamina);
        }
    }

    public void Heal(float restore)
    {
        if (stamina < maxStamina)
        {
            stamina += restore;
            staminaDisplayer.UpdateDisplay(stamina);
        }
    }

    public void KillBody()
    {
        PossessionAbility.PossessionAbility.LeavePossession(player.currentPossessedBody, leaveBodyDelay);
        player.currentPossessedBody.SetActive(false);
        BodySwitch();
    }
}
