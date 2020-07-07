using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { 
    PATROL,
    CHASE,
    ATTACK

}

public class EnemyController : MonoBehaviour {
    private EnemyAnimations enemy_Anin;
    private NavMeshAgent navAgent;

    private EnemyState enemy_state;

    public float walk_speed = 0.5f;
    public float run_speed = 6f;

    public float chase_distance = 20f;
    private float current_chase_distance;
    public float attack_distance = 1.8f;
    public float chase_after_attack_distance = 2f;

    public float patrol_radius_min = 20f, patrol_radius_max = 60f;
    public float patrol_for_this_time = 15f;
    public float patrol_Timer;

    public float wait_before_attack = 2f;
    private float attack_Timer;

    private Transform target;

    public GameObject attack_point;

    private EnemyAudio enemy_audio;
    void Awake()
    {
        enemy_Anin = GetComponent<EnemyAnimations>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;

        enemy_audio = GetComponentInChildren<EnemyAudio>();
    }


	// Use this for initialization
	void Start () {

        enemy_state = EnemyState.PATROL;

        patrol_Timer = patrol_for_this_time;

        //when enemy first gets to player, attack straight away
        attack_Timer = wait_before_attack;

        //memorize value of chase distance
        current_chase_distance = chase_distance;
		
	}
	
	// Update is called once per frame
	void Update () {

        if(enemy_state == EnemyState.PATROL)
        {
            Patrol();
        }

        if (enemy_state == EnemyState.CHASE)
        {
            Chase();      
        }

        if (enemy_state == EnemyState.ATTACK)
        {
            Attack();
        }
		
	}

    void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walk_speed;

        patrol_Timer += Time.deltaTime;

        if(patrol_Timer > patrol_for_this_time)
        {
            SetNewRandomDestination();

            patrol_Timer = 0f;
        }

        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anin.Walk(true);

        } else
        {
            enemy_Anin.Walk(false);
        }

        //check distance between player and enemy
        if(Vector3.Distance(transform.position, target.position) <= chase_distance)
        {
            enemy_Anin.Walk(false);

            enemy_state = EnemyState.CHASE;

            //play audio
            enemy_audio.Play_ScreamSound();
        }
    }

    void Chase()
    {
        navAgent.isStopped = false;
        navAgent.speed = run_speed;

        //set player position as destination
        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anin.Run(true);

        }
        else
        {
            enemy_Anin.Run(false);
        }

        //if distance between enemy and player is less than attack distance
        if (Vector3.Distance(transform.position, target.position) <= attack_distance)
        {
            //stop animations
            enemy_Anin.Run(false);
            enemy_Anin.Walk(false);
            enemy_state = EnemyState.ATTACK;

            //reset chase chase distance to the previous one
            if (chase_distance != current_chase_distance)
            {
                chase_distance = current_chase_distance;
            }



        } else if(Vector3.Distance(transform.position, target.position) > chase_distance)
        {
            //player runs away from enemy

            //stop chasing
            enemy_Anin.Run(false);
            enemy_state = EnemyState.PATROL;

            //reset patrol timer
            patrol_Timer = patrol_for_this_time;

            if (chase_distance != current_chase_distance)
            {
                chase_distance = current_chase_distance;
            }
        }


    }

    void Attack()
    {

        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;

        if(attack_Timer > wait_before_attack)
        {
            enemy_Anin.Attack();

            attack_Timer = 0f;

            //play attack sound
            enemy_audio.Play_AttackSound();
        }

        if(Vector3.Distance(transform.position, target.position) >
            attack_distance + chase_after_attack_distance)
        {
            enemy_state = EnemyState.CHASE;
        }
    }

    void SetNewRandomDestination()
    {

        float rand_Radius = Random.Range(patrol_radius_min, patrol_radius_max);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        navAgent.SetDestination(navHit.position);

    }

    void Turn_On_AttackPoint()
    {
        attack_point.SetActive(true);
    }

    void Turn_Off_AttackPoint()
    {
        if (attack_point.activeInHierarchy)
        {
            attack_point.SetActive(false);
        }
    }

    public EnemyState Enemy_State
    {
        get; set;
    }
}
