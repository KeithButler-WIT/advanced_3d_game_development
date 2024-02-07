using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyNPC() {
        Destroy(gameObject);
    }

    public int GetHealth() { return health; }
    public void DecreaseHealth(int amount) { SetHealth(health-amount); }
    public void SetHealth(int amount) {
        health = amount;
        if (health <= 0) DestroyNPC();
    }

}
