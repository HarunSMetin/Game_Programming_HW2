using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static GameData;

public class PlayerManager : MonoBehaviour
{

    [Header("Player Values")]
    public Camera TPCam;
    public Camera FPCam;

    [Header("Player Values")]
    public float HP = 50;
    public int totalFire = 0;
    public int totalHit = 0;
    public int totalKill = 0;
    public int totalDamage = 0;

    GameObject CharacterObject;
    Animator  animator;
    Camera currentCam;

    [Header("")]
    public Transform gunBarrel; // Silahýn namlusunun pozisyonu
    public GameObject bulletPrefab; // Ateþ edilecek mermi (bullet) prefab'i

    public float rotationSpeed = 1000f;
    public float maxEnergy = 100f;
    public float energyIncreaseRate = 20f; 
     
    private bool isAiming = false; 
    private bool isLRF = false;
    private float chargeTime = 0f;
    Weapon myWeapon;

    private void Start()
    {
        currentCam = TPCam;
        CharacterObject = GameObject.FindGameObjectsWithTag("Player")[0];
        animator = CharacterObject.GetComponent<Animator>();   
        switch (GameManager.Instance.currentEqData)
        {
            case GameData.eq.eq1:

                break;
            case GameData.eq.eq2:

                break;
            case GameData.eq.eq3:

                break;
        } 
        myWeapon = new Weapon();
    }
    private void Update()
    {
        myWeapon.CoolingForUpdate();
        HandleRotation();

        if (!isAiming && Input.GetMouseButton(1))
        {
            StartAim();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopAim();
        }
        
        if ( myWeapon.IsReadyToFire() && isAiming && Input.GetMouseButton(0))
        {
            //Debug.Log("chargeTime: " + chargeTime);
            if (chargeTime<5)
                chargeTime+=Time.deltaTime;
        }
        else if (isAiming && Input.GetMouseButtonUp(0))
        {
            FireBullet();
             

            chargeTime =0;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (isLRF)
            {
                ExitLRFMode();
                isLRF = false;
            }
            else
            {
                EnterLRFMode();
                isLRF = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            if (TPCam.gameObject.activeInHierarchy)
            {
                FPCamView();
                currentCam = FPCam;
            }
            else
            {
                TPCamView();
                currentCam = TPCam;
            }

        }
    }
    private void LateUpdate()
    {
        animator.SetBool("Shoot", false);
    }

    private void HandleRotation()
    {
        // Mouse pozisyonunu alarak player'ý rotasyonunu deðiþtirelim.
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, mouseX); // Y ekseni etrafýnda dönme
        transform.Rotate(Vector3.left, mouseY); // X ekseni etrafýnda dönme (kamera açýsý)

        // Player'ýn rotasyonunu sýnýrlayalým (isteðe baðlý)
        // Bu kod, player'ýn yukarý-aþaðý dönmesini sýnýrlar. Tam da üstüne bakamaz veya tam aþaðýya bakamaz.
        float currentXRotation = transform.rotation.eulerAngles.x;

        if (currentXRotation > 20f && currentXRotation < 90f)
        {
            currentXRotation = 20f;
        }
        else if (currentXRotation > 180f && currentXRotation < 340f)
        {
            currentXRotation = 340f;
        }

        transform.rotation = Quaternion.Euler(currentXRotation, transform.rotation.eulerAngles.y, 0f);
    }

    private void StartAim()
    {
        animator.SetBool("Aim", true);
        isAiming = true;
    }

    private void StopAim()
    {
        animator.SetBool("Aim", false);
        isAiming = false; 
    }
     

