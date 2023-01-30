using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public Sprite Circle => _circle;
    public Sprite Cross => _cross;
    public bool IsMyPhase => _isMyPhase;
    public bool IsCircle => _isCircle;

    [SerializeField]
    [Header("マル")]
    private Sprite _circle;

    [SerializeField]
    [Header("バツ")]
    private Sprite _cross;

    bool _isMyPhase;
    bool _isCircle;

    const int OFFSET = 1;

    private void Awake()
    {
        if(Random.Range(0, OFFSET) == 0)
        {
            _isMyPhase = true;
            Debug.Log("自分のターン");
        }
        else
        {
            _isMyPhase = false;
            Debug.Log("相手のターン");
        }
        if (Random.Range(0, OFFSET) == 0)
        {
            _isCircle = true;
        }
    }

    public void ChangePhaseAndMark()
    {
        if (_isMyPhase)
        {
            _isMyPhase = false;
            Debug.Log("相手のターン");
        }
        else
        {
            _isMyPhase = true;
            Debug.Log("自分のターン");
        }
        if (_isCircle) _isCircle = false;
        else _isCircle = true;
    }
}
