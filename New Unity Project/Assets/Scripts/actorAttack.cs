using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class actorAttack : MonoBehaviour
{
    public GameObject actor;
    public GameObject cam;
    public bool attack_available; //change to private
    public GameObject target; // ----
    public GameObject[] temp;
    private Vector3 target_loc;
    private Vector3 actor_loc;
    private Vector3 arc_middle;
    private GameObject arc_tester;
    public float t;
    public bool attacking;
    private AssetBundle scenes;
    private Vector3 get_bez_point(float t)
    {
        return (1f - t) * ((1f - t) * actor_loc + t * arc_middle) + t * ((1f - t) * arc_middle + t * target_loc);
    }
    // Start is called before the first frame update
    public bool can_attack()
    {
        return attack_available;
    }
    public GameObject get_target()
    {
        return target;
    }
    void Start()
    {

        temp = GameObject.FindGameObjectsWithTag("enemy");
        target = null;
        arc_tester = GameObject.CreatePrimitive(PrimitiveType.Cube);
        t = 0;
        attacking = false;
        scenes = AssetBundle.LoadFromFile("Assets/AssetBundles/scenes");

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(enemies.Count);
        temp = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject i in temp)
        {
            //Debug.Log((i.transform.position - actor.transform.position).magnitude);
            if ((i.transform.position - actor.transform.position).magnitude<10)
            {
                //Debug.Log("Enemy near!");
                attack_available = true;
                target = i;
                break;
            }
        }
        if(target && (target.transform.position - actor.transform.position).magnitude > 10)
        {
            attack_available = false;
            target = null;
        }
        if(target)
        {
            if(!attacking)
            {
                target_loc = target.transform.position + Vector3.up;
                actor_loc = actor.transform.position;
                arc_middle = 8 * Vector3.up + (actor_loc + target_loc) / 2;
                attacking = true;
            }
            else
            {
                actor.transform.position = get_bez_point(t);
                t = t + 0.01f;
                if(t > 1f)
                {
                    attacking = false;
                    this.enabled = false;
                    //SceneManager.LoadScene("battle");
                    t = 0;
                }
            }
            

            
            //arc_tester.transform.position = arc_middle;
        }
        else
        {
            arc_tester.transform.position = Vector3.zero;
        }
    }
}
