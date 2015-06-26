using System;

namespace gbd.Dominion.Model.GameMechanics
{
    public class InsufficientResourcesException : Exception
    {
        public InsufficientResourcesException(string s): base(s) { }
    }
}