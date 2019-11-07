using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class SmokeController : MonoBehaviour {
    public float engineRevs;
    public float exhaustRate;
    public GameObject car;
    public CarController carController;
    private ParticleSystem exhaust;

    private void Start() {
        exhaust = GetComponent<ParticleSystem>();
        car = transform.parent.parent.gameObject;
        carController = car.GetComponent<CarController>();
        exhaustRate = 5000;
    }

    [System.Obsolete]
    private void Update() {
        engineRevs = carController.Revs;
        exhaust.emissionRate = engineRevs * exhaustRate + 10;
        var col = exhaust.colorOverLifetime;
        var grad = new Gradient();
        var colorKeys = new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(new Color(214, 189, 151), 0.079f), new GradientColorKey(Color.white, 1.0f) };
        var alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(0.0f, 0.0f), new GradientAlphaKey(20f / 255f, 0.061f), new GradientAlphaKey(0.0f, 1.0f) };
        grad.SetKeys(colorKeys, alphaKeys);
        col.color = grad;
    }
}