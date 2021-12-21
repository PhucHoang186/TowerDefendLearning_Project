using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    public NodeUI nodeUI;
    private void Awake()
    {
        if(Instance== null)
        {
            Instance = this;
        }
        else if(Instance!=this)
        {
            Destroy(gameObject);
        }
    }
    public bool canBuild { get { return turretToBuild!= null; } }
    public bool hasMoney { get { return PlayerStats.money >= turretToBuild.cost; } }


    public TurretBlueprint turretToBuild;
    private Node selectedNode;
    public GameObject BuildEffect;
    public GameObject SellEffect;


    public void SelectedNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        turretToBuild = null;
        nodeUI.SetTarget(node);
    }
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.HideUI();
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

}
