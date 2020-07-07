using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowAndBowScript : MonoBehaviour {


    private Rigidbody myBody;

    public float speed = 30f;

    public float deactivate_Timer = 3f;

    public float damage = 25f;

    public int attackScore = 50;

    public HealthScript script;

    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

	void Start () {
        Invoke("DeactivateGameObject", deactivate_Timer);
	}

    void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    public void Launch(Camera mainCamera)
    {
        myBody.velocity = mainCamera.transform.forward * speed;

        transform.LookAt(transform.position + myBody.velocity);
    }
	
    void OnTriggerEnter(Collider target)
    {
        //after enemy hit deactivate game object

        if(target.tag == Tags.ENEMY_TAG)
        {
            target.GetComponent<HealthScript>().ApplyDamage(damage);
            HealthScript.score = HealthScript.score + attackScore;
            gameObject.SetActive(false);
        }
    }
}
