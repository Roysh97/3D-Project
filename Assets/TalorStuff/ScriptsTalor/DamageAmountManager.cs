using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAmountManager : Army
{
    public int damageAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Damage Amount" + damageAmount);
    }

    public void DamageAmount(int _damageAmount)
    {
        damageAmount = _damageAmount;
    }
}
