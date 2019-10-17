using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleController : MonoBehaviour
{
    public GameObject player;
    private GameObject cam;
    private int aliveEnemies;


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
    // Start is called before the first frame update
    void Start()
    {
        cam = player.transform.GetChild(0).gameObject;
        cam.GetComponent<cameraController>().enabled = false;
        //Disable player camera controller

        //Start battle intro animation script
        
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position += Vector3.up; //successfully took control of camera
        //Actual battle logic
    }
}
