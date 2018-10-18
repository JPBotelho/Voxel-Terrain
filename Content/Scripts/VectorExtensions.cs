using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
	public static void RoundUp(this Vector3 v)
	{
		v.x = Mathf.Round(v.x+.5f);
		v.y = Mathf.Round(v.y+.5f);
		v.z = Mathf.Round(v.z+.5f);
	}

	public static void RoundDown(this Vector3 v)
	{
		v.x = Mathf.Round(v.x-.5f);
		v.y = Mathf.Round(v.y-.5f);
		v.z = Mathf.Round(v.z-.5f);
	}

	public static void Round(this Vector3 v)
	{
		v.x = Mathf.Round(v.x);
		v.y = Mathf.Round(v.y);
		v.z = Mathf.Round(v.z);
	}

	public static void Floor(this Vector3 v)
	{
		v.x = Mathf.Floor(v.x);
		v.y = Mathf.Floor(v.y);
		v.z = Mathf.Floor(v.z);
	}

	//

	public static Vector3 RoundedUp(this Vector3 t)
	{
		Vector3 v = new Vector3();
		v.x = Mathf.Round(t.x+.5f);
		v.y = Mathf.Round(t.y+.5f);
		v.z = Mathf.Round(t.z+.5f);
		return v;
	}

	public static Vector3 RoundedDown(this Vector3 t)
	{
		Vector3 v = new Vector3();
		v.x = Mathf.Round(t.x-.5f);
		v.y = Mathf.Round(t.y-.5f);
		v.z = Mathf.Round(t.z-.5f);
		return v;
	}

	public static Vector3 Rounded(this Vector3 t)
	{	
		Vector3 v = new Vector3();
		v.x = Mathf.Round(t.x);
		v.y = Mathf.Round(t.y);
		v.z = Mathf.Round(t.z);
		return v;
	}

	public static Vector3 Floored(this Vector3 t)
	{	
		Vector3 v = new Vector3();
		v.x = Mathf.Floor(t.x);
		v.y = Mathf.Floor(t.y);
		v.z = Mathf.Floor(t.z);
		return v;
	}
}