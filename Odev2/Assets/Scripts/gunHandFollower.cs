using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class gunHandFollower : MonoBehaviour
{
    public GameObject hand;
    GameObject gun;
    GameObject createdgun;
    Vector3 eulerAng;
    // Start is called before the first frame update
    void Start()
    {  
        gun = GameManager.Instance.currentWeapon;
        createdgun = Instantiate(gun, hand.transform.position, hand.transform.rotation, hand.transform);
         
        eulerAng.x = -90;
        eulerAng.y = +90;
        eulerAng.z = 0;

        createdgun.transform.localRotation= Quaternion.Euler(eulerAng);

    }

    // Update is called once per frame
    void Update()
    { 

    }
}
