using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Module", menuName = "Weapon/Modules/Add Reload Module")]

public class ReloadWeaponModule : WeaponModuleBase
{
    private int _remainingAmmo = 0;
    private float _lastActivationTime = 0;
    private bool bIsReloading = false;
    public override void SetUpWeaponModule(FireWeaponDefinition weaponDefinition)
    {
        base.SetUpWeaponModule(weaponDefinition);
        _remainingAmmo = weaponDefinition.ClipSize;
        _lastActivationTime = 0.0f;
    }
    public override bool CanActivateModule()
    {
        if (!base.CanActivateModule())
            return false;
       
        return _remainingAmmo <= 0 || bIsReloading;
    }
    public override void ActivateModule()
    {
        if (!bIsReloading)
        {
            base.ActivateModule();
            bIsReloading = true;
            _lastActivationTime = Time.time;
        }
        else if (Time.time - _lastActivationTime > _weaponDefinition.ReloadTime)
        {
            DeactivateModule();
            return;
        }       
    }

    public override void DeactivateModule()
    {
        bIsReloading = false;
        _remainingAmmo = _weaponDefinition.ClipSize;
        base.DeactivateModule();
    }

    public void RemoveBullet()
    {
        _remainingAmmo -= 1;
    }
}
