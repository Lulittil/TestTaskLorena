using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lorena_Library_Salons
{
    public class Salons
    {
        public int id;
        public string name_salon;
        public double discount;
        public bool addiction;
        public string description;
        public int parent_id;


        public Salons(int id, string name_salon, double discount, string description, int parent_id)
        {
            this.id = id;
            this.name_salon = name_salon;
            this.discount = discount;
            this.description = description;
            this.parent_id = parent_id;
            if (parent_id != 0)
            {
                this.addiction = true;
            }
            else
            {
                this.addiction = false;
            }

        }


        public double SetPrice(double start_price, double parent_discount)
        {
            return (start_price - (start_price * ((this.discount + parent_discount) / 100)));
        }


    }

    public class List_Salons
    {
        public List<Salons> list = new List<Salons>();
        public List_Salons(List<Salons> list)
        {
            this.list = list;
        }

        public double GetParentDiscount(int id)
        {

            //int elem_in_list = id - 1;
            //if (list[elem_in_list].parent_id != 0)
            //{
            //    elem_in_list = list[elem_in_list].parent_id;
            //    double parent_discount = 0;
            //    while (list[elem_in_list].parent_id != 0)
            //    {
            //        parent_discount += list[elem_in_list].discount;
            //        elem_in_list = list[elem_in_list].parent_id - 1;
            //    }
            //    parent_discount += list[elem_in_list].discount;
            //    return parent_discount;
            //}
            //else return 0;

            int elem_id = id-1;
            double parent_discount = 0;
            while(list[elem_id].parent_id!=0)
            {
                elem_id = list[elem_id].parent_id-1;
                parent_discount+=list[elem_id].discount;
            }
            return parent_discount;

        }
    }
}
