using UnityEngine;

//This allows us to create new definitions directly by right clicking on the assets folder
[CreateAssetMenu(fileName = "New Weapon Definition", menuName = "Weapon/Add Fire Weapon Definition")]
public class FireWeaponDefinition : ScriptableObject
{
    [Tooltip("Which bullet shall be spawned by this weapon")]
    public GameObject AmmunitionPrefab;
    [Tooltip("Time between shoots")]
    public float FireCooldown;
    [Tooltip("Time to reload the weapon")]
    public float ReloadTime;
    [Tooltip("Max amount of bullets that can be shooted without reloading")]
    public int ClipSize;
    [Tooltip("Shooting definition encapsulated in a module")]
    public WeaponModuleBase PrimaryWeaponModule;
    [Tooltip("aiming definition encapsulated in a module")]
    public WeaponModuleBase SecondaryWeaponModule;
    [Tooltip("reload definition encapsulated in a module")]
    public WeaponModuleBase AuxiliaryWeaponModule;
}
