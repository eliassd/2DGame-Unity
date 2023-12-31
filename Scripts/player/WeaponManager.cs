using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    private int totalWeapons;
    [SerializeField] int currentWeaponIndex;

    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun;

    void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        for(int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }
        guns[0].SetActive(true);
        currentGun = guns[0];
        currentWeaponIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            if(currentWeaponIndex < totalWeapons - 1)
            {
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex++;
                //guns[currentWeaponIndex].SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentWeaponIndex > 0)
            {
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex--;
                //guns[currentWeaponIndex].SetActive(true);
            }
        }
        guns[currentWeaponIndex].SetActive(true);


    }
}
