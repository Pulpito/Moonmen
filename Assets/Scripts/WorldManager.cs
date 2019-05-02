using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;

    public Vector3 ColorSky;
    public SpriteRenderer SkySprite;

    public Vector3 ColorLand;
    public SpriteRenderer LandSprite;

    public Vector3 ColorPlants;
    public SpriteRenderer[] PlantsSprite;

    public Vector3 ColorMountains;
    public SpriteRenderer[] MountainsSprite;

    public Vector3 ColorClouds;
    public SpriteRenderer[] CloudsSprite;

    [SerializeField]
    private SpriteRenderer Flash;

    [SerializeField]
    private AudioClip Hot;
    [SerializeField]
    private AudioClip Cold;

    private bool canSoundHot = true;
    private bool canSoundCold = true;

    public GameObject Moonmen;
    public GameObject FireWorld;
    public GameObject IceWorld;

    // PLACEABLE OBJECTS:
    [SerializeField]
    private GameObject thunderPrefab;
    [SerializeField]
    private GameObject yetiPrefab;
    [SerializeField]
    private GameObject moonmenPrefab;
    [SerializeField]
    private GameObject virusPrefab;

    private GameObject playerSelectedPower;
    private float holdingLerpSpeed = 10f;

    // ENVIRONMENT:
    private struct Stat { public float value, min, max; public Stat(float val, float mi, float ma) { value = val; min = mi; max = ma; } }
    private Stat humidity       = new Stat(10, 0, 30);      // Default Humidity at Start.
    private Stat temperature    = new Stat(25, -10, 60);    // Default Temperature at Start.
    private Stat gravity        = new Stat(9.81f, 0, 20);   // Default Gravity at Start.
    [SerializeField] private Slider humiditySlider;
    [SerializeField] private Slider temperatureSlider;
    [SerializeField] private Slider gravitySlider;

    // MISC:
    private Vector3 tempVec;
    public Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);

    void Awake() { if (instance == null) instance = this; }

    void Start()
    {
        playerSelectedPower = null;
        // SET THE SLIDERS TO WORLD VALUES:
        humiditySlider.minValue = humidity.min;
        humiditySlider.maxValue = humidity.max;
        temperatureSlider.minValue = temperature.min;
        temperatureSlider.maxValue = temperature.max;
        gravitySlider.minValue = gravity.min;
        gravitySlider.maxValue = gravity.max;

        humiditySlider.value = humidity.value;
        temperatureSlider.value = temperature.value;
        gravitySlider.value = gravity.value;
    }

    void Update()
    {
        //Moonmen stats
        {
            if (gravity.value < 4)
            {
                for (int i = 0; i < Moonmen.gameObject.transform.childCount; i++)
                    Moonmen.gameObject.transform.GetChild(i).gameObject.GetComponent<Moonman>().isGravited = 0;
            }
            else if(gravity.value > 17)
            {
                for (int i = 0; i < Moonmen.gameObject.transform.childCount; i++)
                    Moonmen.gameObject.transform.GetChild(i).gameObject.GetComponent<Moonman>().isGravited = 2;
            }
            else
            {
                for (int i = 0; i < Moonmen.gameObject.transform.childCount; i++)
                    Moonmen.gameObject.transform.GetChild(i).gameObject.GetComponent<Moonman>().isGravited = 1;
            }

            if (humidity.value < 5 || humidity.value+1 < Moonmen.gameObject.transform.childCount)
            {
                for (int i = 0; i < Moonmen.gameObject.transform.childCount; i++)
                    Moonmen.gameObject.transform.GetChild(i).gameObject.GetComponent<Moonman>().isDrowned = true;
            }
            else
            {
                for (int i = 0; i < Moonmen.gameObject.transform.childCount; i++)
                    Moonmen.gameObject.transform.GetChild(i).gameObject.GetComponent<Moonman>().isDrowned = false;
            }

            if (temperature.value < -5)
            {
                canSoundHot = true;

                for (int i = 0; i < Moonmen.gameObject.transform.childCount; i++)
                {
                    Moonmen.gameObject.transform.GetChild(i).gameObject.GetComponent<Moonman>().isIced = true;
                    Moonmen.gameObject.transform.GetChild(i).gameObject.GetComponent<Moonman>().isFired = false;
                }
                if (canSoundCold)
                {
                    this.GetComponent<AudioSource>().PlayOneShot(Cold);
                    canSoundCold = false;
                }

                IceWorld.SetActive(true);
                FireWorld.SetActive(false);
            }
            else if (temperature.value > 55)
            {
                canSoundCold = true;

                for (int i = 0; i < Moonmen.gameObject.transform.childCount; i++)
                {
                    Moonmen.gameObject.transform.GetChild(i).gameObject.GetComponent<Moonman>().isIced = false;
                    Moonmen.gameObject.transform.GetChild(i).gameObject.GetComponent<Moonman>().isFired = true;
                }
                if (canSoundHot)
                {
                    this.GetComponent<AudioSource>().PlayOneShot(Hot);
                    canSoundHot = false;
                }

                FireWorld.SetActive(true);
                IceWorld.SetActive(false);
            }
            else
            {
                canSoundCold = true;
                canSoundHot = true;

                for (int i = 0; i < Moonmen.gameObject.transform.childCount; i++)
                {
                    Moonmen.gameObject.transform.GetChild(i).gameObject.GetComponent<Moonman>().isIced = false;
                    Moonmen.gameObject.transform.GetChild(i).gameObject.GetComponent<Moonman>().isFired = false;
                }

                FireWorld.SetActive(false);
                IceWorld.SetActive(false);
            }
        }

        if (Flash.color.a > 0)
            Flash.color -= new Color(0, 0, 0, Time.deltaTime);

        SkySprite.color = new Color(ColorSky.x + (temperature.value / 70), ColorSky.y, ColorSky.z + (humidity.value / 50) - (temperature.value / 90));
        LandSprite.color = new Color(ColorLand.x + (temperature.value / 200), ColorLand.y + (humidity.value / 200), ColorLand.z + (humidity.value / 150) - (temperature.value / 190));

        for(int i = 0; i < PlantsSprite.Length; i++)
        { 
            PlantsSprite[i].color = new Color(ColorPlants.x + (temperature.value / 100), ColorPlants.y + (humidity.value / 50), ColorPlants.z + (humidity.value / 50) - (temperature.value / 390));
            PlantsSprite[i].transform.localScale = new Vector3(humidity.value / 20, humidity.value / 20, humidity.value / 20);
        }

        for (int i = 0; i < CloudsSprite.Length; i++)
        {
            CloudsSprite[i].color = new Color(ColorClouds.x + (temperature.value / 230), ColorClouds.y, ColorClouds.z + (humidity.value / 230));
            CloudsSprite[i].transform.localScale = new Vector3(humidity.value/70, humidity.value/70, humidity.value/70);
        }

        for (int i = 0; i < MountainsSprite.Length; i++)
            MountainsSprite[i].color = new Color(ColorMountains.x + (temperature.value / 330), ColorMountains.y - (temperature.value / 330), ColorMountains.z + (humidity.value / 330));

        if (playerSelectedPower != null)
        {
            screenRect = new Rect(0, 0, Screen.width, Screen.height);
            if (screenRect.Contains(Input.mousePosition))
            {
                tempVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            playerSelectedPower.transform.position =
                    Vector3.Lerp(playerSelectedPower.transform.position, new Vector3(tempVec.x, tempVec.y, 0), Time.deltaTime*holdingLerpSpeed);
        }
    }

    // Powers:
    private void Spawn(GameObject prefab)
    {
        if (playerSelectedPower != null) return;
        playerSelectedPower = Instantiate(prefab);
        playerSelectedPower.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerSelectedPower.transform.parent = transform;
    }

    public void ChoseThunder() { Spawn(thunderPrefab); }
    public void ChoseYeti() { Spawn(yetiPrefab); }
    public void ChoseVirus() { Spawn(virusPrefab); }
    public void ChoseMMBox() { Spawn(moonmenPrefab); }

    public void StopHolding() { playerSelectedPower = null; }

    public Rect GetScreenRect() { return screenRect; }

    // Sliders Update World:
    public void UpdateHumidity() { humidity.value = humiditySlider.value; }
    public void UpdateTemperature() { temperature.value = temperatureSlider.value; }
    public void UpdateGravity() { gravity.value = gravitySlider.value; }

    // Getters:
    public float GetHumidity() { return humidity.value; }
    public float GetTemperature() { return temperature.value; }
    public float GetGravity() { return gravity.value; }
}
