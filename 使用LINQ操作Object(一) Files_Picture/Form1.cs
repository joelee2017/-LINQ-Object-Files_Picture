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

namespace 使用LINQ操作Object_一__Files_Picture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// LINQ 針對files 操作
        /// DirectoryInfo di = new DirectoryInfo(@"C: \Users\III\Downloads\圖庫");
        /// 初始化路徑
        /// IEnumerable<FileInfo>提供建立、複製、刪除、移動和開啟檔案的屬性和執行個體方法
        /// di.EnumerateFiles 傳回符合指定之搜尋模式和搜尋子目錄選項的檔案資訊的可列舉集合。
        /// SearchOption.AllDirectories
        /// 指定要搜尋目前目錄，還是要搜尋目前目錄和所有子目錄/搜尋作業中包含目前目錄和所有其子目錄。這個選項會在搜尋中包含重新剖析點 (例如掛接磁碟和符號連結)。
        /// Select((file, index)=> new /新增流水編號   
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(@"F:\JOE");
            IEnumerable<FileInfo> files = di.EnumerateFiles("*.*", SearchOption.AllDirectories);
            var query = (from file in files
                        where file.Length < 1024 //&&file.Name.StartsWith("")
                        let filecontent =ReadFile(file.FullName)
                        where filecontent.Contains("a")//Contains("a")含有a文字的內容
                        orderby file.Length descending
                        select file).Select((file, index)=> new
                        {
                            檔案編號= index+1,
                            檔案名稱 = file.Name,
                            檔案路徑=file.FullName,
                            檔案大小 = file.Length,
                            建立日期 = file.CreationTime,
                            最後修改日期 = file.LastWriteTime
                        });
            dataGridView1.DataSource = query.ToArray();
            dataGridView1.AutoResizeColumns();
        }

        private string ReadFile(string fullName)
        {
            return File.ReadAllText(fullName);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            string Filename = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            textBox1.Text = ReadFile(Filename);
        }
    }
}
