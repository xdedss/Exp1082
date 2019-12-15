using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlitRenderer : MonoBehaviour
{
    public ObservePanelControl observePanel;
    public RawImage scopeImg;
    public RawImage crossImg;
    public Texture2D crossMat;
    public Texture2D crossMatG;
    public Color crossColor;
    public Color crossColorG;
    public Text titleText;
    public Transform panel;
    [Space]
    public Transform leftSlit;
    public Transform rightSlit;
    public Material matLeftSlit;
    public Material matRightSlit;
    public Transform transformWave;
    public Material matWave;
    [Space]
    public Transform E;
    public Transform P;
    public Transform L;
    public Transform B;
    public Transform K;
    public Transform Krot;

    float lens = 1.5f / 100f;
    float focal = 20f / 100f;
    float slitH = 1.5f / 100f;
    float scrScale = 1.5f;
    float additiveScale = 20 / 3f;
    float waveLen = 589.3e-9f;
    float sumIntensity = 4;

    void Start()
    {
        
    }
    
    void Update()
    {
        bool hasScreen = P.gameObject.activeInHierarchy;
        bool hasScope = E.gameObject.activeInHierarchy;
        
        bool hasImage = K.gameObject.activeInHierarchy && L.gameObject.activeInHierarchy && (hasScreen || hasScope);
        float additiveScale_ = hasScreen ? 1 : additiveScale;
        crossImg.texture = hasScreen ? crossMatG : crossMat;
        crossImg.color = hasScreen ? crossColorG : crossColor;
        scopeImg.gameObject.SetActive(!hasScreen);
        observePanel.canAdjustCross = !hasScreen;
        panel.gameObject.SetActive(hasScreen || hasScope);
        titleText.text = hasScreen ? "<b>观察窗口（白屏）</b>" : "<b>观察窗口（测微目镜）</b>";
        if (hasImage)
        {
            leftSlit.gameObject.SetActive(true);
            rightSlit.gameObject.SetActive(true);
            float b = 0;
            var receiver = hasScreen ? P : E;
            if (B.gameObject.activeInHierarchy)
            {
                b = Mathf.Abs(K.position.z - B.position.z) / 0.2f * 1.6f / 1000f; // m
            }
            float scale = Mathf.Abs(L.position.z - receiver.position.z) / Mathf.Abs(K.position.z - L.position.z);
            float h1 = lens * scale;
            float h2 = (Mathf.Abs(receiver.position.z - L.position.z) - focal) / focal * lens;
            float slitHeight = slitH * scale;
            float slitSep = b * scale;
            float blurRelative = Mathf.Abs(h1 - h2) / slitHeight;
            Vector3 horBias = (L.localPosition - K.localPosition).SetZ(0) * scale + L.localPosition.SetZ(0) - receiver.localPosition.SetZ(0);
            Vector3 BBias = B.localPosition;
            float leftIntensity = Mathf.Clamp(BBias.x / b + sumIntensity / 2, 0, sumIntensity);
            float rightIntensity = sumIntensity - leftIntensity;
            float bankAngle = -Krot.localEulerAngles.z;

            leftSlit.localPosition = new Vector3(-horBias.x - slitSep / 2, horBias.y, 0) * scrScale * additiveScale_;
            rightSlit.localPosition = new Vector3(-horBias.x + slitSep / 2, horBias.y, 0) * scrScale * additiveScale_;
            leftSlit.localScale = new Vector3(slitHeight, slitHeight, 1) * scrScale * additiveScale_;
            rightSlit.localScale = new Vector3(slitHeight, slitHeight, 1) * scrScale * additiveScale_;
            leftSlit.localEulerAngles = new Vector3(0, 0, bankAngle);
            rightSlit.localEulerAngles = new Vector3(0, 0, bankAngle);
            matLeftSlit.SetFloat("_Blur", blurRelative * 100);
            matLeftSlit.SetFloat("_Intensity", leftIntensity);
            matRightSlit.SetFloat("_Blur", blurRelative * 100);
            matRightSlit.SetFloat("_Intensity", rightIntensity);
        }
        else
        {
            leftSlit.gameObject.SetActive(false);
            rightSlit.gameObject.SetActive(false);
        }

        bool hasWave = (hasScreen || hasScope) && !L.gameObject.activeInHierarchy && B.gameObject.activeInHierarchy;

        if (hasWave)
        {
            var receiver = hasScreen ? P : E;
            var a = Mathf.Abs(K.position.z - B.position.z) / 0.2f * 1.6f / 1000f; // m
            var D = Mathf.Abs(receiver.position.z - K.position.z);
            var dx = D / a * waveLen;
            Vector3 horBias = receiver.localPosition.SetZ(0);
            Vector3 BBias = B.localPosition;
            float leftIntensityU = (Mathf.Clamp(BBias.x / a + sumIntensity / 2, 0, sumIntensity)) / sumIntensity;
            float rightIntensityU = 1 - leftIntensityU;
            float bankAngle = -Krot.localEulerAngles.z;


            matWave.SetFloat("_T", dx * 100);

            matWave.SetFloat("_Center", 0.3f);
            matWave.SetFloat("_Strength", Mathf.Min(leftIntensityU, rightIntensityU) * (1 - Mathf.Abs(NormalAngle(bankAngle)) / 3) * 0.3f);

            transformWave.localPosition = new Vector3(horBias.x, -horBias.y) * 10 * additiveScale_ / additiveScale;
            transformWave.localScale = new Vector3(1, 1, 1) * 0.1f * additiveScale_ / additiveScale;
            transformWave.localEulerAngles = new Vector3(0, 0, bankAngle);
        }
        else
        {
            matWave.SetFloat("_Center", 0);
            matWave.SetFloat("_Strength", 0);
        }
    }

    float NormalAngle(float angle)
    {
        while(angle > 180)
        {
            angle -= 360;
        }
        while(angle <= -180)
        {
            angle += 360;
        }
        return angle;
    }
}
