﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using org.gbd.Dominion.Model;
using org.gbd.Dominion.Tools;

namespace org.gbd.Dominion.Contents
{
    [TestFixture]
    public class ContentPresenceTests: BaseTest
    {

        [Test]
        public void BaseExtensionIsComplete()
        {
            IEnumerable<object> inSelectedExtension = CardsFinder.GetCardInstances().Where(c => c.Extension == GameExtension.BaseGame);

            Assert.That(inSelectedExtension.Count(), Is.EqualTo(25));
        }

        [Test]
        public void SeasideExtensionIsComplete()
        {
            IEnumerable<object> inSelectedExtension = CardsFinder.GetCardInstances().Where(c => c.Extension == GameExtension.Seaside);

            Assert.That(inSelectedExtension.Count(), Is.EqualTo(26));
        }

    }

    public enum GameExtension
    {
        AlwaysPresent,
        BaseGame,
        Seaside,
        
    }
}