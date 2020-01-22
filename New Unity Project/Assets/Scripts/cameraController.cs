using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraController : MonoBehaviour
{
    public GameObject actor;
    public GameObject cam;
    private float theta;
    private float phi;
    public float r;
    //private bool moving;

    private Vector3 get_new_xyz(float x_input, float y_input)
    {
        //x_input = x_input;
        //y_input = y_input;
        Vector3 rel_pos = (cam.transform.position - actor.transform.position);
        phi = Mathf.Atan2(rel_pos.x, rel_pos.z);
        theta = Mathf.Acos(rel_pos.y / rel_pos.magnitude);
        phi += x_input * 0.1f;
        theta += y_input * 0.1f;
        float new_y = r * Mathf.Cos(theta);
        float new_x = r * Mathf.Sin(theta) * Mathf.Sin(phi);
        float new_z = r * Mathf.Sin(theta) * Mathf.Cos(phi);
        Vector3 lookPath = -rel_pos;
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(lookPath), Time.deltaTime * 1000f);
        //cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, Quaternion.LookRotation(lookPath), Time.deltaTime * (Mathf.Pow(Quaternion.Angle(cam.transform.rotation, Quaternion.LookRotation(lookPath)), 4)));
        //cam.transform.LookAt(actor.transform.position);
        return new Vector3(new_x, new_y, new_z);
    }
    // Start is called before the first frame update
    void Start()
    {
        theta = Mathf.Acos(-(cam.transform.position - actor.transform.position).y / (cam.transform.position - actor.transform.position).magnitude);
        phi = Mathf.Atan2((cam.transform.position - actor.transform.position).x, (cam.transform.position - actor.transform.position).z);
        r = (cam.transform.position - actor.transform.position).magnitude;
        //moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        //cam.transform.position = actor.transform.position + get_new_xyz(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 new_pos = actor.transform.position + get_new_xyz(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        cam.transform.position = Vector3.Slerp(cam.transform.position, new_pos, 1000f * Time.deltaTime);
    }
}
