using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    Camera cam;
    public static GameObject hitObject;
    private static Raycaster instance;

    [DllImport("User32")]
    private static extern bool SetCursorPos(int x, int y);
    [DllImport("User32")]
    private static extern bool GetCursorPos(out Point pt);

    private static Point savedPos;

    public static void LockCursor()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            GetCursorPos(out savedPos);
            //Debug.Log(savedPos);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public static void RestoreCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            instance.StartCoroutine(instance.SetCursorPosDelayed(savedPos.X, savedPos.Y));
        }
    }

    private IEnumerator SetCursorPosDelayed(int x, int y)
    {
        yield return 0;
        SetCursorPos(x, y);
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = GetComponent<Camera>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetCursorPos(savedPos.X, savedPos.Y);
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            hitObject = hit.collider.gameObject;
        }
        else
        {
            hitObject = null;
        }
    }
}
