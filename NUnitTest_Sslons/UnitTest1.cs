using NUnit.Framework;
using Lorena_Library_Salons;
using System.Collections.Generic;

namespace Nunit_Test_Class_Salons
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Get_discount()
        {

            //arrange
            List<Salons> Salone_list = new List<Salons>();  //—писок объектов класса салоны
            for (int i = 1; i < 6; i++)
            {
                Salons t = new Salons(i, "awdw", 4, "rrsfsgf", 0);  //—оздание экземпл€ров класса Salons без родительских веток 
                Salone_list.Add(t);
            }
            Salons ts = new Salons(6, "awffdawddw", 5, "rrgsdgsfsgf", 1); // Ёкземпл€р класса, у которого родитель - салон с id 1 
            Salone_list.Add(ts);
            Salons td = new Salons(7, "dgtrd", 2, "htghf", 6);  //Ёкземпл€р класса с родителем, у которого id 6 (ветка выше)
            Salone_list.Add(td);
            double expected;
            double actual = 11;
            List_Salons sal_list = new List_Salons(Salone_list);

            //act
            expected = sal_list.GetParentDiscount(7)+td.discount;

            //assert
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void Test_SetPrice()
        {
            //arrange
            //arrange
            List<Salons> Salone_list = new List<Salons>();  //—писок объектов класса салоны
            for (int i = 1; i < 6; i++)
            {
                Salons t = new Salons(i, "awdw", 4, "rrsfsgf", 0);  //—оздание экземпл€ров класса Salons без родительских веток 
                Salone_list.Add(t);
            }
            Salons ts = new Salons(6, "awffdawddw", 5, "rrgsdgsfsgf", 1); // Ёкземпл€р класса, у которого родитель - салон с id 1 
            Salone_list.Add(ts);
            Salons td = new Salons(7, "dgtrd", 2, "htghf", 6);  //Ёкземпл€р класса с родителем, у которого id 6 (ветка выше)
            Salone_list.Add(td);
            
            double expected;
            double actual = 121520.6;
            List_Salons sal_list = new List_Salons(Salone_list);
            double parent_disconts=sal_list.GetParentDiscount(7);
            //set
            //expected = td.SetPrice(136540, parent_disconts);
            expected = td.SetPrice(136540, 11);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}