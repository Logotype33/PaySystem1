using PaySystemBL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace PaySystem1
{
    public partial class Form2 : Form
    {
        //Что бы не подключать кучу пространств имён, надо сделать что то типо сервайса, через который будет доступ к методам (наверное)
        DefaultCommands commands = new DefaultCommands();
        Balance balance = new Balance();
        Orders orders = new Orders();
        Pays pays = new Pays();
        DataTable dt;
        float orderPrice = 0;
        float balanceSumm = 0;
        private string status = null;
        List<int> OrdersItemId = new List<int>();
        List<int> ScoresId = new List<int>();
        public Form2()
        {
            InitializeComponent();
            LoadScores();
            LoadOrders();
        }
        public void LoadOrders()
        {
            var dt = orders.GetOrderItems();
            listView1.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem lvi = new ListViewItem(Convert.ToString(dr["Id"]));
                lvi.SubItems.Add(Convert.ToString(dr["Name"]));
                lvi.SubItems.Add(Convert.ToString(dr["Price"]));
                lvi.SubItems.Add(Convert.ToString(dr["Status"]));
                status = Convert.ToString(dr["Status"]);
                listView1.Items.Add(lvi);
                if (status == "payed")
                {
                    lvi.Remove();
                }
            }
        }
        public void LoadScores()
        {
            balance = new Balance();
            dt = balance.ShowUnitOfScore();
            listView2.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem lvi = new ListViewItem(Convert.ToString(dr["Id"]));
                lvi.SubItems.Add(Convert.ToString(dr["Balance_sum"]));
                listView2.Items.Add(lvi);
            }
            textBox1.Text = balance.ShowTotalBalance().ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            float num;
            bool isNum = float.TryParse(textBox3.Text, out num);
            if (isNum)
            {
                balance.CreateNewScore(num);
                LoadScores();
            }
            else
            {
                MessageBox.Show("Введите корректное значение");
            }
            
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            float num;
            if (listView1.CheckedItems.Count >0 && listView2.CheckedItems.Count > 0)
            {
                foreach (ListViewItem item in listView1.CheckedItems)
                {
                    OrdersItemId.Add(Convert.ToInt32(item.Text));
                    bool isNum = float.TryParse(item.SubItems[2].Text, out num);
                    if (isNum)
                    {
                        orderPrice += num;
                    }
                }
                foreach (ListViewItem item in listView2.CheckedItems)
                {
                    ScoresId.Add(Convert.ToInt32(item.Text));
                    bool isNum = float.TryParse(item.SubItems[1].Text, out num);
                    if (isNum)
                    {
                        balanceSumm += num;
                    }

                }
                if (balanceSumm >= orderPrice)
                {
                    MessageBox.Show("Можно оплатить");
                    pays.PayOrder(OrdersItemId, ScoresId);
                    LoadScores();
                    LoadOrders();
                }
                    
                else
                    MessageBox.Show("Не хватает денег для оплаты");

            }
            else
            {
                MessageBox.Show("Выберите товары и/или счёт для оплаты");
            }
            balanceSumm = 0;orderPrice = 0;
        }
    }
}
