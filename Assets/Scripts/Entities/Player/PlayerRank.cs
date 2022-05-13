using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRank : MonoBehaviour
{
    private RankDisplay rankDisplayer;
    [HideInInspector] public int playerRank;
    public int startingRank = 1;

    private void Awake()
    {
        rankDisplayer = GameObject.Find("Rank").GetComponent<RankDisplay>();
    }

    void Start()
    {
        playerRank = startingRank;
        rankDisplayer.SetStartingRank(startingRank);
    }

    public void RankUp()
    {
        rankDisplayer.UpdateRankDisplay(++playerRank);
    }
}

