using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeapons : MonoBehaviour {

    [SerializeField]
    private WeaponsHandler[] weapons;

    private int currentWeapon;
	// Use this for initialization
	void Start () {
        currentWeapon = 0;
        weapons[currentWeapon].gameObject.SetActive(true);
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TurnOnSelectedWeapon(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TurnOnSelectedWeapon(5);
        }

    }

    void TurnOnSelectedWeapon(int weaponIndex)
    {
        if (currentWeapon == weaponIndex)
            return;

        //hide current weapon
        weapons[currentWeapon].gameObject.SetActive(false);

        //get selected weapon
        weapons[weaponIndex].gameObject.SetActive(true);


        //store selected weapon
        currentWeapon = weaponIndex;
    }

    public WeaponsHandler GetCurrentSelectedWeapon()
    {
        return weapons[currentWeapon];
    }
}
