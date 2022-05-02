using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public GameObject monyCanvas;
    public Text moneyValue;

    public float numberOfCoins;

    // Start is called before the first frame update
    void Start()
    {
        numberOfCoins = 10000;
        moneyValue.text = numberOfCoins.ToString() + "$";
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyValue.text = numberOfCoins.ToString() + "$";
    }
}
