using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlitAdjustControl : MonoBehaviour
{
    bool dragging;
    public Material matNormal;
    public Material matHighlight;
    public MeshRenderer mr;
    public AdjustableControl SlitControl;
    public float zAngle = 0;

    void Start()
    {
        //mr = GetComponent<MeshRenderer>();
    }
    
    void Update()
    {
        var hit = Raycaster.hitObject == mr.gameObject;
        mr.material = hit || dragging ? matHighlight : matNormal;
        mr.gameObject.SetActive(AdjustableManager.selected == SlitControl);
        if (hit && Input.GetMouseButtonDown(0))
        {
            dragging = true;
            Raycaster.LockCursor();
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            Raycaster.RestoreCursor();
        }

        if (dragging)
        {
            float move = Input.GetAxis("Mouse X") * Time.deltaTime * 3;

            if (Vector3.Dot(transform.forward, transform.position - Camera.main.transform.position) < 0)
            {
                move *= -1;
            }

            var euler = transform.localEulerAngles;
            zAngle = Mathf.Clamp(zAngle - move, -3, 3);
            euler.z = zAngle;
            transform.localEulerAngles = euler;
        }
    }
}
