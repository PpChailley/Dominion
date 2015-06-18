﻿using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics;

namespace gbd.Dominion.Contents.Cards
{
    public class Curse: AlwaysInSupplyCard
    {
        public Curse()
        {
            Mechanics.Cost = new Resources(0);
            Mechanics.Types.Add(new CurseType(-1));
        }
    }
}

