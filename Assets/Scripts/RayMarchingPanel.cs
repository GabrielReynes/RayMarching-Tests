﻿using System.Collections.Generic;
using System.Linq;
using MarchingObjects;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

[CreateAssetMenu]
public class RayMarchingPanel : ScriptableObject
{
	public int width, height;

	public ComputeShader rayMarchingShader;

	private ComputeBuffer m_sphereBuffer, m_cubeBuffer;

	public void Init(int _sphereCount, int _cubeCount)
	{
		m_sphereBuffer = new ComputeBuffer(_sphereCount, Sphere.SphereByteSize);
		m_cubeBuffer = new ComputeBuffer(_cubeCount, Cube.CubeByteSize);
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
	
	public void Dispatch(RenderTexture _renderTexture)
	{
		rayMarchingShader.SetBuffer(0, "spheres", m_sphereBuffer);
		rayMarchingShader.SetBuffer(0, "cubes", m_cubeBuffer);
		
		rayMarchingShader.SetTexture(0, "result", _renderTexture);
		ComputeHelper.Dispatch(rayMarchingShader, width, height);
	}

	public void Dispose()
	{
		m_sphereBuffer.Dispose();
		m_cubeBuffer.Dispose();
	}
}