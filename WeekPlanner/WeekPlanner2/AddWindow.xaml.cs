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
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace WeekPlanner2
{

    [Serializable]
    public partial class AddWindow : Window
    {
        public AddWindow(string day)
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }


        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {

            MainWindow main = (MainWindow)this.DataContext;

            //Border Time

            Border setBorderTime = new Border();

            setBorderTime.Background = Brushes.CadetBlue;

            CornerRadius crTime = setBorderTime.CornerRadius;

            Thickness borderMarginTime = setBorderTime.Margin;

            crTime.TopLeft = 4;
            crTime.TopRight = 4;
            crTime.BottomLeft = 4;
            crTime.BottomRight = 4;

            borderMarginTime.Bottom = 0;
            borderMarginTime.Left = 10;
            borderMarginTime.Right = 10;
            borderMarginTime.Top = 3;

            setBorderTime.Margin = borderMarginTime;
            setBorderTime.CornerRadius = crTime;

            //TextBlock Time

            TextBlock printTbTime = new TextBlock();

            Thickness marginTime = printTbTime.Margin;

            printTbTime.Text = textBoxHours.Text + " : " + textBoxMinutes.Text;
            printTbTime.TextWrapping = TextWrapping.Wrap;
            marginTime.Left = 4;
            marginTime.Top = 4;
            marginTime.Right = 4;
            marginTime.Bottom = 4;

            printTbTime.TextAlignment = TextAlignment.Center;
            printTbTime.FontSize = 14;
            printTbTime.FontWeight = FontWeights.Bold;

            printTbTime.Background = Brushes.CadetBlue;

            printTbTime.Margin = marginTime;
            setBorderTime.Child = printTbTime;

            //Border Activity

            Border setBorderActivity = new Border();

            CornerRadius crActivity = setBorderActivity.CornerRadius;

            Thickness borderMarginActivity = setBorderActivity.Margin;

            crActivity.TopLeft = 8;
            crActivity.TopRight = 8;
            crActivity.BottomLeft = 8;
            crActivity.BottomRight = 8;

            borderMarginActivity.Bottom = 10;
            borderMarginActivity.Left = 10;
            borderMarginActivity.Right = 10;
            borderMarginActivity.Top = 0;

            setBorderActivity.Margin = borderMarginActivity;
            setBorderActivity.CornerRadius = crActivity;

            //Checkbox

            CheckBox printCheckBox = new CheckBox();
            Thickness marginCheckBox = printCheckBox.Margin;
            marginCheckBox.Left = 10;
            marginCheckBox.Right = 10;

            printCheckBox.Margin = marginCheckBox;

            if (radioButtonVeryImportant.IsChecked == true)
            {
                setBorderActivity.Background = Brushes.Red;
            }

            if (radioButtonImportant.IsChecked == true)
            {
                setBorderActivity.Background = Brushes.DarkOrange;
            }

            if (radioButtonSlightImportant.IsChecked == true)
            {
                setBorderActivity.Background = Brushes.ForestGreen;
            }

            //TextBlock Actvity

            TextBlock printTbActivity = new TextBlock();

            Thickness marginActivity = printTbActivity.Margin;

            printTbActivity.Text = textBoxActivity.Text;

            printTbActivity.TextWrapping = TextWrapping.Wrap;

            marginActivity.Left = 12;
            marginActivity.Top = 12;
            marginActivity.Right = 12;
            marginActivity.Bottom = 12;

            printTbActivity.Width = 100;
            printTbActivity.FontSize = 14;

            printTbActivity.Margin = marginActivity;
            printCheckBox.Content = printTbActivity;
            setBorderActivity.Child = printCheckBox;


            //Checking if all gaps are filled

            if (radioButtonMon.IsChecked == false
                & radioButtonTue.IsChecked == false
                & radioButtonWed.IsChecked == false
                & radioButtonThu.IsChecked == false
                & radioButtonFri.IsChecked == false
                & radioButtonSat.IsChecked == false
                & radioButtonSun.IsChecked == false
                || radioButtonImportant.IsChecked == false
                & radioButtonVeryImportant.IsChecked == false
                & radioButtonSlightImportant.IsChecked == false)

                MessageBox.Show("Please choose all the parameters");

            else if (textBoxActivity.Text == "")
                MessageBox.Show("Please insert activity");

            else if (textBoxHours.Text == "" || textBoxMinutes.Text == "")
                MessageBox.Show("Please insert time");

            else
            {
                //Adding borders with contents to appropriate stackpanel from MainWindow

                if (radioButtonMon.IsChecked == true)
                {
                    main.stackPanelMon.Children.Add(setBorderTime);
                    main.stackPanelMon.Children.Add(setBorderActivity);
                }
                if (radioButtonTue.IsChecked == true)
                {
                    main.stackPanelTue.Children.Add(setBorderTime);
                    main.stackPanelTue.Children.Add(setBorderActivity);
                }
                if (radioButtonWed.IsChecked == true)
                {
                    main.stackPanelWed.Children.Add(setBorderTime);
                    main.stackPanelWed.Children.Add(setBorderActivity);
                }
                if (radioButtonThu.IsChecked == true)
                {
                    main.stackPanelThu.Children.Add(setBorderTime);
                    main.stackPanelThu.Children.Add(setBorderActivity);
                }
                if (radioButtonFri.IsChecked == true)
                {
                    main.stackPanelFri.Children.Add(setBorderTime);
                    main.stackPanelFri.Children.Add(setBorderActivity);
                }
                if (radioButtonSat.IsChecked == true)
                {
                    main.stackPanelSat.Children.Add(setBorderTime);
                    main.stackPanelSat.Children.Add(setBorderActivity);
                }
                if (radioButtonSun.IsChecked == true)
                {
                    main.stackPanelSun.Children.Add(setBorderTime);
                    main.stackPanelSun.Children.Add(setBorderActivity);
                }

            }
        }

        //ESC Handle

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        //Valid hours number

        private void textBoxHours_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidHours(((TextBox)sender).Text + e.Text);
        }

        public static bool IsValidHours(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 0 && i <= 23;
        }

        //Valid minutes number

        private void textBoxMinutes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValidMinutes(((TextBox)sender).Text + e.Text);
        }

        public static bool IsValidMinutes(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 0 && i <= 59;
        }



    }
}
