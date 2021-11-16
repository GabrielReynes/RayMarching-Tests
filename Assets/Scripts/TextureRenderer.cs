using MarchingObjects;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class TextureRenderer : MonoBehaviour
{
	private Camera m_camera;
	private RenderTexture m_renderTexture;

	public Sphere[] spheres;
	public Cube[] cubes;
	public RayMarchingPanel panel;

	private void Awake()
	{
		m_camera = GetComponent<Camera>();
		m_renderTexture = new RenderTexture(panel.width, panel.height, 4) {enableRandomWrite = true};
		
		panel.Init(spheres.Length, cubes.Length);
	}

	public void Update()
	{
		panel.SetShaderParameters(m_camera.cameraToWorldMatrix, m_camera.projectionMatrix.inverse);
		panel.UpdateSphereBuffer(spheres);
		panel.UpdateCubeBuffer(cubes);
	}

	public void OnRenderImage(RenderTexture _src, RenderTexture _dest)
	{
		panel.Dispatch(m_renderTexture);
		Graphics.Blit(m_renderTexture, _dest);
	}
	
	public void OnDestroy()
	{
		panel.Dispose();
	}
}