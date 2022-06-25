using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Man_hours_managementApp
{
    public partial class MypageForm : Form
    {
        public MypageForm()
        {
            InitializeComponent();
        }

        //ListViewコントロールを初期化
        private void InitializeListView()
        {
            //ListViewコントロールのプロパティを設定
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Sorting = SortOrder.Ascending;
            listView1.View = View.Details;

            //列(カラム)ヘッダの作成

        
        }
    }
}
