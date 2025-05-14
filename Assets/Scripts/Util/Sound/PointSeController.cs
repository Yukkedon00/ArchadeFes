using UnityEngine;

public class PointSeController : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public bool IsPlaying => source.isPlaying;

    public void PlayPointSe(Vector3 point, AudioClip clip)
    {
        if (!source.isPlaying)
        {
            this.transform.position = point;
            source.PlayOneShot(clip);
        }
    }
}
