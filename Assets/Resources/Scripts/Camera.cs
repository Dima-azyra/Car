using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform camera_pos;
    [SerializeField] Transform camera_pos_back;
    Transform car;
    [SerializeField] float speed_pos;
    [SerializeField] float speed_rot;
    private void FixedUpdate()
    {
        if (Controller.is_back) car = camera_pos_back;
        else car = camera_pos;

        transform.position = Vector3.Lerp(transform.position, car.position, Time.fixedDeltaTime * speed_pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, car.rotation, Time.fixedDeltaTime * speed_rot);
    }
}
