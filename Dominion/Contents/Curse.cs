﻿using org.gbd.Dominion.Model;

namespace org.gbd.Dominion.Contents
{
    public class Curse:Card
    {
        public Curse()
        {
            Mechanics.Cost = new Resources(0);
            Mechanics.Types.Add(new Model.Cards.Curse(-1));
        }
    }
}
