using UnityEngine;
using System.Collections;

public class AutoIntensity : MonoBehaviour {

    public GameObject stars;
    public GameObject worldProbe;

    public Gradient nightDayColor;

	public float maxIntensity = 3f;
	public float minIntensity = 0.5f;
	public float minPoint = -0.2f;

	public float maxAmbient = 1f;
	public float minAmbient = 0f;
	public float minAmbientPoint = -0.2f;

	public Gradient nightDayFogColor;
	public AnimationCurve fogDensityCurve;
	public float fogScale = 1f;

	public float dayAtmosphereThickness = 0.4f;
	public float nightAtmosphereThickness = 0.87f;

	public Vector3 dayRotateSpeed;
	public Vector3 nightRotateSpeed;

	public float skySpeed = 1;

	private Light mainLight;
    private Material skyMat;

	void Start () 
	{
		mainLight = GetComponent<Light>();
		skyMat = RenderSettings.skybox;
        stars.SetActive(false);
	}

	void Update () 
	{
        stars.transform.rotation = transform.rotation;

        Vector3 tvec = Camera.main.transform.position;
        worldProbe.transform.position = tvec;

        float tRange = 1 - minPoint;
		float dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
		float newIntensity = ((maxIntensity - minIntensity) * dot) + minIntensity;

		mainLight.intensity = newIntensity;

        if (newIntensity < 0.85 && !stars.activeSelf)
            stars.SetActive(true);

        if (newIntensity >= 0.85 && stars.activeSelf)
            stars.SetActive(false);

		tRange = 1 - minAmbientPoint;
		dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
		newIntensity = ((maxAmbient - minAmbient) * dot) + minAmbient;
		RenderSettings.ambientIntensity = newIntensity;

		mainLight.color = nightDayColor.Evaluate(dot);
		RenderSettings.ambientLight = mainLight.color;

		RenderSettings.fogColor = nightDayFogColor.Evaluate(dot);
		RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;

		newIntensity = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
		skyMat.SetFloat("_AtmosphereThickness", newIntensity);

		if (dot > 0) 
			transform.Rotate(dayRotateSpeed * Time.deltaTime * skySpeed);
		else
			transform.Rotate(nightRotateSpeed * Time.deltaTime * skySpeed);
	}
}
