using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluationNode
{
    public int Evaluation => _evaluation;

    private int _evaluation;

    private EvaluationNode _parentNode;

    private List<EvaluationNode> _childNodes;
}
