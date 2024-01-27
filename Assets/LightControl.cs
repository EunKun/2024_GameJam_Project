using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Light light;
    float _strength = 0.5f;
    float _range = 0.8f;
    float _time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        InvokeRepeating("SetLighting", 5f, 1f);
    }

    private void Update()
    {
        light.intensity = Mathf.Lerp(light.intensity, _strength, _time);
        light.range = Mathf.Lerp(light.intensity, _range, _time);
        _time += Time.deltaTime * 3;
    }

    void SetLighting()
    {
        _strength = Random.Range(0.5f, 1f);
        _range = Random.Range(0.8f, 1f);
        _time = 0;
    }
}
