using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed = 8;
    public GameObject model;

    [SerializeField]
    public bool isMoving;
    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(hori, 0, vert);

        if (dir != Vector3.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        Vector3 mouseDir = new Vector3(Input.mousePosition.x, this.transform.position.y, Input.mousePosition.y);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 position = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
            model.transform.LookAt(position);
        }

        //Debug.Log(hit.point);

        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }

    public bool CheckMoving()
    {
        return isMoving;
    }
}
