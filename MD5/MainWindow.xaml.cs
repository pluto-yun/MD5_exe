using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.IO;

namespace MD5
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fdlg = new Microsoft.Win32.OpenFileDialog();


            fdlg.Title = "选择需校验的MD文件";//编辑框标题
            fdlg.InitialDirectory = $"{System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase }";   //设置初始打开位置
            //$后的语句为获取当前程序坐在目录
            fdlg.Filter = "All files（*.*）|*.*";
            fdlg.CheckPathExists = true;
            fdlg.Multiselect = false;//不允许选择多个文件

            /*
             * FilterIndex 属性用于选择了何种文件类型,缺省设置为0,系统取Filter属性设置第一项
             * ,相当于FilterIndex 属性设置为1.如果你编了3个文件类型，当FilterIndex ＝2时是指第2个.
             */
            fdlg.FilterIndex = 1;
            /*
             *如果值为false，那么下一次选择文件的初始目录是上一次你选择的那个目录，
             *不固定；如果值为true，每次打开这个对话框初始目录不随你的选择而改变，是固定的  
             */
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() != true)
            {
                return;

            }
            else
            {

                textBox.Text = fdlg.FileName;

            }

        }

        private void Select_2_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fdlg = new Microsoft.Win32.OpenFileDialog();


            fdlg.Title = "选择需校验的MD文件";//编辑框标题
            fdlg.InitialDirectory = $"{System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase }";   //设置初始打开位置
            //$后的语句为获取当前程序坐在目录
            fdlg.Filter = "All files（*.*）|*.*";
            fdlg.CheckPathExists = true;
            fdlg.Multiselect = false;//不允许选择多个文件

            /*
             * FilterIndex 属性用于选择了何种文件类型,缺省设置为0,系统取Filter属性设置第一项
             * ,相当于FilterIndex 属性设置为1.如果你编了3个文件类型，当FilterIndex ＝2时是指第2个.
             */
            fdlg.FilterIndex = 1;
            /*
             *如果值为false，那么下一次选择文件的初始目录是上一次你选择的那个目录，
             *不固定；如果值为true，每次打开这个对话框初始目录不随你的选择而改变，是固定的  
             */
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() != true)
            {
                return;

            }
            else
            {

                textBox_1.Text = fdlg.FileName;

            }
        }

        private void textBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            //实现拖曳东西在空间范围内的效果是copy的效果
            e.Handled = true;

        }

        private void textBox_PreviewDrop(object sender, DragEventArgs e)
        {
            foreach (string f in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                textBox.Text = f;
            }
        }

        private void textBox_1_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void textBox_1_PreviewDrop(object sender, DragEventArgs e)
        {
            foreach (string f in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                textBox_1.Text = f;
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            string[] filePath = { textBox.Text.Trim(), textBox_1.Text.Trim() };
            string[] md5val = new string[2]; 

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        
            for (int i = 0; i < filePath.Length; i++)
            {
                if (filePath[i].Contains(@"\"))
                {

                    FileStream stream = new FileStream($"{filePath[i]}", FileMode.Open);
                    byte[] b = md5.ComputeHash(stream);

                    stream.Close();

                    StringBuilder testResult = new StringBuilder();

                    for (int j = 0; j < b.Length; j++)
                    {
                        testResult.Append(b[j].ToString("x2"));
                    }

                    md5val[i] = testResult.ToString();
                }
                else
                {
                    md5val[i] = filePath[i];
                }
            }

            textBox.Text = md5val[0];
            textBox_1.Text = md5val[1];


            if(md5val[0]==md5val[1])
            {
                result.Text = "一致";
             }
            else
            {
                result.Text = "不一致";
            }
        }


    } 
}
