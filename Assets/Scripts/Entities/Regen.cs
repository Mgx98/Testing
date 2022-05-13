using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regen : MonoBehaviour
{

    public PlayerHealth health;

    bool possessed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (possessed)
        {
            health = FindObjectOfType<PlayerHealth>();
            health.heal(0.015f);
        }
    }
}
