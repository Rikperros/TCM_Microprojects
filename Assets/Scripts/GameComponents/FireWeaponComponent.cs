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

    //Here we only define the vars that we need to control the component logic
    private float ElapsedFireCooldown = 0;
    private float ElapsedReloadTime = 0;
    private int remainingBullets = 0;
    private bool bIsReloading = false;

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
        //if everything is fine se the weapon ready
        ReloadWeapon();
    }

    void Update()
    {
        //We always want to update the weapon fire cooldown
        ElapsedFireCooldown += Time.deltaTime * Time.timeScale;

        if(!bIsReloading)
        {
            if (Input.GetButton(FireInputAxis) && ElapsedFireCooldown >= WeaponDefinition.FireCooldown && remainingBullets > 0)
            {
                Shoot();
            }

            if (Input.GetButtonDown(ReloadInputAxis) || remainingBullets <= 0)
            {
                bIsReloading = true;
                //Invoke Delegates
                OnReloadStateChanged?.Invoke(bIsReloading);
            }
        }  
        else if(ElapsedReloadTime >= WeaponDefinition.ReloadTime)
        {
            ReloadWeapon();
        }
        else
        {
            //WeaponDefinition only want to update this cooldown if we are reloading the weapon
            ElapsedReloadTime += Time.deltaTime * Time.timeScale;
        }
    }

    void ReloadWeapon()
    {
        //Reset weapon state
        remainingBullets = WeaponDefinition.ClipSize;
        ElapsedFireCooldown = WeaponDefinition.FireCooldown;
        //Reset Reload control vars
        bIsReloading = false;
        ElapsedReloadTime = 0;
        //Invoke Delegates
        OnReloadStateChanged?.Invoke(bIsReloading);

    }

    void Shoot()
    {
        SetUpProjectile();
        //Update ammo info
        remainingBullets -= 1;
        //Reset shooting control vars
        ElapsedFireCooldown = 0;
    }

    void SetUpProjectile()
    {
        BulletMovement l_bulletMovement = Instantiate(WeaponDefinition.AmmunitionPrefab).GetComponent<BulletMovement>();
        l_bulletMovement.transform.position = transform.position;
        l_bulletMovement.movementDirection = transform.right;
    }

    public void ChangeWeaponData(FireWeaponDefinition fireWeaponDefinition)
    {
        WeaponDefinition = fireWeaponDefinition;
        ReloadWeapon();
    }
}
