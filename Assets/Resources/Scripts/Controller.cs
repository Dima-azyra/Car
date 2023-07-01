using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] float max_speed;
    [SerializeField] float accelerat;
    [SerializeField] float max_back_speed;
    [SerializeField] float fading;
    [SerializeField] float angle;

    [Space(20)]
    [SerializeField] WheelCollider al;
    [SerializeField] WheelCollider ar;
    [SerializeField] WheelCollider bl;
    [SerializeField] WheelCollider br;

    [SerializeField] Event_object gas;
    [SerializeField] Event_object back;
    [SerializeField] Event_object left;
    [SerializeField] Event_object right;
    [SerializeField] Event_object stop;

    [SerializeField] Transform al_m;
    [SerializeField] Transform ar_m;
    [SerializeField] Transform bl_m;
    [SerializeField] Transform br_m;

    public static bool is_back;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    private void FixedUpdate()
    {
        if ((gas.push || Input.GetKey(KeyCode.W)) && (!stop.push && !Input.GetKey(KeyCode.Space)) && (!back.push && !Input.GetKey(KeyCode.S)))
        {
            if (max_speed > al.motorTorque && max_speed > ar.motorTorque)
            {
             
                al.motorTorque += accelerat;
                ar.motorTorque += accelerat;
            }
            al.brakeTorque = 0;
            ar.brakeTorque = 0;
            bl.brakeTorque = 0;
            br.brakeTorque = 0;
        }


        if (stop.push || Input.GetKey(KeyCode.Space))
        {
            al.motorTorque = 0;
            ar.motorTorque = 0;
            al.brakeTorque = 2000;
            ar.brakeTorque = 2000;
            bl.brakeTorque = 2000;
            br.brakeTorque = 2000;
        }

        if (back.push || Input.GetKey(KeyCode.S))
        {
            if (al.rotationSpeed > 0 && ar.rotationSpeed > 0)
            {
                al.motorTorque = 0;
                ar.motorTorque = 0;
                al.brakeTorque = 2000;
                ar.brakeTorque = 2000;
                bl.brakeTorque = 2000;
                br.brakeTorque = 2000;
            }
            else
            {
                is_back = true;
                al.brakeTorque = 0;
                ar.brakeTorque = 0;
                bl.brakeTorque = 0;
                br.brakeTorque = 0;

                if (max_back_speed < al.motorTorque && max_back_speed < ar.motorTorque)
                {
                    al.motorTorque -= accelerat;
                    ar.motorTorque -= accelerat;
                }
            }     
        }
        else is_back = false;


        if (!gas.push && !back.push && !left.push && !right.push && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            al.motorTorque = 0;
            ar.motorTorque = 0;
            al.brakeTorque = fading;
            ar.brakeTorque = fading;
            bl.brakeTorque = fading;
            br.brakeTorque = fading;
        }
        else if (!gas.push && !Input.GetKey(KeyCode.W) && !back.push && !Input.GetKey(KeyCode.S))
        {
            al.motorTorque = 0;
            ar.motorTorque = 0;
            al.brakeTorque = fading;
            ar.brakeTorque = fading;
            bl.brakeTorque = fading;
            br.brakeTorque = fading;
        }

        if (left.push || Input.GetKey(KeyCode.A))
        {
            al.steerAngle = -angle;
            ar.steerAngle = -angle;
        }
        else if (right.push || Input.GetKey(KeyCode.D))
        {
            al.steerAngle = angle;
            ar.steerAngle = angle;
        }
        else
        {
            al.steerAngle = 0;
            ar.steerAngle = 0;
        }

        Rotate(al, al_m);
        Rotate(ar, ar_m);
        Rotate(bl, bl_m);
        Rotate(br, br_m);
    }

    void Rotate(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }

    Vector3 ForwardVelocity()
    {
        return transform.forward * Vector3.Dot(GetComponent<Rigidbody>().velocity, transform.forward);
    }

    Vector3 SidewaysVelocity()
    {
        return transform.right * Vector3.Dot(GetComponent<Rigidbody>().velocity, transform.right);
    }
}
