using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gbd.Dominion.Injection;
using gbd.Dominion.Model.GameMechanics;
using gbd.Dominion.Model.GameMechanics.AI;
using gbd.Dominion.Test.Utilities;
using gbd.Tools.NInject;
using Ninject;
using NLog;
using NUnit.Framework;

namespace gbd.Dominion.Test.Scenarios
{
    [TestFixture]
    class AiFightTests: BaseTest
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        [SetUp]
        public new void SetUp()
        {
            IoC.Kernel = new StandardKernel(    new IoCCardsModule(),
                                                new IoCMechanicsModule(),
                                                new IoCStandardGameModule());
        }

        [Test]
        public void Random1On1()
        {
            Log.Warn("New test - Random 1 on 1");
            Log.Warn("");
            Log.Warn("");


            IoC.Kernel.Unbind<IIntelligence>();
            IoC.Kernel.Unbind<IPlayer>();
            IoC.Kernel.Bind<IIntelligence>().To<RandomAi>();
            IoC.Kernel.BindTo<IPlayer, Player>(2);
            
            var game = IoC.Kernel.Get<IGame>();
            game.Ready();

            Log.Warn("Game is ready - start playing");

            while (game.SupplyZone.EndOfGameCondition == false)
            {
                game.CurrentPlayer.I.PlayTurn();
                game.NextTurn();
            }
            
        }

    }
}
