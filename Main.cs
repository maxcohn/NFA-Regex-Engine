using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFA_Regex_Engine
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine(Notation.infixToPostfix("abb+(abb)+"));

            NFA n = Compiler.Compile(Notation.infixToPostfix("(a|b)*"));

            Console.WriteLine("AHH");
            // DEBUG CODE

        
        }
    }
}
