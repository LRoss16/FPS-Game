using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public float damage = 10f;
    public float radius = 5f;
    public LayerMask layerMask;

    void Update()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);

        if (hits.Length > 0)
        {

            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);

            gameObject.SetActive(false);

        }

    }

} // class




























