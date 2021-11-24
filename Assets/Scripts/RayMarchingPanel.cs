using System.Linq;
using MarchingObjects;
using UnityEngine;

[CreateAssetMenu]
public class RayMarchingPanel : ScriptableObject
{
	public int width, height;
	public Color lightColor, backgroundColor;
	
	[Range(50f, 300f)] public float distanceMax, shadowDistance, reflectionDistance;
	[Range(0,10)] public int blurFactor, levelOfDetail;

	public ComputeShader rayMarchingShader;

	private ComputeBuffer m_sphereBuffer, m_cubeBuffer;

	public void Init(int _sphereCount, int _cubeCount)
	{
		m_sphereBuffer = new ComputeBuffer(_sphereCount, Sphere.c_SphereByteSize);
		m_cubeBuffer = new ComputeBuffer(_cubeCount, Cube.c_CubeByteSize);
		rayMarchingShader.SetInt("sphere_count", _sphereCount);
		rayMarchingShader.SetInt("cube_count", _cubeCount);
	}

	public void SetShaderParameters(Matrix4x4 _cameraToWorldMatrix, Matrix4x4 _inverseProjectionMatrix)
	{
		rayMarchingShader.SetMatrix("camera_to_world", _cameraToWorldMatrix);
		rayMarchingShader.SetMatrix("camera_inverse_projection", _inverseProjectionMatrix);
	}

	public void UpdateSphereBuffer(Sphere[] _spheres)
	{
		m_sphereBuffer.SetData(_spheres.Select(_s => _s.ToMarchingSphere()).ToArray());
	}
	
	public void UpdateCubeBuffer(Cube[] _cubes)
	{
		m_cubeBuffer.SetData(_cubes.Select(_s => _s.ToMarchingCube()).ToArray());
	}
	

	public void UpdateParameters(Vector3 _lightDir)
	{
		rayMarchingShader.SetFloats("light_dir", Vector3ToArray(_lightDir));
		
		rayMarchingShader.SetFloats("light_col", ColorToArray(lightColor));
		rayMarchingShader.SetFloats("background_col", ColorToArray(backgroundColor));
		
		rayMarchingShader.SetFloat("dist_max", distanceMax);
		rayMarchingShader.SetFloat("shadow_dist", shadowDistance);
		rayMarchingShader.SetFloat("refl_dist", reflectionDistance);
		rayMarchingShader.SetFloat("blur_factor", 10/(blurFactor%2+1) * Mathf.Pow(10, -blurFactor));
		rayMarchingShader.SetFloat("lod", 5*Mathf.Pow(10, -levelOfDetail));
	}
	
	public void Dispatch(RenderTexture _renderTexture)
	{
		rayMarchingShader.SetBuffer(0, "spheres", m_sphereBuffer);
		rayMarchingShader.SetBuffer(0, "cubes", m_cubeBuffer);
		
		rayMarchingShader.SetTexture(0, "result", _renderTexture);
		ComputeHelper.Dispatch(rayMarchingShader, width, height);
	}

	private float[] Vector3ToArray(Vector3 _v)
	{
		return new []{ _v.x, _v.y, _v.z };
	}
	
	private float[] ColorToArray(Color _c)
	{
		return new []{ _c.r, _c.g, _c.b, _c.a};
	}

	public void Dispose()
	{
		m_sphereBuffer.Dispose();
		m_cubeBuffer.Dispose();
	}
}