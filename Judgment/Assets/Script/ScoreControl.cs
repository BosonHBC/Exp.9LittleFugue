using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreControl : MonoBehaviour
{
    public static ScoreControl instance;
    public float score;
    public int sinCount;
    public int innoCount;

    [SerializeField] Text tx_score;
    [SerializeField] Text tx_sin;
    [SerializeField] Text tx_inno;


    private void Awake()
    {
        instance = this;
    }

    public void ChageScore(float _delta)
    {
        score += _delta;
        tx_score.text = ((int)(score)).ToString();

        if (_delta > 0)
            sinCount++;
        else if (_delta < 0)
            innoCount++;

        tx_sin.text = sinCount.ToString();
        tx_inno.text = innoCount.ToString();
    }
}
