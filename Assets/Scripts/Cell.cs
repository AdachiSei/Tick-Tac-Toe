using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public CellState State => _state;

    [SerializeField]
    [Header("フェーズマネージャー")]
    PhaseManager _phaseManager;

    [SerializeField]
    [Header("")]
    CellState _state;

    public event Action Judg;

    private SpriteRenderer _spriteRenderer;
    private bool _isMarking = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if(_phaseManager.IsMyPhase) Mark();       
    }

    public void Mark()
    {
        if (_isMarking) return;
        if (_phaseManager.IsCircle)
        {
            _spriteRenderer.sprite = _phaseManager.Circle;
        }
        else
        {
            _spriteRenderer.sprite = _phaseManager.Cross;
        }
        if (_phaseManager.IsMyPhase)
        {
            _state = CellState.Player;
            Judg();
        }
        else
        {
            _state = CellState.Enemy;
            Judg();
        }
        _isMarking = true;
        _phaseManager.ChangePhaseAndMark();
    }
}
