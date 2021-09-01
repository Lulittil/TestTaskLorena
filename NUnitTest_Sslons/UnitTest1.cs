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
            List<Salons> Salone_list = new List<Salons>();  //������ �������� ������ ������
            for (int i = 1; i < 6; i++)
            {
                Salons t = new Salons(i, "awdw", i + 2, "rrsfsgf", 0);  //�������� ����������� ������ Salons ��� ������������ ����� 
                Salone_list.Add(t);
            }
            Salons ts = new Salons(6, "awffdawddw", 23, "rrgsdgsfsgf", 1); // ��������� ������, � �������� �������� - ����� � id 1 
            Salone_list.Add(ts);
            Salons td = new Salons(7, "dgtrd", 5, "htghf", 6);  //��������� ������ � ���������, � �������� id 6 (����� ����)
            Salone_list.Add(td);
            double expected;
            double actual = 31;
            List_Salons sal_list = new List_Salons(Salone_list);

            //act
            expected = sal_list.GetParentDiscount(7);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}