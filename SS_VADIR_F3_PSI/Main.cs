using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SS_VADIR_F3_PSI
{
    public partial class Main : Form
    {
        string path = "./alunos.txt";
        UTF8Encoding encoding = new UTF8Encoding(true);

        public Main()
        {
            InitializeComponent();
        }
        public static bool numberonly_check(string str)
        {
            return !string.IsNullOrEmpty(str) && str.All(char.IsDigit);
        }

        private void btn_inserir_Click(object sender, EventArgs e)
        {
            if (!File.Exists(path))
            {
                Stream ws1 = new FileStream(path, FileMode.Create);
                byte[] bytes = encoding.GetBytes("Nome  Nota1   Nota2   Nota3   Média\n");
                ws1.Write(bytes, 0, bytes.Length);
                ws1.Close();
                MessageBox.Show("O ficheiro foi criado com sucesso!");
            }

            if(txt_nota1.Text == "" || txt_nota2.Text == "" || txt_nota3.Text == "" || txt_nome.Text == "")
            {
                MessageBox.Show("Os campos não podem estar vazios!");
                return;
            }

            if(numberonly_check(txt_nota1.Text) && numberonly_check(txt_nota2.Text) && numberonly_check(txt_nota3.Text))
            {
                if(new[] { Convert.ToInt32(txt_nota1.Text), Convert.ToInt32(txt_nota2.Text), Convert.ToInt32(txt_nota3.Text) }.All(x => x >= 0 && x <= 20))
                {
                    FileStream ws2 = new FileStream(path, FileMode.Append);
                    txt_media.Text = $"{(Convert.ToInt32(txt_nota1.Text)+Convert.ToInt32(txt_nota2.Text)+ Convert.ToInt32(txt_nota3.Text))/3}";
                    string x_str = $"{txt_nome.Text}    {txt_nota1.Text}    {txt_nota2.Text}    {txt_nota3.Text}    {(Convert.ToInt32(txt_nota1.Text) + Convert.ToInt32(txt_nota2.Text) + Convert.ToInt32(txt_nota3.Text)) / 3}\n";
                    byte[] bytes = encoding.GetBytes(x_str);
                    ws2.Write(bytes, 0, bytes.Length);
                    ws2.Close();
                    MessageBox.Show("O aluno foi adicionado com sucesso!");
                } else
                {
                    MessageBox.Show("As notas precisam ser de 0 a 20.");
                }
            } else
            {
                MessageBox.Show("Apenas números inteiros podem ser inseridos nas notas!");
            }
        }

        private void btn_limpar_Click(object sender, EventArgs e)
        {
            txt_media.Text = "";
            txt_nome.Text = "";
            txt_nota1.Text = "";
            txt_nota2.Text = "";
            txt_nota3.Text = "";
        }
    }
}
