using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDevChallenge
{
    class Item
    {
        private string name;
        private int quantity;

        public Item(string name, int quantity)
        {
            this.name = name;
            this.quantity = quantity;
        }

        public string GetName()
        {
            return name;
        }

        public int GetQuantity()
        {
            return quantity;
        }

        public void SetName(String name)
        {
            this.name = name;
        }

        public void SetQuantity(int quantity)
        {
            this.quantity = quantity;
        }

        public string PrintItem()
        {
            return "Name: " + name + "     Quantity: " + quantity;
        }
    }
}
