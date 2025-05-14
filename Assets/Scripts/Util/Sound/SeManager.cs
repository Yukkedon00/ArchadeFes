using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] seClips;
    [SerializeField] private PointSeController[] pointController;

    public static SeManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayPositionSe(Vector3 point, int clipIndex)
    {
        var source = pointController.FirstOrDefault(x => !x.IsPlaying);

        if (source != default)
        {
            source.PlayPointSe(point, seClips[clipIndex]);
        }
    }
}
