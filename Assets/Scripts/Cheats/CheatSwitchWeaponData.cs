using UnityEngine;

public class CheatSwitchWeaponData : MonoBehaviour
{
    //This script provides some cheats to change quickly between weapons!
    public FireWeaponDefinition[] weaponDefinitions;
    public FireWeaponComponent WeaponComponent;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponComponent.ChangeWeaponData(weaponDefinitions[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponComponent.ChangeWeaponData(weaponDefinitions[1]);
        }
    }
}
