using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    [Header("フェーズマネージャー")]
    PhaseManager _phaseManager;

    [SerializeField]
    [Header("全てのマス")]
    Cell[] _cells;

    [SerializeField]
    Line[] _lines;

    private void Awake()
    {
        foreach (var i in _cells)
        {
            i.Judg += Judg;
        }
    }

    private void Judg()
    {
        var isPlayer = _phaseManager.IsMyPhase;
        var state = CellState.Empty;
        var message = "";
        if (isPlayer)
        {
            state = CellState.Player;
            message = "プレイヤーの勝利";
        }
        else
        {
            state = CellState.Enemy;
            message = "プレイヤーの敗北";
        }
        foreach (var pattern in _lines)
        {
            if(pattern.Cells.All(x => x.State == state))
            {
                Debug.Log(message);
                return;
            }
        }

        if (isPlayer)
        {
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        if (_cells.All(x => x.State != CellState.Empty))
        {
            Debug.Log("ゲーム終了");
            yield break;
        }
        _cells[AI()].Mark();
    }


    private int AI()
    {
        var player = CellState.Player;
        var enemy = CellState.Enemy;
        var lines = new List<Line>();
        //勝ち確定なら
        foreach (var line in _lines)
        {
            var pattern =
                line.Cells.All(c => c.State != player) &&
                line.Cells.Any(c => c.State == enemy);
            if (pattern)
            {
                var where = line.Cells.Where(c => c.State == enemy).ToArray();
                if (where.Length == 2)
                {
                    Debug.Log("終了");
                    for (int i = 0; i < line.Cells.Length; i++)
                    {
                        if (line.Cells[i].State == CellState.Empty)
                        {
                            for (int index = 0; index < _cells.Length; index++)
                            {
                                if (_cells[index] == line.Cells[i])
                                {
                                    return index;
                                }
                            }
                        }
                    }
                }
            }
        }
        //プレイヤーを勝たせない
        foreach (var line in _lines)
        {
            var pattern =
                line
                    .Cells
                    .Where(c => c.State == player)
                    .ToArray();
            if (pattern.Length == 2)
            {
                Debug.Log("阻止");
                for (int i = 0; i < line.Cells.Length; i++)
                {
                    if (line.Cells[i].State == CellState.Empty)
                    {
                        for (int index = 0; index < _cells.Length; index++)
                        {
                            if (_cells[index] == line.Cells[i])
                            {
                                return index;
                            }
                        }
                    }
                }
            }
        }
        //エネミー自身が勝つ
        foreach (var line in _lines)
        {
            var pattern =
                line.Cells.All(c => c.State != player) &&
                line.Cells.Any(c => c.State == enemy);
            if(pattern)
            {
                var where = line.Cells.Where(c => c.State == enemy).ToArray();
                if (where.Length == 2)
                {
                    Debug.Log("終了");
                    for (int i = 0; i < line.Cells.Length; i++)
                    {
                        if (line.Cells[i].State == CellState.Empty)
                        {
                            for (int index = 0; index < _cells.Length; index++)
                            {
                                if (_cells[index] == line.Cells[i])
                                {
                                    return index;
                                }
                            }
                        }
                    }
                }
                lines.Add(line);
            }
        }
        if (lines.Count != 0)
        {
            Debug.Log("AI");
            var newline = lines[Random.Range(0, lines.Count)];
            for (int i = 0; i < newline.Cells.Length; i++)
            {
                if (newline.Cells[i].State == CellState.Empty)
                {
                    for (int index = 0; index < _cells.Length; index++)
                    {
                        if (_cells[index] == newline.Cells[i])
                        {
                            return index;
                        }
                    }
                }
            }
        }
        //適当に動く
        return RandomRange();
    }

    private int RandomRange()
    {
        Debug.Log("Random");
        var random = Random.Range(0, _cells.Length);
        if (_cells[random].State != CellState.Empty)
        {
            return RandomRange();
        }
        return random;
    }
}

[System.Serializable]
public class Line
{
    public Cell[] Cells => _cells;

    [SerializeField]
    private Cell[] _cells;
}
