using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleIntroAnimation : MonoBehaviour
{
    //public GameObject player;
    public GameObject cam;
    private float t;
    private float phi;
    private float end_phi;
    private float delta_phi;
    private float radius;
    private int number_of_enemies;
    private Vector3 origo;
    // Start is called before the first frame update
    private Vector3 get_xyz(float phi, float radius)
    {
        float x = radius * Mathf.Cos(phi);
        float z = radius * Mathf.Sin(phi);
        return new Vector3(x, 0, z);
    }
    private float get_radius(int number_of_enemies, float t)
    {
        if (number_of_enemies >= 4)
        {
            return (10f + (Mathf.Pow(t, 2) - t + 1f) * 10f);
        }
        else if (number_of_enemies == 3)
        {
            return (10f + (Mathf.Pow(t, 2) - t + 1f) * 10f);
        }
        else
        {
            return (10f + (Mathf.Pow(t, 2) - t + 1f) * 10f);
        }
    }
    void Start()
    {
        //cam = player.transform.GetChild(0).gameObject;
        t = 0;
        origo = new Vector3(0, 1, 15);
        
        phi = (5 * Mathf.PI) / 4f;
        end_phi = (7 * Mathf.PI) / 4f;
        delta_phi = end_phi - phi;
        number_of_enemies = this.GetComponent<battleController>().aliveEnemies;
        //Debug.Log("camenemies: " + number_of_enemies);
        radius = 10f + (t * 10f);
        //x = r*cos(phi)
        //z = r*sin(phi)
    }

    // Update is called once per frame
    void Update()
    {
        
        if (t < 1f)
        {
            cam.transform.LookAt(origo);
            cam.transform.position = get_xyz(phi + (delta_phi * t), get_radius(number_of_enemies, t)) + origo;
            //phi = phi + (delta_phi * t);
            t += Time.deltaTime * 0.3f * (Mathf.Pow(t, 2) - t + 1);
        }
        else
        {
            this.enabled = false;
        }
        
        
    }
}
