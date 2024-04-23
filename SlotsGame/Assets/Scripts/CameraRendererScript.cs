using UnityEngine;

public class CameraRendererScript : MonoBehaviour
{
	public Vector2 defaultResolution = new Vector2(1080, 1920);
	[Range(0f, 1f)] public float widthOrHeight = 0;

	Camera componentCamera;

	float initialSize;
	float targetAspect;

	float initialFov;
	float horizontalFov = 120f;

	float numOfCum;
	bool isReady;
	void Start()
	{
		componentCamera = GetComponent<Camera>();
		initialSize = componentCamera.orthographicSize;

		targetAspect = defaultResolution.x / defaultResolution.y;

		initialFov = componentCamera.fieldOfView;
		horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);
	}

	void Update()
	{
		if (componentCamera.orthographic)
		{
			float constantWidthSize = initialSize * (targetAspect / componentCamera.aspect);
			componentCamera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, widthOrHeight);
		}
		else
		{
			float constantWidthFov = CalcVerticalFov(horizontalFov, componentCamera.aspect);
			componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, widthOrHeight);
		}
	}

	float CalcVerticalFov(float hFovInDeg, float aspectRatio)
	{
		float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

		float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

		return vFovInRads * Mathf.Rad2Deg;
	}
}