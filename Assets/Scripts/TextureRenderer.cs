using MarchingObjects;
using UnityEngine;

public class TextureRenderer : MonoBehaviour
{
	private Camera m_camera;
	private RenderTexture m_renderTexture;

	public new GameObject light;
	public Sphere[] spheres;
	public Cube[] cubes;
	public RayMarchingPanel panel;

	private void Awake()
	{
		m_camera = GetComponent<Camera>();
		m_renderTexture = new RenderTexture(panel.width, panel.height, 4) {enableRandomWrite = true};
		
		panel.Init(spheres.Length, cubes.Length);
	}

	public void FixedUpdate()
	{
		panel.SetShaderParameters(m_camera.cameraToWorldMatrix, m_camera.projectionMatrix.inverse);
		panel.UpdateSphereBuffer(spheres);
		panel.UpdateCubeBuffer(cubes);
		panel.UpdateParameters(light.transform.forward);
		
		panel.Dispatch(m_renderTexture);
	}

	public void OnRenderImage(RenderTexture _src, RenderTexture _dest)
	{
		Graphics.Blit(m_renderTexture, _dest);
	}
	
	public void OnDestroy()
	{
		panel.Dispose();
	}
}