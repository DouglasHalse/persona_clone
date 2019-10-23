using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy_model;
    private GameObject cam;
    public int aliveEnemies;
    private List<GameObject> enemies;


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
        Vector3 model_offset = new Vector3(0, 1, 0);
        foreach(Vector3 position in positions)
        {
            GameObject enemy = Instantiate(enemy_model, position + model_offset, Quaternion.identity);
            enemies.Add(enemy);
        }
    }
    private bool is_idle()
    {
        if (this.GetComponent<battleIntroAnimation>().enabled)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void move_cam_to_battle_pos()
    {
        Vector3 start_loc = cam.transform.position;
        Vector3 battle_loc = new Vector3(3, 3, -18);
        Vector3 look_at_loc = new Vector3(0, 2, 10);
        cam.transform.position = Vector3.Lerp(start_loc, battle_loc, Time.deltaTime * 20f);
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(-cam.transform.position + look_at_loc), Time.deltaTime * 20f);
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = player.transform.GetChild(0).gameObject;
        cam.GetComponent<cameraController>().enabled = false;

        enemies = new List<GameObject>();
        aliveEnemies = Random.Range(1, 5);
        create_enemies(aliveEnemies, get_enemy_stations(aliveEnemies));
        this.GetComponent<battleIntroAnimation>().enabled = true;
        
        //Debug.Log("Number of enemies: " + aliveEnemies);
        //Disable player camera controller

        //Start battle intro animation script

    }

    // Update is called once per frame
    void Update()
    {
        if(is_idle())
        {
            move_cam_to_battle_pos();

        }
        else
        {
            Debug.Log("Camera animation in progress");
        }
        
        //successfully took control of camera
        //Actual battle logic
    }
}
