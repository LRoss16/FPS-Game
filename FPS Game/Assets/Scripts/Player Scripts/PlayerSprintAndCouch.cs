using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCouch : MonoBehaviour {

    private PlayerMovement playerMovement;

    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f;

    private Transform look_Root;
    private float stand_height = 1.6f;
    private float crouch_height = 1f;

    private bool is_Crouching;

    private PlayerFootSteps player_Footsteps;

    private float sprint_volume = 1f;
    private float crouch_volume = 0.1f;
    private float walk_volume_min = 0.2f, walk_volume_max = 0.6f;

    private float walk_step_distance = 0.4f;
    private float sprint_step_distance = 0.25f;
    private float crouch_step_distance = 0.5f;

    private PlayerStats player_Stats;

    private float sprint_value = 100f;

    public float sprint_threshold = 10f;
	// Use this for initialization
	void Awake () {

        playerMovement = GetComponent<PlayerMovement>();

        look_Root = transform.GetChild(0);

        player_Footsteps = GetComponentInChildren<PlayerFootSteps>();

        player_Stats = GetComponent<PlayerStats>();
	}

    void Start()
    {
        player_Footsteps.volume_Min = walk_volume_min;
        player_Footsteps.volume_Max = walk_volume_max;
        player_Footsteps.step_Distance = walk_step_distance;

    }
	
	// Update is called once per frame
	void Update () {
        Sprint();
        Crouch();
	}

    void Sprint()
    {
        if (sprint_value > 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching)
            {
                playerMovement.speed = sprint_Speed;

                player_Footsteps.step_Distance = sprint_step_distance;
                player_Footsteps.volume_Min = sprint_volume;
                player_Footsteps.volume_Max = sprint_volume;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching)
        {
            playerMovement.speed = move_Speed;

            player_Footsteps.volume_Min = walk_volume_min;
            player_Footsteps.volume_Max = walk_volume_max;
            player_Footsteps.step_Distance = walk_step_distance;

        }

        if (Input.GetKey(KeyCode.LeftShift) && !is_Crouching)
        {
            sprint_value -= sprint_threshold * Time.deltaTime;

            if(sprint_value <= 0)
            {
                sprint_value = 0f;

                //reset speed and sound
                playerMovement.speed = move_Speed;
                player_Footsteps.volume_Min = walk_volume_min;
                player_Footsteps.volume_Max = walk_volume_max;
                player_Footsteps.step_Distance = walk_step_distance;
            }

            player_Stats.Display_StaminaStats(sprint_value);

        }
        else
        {
            if (sprint_value != 100f)
            {
                sprint_value += (sprint_threshold / 2f) * Time.deltaTime;

                player_Stats.Display_StaminaStats(sprint_value);

                if (sprint_value > 100f)
                {
                    sprint_value = 100f;
                }
            }
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (is_Crouching)
            {
                look_Root.localPosition = new Vector3(0f, stand_height, 0f);
                playerMovement.speed = move_Speed;
                player_Footsteps.volume_Min = walk_volume_min;
                player_Footsteps.volume_Max = walk_volume_max;
                player_Footsteps.step_Distance = walk_step_distance;

                is_Crouching = false;
            } else
            {
                look_Root.localPosition = new Vector3(0f, crouch_height, 0f);
                playerMovement.speed = crouch_Speed;
                player_Footsteps.step_Distance = crouch_step_distance;
                player_Footsteps.volume_Min = crouch_volume;
                player_Footsteps.volume_Max = crouch_volume;

                is_Crouching = true;

            }
        }

    }
}
