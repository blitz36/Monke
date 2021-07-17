using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineLights : MonoBehaviour
{
    public Light statusLight;
    public bool lightLerpDirection;
    public float lightLerpSpeed;
    public Color lightColor;
    [SerializeField] [Range(0f, 1f)] public float lightLerpTimer;
    void Start()
    {
      statusLight.color = lightColor;
      statusLight.intensity = 0;
      lightLerpDirection = true;
      //ColorUtility.TryParseHtmlString("#E40C0C", out lightColor);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
