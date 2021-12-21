using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject nodeUI;
    public Button upgradeButton;
    public Text upgradeCostText;
    public Text sellCostText;

    public void SetTarget(Node _target)
    {
        sellCostText.text = "$" + _target.turretBlueprint.SellAmount().ToString();
        if(!_target.isUpgrade)
        {
            upgradeCostText.text = "$" +_target.turretBlueprint.upgradeCost.ToString();
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
            upgradeCostText.text = "Done!";
        }
        nodeUI.SetActive(true);
        this.target = _target;
        transform.position = target.transform.position;

    }

    // Update is called once per frame
    public void HideUI()
    {
        nodeUI.SetActive(false);
    }
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }
    public void Sell()
    {
        target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }
}
