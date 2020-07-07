using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

    private ManageWeapons weapon_manager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;

    private Animator zoomCameraAnin;
    private bool zoomed;

    private Camera mainCam;

    private GameObject crosshair;

    private bool is_Aiming;

    [SerializeField]
    private GameObject arrow_Prefab, spear_Prefab;

    [SerializeField]
    private Transform arrow_Bow_StartPosition;

    public int attackScore = 50;

    public Text score;

    void Awake()
    {
        weapon_manager = GetComponent<ManageWeapons>();

        zoomCameraAnin = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();

        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

        crosshair.SetActive(false);

        mainCam = Camera.main;
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        WeaponShoot();
        ZoomInAndOut();
	}

    void WeaponShoot()
    {
        //for assault rifle
        if (weapon_manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            //fire when left mouse button is clicked
                if(Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;

                weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired();


            }
        } else //if not assualt rifle
        {
            if (Input.GetMouseButtonDown(0))
            {
                //handle ax
                if(weapon_manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                //handle shoot
                if (weapon_manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();

                   BulletFired();


                } else
                {
                    //arror or spear

                if (is_Aiming)
                    {
                        weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();

                        if(weapon_manager.GetCurrentSelectedWeapon().bulletType 
                            == WeaponBulletType.ARROW)
                        {
                            //fire arrow
                            ThrowArrowOrSpear(true);
                        } else if (weapon_manager.GetCurrentSelectedWeapon().bulletType
                            == WeaponBulletType.SPEAR)
                        {
                            //throw spear
                            ThrowArrowOrSpear(false);
                        }
                    }
                    
                }
            }
        }
    }

    void ZoomInAndOut()
    {

        // we are going to aim with our camera on the weapon
        if (weapon_manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.AIM)
        {

            // if we press and hold right mouse button
            if (Input.GetMouseButtonDown(1))
            {

                zoomCameraAnin.Play(AnimationTags.ZOOM_IN_ANIM);

                crosshair.SetActive(true);
            }

            // when we release the right mouse button click
            if (Input.GetMouseButtonUp(1))
            {

                zoomCameraAnin.Play(AnimationTags.ZOOM_OUT_ANIM);

                crosshair.SetActive(false);
            }

        } // if we need to zoom the weapon

        if (weapon_manager.GetCurrentSelectedWeapon().weapon_Aim == WeaponAim.SELF_AIM)
        {

            if (Input.GetMouseButtonDown(1))
            {

                weapon_manager.GetCurrentSelectedWeapon().Aim(true);

                is_Aiming = true;

            }

            if (Input.GetMouseButtonUp(1))
            {

                weapon_manager.GetCurrentSelectedWeapon().Aim(false);

                is_Aiming = false;

            }

        } // weapon self aim

    }

    void ThrowArrowOrSpear(bool throwArrow)
    {
        if(throwArrow)
        {
            GameObject arrow = Instantiate(arrow_Prefab);
            arrow.transform.position = arrow_Bow_StartPosition.position;

            arrow.GetComponent<ArrowAndBowScript>().Launch(mainCam);

        }else
        {
            GameObject spear = Instantiate(spear_Prefab);
            spear.transform.position = arrow_Bow_StartPosition.position;

            spear.GetComponent<ArrowAndBowScript>().Launch(mainCam);
        }
    }

    void BulletFired()
    {

        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {

            if (hit.transform.tag == Tags.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
                HealthScript.score = HealthScript.score + attackScore;
               // Debug.Log("score is " + HealthScript.score);
                score.text = "Score: " + HealthScript.score;

                //print("Player hit " + hit.transform.gameObject.name);
            }

        }

    }

 
}
