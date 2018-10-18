using System.Collections.Generic;
using UnityEngine;

namespace MarchingCubesProject
{
	public class CameraRay : MonoBehaviour
	{
		public GameObject temp;
		private MarchingCubesProject.Terrain example;
		private void Start()
		{
			example = FindObjectOfType<Terrain>();
		}
		private void Update()
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				Ray ray = new Ray(transform.position, transform.forward);
				RaycastHit hit;

				if(Physics.Raycast(ray, out hit))
				{
					Vector3 pos = hit.point;
					pos.x = Mathf.Round(pos.x);
					pos.y = Mathf.Round(pos.y);
					pos.z = Mathf.Round(pos.z);

					if(example.ValidVoxel(pos.x, pos.y, pos.z))
					{
						pos.x = Mathf.Round(pos.x);
						pos.y = Mathf.Round(pos.y);
						pos.z = Mathf.Round(pos.z);

						example.SetAt(pos, 0);
						example.SetAt(pos + new Vector3(1, 0, 0), 1);
						example.SetAt(pos + new Vector3(0, 0, 1), 1);
						example.SetAt(pos + new Vector3(1, 0, 1), 1);
						
						example.SetAt(pos + new Vector3(0, 1, 0), 1);
						example.SetAt(pos + new Vector3(1, 1, 0), 1);
						example.SetAt(pos + new Vector3(0, 1, 1), 1);
						example.SetAt(pos + new Vector3(1, 1, 1), 1);
						example.GenerateMeshes();
						example.GenerateMeshes();
					}
				}
			}
			else if(Input.GetKeyDown(KeyCode.Mouse1))
			{
				Ray ray = new Ray(transform.position, transform.forward);
				RaycastHit hit;

				if(Physics.Raycast(ray, out hit))
				{
					Vector3 pos = hit.point;
					pos.x = Mathf.Round(pos.x);
					pos.y = Mathf.Round(pos.y);
					pos.z = Mathf.Round(pos.z);

					example.SetAt(pos, 0);
					example.SetAt(pos + new Vector3(1, 0, 0), 0);
					example.SetAt(pos + new Vector3(0, 0, 1), 0);
					example.SetAt(pos + new Vector3(1, 0, 1), 0);
					
					example.SetAt(pos + new Vector3(0, 1, 0), 0);
					example.SetAt(pos + new Vector3(1, 1, 0), 0);
					example.SetAt(pos + new Vector3(0, 1, 1), 0);
					example.SetAt(pos + new Vector3(1, 1, 1), 0);
					example.GenerateMeshes();
					
				}
			}
		}

		private Vector3 EvaluateAxis(Vector3 point, Vector3 position)
		{
			Vector3 direction = point - position;
			direction.Normalize();

			float max = Mathf.Max(direction.x, direction.y);
			max = Mathf.Max(max, direction.z);

			if(max == direction.x)
				return new Vector3(1, 0, 0);
			else if(max == direction.y)
				return new Vector3(0, 1, 0);
			else
				return new Vector3(0, 0, 1);
		}
	}
}