using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public GameController GC;

    private bool Started = false;

    public GameObject pressEToRepair;
    public GameObject pressEToBuy;
    private BuildingController BC;

    public GameObject Robot;
    public GameObject mine;
    public GameObject factory;
    public GameObject market;

    public float speed = 12f;
    public float springSpeed = 24f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private int RobotPrice = 600;
    private int buildingCost = 300;
    
    public bool isRobotSpawned = false;
    public int UpgradeCounter = 1;

    Vector3 velocity;

    private bool isGrounded;

    void Update()
    {
        if (Started)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            if (Input.GetButton("sprint"))
            {
                controller.Move(move * springSpeed * Time.deltaTime);
            }
            else
            {
                controller.Move(move * speed * Time.deltaTime);
            }

            //jump mechanic, finish map layout then change all tags(layer) to ground 
            if (Input.GetButton("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Building")
        {
            BC = other.GetComponent<BuildingController>();

            if (BC.isFixed == false)
            {
                pressEToRepair.SetActive(true);
            }
        }
        else if(other.tag == "RobotBuy")
        {
            pressEToBuy.SetActive(true);
            if (isRobotSpawned == false)
            {
                pressEToBuy.GetComponent<Text>().text = "Press E to buy\nCost: " + RobotPrice;
            }
            else
            {
                pressEToBuy.GetComponent<Text>().text = "Press E to upgrade\nCost: " + RobotPrice;
            }
        }
        else if(other.tag == "BuildMine")
        {
            pressEToBuy.GetComponent<Text>().text = "Press E to buy\nCost: " + buildingCost;
            pressEToBuy.SetActive(true);
        }
        else if (other.tag == "BuildFactory")
        {
            pressEToBuy.GetComponent<Text>().text = "Press E to buy\nCost: " + buildingCost;
            pressEToBuy.SetActive(true);
        }
        else if (other.tag == "BuildMarket")
        {
            pressEToBuy.GetComponent<Text>().text = "Press E to buy\nCost: " + buildingCost;
            pressEToBuy.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Building")
        {
            if (Input.GetButtonDown("Repair") && BC.isFixed == false)
            {
                BC.Repair();
                pressEToRepair.SetActive(false);
            }
        }
        else if(other.tag == "RobotBuy")
        {
            if (Input.GetButtonDown("Repair"))
            {
                if (isRobotSpawned == false)
                {
                    if (GC.MoneyAmount >= RobotPrice)
                    {
                        GC.MoneyAmount -= RobotPrice;

                        Instantiate(Robot, other.transform.position, other.transform.rotation);
                        RobotPrice = Mathf.RoundToInt(RobotPrice * 1.2f);
                        isRobotSpawned = true;
                    }
                }
                else
                {
                    if (GC.MoneyAmount >= RobotPrice)
                    {
                        GC.MoneyAmount -= RobotPrice;
                        RobotPrice = Mathf.RoundToInt(RobotPrice * 1.2f);

                        UpgradeCounter++;
                        RobotController RC = GameObject.FindGameObjectWithTag("Robot").GetComponent<RobotController>();
                        RC.speed = UpgradeCounter;
                    }
                }
            }
        }
        else if (other.tag == "BuildMine")
        {
            if (Input.GetButtonDown("Repair"))
            {
                if (GC.MoneyAmount >= buildingCost)
                {
                    GC.MoneyAmount -= buildingCost;
                    pressEToBuy.SetActive(false);
                    Instantiate(mine, new Vector3(other.transform.position.x, -0.6f, other.transform.position.z), other.transform.rotation);
                    buildingCost = Mathf.RoundToInt(buildingCost * 1.2f);
                    Destroy(other.gameObject);
                }
            }
        }
        else if (other.tag == "BuildFactory")
        {
            if (Input.GetButtonDown("Repair"))
            {
                if (GC.MoneyAmount >= buildingCost)
                {
                    GC.MoneyAmount -= buildingCost;
                    pressEToBuy.SetActive(false);
                    Instantiate(factory, new Vector3(other.transform.position.x, 0f, other.transform.position.z), other.transform.rotation);
                    buildingCost = Mathf.RoundToInt(buildingCost * 1.2f);
                    Destroy(other.gameObject);
                }
            }
        }
        else if (other.tag == "BuildMarket")
        {
            if (Input.GetButtonDown("Repair"))
            {
                if (GC.MoneyAmount >= buildingCost)
                {
                    GC.MoneyAmount -= buildingCost;
                    pressEToBuy.SetActive(false);
                    Instantiate(market, new Vector3(other.transform.position.x, -0.6f, other.transform.position.z), other.transform.rotation);
                    buildingCost = Mathf.RoundToInt(RobotPrice * 1.2f);
                    Destroy(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Building")
        {
            pressEToRepair.SetActive(false);
        }
        else if(other.tag == "RobotBuy")
        {
            pressEToBuy.SetActive(false);
        }
        else if (other.tag == "BuildMine")
        {
            pressEToBuy.SetActive(false);
        }
        else if (other.tag == "BuildFactory")
        {
            pressEToBuy.SetActive(false);
        }
        else if (other.tag == "BuildMarket")
        {
            pressEToBuy.SetActive(false);
        }
    }

    public void StartGame()
    {
        Started = true;
    }
}
