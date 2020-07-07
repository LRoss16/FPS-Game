using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}

public class WeaponsHandler : MonoBehaviour {

    private Animator anim;

    public WeaponAim weapon_Aim;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource shootSound, reload_sound;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;

    public GameObject attack_point;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
		
	}

    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    public void Aim(bool canAim)
    {
        anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }

    void Turn_On_MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    void Turn_Off_MuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void Play_ShootSound()
    {
        shootSound.Play();
    }

    void Play_ReloadSound()
    {
        reload_sound.Play();
    }

    void Turn_On_AttackPoint()
    {
        attack_point.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (attack_point.activeInHierarchy) { 
        attack_point.SetActive(false);
            }
    }
}
