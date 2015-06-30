using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using gbd.Dominion.Model.Cards;
using gbd.Dominion.Model.GameMechanics.AI;
using NUnit.Framework;

namespace gbd.Dominion.Test.Utilities

{
    public class ReflectionClassFinder
    {

        public static IEnumerable<TestCaseData> GetAllInterfaces
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.IsInterface)
                    .Select(type => new TestCaseData(type))
                    .ToList();
            }
        }
        

        public static IEnumerable<TestCaseData> GetAllImplementedCardsTestData
        {
            get
            {
                var types = GetAllImplementedCards();

                var testCaseData = types.Select(c => new TestCaseData(c));

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