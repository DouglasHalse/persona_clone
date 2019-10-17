using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    public GameObject enemy;
    public GameObject path_nodes;
    private LinkedList<Vector3> nodes;
    private LinkedListNode<Vector3> current_target;
    private LinkedListNode<Vector3> next_target;
    private Vector3 target_dir;
    private Transform body;

    // Start is called before the first frame update

        /*
         *      Simple patrolling script that moves enemy to all the nodes in order.
         *      enemy is an empty GameObject that could have a child that represents the enemy ingame
         *      path_nodes is an empty GameObject that has a number of empty gameobjects as children that represent the nodes along the path.
         *      The patrolling loops so the first and last node should have line of sight to ensure that it works.
         */
    void Start()
    {
        body = enemy.transform;
        nodes = new LinkedList<Vector3>();
        foreach(Transform child in path_nodes.transform)
        {
            nodes.AddLast(child.transform.position);    //This assumes that the container for all the nodes are located at origo!!!
        }
        current_target = nodes.First;
        next_target = nodes.First.Next;
        target_dir = (current_target.Value - enemy.transform.position).normalized;
        body.transform.LookAt(target_dir);
    }

    // Update is called once per frame
    void Update()
    {
    target_dir = (current_target.Value - enemy.transform.position).normalized;
    if((enemy.transform.position - current_target.Value).magnitude < 1f)    //Reached the current target
    {
        current_target = next_target;                                       //Itterate target
        if (next_target.Next == null)                                       //Update next target, if NULL restart from start of list
        {
            next_target = nodes.First;
        }
        else
        {
            next_target = next_target.Next;
        }
        
        target_dir = (current_target.Value - enemy.transform.position).normalized;  //Define new direction for the new target
    }
        body.transform.LookAt(target_dir + enemy.transform.position);               //Rotates the enemy towards the target
        enemy.transform.position += target_dir * Time.deltaTime * 4f;               //moves the enemy towards to the target.

    }
}
