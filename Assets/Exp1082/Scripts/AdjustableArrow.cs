using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustableArrow : MonoBehaviour
{
    public bool vertical;
    public Material matNormal;
    public Material matHighlight;
    MeshRenderer mr;

    bool dragging = false;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }
    
    void Update()
    {
        var hit = Raycaster.hitObject == gameObject;
        mr.material = hit || dragging ? matHighlight : matNormal;

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
            Vector2 move = Vector2.zero;
            if (vertical)
            {
                move.y = Input.GetAxis("Mouse Y") * Time.deltaTime;
            }
            else
            {
                move.x = Input.GetAxis("Mouse X") * Time.deltaTime;
            }

            if(Vector3.Dot(transform.forward, transform.position - Camera.main.transform.position) < 0)
            {
                move.x *= -1;
            }
            AdjustableManager.instance.TryMove(AdjustableManager.selected, move);
        }
    }
}
