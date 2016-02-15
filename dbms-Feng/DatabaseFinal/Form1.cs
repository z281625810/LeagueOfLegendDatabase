/*
Author: Feng Sun, Erick Muller
Class: 300 <Section 02>
Assignment: Final project
Date Assigned: 4/6/2015
Due Date: 4/26/2015
Description: a program that can search data from the local lol database
Certification of Authenticity:
I certify that this assignment is entirely my own work.
*/

using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System;

namespace DatabaseFinal
{
    public partial class Form1 : Form
    {
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter da;
        private DataTable dt;
        private int number;

        public Form1()
        {
            InitializeComponent();
            initialAll();
            connectDB();
        }

        //initialize everything here
        public void initialAll() 
        {
            //add options
            comboBox1.Items.Add("Champion Basic Information by Champion name");
            comboBox1.Items.Add("Skill by Champion name");
            comboBox1.Items.Add("Champion by type");
            comboBox1.Items.Add("Highest Damage of a skill ");
            comboBox1.Items.Add("Equipment by Equip name");
            comboBox1.Items.Add("All Equipments");
            comboBox1.Items.Add("All Equipments Types");
            comboBox1.Items.Add("Equipment by type");
            comboBox1.Items.Add("All Champion Type");
            comboBox1.Items.Add("All Champions");

            comboBox1.SelectedIndex = 0;

            dgv.Visible = false;
        }

        //connect to DB
        public void connectDB() 
        {
            connection = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\lol.mdf;Integrated Security=True");
            connection.Open();  
        }

        public void getTable() 
        {
            dgv.Visible = true;
            string cmd = "";


            //add queries here
            switch (number)
            {
                case 0:
                    cmd = "select ChampionBasic.chName, chGender, chHealth, chAttack, chRange,chDescription from ChampionBasic Left Join ChampionStats on ChampionBasic.chName = ChampionStats.chName where ChampionBasic.chName = '" + textBox1.Text + "'";
                    break;
                case 1:
                    cmd = "select HaveSkillOf.chName, Skills.skName, HaveSkillOf.skillNum,Skills.skType,Skills.skDamage from HaveSkillOf, Skills where HaveSkillOf.skName =  Skills.skName and HaveSkillOf.chName = '" + textBox1.Text + "'";
                    break;
                case 2:
                    cmd = "select ChampTypeOf.chName, ChampType.tName from ChampType, ChampTypeOf where ChampType.tID = ChampTypeOf.tID and ChampType.tName = '" + textBox1.Text + "'";
                    break;
                case 3:
                    cmd = "select Skills.skName, Skills.skType, Skills.skDamage from (select max(skDamage) as highestDamage from Skills)S1, Skills where S1.highestDamage = Skills.skDamage";
                    break;
                case 4:
                    cmd = "select * from Equipment where eqName = '" + textBox1.Text + "'";
                    break;
                case 5:
                    cmd = "select * from Equipment";
                    break;
                case 6:
                    cmd = "select eName from EquipType";
                    break;
                case 7:
                    cmd = "select R1.eName, EquipTypeOf.eqName from (select * from EquipType where eName = '" + textBox1.Text + "')R1, EquipTypeOf where R1.eqID = EquipTypeOf.eqID;";
                    break;
                case 8:
                    cmd = "select tName from ChampType";
                    break;
                case 9:
                    cmd = "select ChampionBasic.chName, chGender, chHealth, chAttack, chRange,chDescription from ChampionBasic Left Join ChampionStats on ChampionBasic.chName = ChampionStats.chName;";
                    break;
                default:
                    break;
            }

            command = new SqlCommand(cmd, connection);
            da = new SqlDataAdapter(command);
            dt = new DataTable();
            da.Fill(dt);
            dgv.DataSource = dt;  
        }

        //search button event
        private void button1_Click(object sender, EventArgs e)
        {
            getTable();
            if (dt.Rows.Count != 0)
            {
                MessageBox.Show(dt.Rows.Count.ToString() + " results!", "result");
            }
            else 
            {
                MessageBox.Show(textBox1.Text + " is not in the database","result");
            }
        }

        //exit button event
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "exit info", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        //change the prompts on textbox according to the different option of combobox
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            number = comboBox1.SelectedIndex;

            switch (number) 
            {
                case 0:
                case 1:
                    textBox1.Text = "enter champion name here";
                    break;
                case 2:
                    textBox1.Text = "enter type name here";
                    break;
                case 3:
                    textBox1.Text = "nothing needs to be added";
                    break;
                case 4:
                    textBox1.Text = "enter equipment name here";
                    break;
                case 5:
                    textBox1.Text = "nothing needs to be added";
                    break;
                case 6:
                    textBox1.Text = "nothing needs to be added";
                    break;
                case 7:
                    textBox1.Text = "enter type name here";
                    break;
                case 8:
                    textBox1.Text = "nothing needs to be added";
                    break;
                case 9:
                    textBox1.Text = "nothing needs to be added";
                    break;
                default:
                    textBox1.Text = "enter information here";
                    break;
            }
        }

        //disallow ' to be entered
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\'') 
            {
                e.Handled = true;
            }
        }

       

    }
}
