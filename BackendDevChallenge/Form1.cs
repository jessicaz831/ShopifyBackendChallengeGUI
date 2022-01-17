// Jessica Zhu
// Feature chosen: export product data to CSV

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BackendDevChallenge
{
    public partial class Form1 : Form
    {
        List<Item> inventory = new List<Item>();
        int current = 0;
        Boolean edit = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            lblName.Visible = true;
            lblQuantity.Visible = true;
            lblName.Text = "Name: ";
            lblQuantity.Text = "Quantity: ";
            txtName.Visible = true;
            txtName.Text = "";
            txtQuantity.Visible = true;
            txtQuantity.Text = "";
            btnSave.Visible = true;
            btnCreate.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnLeft.Visible = false;
            btnRight.Visible = false;
            btnList.Visible = false;
            btnCSV.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtQuantity.Text != "")
            {
                int num;
                Boolean found = false;
                if (int.TryParse(txtQuantity.Text, out num))
                {
                    // check for duplicates
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        if (inventory[i].GetName() == txtName.Text)
                        {
                            found = true;
                        }
                    }

                    if (found == false)
                    {
                        if (edit == true)
                        {
                            inventory[current].SetName(txtName.Text);
                            inventory[current].SetQuantity(num);
                            edit = false;
                        }
                        else
                        {
                            inventory.Add(new Item(txtName.Text, num));
                        }

                        lblError.Visible = false;

                        lblName.Visible = false;
                        lblQuantity.Visible = false;
                        txtName.Visible = false;
                        txtQuantity.Visible = false;
                        btnSave.Visible = false;
                        DisplayInventory();

                        btnCreate.Visible = true;
                        btnRight.Visible = true;
                        btnLeft.Visible = true;
                        btnEdit.Visible = true;
                        btnDelete.Visible = true;
                        btnList.Visible = true;
                    }
                    else
                    {
                        lblError.Text = txtName.Text + " is already in inventory";
                        lblError.Visible = true;
                    }
                }
                else
                {
                    lblError.Text = "Quantity is not an integer";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "All fields are required";
                lblError.Visible = true;
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ListInventory();
            btnList.Visible = false;
            btnCreate.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnBack.Visible = true;
            lblName.Visible = false;
            lblQuantity.Visible = false;
            btnLeft.Visible = false;
            btnRight.Visible = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            btnList.Visible = true;
            btnCreate.Visible = true;
            btnBack.Visible = false;
            lblInv.Visible = false;

            DisplayInventory();
        }

        public void DisplayInventory()
        {
            if (inventory.Count > 0)
            {
                btnEdit.Visible = true;
                btnDelete.Visible = true;
                lblName.Visible = true;
                lblQuantity.Visible = true;
                btnLeft.Visible = true;
                btnRight.Visible = true;
                btnCSV.Visible = true;

                lblName.Text = "Name: " + inventory[current].GetName();
                lblQuantity.Text = "Quantity: " + inventory[current].GetQuantity();
            }
            else
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                lblName.Visible = false;
                lblQuantity.Visible = false;
                btnLeft.Visible = false;
                btnRight.Visible = false;
                btnCSV.Visible = false;
            }
        }

        public void ListInventory()
        {
            if (inventory.Count == 0)
            {
                lblInv.Text = "No inventory";
            }
            else
            {
                lblInv.Text = "";
                for (int i = 0; i < inventory.Count; i++)
                {
                    lblInv.Text = lblInv.Text + inventory[i].PrintItem() + "\n";
                }
            }

            lblInv.Visible = true;
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (current == inventory.Count - 1)
            {
                current = 0;
            }
            else
            {
                current++;
            }

            DisplayInventory();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (current == 0)
            {
                current = inventory.Count - 1;
            }
            else
            {
                current--;
            }

            DisplayInventory();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            lblName.Visible = true;
            lblQuantity.Visible = true;
            lblName.Text = "Name: ";
            lblQuantity.Text = "Quantity: ";
            txtName.Visible = true;
            txtName.Text = inventory[current].GetName();
            txtQuantity.Visible = true;
            txtQuantity.Text = inventory[current].GetQuantity().ToString();
            btnSave.Visible = true;
            btnCreate.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            btnLeft.Visible = false;
            btnRight.Visible = false;
            btnList.Visible = false;
            btnCSV.Visible = false;

            edit = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            inventory.RemoveAt(current);

            if (current > 0)
            {
                current--;
            }

            DisplayInventory();
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            using (StreamWriter file = new StreamWriter("inventory.txt"))
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    file.WriteLine(inventory[i].GetName() + ", " + inventory[i].GetQuantity());
                }
                MessageBox.Show("CSV exported");
            }
        }
    }
}