    private void FireBullet()
    {
        animator.SetBool("Shoot", true); 
        Physics.Raycast(currentCam.transform.position, currentCam.transform.forward, out RaycastHit hit, myWeapon.range); 
        if  (hit.transform != null && hit.transform.tag == "Enemy")
        {
            Enemy enemy = hit.transform.parent.GetComponent<Enemy>();  
            myWeapon.FireProcedure(chargeTime, Vector3.Distance(enemy.transform.position,CharacterObject.transform.position));
            enemy.TakeDamage(myWeapon.damage); 

        }
        else if (hit.transform == null )
        {
            myWeapon.FireProcedure(chargeTime, myWeapon.range);
        }
        else
        {
            myWeapon.FireProcedure(chargeTime, Vector3.Distance(hit.point, CharacterObject.transform.position));
        }
    }
    private void EnterLRFMode()
    {
        RenderSettings.fogEndDistance = 30;
    }
    private void ExitLRFMode()
    {
        RenderSettings.fogEndDistance = 7;
    }
    void TPCamView()
    {
        TPCam.gameObject.SetActive(true);
        FPCam.gameObject.SetActive(false);
    }
    void FPCamView()
    {
        TPCam.gameObject.SetActive(false);
        FPCam.gameObject.SetActive(true);
    }

}

public class Weapon
{
    public float batteryCapacity;
    public float range;
    public float currentBatteryCapacity;

    float LE;

    public float damage;
    float UER;

    float CD;
    float coolingDurationCounter;

    public float currentTemperature;
    public bool inCooling;
    private float coolingTempPerSecond=10f;


    public Weapon()
    {
        this.range = GetWeaponRange();
        this.batteryCapacity = GetWeaponBattaryCapacity();
        this.currentTemperature=20f;
    }

    float CalculateLE(float ELD)
    {
        LE = Mathf.Pow(2, ELD) * 100;
        return LE;
    }
    float CalculateCurrentBattaryCapacity(float ELD)
    {
        CalculateLE(ELD);
        currentBatteryCapacity = batteryCapacity - LE;
        return currentBatteryCapacity;
    }
    float CalculateUER(float ELD, float distanceToEnemy)
    {
        CalculateLE(ELD);
        UER = LE * Mathf.Log(distanceToEnemy, 20);
        return UER;
    }
    float CalculateDamage(float ELD, float distanceToEnemy)
    {
        CalculateUER(ELD, distanceToEnemy);
        damage = LE - UER;
        return damage;
    }

    public void CoolingForUpdate()
    {
        if (inCooling)
        {
            coolingDurationCounter += Time.deltaTime;
            currentTemperature -= coolingTempPerSecond * Time.deltaTime;

            if (coolingDurationCounter >= CD)
            {
                currentTemperature = 20;
                coolingTempPerSecond = 0;
                coolingDurationCounter = 0;
                inCooling = false;
            }
        }
    }
    float CalculateCoolingDuration(float ELD)
    { 
        currentTemperature += 10 * ELD;
        CD = Mathf.Pow(2, currentTemperature / 10) / Mathf.Pow(2, currentTemperature / 20);
        coolingTempPerSecond =(currentTemperature - 20)/CD;
        return CD;
    } 
    public void FireProcedure(float ELD,float distanceToEnemy)
    {
        inCooling=true;
        CalculateDamage(ELD, distanceToEnemy);
        CalculateCoolingDuration(ELD);
        CalculateCurrentBattaryCapacity(ELD); 
    }

    public bool IsReadyToFire()
    {
        if (inCooling || currentTemperature>20)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    float GetWeaponRange()
    {
        switch (GameManager.Instance.currentWeaponData)
        {
            case GameData.weapon.weapon1:
                return 10f;
            case GameData.weapon.weapon2:
                return 15f;
            case GameData.weapon.weapon3:
                return 20f;
            default:
                return 10f;
        }
    }
    float GetWeaponBattaryCapacity()
    {
        switch (GameManager.Instance.currentWeaponData)
        {
            case GameData.weapon.weapon1:
                return 5000f;
            case GameData.weapon.weapon2:
                return 10000f;
            case GameData.weapon.weapon3:
                return 15000f;
            default:
                return 5000f;
        }
    }

}
