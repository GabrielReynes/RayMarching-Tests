using System.Linq;
using UnityEngine;

namespace MarchingObjects
{
	public abstract class Object : MonoBehaviour
	{
		//									 - LocalToWorldMatrix - Color	          - Repetitions		- Frequency
		protected const int c_ObjectByteSize = sizeof(float) * 16 + sizeof(float) * 4 + sizeof(int) * 3 + sizeof(float);
		
		public Color color;

		public Vector3Int repetitions;
		public float frequency;

		protected MarchingObject ToMarchingObject()
		{
			return new MarchingObject
			{
				WorldToLocal = transform.worldToLocalMatrix,
				Color = color,
				Repetitions = repetitions,
				Frequency = frequency
			};
		}
	}

	public struct MarchingObject
	{
		public Matrix4x4 WorldToLocal;
		public Color Color;
		public Vector3Int Repetitions;
		public float Frequency;
	}
}