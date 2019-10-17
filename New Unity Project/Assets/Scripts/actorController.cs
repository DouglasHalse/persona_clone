using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actorController : MonoBehaviour
{
    public GameObject actor;
    public GameObject cam;
    private int num_of_keys()
    {
        int i = 1;
        if (Input.GetButton("W"))
        {
            i++;
        }
        if (Input.GetButton("A"))
        {
            i++;
        }
        if (Input.GetButton("S"))
        {
            i++;
        }
        if (Input.GetButton("D"))
        {
            i++;
        }
        return i;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPath = (actor.transform.position - cam.transform.position).normalized * Time.deltaTime * 30f;
        Vector3 dir = new Vector3(camPath.x, 0, camPath.z);
        if (Input.GetButton("W"))
        {
            actor.transform.position += new Vector3(camPath.x, 0, camPath.z) / num_of_keys();
        }
        if (Input.GetButton("S"))
        {
            actor.transform.position -= new Vector3(camPath.x, 0, camPath.z) / num_of_keys();
        }
        if (Input.GetButton("A"))
        {
            actor.transform.position += Vector3.Cross(dir, new Vector3(0, 1, 0) / num_of_keys());
        }
        if (Input.GetButton("D"))
        {
            actor.transform.position += -Vector3.Cross(dir, new Vector3(0, 1, 0) / num_of_keys());
        }
    }
}
