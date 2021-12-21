using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    private Renderer rend;
    private Color startColor;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgrade =false;
    public Vector3 offsetPosition;
    BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.Instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.canBuild)
        {
            return;
        }
        if(buildManager.hasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;

        }
    }
    private void OnMouseExit()
    {
      
        rend.material.color = startColor;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (turret != null)
        {
            buildManager.SelectedNode(this);
            return;
        }

        if (!buildManager.canBuild)
        {
            return;
        }
        BuildTurret(buildManager.turretToBuild);

        void BuildTurret(TurretBlueprint blueprint)
        {
            if (PlayerStats.money < blueprint.cost)
            {
                Debug.Log("Not enough money");
                return;
            }
            turretBlueprint = blueprint;
            PlayerStats.money -= blueprint.cost;
            GameObject _turret = Instantiate(blueprint.prefab, transform.position + offsetPosition, transform.rotation);
            turret = _turret;
            GameObject BuildFx = Instantiate(buildManager.BuildEffect, transform.position + offsetPosition, Quaternion.identity);
            Destroy(BuildFx, 5f);
            Debug.Log("Turret Build! Money left " + PlayerStats.money);
        }
    }
    public void UpgradeTurret()
    {
        if (PlayerStats.money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade");
            return;
        }
        if (!isUpgrade)
        {
            PlayerStats.money -= turretBlueprint.upgradeCost;
            Destroy(turret);
            GameObject _turret = Instantiate(turretBlueprint.upgradePrefab, transform.position + offsetPosition, transform.rotation);
            turret = _turret;
            isUpgrade = true;
            GameObject BuildFx = Instantiate(buildManager.BuildEffect, transform.position + offsetPosition, Quaternion.identity);
            Destroy(BuildFx, 5f);
        }
    }
    public void SellTurret()
    {
        PlayerStats.money += turretBlueprint.SellAmount();
        GameObject BuildFx = Instantiate(buildManager.SellEffect, transform.position + offsetPosition, Quaternion.identity);
        Destroy(BuildFx, 5f);
        Destroy(turret);
        turretBlueprint = null;
    }
}
