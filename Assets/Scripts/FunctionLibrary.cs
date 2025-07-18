using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary {

    //math functions
    public static Vector3 Wave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;
        return p;
    }

    public static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 p; 
        p.x = u;
        p.y = Sin(PI * (u + 0.5f * t));
        p.y += 0.5f * Sin(2f * PI *(v + t));
        p.y += Sin(PI * (u + v + 0.25f * t));
        p.y *= (1f/2.5f);
        p.z = v;
        return p;
    }

    public static Vector3 Ripple(float u, float v, float t)
    {
        float d = Sqrt(u * u + v * v);
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (4f * d - t))/ (1f +10f * d);
        p.z = v;
        return p;
    }

    public static Vector3 Sphere(float u, float v, float t)
    {
        Vector3 p;
        float r = Cos(0.5f * PI * v);
        p.x = r * Sin(PI * u);
        p.y = Sin(PI * 0.5f * v);
        p.z = r * Cos(PI * u); 
        return p;
    }

    public static Vector3 DistortedSphere(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        float s = r * Cos(0.5f * PI * v);
        p.x = s * Sin(PI * u);
        p.y = Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    public static Vector3 Torus (float u, float v, float t)
    {
        float r1 = 0.75f;
        float r2 = 0.25f;
        float s = r1 + r2 * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    public static Vector3 DistortedTorus(float u, float v, float t)
    {
        float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
        float s = r1 + r2 * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }

    public static Vector3 Morph (
        float u, float v, float t, Function from, Function to, float progress)
    {
        return Vector3.LerpUnclamped(from(u,v,t), to(u,v,t), SmoothStep(0f, 1f, progress));
    }


    //functions delegate, array, and  getter math functions
    public delegate Vector3 Function(float u, float v, float t);
    public enum FunctionName {Wave, MultiWave, Ripple, Sphere, DistortedSphere, Torus , DistortedTorus }
    static Function[] functions = {Wave, MultiWave, Ripple, Sphere, DistortedSphere, Torus, DistortedTorus };

    public static int FunctionCount => functions.Length;

    public static Function GetFunction(FunctionName name) => functions[(int)name];

    public static FunctionName GetNextFunctionName(FunctionName name) => (int)name < functions.Length - 1 ? name + 1 : 0;
    

    public static FunctionName GetRandomFunctionOther (FunctionName name) {
        var choice = (FunctionName)Random.Range(1, functions.Length);
        return choice == name ? 0 : choice;
    }
}
