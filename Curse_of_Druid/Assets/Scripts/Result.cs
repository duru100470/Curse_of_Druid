using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;

    private void Awake()
    {
        var result = Timer.Inst.CurTime;

        score.text = $"Score : {result.ToString()}";
        Timer.Inst.ResetTimer();
    }
}
