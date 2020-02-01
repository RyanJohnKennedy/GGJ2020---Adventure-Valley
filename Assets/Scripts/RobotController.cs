using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    private BuildingController BC;

    public GameObject[] wayPoints;
    private int current = 0;
    private float rotSpeed;
    public float speed;
    private float WpRadius = 2;

    public bool isRepairing = false;

    private void Start()
    {
        FindBuildings();
    }

    // Update is called once per frame
    void Update()
    {
        FindBuildings();

        MoveToBuilding();
    }

    public void FindBuildings()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("Building");
    }

    public void MoveToBuilding()
    {
        BC = wayPoints[current].GetComponent<BuildingController>();

        if (BC.isFixed == false)
        {
            if (Vector3.Distance(wayPoints[current].transform.position, transform.position) < WpRadius)
            {
                BC.Repair();

                current++;
                if (current >= wayPoints.Length)
                {
                    current = 0;
                }
            }

            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, -0.8f, transform.position.z), wayPoints[current].transform.position,
                Time.deltaTime * speed);

            transform.LookAt(wayPoints[current].transform);
        }
        else
        {
            current++;
            if (current >= wayPoints.Length)
            {
                current = 0;
            }
        }
    }
}
