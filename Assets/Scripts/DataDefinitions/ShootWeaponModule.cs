using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Module", menuName = "Weapon/Modules/Add Shoot Module")]
public class ShootWeaponModule : WeaponModuleBase
{
    private float _lastActivationTime = 0;

    public override void SetUpWeaponModule(FireWeaponDefinition weaponDefinition)
    {
        base.SetUpWeaponModule(weaponDefinition);
        _lastActivationTime = 0.0f;
    }
    override public bool CanActivateModule()
    {
        if (!base.CanActivateModule())
            return false;
        return Time.time - _lastActivationTime >= _weaponDefinition.FireCooldown;
         
    }

    override public void ActivateModule()
    {
        base.ActivateModule();
        _lastActivationTime = Time.time;
    }
}
