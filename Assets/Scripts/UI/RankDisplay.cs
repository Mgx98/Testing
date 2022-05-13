using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankDisplay : MonoBehaviour
{
    [SerializeField] private Text playerRankText;
        
    public void SetStartingRank(int startRank)
    {
        playerRankText.text = "" + startRank;
    }

    public void UpdateRankDisplay(int rank)
    {
        playerRankText.text = "" + rank;
    }
}
