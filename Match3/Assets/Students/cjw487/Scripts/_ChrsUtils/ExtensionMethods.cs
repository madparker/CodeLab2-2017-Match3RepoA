using UnityEngine;
using System.Collections;
/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	ExtensionMethods: Convenience methods												*/
/*																						*/
/*		Functions:																		*/
/* 			public:																		*/					
/*				static Vector2 ToVector2(this Vector3 vec3)								*/
/*				static float ToDegree(this float radians)								*/
/*				static float ToRadians (this float degrees)								*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public static class ExtensionMethods
{
	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	toVector2: turns a Vector3 into a Vecotr2											*/
	/*		param: Vector3 vec3																*/
	/*		returns: the same Vector 3 without the z value in a Vector2						*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public static Vector2 ToVector2(this Vector3 vec3)
	{
		return new Vector2(vec3.x, vec3.y);
	}

    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	CreateVector3:creates a vector 3 of one float											*/
    /*		param: Vector3 vec3																*/
    /*		returns: the same Vector 3 without the z value in a Vector2						*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    public static Vector3 CreateVector3(this float f)
    {
        return new Vector3(f, f, f);
    }


    /*--------------------------------------------------------------------------------------*/
    /*																						*/
    /*	ToDegree: turns a Vector3 into a Vecotr2											*/
    /*		param: 																			*/
    /* 			float radians																*/
    /*																						*/
    /*		returns: 																		*/
    /* 			float: radians in degrees													*/
    /*																						*/
    /*--------------------------------------------------------------------------------------*/
    public static float ToDegree(this float radians)
	{
		return radians * (180.0f / Mathf.PI);
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	ToRadians: turns a Vector3 into a Vecotr2											*/
	/*		param: 																			*/
	/* 			float degrees																*/
	/*																						*/
	/*		returns: 																		*/
	/* 			float: degress in radians													*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public static float ToRadians (this float degrees)
	{
		return degrees * (Mathf.PI / 180.0f);
	}

	public static Vector2 GetPointOnCircle(float radius, float angleInDegrees, Vector3 origin)
	{
		// Convert from degrees to radians via multiplication by PI/180        
		float x = (float)(radius * Mathf.Cos(angleInDegrees * Mathf.PI / 180F)) + origin.x;
		float y = (float)(radius * Mathf.Sin(angleInDegrees * Mathf.PI / 180F)) + origin.z;

		return new Vector2(x, y);
	}

	public static Vector2 GetPointOnCircle(float radius, float angleInDegrees, Vector2 origin)
	{
		// Convert from degrees to radians via multiplication by PI/180        
		float x = (float)(radius * Mathf.Cos(angleInDegrees * Mathf.PI / 180F)) + origin.x;
		float y = (float)(radius * Mathf.Sin(angleInDegrees * Mathf.PI / 180F)) + origin.y;

		return new Vector2(x, y);
	}

	public static float Map(float value, float istart, float istop, float ostart, float ostop) 
	{
		return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
	}
}


