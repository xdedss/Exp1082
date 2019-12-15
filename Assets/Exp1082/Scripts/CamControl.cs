using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#if UNITY_EDITOR
//[ExecuteInEditMode]
//#endif
public class CamControl : MonoBehaviour
{
    public Transform CamTarget;
    public Transform CamBase;
    public Transform Cam;
    float yaw;
    [Range(0, 1)]
    public float yawt;
    //float vyaw = 0;
    //float vmyaw = 0.15f;
    float lr;
    [Range(0, 1)]
    public float lrt;
    //float vlr = 0;
    //float vmlr = 0.15f;
    float zoom;
    [Range(0, 1)]
    public float zoomt;
    //float vzoom = 0;
    //float vmzoom = 0.4f;

    void Start()
    {
        
    }
    
    void Update()
    {
        CamTarget.transform.position = CamTarget.transform.position.SetZ(Mathf.Clamp01(yaw) * -1.5f);
        transform.position = transform.position.SetZ(Mathf.Clamp01(lr) * -1.5f);
        Cam.localPosition = Cam.localPosition.SetX(Mathf.Exp(Mathf.Clamp01(zoom)) / 4);

        lrt = Mathf.Clamp01(lrt -Input.GetAxis("AD") * Time.deltaTime / 5);
        lr = Mathf.Lerp(lr, lrt, 0.05f);
        yawt = Mathf.Clamp01(yawt + (-Input.GetAxis("QE") - Input.GetAxis("AD")) * Time.deltaTime / 5);
        yaw = Mathf.Lerp(yaw, yawt, 0.05f);
        zoomt = Mathf.Clamp01(zoomt - Input.GetAxis("SW") * Time.deltaTime / 1);
        zoom = Mathf.Lerp(zoom, zoomt, 0.05f);

        //vlr = Mathf.Lerp(vlr, Mathf.Clamp(-Input.GetAxis("AD"), -vmlr, vmlr), 0.05f);
        //lr = Mathf.Clamp01(lr + vlr * Time.deltaTime);
        //vyaw = Mathf.Lerp(vyaw, Mathf.Clamp(-Input.GetAxis("QE"), -vmyaw, vmyaw), 0.05f);
        //yaw = Mathf.Clamp01(yaw + vyaw * Time.deltaTime);
        //vzoom = Mathf.Lerp(vzoom, Mathf.Clamp(-Input.GetAxis("SW"), -vmzoom, vmzoom), 0.05f);
        //zoom = Mathf.Clamp01(zoom + vzoom * Time.deltaTime);
    }
}
