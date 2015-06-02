using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using org.gbd.Dominion.Model;

namespace org.gbd.Dominion.Tools

{
    public class CardsFinder
    {
        public static IEnumerable TestCases
        {
            get
            {
                var cards = GetCardInstances();

                IEnumerable<ICard> castedCards = cards.Cast<ICard>();

                IEnumerable<TestCaseData> testCaseData = castedCards.Select(c => new TestCaseData(c));

                return testCaseData;
            }
        }

        public  static IEnumerable<ICard> GetCardInstances()
        {
            Type[] classes = Assembly.GetExecutingAssembly().GetTypes();

            IEnumerable<Type> cardClasses = classes.Where(t => typeof (ICard).IsAssignableFrom(t)
                                                               && t.IsInterface == false
                                                               && t.IsAbstract == false);

            IEnumerable<ICard> cards = cardClasses.Select(type => (ICard) Activator.CreateInstance(type));
            return cards;
        }
    }
}