using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppDesktop01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //txtNome.Text = "thiago";

            //Thiado.DataDll.Entidades.UsuarioEntidade usuario = new Thiado.DataDll.Entidades.UsuarioEntidade();
            Thiado.DataDll.Entidades.UsuarioEntidade usuario = new Thiado.DataDll.Entidades.UsuarioEntidade();
            usuario.Id = 0;

            usuario.Nome = nome.Text;

            usuario.Sexo = sexo.Text;
//TRATANDO A IDADE
            if(string.IsNullOrEmpty(idade.Text))// é o mesmo que comparar com ==null || =="". porem o isnull e o .equals() performam melhor
            {
                //usuario.Idade = 0;
                MessageBox.Show("Informe a idade","Mensagem do Sistema");
                return;// esse return sozinho sai do metodo na hora.
            }
            // aqui em baixo verifico se a idade é um número////////////////////////////////////
            string numString = idade.Text;
            int number1 = 0;
            bool canConvert = int.TryParse(numString, out number1);

            if(canConvert == false)
            {
                MessageBox.Show("Idade deve ser um número.");
                return;
            }
            // aqui em acima verifico se a idade é um número//////////////////////////////////////
//TRATANDO A IDADE
            usuario.Idade = Convert.ToInt32(idade.Text);
            
            usuario.Ativo = ativo.IsChecked == null ? false : Convert.ToBoolean(ativo.IsChecked);

            Thiado.DataDll.Services.UsuarioService salvaBanco = new Thiado.DataDll.Services.UsuarioService();
            salvaBanco.Salvar(usuario);

            MessageBox.Show("Contato Salvo");
        }

        private void nome_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void idade_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
