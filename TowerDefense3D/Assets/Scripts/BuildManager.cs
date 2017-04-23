using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;

    public bool CanBuild
    {
        get { return turretToBuild != null; }
    }

    public bool HasMoney // check if player has enough money
    {
        get { return PlayerStats.Money >= turretToBuild.cost; }
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money >= turretToBuild.cost)
        {
            PlayerStats.Money -= turretToBuild.cost;
            GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
            node.turret = turret;

            GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);
            Debug.Log("Turret build! Money left: " + PlayerStats.Money);
        } else
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
