using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObservePanelControl : MonoBehaviour
{
    public Transform imageLayers;
    public Transform arrowTransform;
    public Text btnText;
    public Text arrowInfoText;
    public RawImage arrowImg;
    public Texture2D arrowTex;
    public Texture2D arrowTexHigh;
    public Color arrowCol;
    public Color arrowColHigh;
    public RawImage crossImg;
    public RawImage wImage;
    bool expanded = true;
    bool isDragging;
    public bool canAdjustCross;
    float crossX = 0;

    public void ExpandClicked()
    {
        expanded ^= true;
        imageLayers.gameObject.SetActive(expanded);
        arrowTransform.gameObject.SetActive(expanded);
        btnText.text = expanded ? "收起" : "展开";
    } 

    public void SlideEnter()
    {
        arrowImg.color = arrowColHigh;
    }

    public void SlideExit()
    {
        arrowImg.color = arrowCol;
    }

    public void SlideDown()
    {
        isDragging = true;
        Raycaster.LockCursor();
        arrowImg.texture = arrowTexHigh;
    }

    public void SlideUp()
    {
        isDragging = false;
        Raycaster.RestoreCursor();
        arrowImg.texture = arrowTex;
    }


    void Start()
    {
        ExpandClicked();
    }
    
    void Update()
    {
        arrowTransform.gameObject.SetActive(canAdjustCross && expanded);
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Raycaster.RestoreCursor();
        }

        if (canAdjustCross)
        {
            if (isDragging)
            {
                crossX = Mathf.Clamp(crossX + -Input.GetAxis("Mouse X") * Time.deltaTime / 5f, -0.5f, 0.5f);
            }
            arrowInfoText.text = string.Format("拖动箭头调整叉丝位置：{0:0.000}mm", (crossX + 0.5f) * 10);
            
            var rect = crossImg.uvRect;
            rect.x = crossX;
            crossImg.uvRect = rect;

            wImage.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            crossImg.uvRect = new Rect(0, 0, 1, 1);
            wImage.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
