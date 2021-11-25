using System.Linq;
using UnityEngine;

namespace MarchingObjects
{
	public abstract class Object : MonoBehaviour
	{
		//									 - LocalToWorldMatrix - Color			  - Reflectivity  - Repetitions		- Frequency
		protected const int c_ObjectByteSize = sizeof(float) * 16 + sizeof(float) * 4 + sizeof(float) + sizeof(int) * 3 + sizeof(float);
		
		public Color color;
		[Range(0f,1f)] public float reflectivity;
		public Vector3Int repetitions;
		public float frequency;

		protected MarchingObject ToMarchingObject()
		{
			return new MarchingObject
			{
				WorldToLocal = transform.worldToLocalMatrix,
				Color = color,
				Reflectivity = reflectivity,
				Repetitions = repetitions,
				Frequency = frequency
			};
		}
	}

	public struct MarchingObject
	{
		public Matrix4x4 WorldToLocal;
		public Color Color;
		public float Reflectivity;
		public Vector3Int Repetitions;
		public float Frequency;
	}
}