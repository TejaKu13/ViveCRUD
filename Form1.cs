using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViveCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        klubEntities db = new klubEntities();
        Druzyna gracz = new Druzyna();
        int PlayerId = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            ClearData();
            SetDataInGridView();
        }
        public void ClearData()
        {
            txtNrKoszulki.Text = txtImie.Text = txtNazwisko.Text = txtDataUrodzenia.Text = txtPozycja.Text= txtPozycja.Text = string.Empty;
            btnUsun.Enabled = false;
            btnZapisz.Text = "Zapisz";
            PlayerId = 0;
        }

        public void SetDataInGridView()
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = db.Druzyna.ToList();
        }
        private void btnZapisz_Click(object sender, EventArgs e)
        {
            gracz.NrKoszulki = Convert.ToInt32(txtNrKoszulki.Text.Trim());
            gracz.Imie = txtImie.Text.Trim();
            gracz.Nazwisko = txtNazwisko.Text.Trim();
            gracz.DataUrodzenia = Convert.ToDateTime(txtDataUrodzenia.Text.Trim());
            gracz.Pozycja = txtPozycja.Text.Trim();
            gracz.Kraj = txtKraj.Text.Trim();
            if (PlayerId > 0)
                db.Entry(gracz).State = EntityState.Modified;
            else
            {
                db.Druzyna.Add(gracz);
            }
            db.SaveChanges();
            ClearData();
            SetDataInGridView();
            MessageBox.Show("Dane zawodnika zapisane");
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex != -1)
            {
                PlayerId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["NrZawodnika"].Value);
                gracz = db.Druzyna.Where(x => x.NrZawodnika == PlayerId).FirstOrDefault();
                txtNrKoszulki.Text = gracz.NrKoszulki.ToString();
                txtImie.Text = gracz.Imie;
                txtNazwisko.Text = gracz.Nazwisko;
                txtDataUrodzenia.Text = gracz.DataUrodzenia.ToString();
                txtPozycja.Text = gracz.Pozycja;
                txtKraj.Text = gracz.Kraj;
            }
            btnZapisz.Text = "Odśwież";
            btnUsun.Enabled = true;
        }

        private void btnUsun_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Jesteś pewny,że chcesz usunąć zawodnika z listy ?", "Usunąć ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                db.Druzyna.Remove(gracz);
                db.SaveChanges();
                ClearData();
                SetDataInGridView();
                MessageBox.Show("dane usunięte");
            }
        }

        private void btnAnuluj_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}
