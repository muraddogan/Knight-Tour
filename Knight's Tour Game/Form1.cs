using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace knightstour
{
    public partial class Form1 : Form
    {

        KnightTour game;
        int Boyut;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Boyut = 5;
            Start();
            dataGridView1.Enabled = false;
        }

        PointCord click = new PointCord();
        PointCord[] dizi;
        PointCord[] oncekiDizi;
        int tiklama = 0;
        public void Solve(PointCord point)
        {

            if (tiklama == 0 || dataGridView1.Rows[point.x].Cells[point.y].Style.BackColor == Color.Thistle)
            {
                int numbert = game.Click(point);
                dataGridView1.Rows[point.x].Cells[point.y].Value = numbert;

                dataGridView1.CurrentCell.Style.BackColor = Color.PowderBlue;
                dataGridView1.CurrentCell.Style.SelectionBackColor = Color.PowderBlue;

                dizi = game.Identify(point);

                tiklama++;

                lblScore.Text = tiklama + "";

            }

            if (oncekiDizi != null)
                for (int i = 0; oncekiDizi[i] != null & i < oncekiDizi.Length; i++)
                {
                    int x = oncekiDizi[i].x;
                    int y = oncekiDizi[i].y;
                    if (dataGridView1.Rows[x].Cells[y].Style.BackColor == Color.Thistle)
                        dataGridView1.Rows[x].Cells[y].Style.BackColor = Color.White;

                }

            if (dizi != null)
            {
                int i = 0;
                for ( i = 0; dizi[i] != null & i < dizi.Length; i++)
                {
                    int x = dizi[i].x;
                    int y = dizi[i].y;
                    dataGridView1.Rows[x].Cells[y].Style.BackColor = Color.Thistle;
                }

                label2.Text = "" + i;
            }

            oncekiDizi = dizi;

            bool alan = false;
            for (int i = 0; i < Boyut; i++)
            {
                for (int j = 0; j < Boyut; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.Thistle)
                    {
                        alan = true;
                    }
                }

            }

            if(tiklama == Boyut*Boyut)
            {
                MessageBox.Show("Tebrikler Oyunu Kazandınız...");
                dataGridView1.Enabled = false;
                tiklama = 0;
            }

            if (!alan)
            {
                MessageBox.Show("Game Over");
                dataGridView1.Enabled = false;
                tiklama = 0;
            }


        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Solve(new PointCord { x = e.RowIndex, y = e.ColumnIndex });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (radioButton5x5.Checked)
                Boyut = 5;
            if (radioButton6x6.Checked)
                Boyut = 6;
            if (radioButton7x7.Checked)
                Boyut = 7;
            if (radioButton8x8.Checked)
                Boyut = 8;
            if (radioButton9x9.Checked)
                Boyut = 9;

            Start();
        }

        public void Start()
        {
            dataGridView1.Enabled = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            dataGridView1.Size = new Size(Boyut * 35, Boyut * 35);
            dataGridView1.RowTemplate.Height = 35;

            game = new KnightTour(Boyut);
            game.Define();


            click = new PointCord();
            dizi = null;
            oncekiDizi = null;
            tiklama = 0;


            for (int i = 0; i < Boyut; i++)
            {
                dataGridView1.Columns.Add("", "");

            }

            for (int i = 0; i < Boyut; i++)
            {
                dataGridView1.Rows.Add();
            }

            dataGridView1.CurrentCell.Style.SelectionBackColor = Color.White;
        }
    }
}
