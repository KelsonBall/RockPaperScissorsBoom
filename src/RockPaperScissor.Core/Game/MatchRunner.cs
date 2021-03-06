﻿using System.Collections.Generic;
using System.Linq;
using RockPaperScissor.Core.Game.Results;

namespace RockPaperScissor.Core.Game
{
    public class MatchRunner
    {
        public MatchResult RunMatch(IBot player1, IBot player2)
        {
            var roundResults = new List<RoundResult>();
            var roundRunner = new RoundRunner();

            RoundResult previoResult = new RoundResult();

            for (int i = 0; i < 1000; i++)
            {
                previoResult = roundRunner.RunRound(player1, player2, previoResult);
                roundResults.Add(previoResult);
            }

            return GetMatchResultFromRoundResults(player1, player2, roundResults);
        }

        private MatchResult GetMatchResultFromRoundResults(IBot player1, IBot player2, List<RoundResult> roundResults)
        {
            var matchResult = new MatchResult();

            var winner = roundResults.GroupBy(x => x.Winner).OrderByDescending(x => x.Count()).Select(x => x.Key).First();
            matchResult.Winner = winner;
            matchResult.Loser = winner == player1 ? player2 : player1;

            matchResult.RoundResults = roundResults;

            return matchResult;
        }
    }
}