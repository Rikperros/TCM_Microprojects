using System;
using UnityEngine;

public class WeaponModuleBase : ScriptableObject
{
    public event Action OnModuleActivate;
    public event Action OnModuleDeactivate;

    protected FireWeaponDefinition _weaponDefinition;
    public virtual void SetUpWeaponModule(FireWeaponDefinition weaponDefinition)
    {
        _weaponDefinition = weaponDefinition;
    }
    public virtual bool CanActivateModule() 
    { 
        return _weaponDefinition != null; 
    }
    public virtual void ActivateModule() 
    {
        OnModuleActivate?.Invoke();
    }
    public virtual void DeactivateModule() 
    {
        OnModuleDeactivate?.Invoke();
    }
}
