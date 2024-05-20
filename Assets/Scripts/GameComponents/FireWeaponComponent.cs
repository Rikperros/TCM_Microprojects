using System;
using UnityEngine;

public class FireWeaponComponent : MonoBehaviour
{
    //All params and data will exist in here!
    [SerializeField]
    FireWeaponDefinition WeaponDefinition;

    //In this case I consider the input weapon independant but we can add those inside the data definition
    [SerializeField]
    string ReloadInputAxis = "Reload";
    [SerializeField]
    string FireInputAxis = "Fire1";

    //Observer to handle reloading text in hud
    public event Action<bool> OnReloadStateChanged;  

    void Awake()
    {
        //This will throw an error message if the condition isn't meet
        Debug.Assert(WeaponDefinition != null, "Fire weapon definition is missing!", this);
       
        if (WeaponDefinition == null)
        {
            //If we dont have a config file deactivate this component to avoid breaking the game
            this.enabled = false;
            return;
        }

        SetUpWeaponModules();
    }

    void Update()
    {
        if (!WeaponDefinition.AuxiliaryWeaponModule.CanActivateModule())  //If I have bullets
        {
            if (WeaponDefinition.PrimaryWeaponModule.CanActivateModule()) //If cooldown has passed
            {
                if (Input.GetButtonDown(FireInputAxis) || Input.GetButton(FireInputAxis)) //if player press fire input
                {
                    WeaponDefinition.PrimaryWeaponModule.ActivateModule(); //fire
                }
            }

            if (Input.GetButton(ReloadInputAxis)) //if player press reload input
            {
                WeaponDefinition.AuxiliaryWeaponModule.ActivateModule(); //force reload
            }
        }
        else //if I have no bullets
        {
            WeaponDefinition.AuxiliaryWeaponModule.ActivateModule(); //Reload
        }
    }

    void SetUpProjectile()
    {
        BulletMovement l_bulletMovement = Instantiate(WeaponDefinition.AmmunitionPrefab).GetComponent<BulletMovement>();
        l_bulletMovement.transform.position = transform.position;
        l_bulletMovement.movementDirection = transform.right;
    }

    public void ChangeWeaponData(FireWeaponDefinition fireWeaponDefinition)
    {
        RemoveBindedData();
        WeaponDefinition = fireWeaponDefinition;
        SetUpWeaponModules();
    }

    private void SetUpWeaponModules()
    {
        WeaponDefinition.PrimaryWeaponModule.SetUpWeaponModule(WeaponDefinition);
        WeaponDefinition.AuxiliaryWeaponModule.SetUpWeaponModule(WeaponDefinition);

        WeaponDefinition.PrimaryWeaponModule.OnModuleActivate += SetUpProjectile;

        ReloadWeaponModule ReloadModule = WeaponDefinition.AuxiliaryWeaponModule as ReloadWeaponModule;
        WeaponDefinition.PrimaryWeaponModule.OnModuleActivate += ReloadModule.RemoveBullet;
        ReloadModule.OnModuleActivate += OnAuxiliaryModuleActive;
        ReloadModule.OnModuleDeactivate += OnAuxiliaryModuleDeactivated;
    }

    private void RemoveBindedData()
    {
        WeaponDefinition.PrimaryWeaponModule.OnModuleActivate -= SetUpProjectile;

        ReloadWeaponModule ReloadModule = WeaponDefinition.AuxiliaryWeaponModule as ReloadWeaponModule;
        WeaponDefinition.PrimaryWeaponModule.OnModuleActivate -= ReloadModule.RemoveBullet;
        ReloadModule.OnModuleActivate -= OnAuxiliaryModuleActive;
        ReloadModule.OnModuleDeactivate -= OnAuxiliaryModuleDeactivated;
    }

    private void OnAuxiliaryModuleActive()
    {
        OnReloadStateChanged?.Invoke(true);
    }

    private void OnAuxiliaryModuleDeactivated()
    {
        OnReloadStateChanged?.Invoke(false);
    }
}
