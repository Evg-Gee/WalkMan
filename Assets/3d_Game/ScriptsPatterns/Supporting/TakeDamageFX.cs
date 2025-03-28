using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Threading.Tasks;

public class TakeDamageFX : MonoBehaviour
{
    public float intensity = 0;
    PostProcessVolume _volume;
    Vignette _vignette;

    void Start()
    {
        _volume = GetComponent<PostProcessVolume>();
        _volume.profile.TryGetSettings<Vignette>(out _vignette);
        if (!_vignette)
        {
            Debug.Log("erroor, vignette empty");
        }
        else
        {
            _vignette.enabled.Override(false);
        }
    }

    public IEnumerator TakeDamageFXVignette()
    {
        intensity = 0.4f;

        _vignette.enabled.Override(true);
        _vignette.intensity.Override(0.4f);

        yield return new WaitForSeconds(0.4f);

        while(intensity > 0)
        {
            intensity -= 0.01f;
            if (intensity < 0)
            {
                intensity = 0;
            }
            _vignette.intensity.Override(intensity);

            yield return new WaitForSeconds(0.1f);
        }

        _vignette.enabled.Override(false);
        yield break;
    }
    public async Task TakeDamageFXVignetteTask()
    {
        intensity = 0.4f;

        _vignette.enabled.Override(true);
        _vignette.intensity.Override(0.4f);

        await Task.Delay(400); // 0.4 секунды

        while (intensity > 0)
        {
            intensity -= 0.01f;
            if (intensity < 0)
            {
                intensity = 0;
            }
            _vignette.intensity.Override(intensity);

            await Task.Delay(100); // 0.1 секунды
        }

        _vignette.enabled.Override(false);
    }
}