using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandCenterButtons : MonoBehaviour
{
    public GameObject powerBuilding;
    public GameObject controlTower;
    public GameObject fuelFactory;
    public GameObject warFactory;

    GameObject moneyToSpend;

    private void Start()
    {
        moneyToSpend = GameObject.Find("MoneyManager");
    }

    public void PowerBuildong()
    {
        if (moneyToSpend.GetComponent<MoneyManager>().numberOfCoins >= 800)
        {
            Instantiate(powerBuilding);
        }
        
    }

    public void ControlTower()
    {
        if (moneyToSpend.GetComponent<MoneyManager>().numberOfCoins >= 1000 && PowerBuildingLowOpacisy.havePowerBuilding == true)
        {
            Instantiate(controlTower, new Vector3(controlTower.transform.position.x, controlTower.transform.position.y - 0.14f, controlTower.transform.position.z), controlTower.transform.rotation);
        }
        
    }

    public void FuelFactory()
    {
        if (moneyToSpend.GetComponent<MoneyManager>().numberOfCoins >= 2000 && PowerBuildingLowOpacisy.havePowerBuilding == true)
        {
            Instantiate(fuelFactory, new Vector3(fuelFactory.transform.position.x, fuelFactory.transform.position.y + 4.22f, fuelFactory.transform.position.z), fuelFactory.transform.rotation);
        }
        
    }

    public void WarFactory()
    {
        if (moneyToSpend.GetComponent<MoneyManager>().numberOfCoins >= 2500 && PowerBuildingLowOpacisy.havePowerBuilding == true && ControlTowerLowOpacity.haveControlTower == true && FuelFactoryLowOpacity.haveFuelFactory == true)
        {
            Instantiate(warFactory);
        }
        
    }
}
