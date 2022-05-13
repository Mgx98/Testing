using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRank : MonoBehaviour
{
    [SerializeField] private RankDisplay rankDisplayer;
    public int playerRank;
    public int startingRank = 1;
    void Start()
    {
        playerRank = startingRank;
        rankDisplayer.SetStartingRank(startingRank);
    }

    void Update()
    {
    }

    public void RankUp()
    {
        rankDisplayer.UpdateRankDisplay(++playerRank);
    }
}

