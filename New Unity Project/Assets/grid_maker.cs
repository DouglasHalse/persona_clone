using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid_maker : MonoBehaviour
{
    // Start is called before the first frame update
    private int size_x;
    private int size_z;
    private int total_nodes;
    private List<Vector3> positions;
    private List<GameObject> nodes;
    private int[,] adjmatrix = new int[10, 10];

    public void set_xz_size(int x, int z)
    {
        size_x = x;
        size_z = z;
        total_nodes = x * z;

    }
    void Start()
    {
        size_x = 10;
        size_z = 10;
        total_nodes = 100;
        positions = new List<Vector3>();
        for (int i = 0; i < total_nodes; i++)
        {
            positions.Add(new Vector3(i % size_x, 0, i / size_z));
        }
        foreach(Vector3 pos in positions)
            {
            GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Quad);
            temp.name = pos.x.ToString() + pos.z.ToString();
            temp.transform.Rotate(Vector3.right, -90f);
            temp.transform.localScale = new Vector3(5, 5, 0);
            temp.transform.position = pos*5f + new Vector3(-22.5f, 0, -22.5f);
            temp.transform.parent = this.transform;
            //Instantiate(new GameObject("Sphere")).transform.position = pos;
            }
        foreach(Transform child in this.transform)
        {
            nodes.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
