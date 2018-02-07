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
using System.IO;
using System.Xml;
using System.Windows.Markup;

namespace WeekPlanner2
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

        }
               
        //Open AddWindow

        private void btnAddMon_Click(object sender, RoutedEventArgs e)
        {
            AddWindow add = new AddWindow("monday");
            add.DataContext = this;
            add.Show();
        }

        //Save stackpanel

        public void saveStackPanel(string fileName, StackPanel stackPanelName)
        {
            if (File.Exists(@fileName + ".xaml") == true)
                File.Delete(@fileName + ".xaml");

            StringBuilder outstr = new StringBuilder();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            XamlDesignerSerializationManager dsm = new XamlDesignerSerializationManager(XmlWriter.Create(outstr, settings));
            dsm.XamlWriterMode = XamlWriterMode.Expression;

            XamlWriter.Save(stackPanelName, dsm);

            string savedControls = outstr.ToString();

            File.WriteAllText(@fileName + ".xaml", savedControls);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            saveStackPanel("mon", stackPanelMon);
            saveStackPanel("tue", stackPanelTue);
            saveStackPanel("wed", stackPanelWed);
            saveStackPanel("thu", stackPanelThu);
            saveStackPanel("fri", stackPanelFri);
            saveStackPanel("sat", stackPanelSat);
            saveStackPanel("sun", stackPanelSun);

        }

        //Load stackpanel

        public void loadStackPanel(string fileName, StackPanel stackPanelName)
        {
            if (File.Exists(@fileName + ".xaml") == true)
            {
                StreamReader sR = new StreamReader(@fileName + ".xaml");
                string text = sR.ReadToEnd();
                sR.Close();

                StringReader stringReader = new StringReader(text);
                XmlReader xmlReader = XmlReader.Create(stringReader);

                StackPanel wp = (StackPanel)System.Windows.Markup.XamlReader.Load(xmlReader);

                stackPanelName.Children.Clear(); // clear the existing children

                foreach (FrameworkElement child in wp.Children) // and for each child in the WrapPanel we just loaded (wp)
                {
                    stackPanelName.Children.Add(CloneFrameworkElement(child)); // clone the child and add it to our existing wrap panel
                }
            }

        }

        FrameworkElement CloneFrameworkElement(FrameworkElement originalElement)
        {
            string elementString = XamlWriter.Save(originalElement);

            StringReader stringReader = new StringReader(elementString);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            FrameworkElement clonedElement = (FrameworkElement)XamlReader.Load(xmlReader);

            return clonedElement;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                loadStackPanel("mon", stackPanelMon);
                loadStackPanel("tue", stackPanelTue);
                loadStackPanel("wed", stackPanelWed);
                loadStackPanel("thu", stackPanelThu);
                loadStackPanel("fri", stackPanelFri);
                loadStackPanel("sat", stackPanelSat);
                loadStackPanel("sun", stackPanelSun);
            }
            catch
            {
                MessageBox.Show("You have not saved any activities");
            }
        }
        
        //Clear buttons

        private void btnClearMon_Click(object sender, RoutedEventArgs e)
        {
            stackPanelMon.Children.Clear();
        }

        private void btnClearTue_Click(object sender, RoutedEventArgs e)
        {
            stackPanelTue.Children.Clear();
        }

        private void btnClearWed_Click(object sender, RoutedEventArgs e)
        {
            stackPanelWed.Children.Clear();
        }

        private void btnClearThu_Click(object sender, RoutedEventArgs e)
        {
            stackPanelThu.Children.Clear();
        }

        private void btnClearFri_Click(object sender, RoutedEventArgs e)
        {
            stackPanelFri.Children.Clear();
        }

        private void btnClearSat_Click(object sender, RoutedEventArgs e)
        {
            stackPanelSat.Children.Clear();
        }

        private void btnClearSun_Click(object sender, RoutedEventArgs e)
        {
            stackPanelSun.Children.Clear();
        }

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            stackPanelMon.Children.Clear();
            stackPanelTue.Children.Clear();
            stackPanelWed.Children.Clear();
            stackPanelThu.Children.Clear();
            stackPanelFri.Children.Clear();
            stackPanelSat.Children.Clear();
            stackPanelSun.Children.Clear();
        }

        //ESC Handle

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }

}
