using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using gbd.Dominion.AI;
using gbd.Dominion.Model;

namespace gbd.Dominion.Tools

{
    public class ReflectionClassFinder
    {
        public static IEnumerable<TestCaseData> GetAllImplementedCardsTestData
        {
            get
            {
                var cards = GetAllImplementedCards().Select(type => (ICard)Activator.CreateInstance(type));

                // TODO: try to remove this line. Should be useless and safe...
                var castedCards = cards.Cast<ICard>();

                var testCaseData = castedCards.Select(c => new TestCaseData(c));

                return testCaseData;
            }
        }

        public static IEnumerable<Type> GetAllImplementedCards()
        {
            Type[] classes = Assembly.GetExecutingAssembly().GetTypes();

            return classes.Where(t => typeof (ICard).IsAssignableFrom(t)
                                                               && t.IsInterface == false
                                                               && t.IsAbstract == false);
        }




        public static IEnumerable<TestCaseData> GetAllAiTestCaseData()
        {
            return GetAllAi().Select(ai => new TestCaseData(ai));
        }

        public static IEnumerable<Type> GetAllAi()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass)
                .Where(t => typeof (IAi).IsAssignableFrom(t));
        }

        public static IEnumerable<TestCaseData> GetAllTestRelatedClassesTestCaseData()
        {
            return GetAllTestRelatedClasses().Select(c => new TestCaseData(c));
        }

        public static IEnumerable<Type> GetAllTestRelatedClasses()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(     t =>    t.GetCustomAttributes(typeof(TestFixtureAttribute)).Any()
                                ||  t.GetMethods().Any(m => m.GetCustomAttributes(typeof(TestAttribute)).Any())
                                || t.GetMethods().Any(m => m.GetCustomAttributes(typeof(TestCaseAttribute)).Any())
                    );
        }
    }
}