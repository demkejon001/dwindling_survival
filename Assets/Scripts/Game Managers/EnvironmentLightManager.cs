using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnvironmentLightManager : MonoBehaviour
{
    [SerializeField]
    private Light2D sunLight;
    private GameManager gameManager;
    [SerializeField]
    private float maxIntesity = 1f;
    [SerializeField]
    private float minIntesity = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        sunLight.intensity = Mathf.Min(maxIntesity * Mathf.Pow(gameManager.GameCycle(),5) + minIntesity, maxIntesity);
    }
}

