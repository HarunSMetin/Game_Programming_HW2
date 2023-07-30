using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpBarPanelController : MonoBehaviour
{
    public Image CustomizationImage;
    public Image InventoryImage;
    public Image SettingsImage;

    public GameObject CustomizationPanel;
    public GameObject InventoryPanel;
    public GameObject SettingsPanel;

    private float alphaValue = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CustPanel_Onclick()
    {
        CustomizationImage.color = new Color(CustomizationImage.color.r, CustomizationImage.color.g, CustomizationImage.color.b, alphaValue);
        InventoryImage.color = new Color(InventoryImage.color.r, InventoryImage.color.g, InventoryImage.color.b, 0f);
        SettingsImage.color = new Color(SettingsImage.color.r, SettingsImage.color.g, SettingsImage.color.b, 0f);

        CustomizationPanel.SetActive(true);
        InventoryPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }
    public void InvPanel_Onclick()
    {

        CustomizationImage.color = new Color(CustomizationImage.color.r, CustomizationImage.color.g, CustomizationImage.color.b, 0f);
        InventoryImage.color = new Color(InventoryImage.color.r, InventoryImage.color.g, InventoryImage.color.b, alphaValue);
        SettingsImage.color = new Color(SettingsImage.color.r, SettingsImage.color.g, SettingsImage.color.b, 0f);

        CustomizationPanel.SetActive(false);
        InventoryPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }
    public void SettingsPanel_Onclick()
    {
        CustomizationImage.color = new Color(CustomizationImage.color.r, CustomizationImage.color.g, CustomizationImage.color.b, 0f);
        InventoryImage.color = new Color(InventoryImage.color.r, InventoryImage.color.g, InventoryImage.color.b, 0f);
        SettingsImage.color = new Color(SettingsImage.color.r, SettingsImage.color.g, SettingsImage.color.b, alphaValue);
        CustomizationPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
}
