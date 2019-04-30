using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBehaviour : MonoBehaviour
{
    private SpriteRenderer skySprite;

    [System.Serializable]
    struct Sky_TempColor_Relation { public string name; public float temp; public Color color; }

    [SerializeField] private Sky_TempColor_Relation coldTempColor;
    [SerializeField] private Sky_TempColor_Relation normalTempColor;
    [SerializeField] private Sky_TempColor_Relation hotTempColor;

    private float localTemp;
    private Color localColor;

    // Start is called before the first frame update
    void Start()
    {
        skySprite = GetComponent<SpriteRenderer>();
        localTemp = WorldManager.instance.GetTemperature();
    }

    // Update is called once per frame
    void Update()
    {
        localTemp = WorldManager.instance.GetTemperature();
        localColor = (coldTempColor.temp / localTemp)*coldTempColor.color
                    + (normalTempColor.temp / localTemp)*normalTempColor.color
                    + (hotTempColor.temp / localTemp)*hotTempColor.color;
        skySprite.color = localColor;
    }
}
