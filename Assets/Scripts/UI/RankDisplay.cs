using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankDisplay : MonoBehaviour
{
    private Text playerRankText;

    void Awake()
    {
        playerRankText = GetComponent<Text>();
    }
    public void SetStartingRank(int startRank)
    {
        playerRankText.text = "" + startRank;
    }

    public void UpdateRankDisplay(int rank)
    {
        playerRankText.text = "" + rank;
    }
}
