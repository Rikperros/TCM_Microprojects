using UnityEngine;
using UnityEngine.UI;

public class ReloadTextController : MonoBehaviour
{
    public FireWeaponComponent FireWeaponComponent;
    public Text Text;

    private void Start()
    {
        Text.enabled = false;

        if(FireWeaponComponent != null)
        {
            FireWeaponComponent.OnReloadStateChanged += SetReloadTextVisibility;
        }
    }

    void SetReloadTextVisibility(bool bShowText)
    {
        Text.enabled = bShowText;
    }
}
