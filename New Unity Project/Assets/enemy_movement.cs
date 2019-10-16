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
    void Start()
    {
        //Debug.Log("children: " + CountChidren())
        body = enemy.transform;
        nodes = new LinkedList<Vector3>();
        foreach(Transform child in path_nodes.transform)
        {
            nodes.AddLast(child.transform.position);
        }
        Debug.Log("First node pos: " + nodes.First.Value);
        current_target = nodes.First;
        next_target = nodes.First.Next;
        target_dir = (current_target.Value - enemy.transform.position).normalized;
        body.transform.LookAt(target_dir);
        Debug.Log("Last next node pos: " + nodes.Last.Next);
    }

    // Update is called once per frame
    void Update()
    {
        target_dir = (current_target.Value - enemy.transform.position).normalized;
        for (int i = 0; i<nodes.Count; i++)
        {
            //Debug.Log("Distance to node " + (enemy.transform.position - node_pos[0]).magnitude);
            Debug.Log("Enemy position: " + enemy.transform.position);
            Debug.Log("Node 0 pos: " + nodes.First.Value);
            //Debug.Log("next for " +i+" is: " + next_list_index(node_pos.Capacity, i));
            //Debug.Log("List capacity: " + node_pos.Capacity);
            if((enemy.transform.position - current_target.Value).magnitude < 2f)
            {
                Debug.Log("checkpoint reached");
                if(next_target.Next == null)
                {
                    next_target = nodes.First;
                }
                else
                {
                    next_target = next_target.Next;
                }
                current_target = next_target;
                target_dir = (current_target.Value - enemy.transform.position).normalized;
            }
        }
        body.transform.LookAt(target_dir + enemy.transform.position);
        enemy.transform.position += target_dir * Time.deltaTime * 4f;

    }
}
