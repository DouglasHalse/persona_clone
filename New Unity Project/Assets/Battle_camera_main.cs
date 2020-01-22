using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_camera_main : MonoBehaviour
{
    public GameObject cam;
    public Vector3 current_cam_pos;
    public Vector3 desired_cam_pos;
    private Vector3 desired_cam_lookat;
    private Vector3 cam_move_rail;
    private Quaternion current_lookat;
    private Quaternion desired_lookat;
    private float angle;
    public float t;
    public bool movement_done;
    public void set_cam_settings(Vector3 desired_cam_pos, Vector3 desired_cam_lookat)
    {
        this.desired_cam_pos = desired_cam_pos;
        this.desired_cam_lookat = desired_cam_lookat;
    }
    public void run()
    {
        this.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        t = 0;
        current_cam_pos = cam.transform.position;
        cam_move_rail = desired_cam_pos - current_cam_pos;
        current_lookat = cam.transform.rotation;
        desired_lookat = Quaternion.LookRotation(-new Vector3(3, 3, -18) + desired_cam_lookat);
        angle = Quaternion.Angle(current_lookat, desired_lookat);
        //Debug.Log("Angle: " + angle);
        //cam.transform.position = Vector3.Lerp(current_cam_pos, desired_cam_pos, Time.deltaTime * 20f);
        //cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.LookRotation(-cam.transform.position + desired_cam_lookat), Time.deltaTime * 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if(t<1f)
        {
            //cam.transform.position = desired_cam_pos;
            cam.transform.position = current_cam_pos + cam_move_rail * t;
            //cam.transform.rotation = current_lookat
            cam.transform.rotation = Quaternion.RotateTowards(current_lookat, desired_lookat, angle*t);//Its shagged
            t = t + 2f * Time.deltaTime;
        }
        else
        {
            t = 0f;
            this.enabled = false;
        }
        
    }
}
