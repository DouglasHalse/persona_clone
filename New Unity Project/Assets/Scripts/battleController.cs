﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class battleController : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy_model;
    public GameObject cam;
    public Text debug_text;
    public int aliveEnemies;
    //public List<GameObject> enemies;
    public List<battle_agent> players;
    public Canvas canvas;
    private Battle_camera_main cam_main;
    public Queue<battle_event> battle_queue;
    public bool players_turn;
    public battle_event current_move;
    public class battle_agent : MonoBehaviour
    {
        public string player_name;
        public int health;
        public int attack_power;
        public GameObject body;
        public bool hostile;
        public void setup_agent(string name, int health, int attack_power, GameObject body, bool hostile)
        {
            //Debug.Log("Creating battle agent...");
            this.player_name = name;
            //this.name = name;
            this.health = health;
            this.attack_power = attack_power;
            this.body = body;
            this.hostile = hostile;
            //Debug.Log("Battle agent made...");
        }
    }

    public class battle_event : MonoBehaviour
    {
        public battle_agent turn_actor;
        public string attacker_name;
        public bool hostile;
        public void setup_battle_event(battle_agent attacker)
        {
            this.turn_actor = attacker;
            this.hostile = turn_actor.hostile;
            this.attacker_name = attacker.player_name;

        }
    }


    private List<Vector3> get_enemy_stations(int number_of_enemies)
    {
        List<Vector3> pos = new List<Vector3>();
        if(number_of_enemies == 1)
        {
            pos.Add(new Vector3(0, 0, 10));
        }
        else if(number_of_enemies == 2)
        {
            pos.Add(new Vector3(-2.5f, 0, 10));
            pos.Add(new Vector3(2.5f, 0, 10));
        }
        else if (number_of_enemies == 3)
        {
            pos.Add(new Vector3(0, 0, 10));
            pos.Add(new Vector3(-5, 0, 10));
            pos.Add(new Vector3(5, 0, 10));
        }
        else if (number_of_enemies == 4)
        {
            pos.Add(new Vector3(-2.5f, 0, 10));
            pos.Add(new Vector3(2.5f, 0, 10));
            pos.Add(new Vector3(-7.5f, 0, 10));
            pos.Add(new Vector3(7.5f, 0, 10));
        }
        else if (number_of_enemies == 5)
        {
            pos.Add(new Vector3(0, 0, 10));
            pos.Add(new Vector3(-5, 0, 10));
            pos.Add(new Vector3(5, 0, 10));
            pos.Add(new Vector3(-2.5f, 0, 15));
            pos.Add(new Vector3(2.5f, 0, 15));
        }

        return pos;
    }
    private void create_enemies(int number_of_enemies, List<Vector3> positions)
    {
        int nr = 0;
        Vector3 model_offset = new Vector3(0, 1, 0);
        foreach(Vector3 position in positions)
        {
            GameObject enemy = Instantiate(enemy_model, position + model_offset, Quaternion.identity);

            var temp = gameObject.AddComponent<battle_agent>();
            temp.setup_agent("Hostile attacker " + nr, 100, 10, enemy, true);
            //temp.name = "Hostile attacker " + nr;

            players.Add(temp);
            //enemies.Add(enemy);
            nr++;
        }
    }
    private bool is_idle()
    {
        //if (this.GetComponent<battleIntroAnimation>().enabled || this.GetComponent<Battle_camera_main>().enabled || !players_turn)
        if (this.GetComponent<battleIntroAnimation>().enabled || this.GetComponent<Battle_camera_main>().enabled)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void move_cam_to_start_pos()
    {
        Vector3 start_loc = cam.transform.position;
        Vector3 battle_loc = new Vector3(3, 3, -18);
        Vector3 look_at_loc = new Vector3(0, 2, 10);
        cam.transform.position = Vector3.Lerp(start_loc, battle_loc, Time.deltaTime * 20f);
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(-cam.transform.position + look_at_loc), Time.deltaTime * 20f);
    }
    private void move_cam_to_pos(Vector3 target_pos)
    {

    }
    // Start is called before the first frame update

    private battle_event next_move(Queue<battle_event> queue)
    {
        battle_event next_move = queue.Dequeue();
        players_turn = !next_move.hostile;
        int z = (int)next_move.turn_actor.body.transform.position.z;
        z = z / (int)Mathf.Abs(z);
        //Debug.Log(z);
        //Vector3 desired_cam_offset = new Vector3(3*-z, 2, 8*z);//change this with cross products
        Vector3 desired_cam_offset;
        if(players_turn)
        {
            desired_cam_offset = new Vector3(3 * -z, 2, 8 * z);
        }
        else
        {
            Vector3 enemy_to_player = next_move.turn_actor.body.transform.position - player.transform.position;
            enemy_to_player.y = 0f;
            //Debug.Log(enemy_to_player);
            Vector3 ortho = Vector3.Cross(Vector3.up, enemy_to_player).normalized;
            //Debug.Log(ortho);
            desired_cam_offset = new Vector3(3*ortho.x, 2, 3*ortho.z) + enemy_to_player.normalized*8;
        }

        Vector3 next_move_agent_pos = next_move.turn_actor.body.transform.position;
        Vector3 final_cam_pos = next_move_agent_pos + desired_cam_offset;
        Vector3 final_look_at_pos;
        //Debug.Log(final_cam_pos);
        if(next_move.hostile)
        {
            final_look_at_pos = player.transform.position;
            //final_look_at_pos = final_cam_pos + player.transform.position;
        }
        else
        {
            final_look_at_pos = new Vector3(0, 2, 10);
        }
        
        cam_main.set_cam_settings(final_cam_pos, final_look_at_pos);
        //cam_main.set_cam_settings(new Vector3(3, 3, -18), new Vector3(0, 2, 10));
        cam_main.run();
        if (queue.Count == 0)
        {
            foreach (battle_agent i in players)
            {
                var temp = gameObject.AddComponent<battle_event>();
                temp.setup_battle_event(i);
                //temp.name = i.player_name;
                battle_queue.Enqueue(temp);
            }
        }

        return next_move;
    }
    void Start()
    {
        cam_main = this.GetComponent<Battle_camera_main>();
        //cam = player.transform.GetChild(0).gameObject;
        cam.GetComponent<cameraController>().enabled = false;
        player.GetComponent<actorController>().enabled = false;
        //enemies = new List<GameObject>();
        battle_queue = new Queue<battle_event>();
        aliveEnemies = Random.Range(1, 5);
        create_enemies(aliveEnemies, get_enemy_stations(aliveEnemies));

        var main_character = gameObject.AddComponent<battle_agent>();
        main_character.setup_agent("Protaganist", 1000, 100, player, false);
        //main_character.name = "Protaganist";
        //main_character.body = player;
        players.Add(main_character);
        players.Reverse();
        //players.Add(new battle_agent("Player One", 1000, 50, player, false));
        this.GetComponent<battleIntroAnimation>().enabled = true;
        foreach (battle_agent i in players)
        {
            var temp = gameObject.AddComponent<battle_event>();
            temp.setup_battle_event(i);
            //temp.name = i.player_name;
            battle_queue.Enqueue(temp);
        }
        //!!!current_move = battle_queue.Dequeue();
        Debug.Log("First move triggered");
        current_move = next_move(battle_queue);
        //Debug.Log("Number of enemies: " + aliveEnemies);
        //Disable player camera controller
        
        //Start battle intro animation script

    }

    
    // Update is called once per frame
    void Update()
    {
        debug_text.text = "Debug: \n" + 
            "Animation in progress: " + !is_idle() + "\n" +
            "Current move: " + current_move.turn_actor.player_name + "\n" + 
            "Current player HP: " + current_move.turn_actor.health + "\n";
        if (is_idle())
        {
            //Debug.Log("Queue count: " + battle_queue.Count);
            
            
            if(!current_move.hostile)
            {
                //players_turn = true;
                //cam_main.set_cam_settings(new Vector3(3, 3, -18), new Vector3(0, 2, 10));
                //cam_main.run();
                //Debug.Log("Players move!");
                if(Input.GetButton("W"))
                {
                    Debug.Log("Next turn!");
                    current_move = next_move(battle_queue);
                    //current_move = battle_queue.Dequeue();
                }
            }
            else if(current_move.hostile)
            {
                //Debug.Log("Enemy turn!");
                if (Input.GetButton("W"))
                {
                    Debug.Log("Next turn!");
                    current_move = next_move(battle_queue);
                    //current_move = battle_queue.Dequeue();
                }
                //AI CONTROLL
            }
            //move_cam_to_start_pos();
            //Debug.Log("enemy name: "+ players[0].name);
            //Debug.Log("Player name: " + players[players.Count - 1].name);

        }
        else
        {
            //Debug.Log("Not idle");
            //Animations are prob running...
            //Debug.Log("Camera animation in progress");
        }
        
        
        //successfully took control of camera
        //Actual battle logic
    }
}
