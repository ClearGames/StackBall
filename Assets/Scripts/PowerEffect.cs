using UnityEngine;

public class PowerEffect : MonoBehaviour
{
    public void ChangeColor(Color color)
    {
        ParticleSystem.MainModule module = GetComponent<ParticleSystem>().main;
        
        Gradient gradiant = new Gradient();
        gradiant.SetKeys(
            new GradientColorKey[]
            {
                new GradientColorKey(Color.white, 0),
                new GradientColorKey(color, 0.5f),
                new GradientColorKey(Color.black, 1),
            },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1, 0),
                new GradientAlphaKey(1, 0.5f),
                new GradientAlphaKey(1, 1),
            }
        );

        module.startColor = gradiant;
    }
}
