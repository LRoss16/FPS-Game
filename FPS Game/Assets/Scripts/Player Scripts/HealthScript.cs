using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    private EnemyAnimations enemyAnimation;
    private NavMeshAgent navAgent;
    private EnemyController enemy_controller;

    public float health = 100f;

    public bool is_Player, is_Tiger, is_Soldier, is_Rhino, is_Snow_Soldier;

    public bool is_Dead;

    private EnemyAudio enemyAudio;

    private PlayerStats player_stats;

    public static int score;

    public Text TextScore;

    public Text TextHighScore;

    public int damageScore = 20;

    public int highScore = 0;



    void OnDestroy()
    {
        saveHighScore();
    }
    void Awake () {

        TextHighScore = GameObject.FindObjectOfType<Text>();
        TextHighScore.text = "High Score: " + PlayerPrefs.GetInt("High Score");

        if (is_Tiger || is_Soldier || is_Rhino || is_Snow_Soldier)
        {
            enemyAnimation = GetComponent<EnemyAnimations>();
            enemy_controller = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();

            // get enemy audio
            enemyAudio = GetComponentInChildren<EnemyAudio>();


        }

        if (is_Player)
        {
            player_stats = GetComponent<PlayerStats>();

        }
		
	}
	
	
    public void ApplyDamage(float damage)
    {
        if (is_Dead)
            return;

        health -= damage;



        if (is_Player)
        {
            //show health value
            player_stats.Display_HealthStats(health);
            Update_Score();
        }

            if (is_Tiger || is_Soldier || is_Rhino || is_Snow_Soldier)
            {
                if (enemy_controller.Enemy_State == EnemyState.PATROL)
                {
                enemy_controller.chase_distance = 100f;

               

            }
            }

            if (health <= 0f)
        {
            PlayerDied();
            is_Dead = true;

        }



       
    }

    void PlayerDied()
    {
 


        if (is_Tiger)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 20f);

            enemy_controller.enabled = false;
            navAgent.enabled = false;
            enemyAnimation.enabled = false;

            StartCoroutine(DeadSound());

            EnemyManager.instance.TigerDied(true);

            //EnemyManager spawns more enemies
        }

        if (is_Snow_Soldier)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_controller.enabled = false;

            enemyAnimation.Dead();

            StartCoroutine(DeadSound());

            // EnemyManager spawn more enemies
            EnemyManager.instance.TigerDied(false);

        }




        if (is_Soldier)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_controller.enabled = false;

            enemyAnimation.Dead();

            StartCoroutine(DeadSound());

            // EnemyManager spawn more enemies
            EnemyManager.instance.RhinoDied(false);

        }

        if (is_Rhino)
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            enemy_controller.enabled = false;

            enemyAnimation.Dead();

            StartCoroutine(DeadSound());

            // EnemyManager spawn more enemies
            EnemyManager.instance.RhinoDied(true);

        }

        if (is_Player)
        {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            // call enemy manager to stop spawning enemis
            EnemyManager.instance.StopSpawning();

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<ManageWeapons>().GetCurrentSelectedWeapon().gameObject.SetActive(false);

        }



        if (tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame", 3f);
            saveHighScore();

        }
        else
        {
            Invoke("TurnOffGameObject", 3f);
        }

    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ForestScene");
       

    }

    void TurnOffGameObject() 
    {
        gameObject.SetActive(false);
    
    }

    IEnumerator DeadSound()
    {
        yield return new WaitForSeconds(0.3f);
        enemyAudio.Play_DieSound();
    }

    public void Update_Score()
    {
        score -= damageScore;
       // Debug.Log("score is " + score);
        TextScore.text = "Score: " + score;

    }

    void saveHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
        PlayerPrefs.SetInt("High Score", score);
        PlayerPrefs.Save();

    }


}
