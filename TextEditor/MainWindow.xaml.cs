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
using System.Windows.Forms;

namespace TextEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.copy.Click += OnClick;
            this.paste.Click += OnClick; 
            this.open.Click += OnClick;
            this.save.Click += OnClick;
            this.cut.Click += OnClick;

        }
        private void OnClick(object sender, RoutedEventArgs e)
        {
            if(sender is System.Windows.Controls.Button)
            {
                string s = (sender as System.Windows.Controls.Button).Content as string;
                switch(s)
                {
                    case "Copy": this.userinput.Copy(); break;
                    case "Paste": this.userinput.Paste(); break;
                    case "Open":
                        OpenFileDialog of = new OpenFileDialog();
                        if(of.ShowDialog()==System.Windows.Forms.DialogResult.OK)
                        {
                            using (var str = of.OpenFile())
                            {
                                System.IO.StreamReader sr= new System.IO.StreamReader(str);
                                userinput.SetText(sr.ReadToEnd());
                                sr.Dispose();
                            }
                        }
                        break;
                    case "Save":
                        SaveFileDialog sf = new SaveFileDialog();
                        sf.Filter = ".txt|*.txt";
                        sf.DefaultExt = ".txt";
                        if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            using (var str = sf.OpenFile())
                            {
                                System.IO.StreamWriter sr = new System.IO.StreamWriter(str);
                                sr.Write(userinput.GetText());
                                sr.Dispose();
                            }
                        }
                        break;
                    case "Cut": this.userinput.Cut(); break;
                }
            }
        }

    }
    public static class Ext
    {
        public static void SetText(this System.Windows.Controls.RichTextBox richTextBox, string text)
        {
            richTextBox.Document.Blocks.Clear();
            richTextBox.Document.Blocks.Add(new Paragraph(new Run(text)));
        }

        public static string GetText(this System.Windows.Controls.RichTextBox richTextBox)
        {
            return new TextRange(richTextBox.Document.ContentStart,
                richTextBox.Document.ContentEnd).Text;
        }
    }
}
