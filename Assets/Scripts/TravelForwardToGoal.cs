using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelForwardToGoal : MonoBehaviour
{
    public Transform goal;
    public GameObject player;
    float speed = 7;
    float rotSpeed = 5;

    Vector3 lookAtGoal;
    Vector3 oldLookAtGoal;
    float goalDistance = 1;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        oldLookAtGoal = goal.position;
        lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
        goalDistance = Vector3.Distance(lookAtGoal, transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);

        //transform.LookAt(lookAtGoal);
        Vector3 direction = lookAtGoal - transform.position;

        distance = Vector3.Distance(lookAtGoal, transform.position);

        bool movingState = player.GetComponent<PlayerMovement>().isMoving;

        if (goalDistance < distance)
        {
            goalDistance += distance - goalDistance;
        }

        float percentageComplete = (goalDistance - distance) / goalDistance;
        Vector3 test = Vector3.Lerp(new Vector3(0, 0, speed*.4f), new Vector3(0,0,speed), .5f * Mathf.Cos(percentageComplete * 2 * Mathf.PI + Mathf.PI) + .5f);
        //float zSpeed = Mathf.Lerp(speed, 0, Mathf.SmoothStep(0, 1, .5f * Mathf.Cos(percentageComplete * 2 * Mathf.PI + Mathf.PI) + .5f));

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);

        //Debug.Log(zSpeed + " | " + percentageComplete + " | " + goalDistance);
        Debug.Log(test + " | " + goalDistance + " | " + distance);

        if (Vector3.Distance(lookAtGoal, transform.position) > 1)
        {
            transform.Translate(test * Time.deltaTime);
        }
        else
        {
            goalDistance = 0;
        }
    }
}
