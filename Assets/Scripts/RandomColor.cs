using UnityEngine;
using UnityEngine.Events;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private UnityEvent<Color> onChanged;
    [SerializeField] private float hueMin = 0;
    [SerializeField] private float hueMax = 1;
    [SerializeField] private float saturationMin = 0.7f;
    [SerializeField] private float saturationMax = 1f;
    [SerializeField] private float valueMin = 0.7f;
    [SerializeField] private float valueMax = 1f;

    public void ColorHSV()
    {
        Color color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
        onChanged?.Invoke(color); // optional chaning (similar with JavaScript)
    }
}
