using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSimulationAtTick : MonoBehaviour
{
    [SerializeField] private ParticleLogic _particleLogic;
    [SerializeField] private PngSaver _pngSaver;

    [SerializeField] private int _step;
    [SerializeField] private List<int> _specialStopTicks;

    [SerializeField] private bool _saveImage;

    private void OnEnable()
    {
        _particleLogic.OnTick += OnTick;
    }

    private void OnDisable()
    {
        _particleLogic.OnTick -= OnTick;
    }

    private void OnTick(int currentTick)
    {
        if (currentTick % _step == 0 || _specialStopTicks.Contains(currentTick))
        {
            _particleLogic.IsSimulationRunning = false;

            if (_saveImage)
            {
                _pngSaver.SaveToPng(currentTick.ToString());
            }
        }
    }
}
