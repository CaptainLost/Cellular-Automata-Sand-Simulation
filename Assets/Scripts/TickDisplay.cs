using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TickDisplay : MonoBehaviour
{
    [SerializeField] private ParticleLogic _particleLogic;

    [SerializeField] private TextMeshProUGUI _tickText;

    private void Update()
    {
        _tickText.text = _particleLogic.ParticleStorage.CurrentTick.ToString();
    }
}
