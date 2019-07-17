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
using System.Threading;


namespace ĐoAn_1
{
    public partial class Form1 : Form
    {
        public List<string> history;
        public List<string> history2;
        public List<bool> flagCopy = new List<bool>();
        public List<bool> flagDir = new List<bool>();
        public List<string> SourceDir = new List<string>();
        public List<string> SourceFile = new List<string>();
        public string strDestDir = "";
        public string strDest = "";
        public List<string> ItemName = new List<string>();
        public int icopy = 0;
        public static int i = 0;
        public static string ac = "";
        public static string savechange = "";


        public Form1()
        {
            InitializeComponent();
            loadComboBox1();
            loadComboBox2();
        }
        private void inithistory()
        {
            history = new List<string>();
            history2 = new List<string>();

        }
        private void loadComboBox1()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                comboBox1.Items.Add(drive.Name);
            }
        }
        private void loadComboBox2()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                comboBox2.Items.Add(drive.Name);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inithistory();
            foreach (DriveInfo dr in DriveInfo.GetDrives())
            {

                comboBox1.Items.Add(dr.Name);
                comboBox2.Items.Add(dr.Name);

            }
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox2.SelectedItem = comboBox2.Items[0];
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //webBrowser1.View
        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void HELPToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Path selecting" })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {

                }

            }
        }

        private void ButtonDetailView_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_2(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void StartView2_Click(object sender, EventArgs e)
        {
            //using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Path selecting" })
            //{
            //    if (fbd.ShowDialog() == DialogResult.OK)
            //    {
            //        listView1.View = new Uri(fbd.SelectedPath);
            //        textBox1.Text = fbd.SelectedPath;
            //    }

            //}
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void WebBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void ButtonNewFolder_Click(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ListView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DriveInfo d = new DriveInfo(comboBox1.SelectedItem.ToString());
            if (d.IsReady == false)
            {
                MessageBox.Show("Drive is not ready", "Error");
            }
            else
            {
                label1.Text = "Free : " + " " + d.TotalFreeSpace / 1048576 + " MB  " + " On : " + " " + d.TotalSize / 1048576 + "MB";
                if (comboBox1.Text == comboBox2.Text)
                {
                    updateListView(textBox2.Text);
                }
                else
                {
                    updateListView(comboBox1.SelectedItem.ToString());
                }
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.GetHashCode() == 13)
            {
                String uri = textBox1.Text;
                updateListView(uri);
            }
        }

        public ListViewItem CreatLVItem(DirectoryInfo dir)
        {

            string[] s = { dir.Name, "File Folder", "", dir.LastWriteTime.ToString(), dir.FullName, dir.Parent.FullName };
            ListViewItem item = new ListViewItem(s);
            //item.Text = "[ " + dir.Name + " ]";
            item.ImageKey = "Folder";
            return item;
        }
        public int CountFile(DirectoryInfo d)
        {
            int i = 0;
            foreach (FileInfo fi in d.GetFiles())
            {
                i++;
            }
            return i;
        }
        public int CountFolder(DirectoryInfo d)
        {
            int countfolder = 0;
            foreach (DirectoryInfo di in d.GetDirectories())
            {
                if ((di.Attributes & FileAttributes.Hidden) == 0 && (di.Attributes & FileAttributes.Temporary) == 0)
                {
                    countfolder++;
                }
            }
            return countfolder;
        }
        public ListViewItem CreatLVItem(FileInfo file)
        {
            string l = ConvertFileSize(file);
            string[] s = { file.Name, file.Extension.ToUpper(), l ,
               file.LastWriteTime.ToString() , file.FullName, file.Directory.FullName };
            ListViewItem item = new ListViewItem(s);
            //item.Text = "[ " + file.Name + " ]";
            switch (file.Extension.ToUpper())
            {
                case ".TXT":
                case ".DIZ":
                case ".LOG":
                    item.ImageKey = "TXT";
                    break;
                case ".PDF":
                    item.ImageKey = "PDF";
                    break;
                case "HTML.":
                    item.ImageKey = "HTM";
                    break;
                case ".DOCX":
                case ".DOC":
                    item.ImageKey = "DOC";
                    break;
                case ".EXE":
                    item.ImageKey = "EXE";
                    break;
                case ".ZIP":
                case ".RAR":
                    item.ImageKey = "RAR";
                    break;
                case ".FLV":
                case ".AVI":
                case ".MKV":
                case ".MP4":
                    item.ImageKey = "AVI";
                    break;
                case ".DLL":
                    item.ImageKey = "DLL";
                    break;
                case ".TORRENT":
                    item.ImageKey = "TORRENT";
                    break;
                case ".JPG":
                case ".BMP":
                case ".PNG":
                    item.ImageKey = "JPG";
                    break;
                case ".SLN":
                    item.ImageKey = "sln";
                    break;
                case ".MP3":
                    item.ImageKey = "MP3";
                    break;
                case ".INF":
                    item.ImageKey = "inf";
                    break;

                default:
                    item.ImageKey = "File";
                    break;
            }
            return item;
        }
        private string ConvertFileSize(FileInfo d)
        {

            long a = d.Length;
            string s = "";
            if (d.Length <= 1024)
            {
                s = 1 + "KB";
            }
            if (d.Length >= 1024 && d.Length < (1024 * 1024))
            {
                a = d.Length / 1024;
                s = a + "KB";
            }

            if (d.Length >= (1024 * 1024) && d.Length <= (1024 * 1024 * 1024))
            {
                a = a / (1024 * 1024);
                s = a + "MB";
            }
            if (d.Length > (1024 * 1024 * 1024))
            {
                a = a / (1024 * 1024 * 1024);
                s = a + "GB";
            }

            return s;

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DriveInfo d = new DriveInfo(comboBox2.SelectedItem.ToString());
            if (d.IsReady == false)
            {
                MessageBox.Show("Drive is not ready", "Error");
            }
            else
            {
                label2.Text = "Free : " + " " + d.TotalFreeSpace / 1048576 + " MB  " + " On : " + " " + d.TotalSize / 1048576 + "MB";
                if (comboBox2.Text == comboBox1.Text)
                {
                    updateListView2(textBox1.Text);
                }
                else
                {
                    updateListView2(comboBox2.SelectedItem.ToString());
                }
            }
        }

        private void ListView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.GetHashCode() == 13)
            {

                String path = textBox2.Text;
                updateListView2(path);
            }
        }
        private void updateListView(String uri)
        {
            history.Add(uri);
            DirectoryInfo test = new DirectoryInfo(uri);
            if (!test.Exists)
            {
                MessageBox.Show("Drire is Wrong ", "Error");
                if (listView1.Items[0].Text == "...")
                {
                    textBox1.Text = Path.GetDirectoryName(listView1.Items[1].SubItems[4].Text);
                }
                else
                {
                    textBox1.Text = Path.GetDirectoryName(listView1.Items[0].SubItems[4].Text);
                }
                return;
            }
            textBox1.Text = uri;
            int a = CountFile(test);
            int b = CountFolder(test);
            //lbCount2.Text = a.ToString() + " Files " + " / " + b.ToString() + " Folders ";
            ListViewItem item = new ListViewItem();
            listView1.Items.Clear();

            listView1.Columns[3].Text = "Date Modified";
            DriveInfo d = new DriveInfo(uri);
            if (d.IsReady == false)
                return;
            DirectoryInfo rootDir = new DirectoryInfo(uri);
            if (uri.Length > 4)
            {

                string[] s = { "...", "File Folder", "", "", rootDir.Parent.FullName, "" };
                item = new ListViewItem(s);
                item.ImageKey = "up";
                item.Tag = rootDir.Parent.FullName;
                listView1.Items.Add(item);
            }
            foreach (DirectoryInfo di in rootDir.GetDirectories())
            {
                if ((di.Attributes & FileAttributes.Hidden) == 0 && (di.Attributes & FileAttributes.Temporary) == 0)
                {
                    item = CreatLVItem(di);
                    item.Tag = di.FullName;
                    listView1.Items.Add(item);
                }
            }
            foreach (FileInfo file in rootDir.GetFiles())
            {
                if ((file.Attributes & FileAttributes.Hidden) == 0 && (file.Attributes & FileAttributes.Temporary) == 0)
                {
                    item = CreatLVItem(file);
                    item.Tag = file.FullName;
                    listView1.Items.Add(item);
                }
            }
        }
        private void updateListView2(string uri)
        {
            history2.Add(uri);
            DirectoryInfo test = new DirectoryInfo(uri);
            if (!test.Exists)
            {
                MessageBox.Show("Drire is Wrong ", "Error");
                if (listView2.Items[0].Text == "...")
                {
                    textBox2.Text = Path.GetDirectoryName(listView2.Items[1].SubItems[4].Text);
                }
                else
                {
                    textBox2.Text = Path.GetDirectoryName(listView2.Items[0].SubItems[4].Text);
                }
                return;
            }
            textBox2.Text = uri;
            int a = CountFile(test);
            int b = CountFolder(test);
            //lbCount2.Text = a.ToString() + " Files " + " / " + b.ToString() + " Folders ";
            ListViewItem item = new ListViewItem();
            listView2.Items.Clear();

            listView2.Columns[3].Text = "Date Modified";
            DriveInfo d = new DriveInfo(uri);
            if (d.IsReady == false)
                return;
            DirectoryInfo rootDir = new DirectoryInfo(uri);
            if (uri.Length > 4)
            {

                string[] s = { "...", "File Folder", "", "", rootDir.Parent.FullName, "" };
                item = new ListViewItem(s);
                item.ImageKey = "up";
                item.Tag = rootDir.Parent.FullName;
                listView2.Items.Add(item);
            }
            foreach (DirectoryInfo di in rootDir.GetDirectories())
            {
                if ((di.Attributes & FileAttributes.Hidden) == 0 && (di.Attributes & FileAttributes.Temporary) == 0)
                {
                    item = CreatLVItem(di);
                    item.Tag = di.FullName;
                    listView2.Items.Add(item);
                }
            }
            foreach (FileInfo file in rootDir.GetFiles())
            {
                if ((file.Attributes & FileAttributes.Hidden) == 0 && (file.Attributes & FileAttributes.Temporary) == 0)
                {
                    item = CreatLVItem(file);
                    item.Tag = file.FullName;
                    listView2.Items.Add(item);
                }
            }


        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.GetHashCode() == 13)
            {

                String path = textBox2.Text;
                updateListView2(path);
            }
        }
        public void initClipboard()
        {
            flagCopy.Clear();
            flagCopy.Clear();
            SourceDir.Clear();
            SourceFile.Clear();
            ItemName.Clear();
            strDestDir = "";
            strDest = "";
        }
        private void COPYToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            initClipboard();
            i = 0;
            flagCopy.Clear();
            flagCopy.Clear();
            SourceDir.Clear();
            SourceFile.Clear();
            ItemName.Clear();

            if (listView1.Focused)
            {
                if (listView1.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Chua chon File, Folder");
                    return;
                }
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    try
                    {

                        if (listView1.SelectedItems.Count > 0)//copy o listview
                        {

                            //item = lvItem.FocusedItem;
                            if (item.SubItems[1].Text.Trim() == "File Folder")//copy folder
                            {
                                flagCopy.Add(true);
                                flagDir.Add(true);
                                DirectoryInfo d = new DirectoryInfo(item.SubItems[4].Text);
                                ItemName.Add(d.Name);
                                string ks = "";
                                if (textBox1.Text.Length > 3)
                                {
                                    ks = textBox1.Text + @"\" + ItemName[ItemName.Count - 1];
                                }
                                else
                                {
                                    ks = textBox1.Text + ItemName[ItemName.Count - 1];
                                }
                                SourceDir.Add(ks);
                                SourceFile.Add("");

                            }
                            else//copy file
                            {
                                flagCopy.Add(true);
                                flagDir.Add(false);
                                FileInfo d = new FileInfo(item.SubItems[4].Text);
                                ItemName.Add(d.Name);
                                string ks = "";
                                if (textBox1.Text.Length > 3)
                                {
                                    ks = textBox1.Text + @"\" + ItemName[ItemName.Count - 1];
                                }
                                else
                                {
                                    ks = textBox1.Text + ItemName[ItemName.Count - 1];
                                }
                                SourceFile.Add(ks);
                                SourceDir.Add("");
                            }
                            //btPaste.Enabled = true;
                            listView1.FocusedItem = null;
                        }
                        else
                        {
                            MessageBox.Show("Chua chon File, Folder");
                        }
                        // pasteToolStripMenuItem.Enabled = true;
                        // pasteToolStripMenuItem1.Enabled = true;
                        pASTEToolStripMenuItem.Enabled = true;
                        //pasteToolStripMenuItem1.Enabled = true;
                        //btPaste.Enabled = true;
                    }
                    catch { }
                }
            }
            if (listView2.Focused)
            {
                if (listView2.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Chua chon File, Folder");
                    return;
                }
                foreach (ListViewItem item in listView2.SelectedItems)
                {
                    try
                    {

                        if (listView2.SelectedItems.Count > 0)//copy o listview
                        {

                            //item = lvItem2.FocusedItem;
                            if (item.SubItems[1].Text.Trim() == "File Folder")//copy folder
                            {
                                flagCopy.Add(true);
                                flagDir.Add(true);
                                DirectoryInfo d = new DirectoryInfo(item.SubItems[4].Text);
                                ItemName.Add(d.Name);
                                string ks = "";
                                if (textBox2.Text.Length > 3)
                                {
                                    ks = textBox2.Text + @"\" + ItemName[ItemName.Count - 1];
                                }
                                else
                                {
                                    ks = textBox2.Text + ItemName[ItemName.Count - 1];
                                }
                                SourceDir.Add(ks);
                                SourceFile.Add("");

                            }
                            else//copy file
                            {
                                flagCopy.Add(true);
                                flagDir.Add(false);
                                FileInfo d = new FileInfo(item.SubItems[4].Text);
                                ItemName.Add(d.Name);
                                string ks = "";
                                if (textBox2.Text.Length > 3)
                                {
                                    ks = textBox2.Text + @"\" + ItemName[ItemName.Count - 1];
                                }
                                else
                                {
                                    ks = textBox2.Text + ItemName[ItemName.Count - 1];
                                }
                                SourceFile.Add(ks);
                                SourceDir.Add("");
                            }
                            //btPaste.Enabled = true;
                            listView2.FocusedItem = null;
                        }
                        else
                        {
                            MessageBox.Show("Chua chon File, Folder");
                        }
                        // pasteToolStripMenuItem.Enabled = true;
                        // pasteToolStripMenuItem1.Enabled = true;

                    }
                    catch { }
                }

            }

        }
        public string pnfd = "";
        public bool flagRename = false;
        public bool flagNewFolder = false;
        public string nfd = "New Folder";
        public static int ifd = 1;
        private void RENAMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flagRename = true;
            if (listView1.SelectedItems.Count == 0 && listView2.SelectedItems.Count == 0)
            {
                return;
            }
            if (listView1.Focused)
            {
                listView1.SelectedItems[0].BeginEdit();
                pnfd = textBox1.Text;
            }
            if (listView2.Focused)
            {
                listView2.SelectedItems[0].BeginEdit();
                pnfd = textBox2.Text;
            }
        }

        private void ListView1_AfterLabelEdit(object sender, LabelEditEventArgs e)

        {
            if (e.Label == null)
            {
                flagRename = false;
                if (flagNewFolder)
                {
                    ListViewItem curr = listView1.FocusedItem;
                    e.CancelEdit = true;
                    string s = pnfd + curr.Text;
                    listView1.FocusedItem.Text = curr.Text.ToString();
                    DirectoryInfo m = Directory.CreateDirectory(s);
                    string[] l = { m.Name, "File Folder", "", m.CreationTime.ToLongDateString(), m.FullName, m.Parent.FullName };
                    listView1.FocusedItem = new ListViewItem(l);
                    listView1.FocusedItem.Tag = s;

                }

                flagNewFolder = false;
                return;
            }
            else
            {
                if (flagRename)
                {

                    ListViewItem curr = listView1.SelectedItems[0];
                    string currpath = curr.SubItems[4].Text;
                    FileInfo fi = new FileInfo(currpath);
                    if (fi.Exists)
                    {

                        string dest = pnfd + e.Label;
                        FileInfo t = new FileInfo(dest);
                        if (t.Exists)
                        {
                            e.CancelEdit = true;
                            listView1.Refresh();

                            MessageBox.Show("Ten file da ton tai ", "error", MessageBoxButtons.OKCancel);

                            listView1.FocusedItem.SubItems[0].Text = curr.SubItems[0].Text.ToString();
                            flagRename = false;
                        }

                        else
                        {
                            if (e.Label == "")
                            {

                                e.CancelEdit = true;
                                listView1.FocusedItem.SubItems[0].Text = curr.SubItems[0].Text.ToString();
                                flagRename = false;
                            }
                            else
                            {
                                File.Move(currpath, dest);
                                e.CancelEdit = true;
                                listView1.FocusedItem.SubItems[0].Text = e.Label;
                                listView1.FocusedItem.SubItems[4].Text = dest;
                                listView1.FocusedItem.Tag = dest;
                                flagRename = false;


                            }
                        }


                    }
                    else
                    {


                        string dest = pnfd + e.Label;
                        DirectoryInfo t = new DirectoryInfo(dest);
                        if (t.Exists)
                        {
                            MessageBox.Show("Ten file da ton tai ", "error");
                            e.CancelEdit = true;
                            listView1.FocusedItem.SubItems[0].Text = curr.SubItems[0].Text.ToString();
                            flagRename = false;
                        }

                        else
                        {
                            if (e.Label == "")
                            {

                                e.CancelEdit = true;
                                listView1.FocusedItem.SubItems[0].Text = curr.SubItems[0].Text.ToString();
                                flagRename = false;

                            }
                            else
                            {

                                Directory.Move(currpath, dest);
                                e.CancelEdit = true;
                                listView1.FocusedItem.SubItems[0].Text = e.Label;
                                listView1.FocusedItem.SubItems[4].Text = dest;
                                listView1.FocusedItem.Tag = dest;
                                flagRename = false;


                            }
                        }

                    }
                }

                if (flagNewFolder)
                {

                    ListViewItem curr = listView1.FocusedItem;
                    string currpath = pnfd + e.Label;
                    string dest = pnfd + e.Label;
                    DirectoryInfo t = new DirectoryInfo(dest);
                    if (t.Exists && e.Label != "")
                    {
                        MessageBox.Show("Ten folder da ton tai ", "error");
                        e.CancelEdit = true;
                        listView1.FocusedItem.SubItems[0].Text = curr.SubItems[0].Text.ToString();
                        listView1.FocusedItem.BeginEdit();
                    }

                    else
                    {
                        if (e.Label == "")
                        {

                            e.CancelEdit = true;
                            string s = pnfd + curr.Text;
                            listView1.FocusedItem.Text = curr.Text.ToString();
                            DirectoryInfo m = Directory.CreateDirectory(s);
                            string[] l = { m.Name, "File Folder", "", m.CreationTime.ToLongDateString(), m.FullName, m.Parent.FullName };
                            listView1.FocusedItem = new ListViewItem(l);
                            listView1.FocusedItem.Tag = s;


                        }
                        else
                        {
                            e.CancelEdit = true;
                            string s = pnfd + e.Label;
                            listView1.FocusedItem.Text = e.Label;
                            DirectoryInfo m = Directory.CreateDirectory(s);
                            string[] l = { m.Name, "File Folder", "", m.CreationTime.ToLongDateString(), m.FullName, m.Parent.FullName };
                            listView1.FocusedItem = new ListViewItem(l);
                            listView1.FocusedItem.Tag = s;

                        }
                    }

                }
            }





        }

        private void NEWFOLDERToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            flagNewFolder = true;
            ListViewItem item = new ListViewItem();

            if (listView1.Focused)
            {
                listView1.LabelEdit = true;
                listView1.FocusedItem = item;
                listView1.Items.Add(item);
                pnfd = textBox1.Text;
                item.Text = newitem();
            }
            if (listView2.Focused)
            {
                listView2.LabelEdit = true;
                listView2.FocusedItem = item;
                listView2.Items.Add(item);
                pnfd = textBox2.Text;
                item.Text = newitem();
            }
            item.ImageKey = "folder";
            item.BeginEdit();
            nfd = "New Folder";
        }
        public string newitem()
        {
            string path = "";

            if (listView1.Focused)
            {
                path = textBox1.Text + nfd;
            }
            if (listView2.Focused)
            {
                path = textBox2.Text + nfd;
            }

            string result = "";
            DirectoryInfo m = new DirectoryInfo(pnfd);
            foreach (DirectoryInfo d in m.GetDirectories())
            {
                if (d.FullName == path)
                {
                    result = "New Folder" + "_" + ifd.ToString();
                    nfd = result;
                    ifd++;
                    newitem();
                }
            }
            return nfd;
        }

        private void PASTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = "";
            int i = 0;
            if (listView1.Focused)
            {
                strDestDir = textBox1.Text;
                s = textBox1.Text;
                i = 1;
            }

            if (listView2.Focused)
            {

                strDestDir = textBox2.Text;
                s = textBox2.Text;
                i = 3;
            }
            Thread t = new Thread(new ThreadStart(paste));
            t.Start();

            updateListView(textBox1.Text);
            updateListView2(textBox2.Text);
        }
        public string autochange(string s)
        {

            DirectoryInfo di = new DirectoryInfo(s);
            FileInfo fi = new FileInfo(s);
            if (di.Exists || fi.Exists)
            {
                i++;
                ac = s + "_" + i.ToString();
                autochange(ac);

            }

            return ac;

        }
        public void paste()
        {
            for (int i = 0; i < ItemName.Count; i++)
            {
                if (strDestDir.Length > 3)
                {
                    strDest = strDestDir + "\\" + ItemName[i];
                }
                else
                {
                    strDest = strDestDir + ItemName[i];
                }

                DirectoryInfo di = new DirectoryInfo(strDest);
                FileInfo fi = new FileInfo(strDest);
                if (di.Exists || fi.Exists)
                {
                    strDest = autochange(strDest);
                    i = 0;
                    ac = "";
                }

                try
                {
                    if (flagCopy[i])//copy/paste
                    {

                        if (flagDir[i])
                        {
                            Microsoft.VisualBasic.FileIO.UIOption a = Microsoft.VisualBasic.FileIO.UIOption.AllDialogs;
                            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(SourceDir[i], strDest, a);

                        }
                        else
                        {
                            Microsoft.VisualBasic.FileIO.UIOption a = Microsoft.VisualBasic.FileIO.UIOption.AllDialogs;

                            Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(SourceFile[i], strDest, a);
                        }



                    }

                    else //move/paste
                    {

                        if (flagDir[i])
                        {
                            Microsoft.VisualBasic.FileIO.UIOption a = Microsoft.VisualBasic.FileIO.UIOption.AllDialogs;
                            Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(SourceDir[i], strDest, a);

                            icopy++;

                        }
                        else
                        {
                            Microsoft.VisualBasic.FileIO.UIOption a = Microsoft.VisualBasic.FileIO.UIOption.AllDialogs;
                            Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(SourceFile[i], strDest, a);
                            icopy++;
                        }
                        //btPaste.Enabled = false;
                        //pasteToolStripMenuItem.Enabled = false;
                        //pasteToolStripMenuItem1.Enabled = false;
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }



            }
            if (icopy > 0)
            {

                initClipboard();
                flagCopy.Clear();
                flagCopy.Clear();
                SourceDir.Clear();
                SourceFile.Clear();
                ItemName.Clear();


            }


        }

        private void ListView2_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {

            {
                if (e.Label == null)
                {
                    flagRename = false;
                    if (flagNewFolder)
                    {
                        ListViewItem curr = listView2.FocusedItem;
                        e.CancelEdit = true;
                        string s = pnfd + curr.Text;
                        listView2.FocusedItem.Text = curr.Text.ToString();
                        DirectoryInfo m = Directory.CreateDirectory(s);
                        string[] l = { m.Name, "File Folder", "", m.CreationTime.ToLongDateString(), m.FullName, m.Parent.FullName };
                        listView2.FocusedItem = new ListViewItem(l);
                        listView2.FocusedItem.Tag = s;

                    }

                    flagNewFolder = false;
                    return;
                }
                else
                {
                    if (flagRename)
                    {

                        ListViewItem curr = listView2.SelectedItems[0];
                        string currpath = curr.SubItems[4].Text;
                        FileInfo fi = new FileInfo(currpath);
                        if (fi.Exists)
                        {

                            string dest = pnfd + e.Label;
                            FileInfo t = new FileInfo(dest);
                            if (t.Exists)
                            {
                                e.CancelEdit = true;
                                listView2.Refresh();

                                MessageBox.Show("Ten file da ton tai ", "error", MessageBoxButtons.OKCancel);

                                listView2.FocusedItem.SubItems[0].Text = curr.SubItems[0].Text.ToString();
                                flagRename = false;
                            }

                            else
                            {
                                if (e.Label == "")
                                {

                                    e.CancelEdit = true;
                                    listView2.FocusedItem.SubItems[0].Text = curr.SubItems[0].Text.ToString();
                                    flagRename = false;
                                }
                                else
                                {
                                    File.Move(currpath, dest);
                                    e.CancelEdit = true;
                                    listView2.FocusedItem.SubItems[0].Text = e.Label;
                                    listView2.FocusedItem.SubItems[4].Text = dest;
                                    listView2.FocusedItem.Tag = dest;
                                    flagRename = false;


                                }
                            }


                        }
                        else
                        {


                            string dest = pnfd + e.Label;
                            DirectoryInfo t = new DirectoryInfo(dest);
                            if (t.Exists)
                            {
                                MessageBox.Show("Ten file da ton tai ", "error");
                                e.CancelEdit = true;
                                listView2.FocusedItem.SubItems[0].Text = curr.SubItems[0].Text.ToString();
                                flagRename = false;
                            }

                            else
                            {
                                if (e.Label == "")
                                {

                                    e.CancelEdit = true;
                                    listView2.FocusedItem.SubItems[0].Text = curr.SubItems[0].Text.ToString();
                                    flagRename = false;

                                }
                                else
                                {

                                    Directory.Move(currpath, dest);
                                    e.CancelEdit = true;
                                    listView2.FocusedItem.SubItems[0].Text = e.Label;
                                    listView2.FocusedItem.SubItems[4].Text = dest;
                                    listView2.FocusedItem.Tag = dest;
                                    flagRename = false;


                                }
                            }

                        }
                    }

                    if (flagNewFolder)
                    {

                        ListViewItem curr = listView2.FocusedItem;
                        string currpath = pnfd + e.Label;
                        string dest = pnfd + e.Label;
                        DirectoryInfo t = new DirectoryInfo(dest);
                        if (t.Exists && e.Label != "")
                        {
                            MessageBox.Show("Ten folder da ton tai ", "error");
                            e.CancelEdit = true;
                            listView2.FocusedItem.SubItems[0].Text = curr.SubItems[0].Text.ToString();
                            listView2.FocusedItem.BeginEdit();
                        }

                        else
                        {
                            if (e.Label == "")
                            {

                                e.CancelEdit = true;
                                string s = pnfd + curr.Text;
                                listView2.FocusedItem.Text = curr.Text.ToString();
                                DirectoryInfo m = Directory.CreateDirectory(s);
                                string[] l = { m.Name, "File Folder", "", m.CreationTime.ToLongDateString(), m.FullName, m.Parent.FullName };
                                listView2.FocusedItem = new ListViewItem(l);
                                listView2.FocusedItem.Tag = s;


                            }
                            else
                            {
                                e.CancelEdit = true;
                                string s = pnfd + e.Label;
                                listView2.FocusedItem.Text = e.Label;
                                DirectoryInfo m = Directory.CreateDirectory(s);
                                string[] l = { m.Name, "File Folder", "", m.CreationTime.ToLongDateString(), m.FullName, m.Parent.FullName };
                                listView2.FocusedItem = new ListViewItem(l);
                                listView2.FocusedItem.Tag = s;

                            }
                        }

                    }
                }
            }
        }

        private void CUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initClipboard();
            //btPaste.Enabled = true;
            flagCopy.Clear();
            flagCopy.Clear();
            SourceDir.Clear();
            SourceFile.Clear();
            ItemName.Clear();
            i = 0;
            if (listView1.Focused)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    try
                    {

                        if (listView1.SelectedItems.Count > 0)//copy o listview
                        {
                            listView1.FocusedItem.ForeColor = Color.LightGray;
                            flagCopy.Add(false);

                            //item = lvItem.FocusedItem;
                            if (item.SubItems[1].Text.Trim() == "File Folder")//copy folder
                            {


                                flagDir.Add(true);
                                DirectoryInfo d = new DirectoryInfo(item.SubItems[4].Text);
                                ItemName.Add(d.Name);
                                string ks = "";
                                if (textBox1.Text.Length > 3)
                                {
                                    ks = textBox1.Text + @"\" + ItemName[ItemName.Count - 1];
                                }
                                else
                                {
                                    ks = textBox1.Text + ItemName[ItemName.Count - 1];
                                }
                                SourceDir.Add(ks);
                                SourceFile.Add("");

                            }
                            else//copy file
                            {
                                flagCopy.Add(false);
                                flagDir.Add(false);
                                FileInfo d = new FileInfo(item.SubItems[4].Text);
                                ItemName.Add(d.Name);
                                string ks = "";
                                if (textBox1.Text.Length > 3)
                                {
                                    ks = textBox1.Text + @"\" + ItemName[ItemName.Count - 1];
                                }
                                else
                                {
                                    ks = textBox1.Text + ItemName[ItemName.Count - 1];
                                }
                                SourceFile.Add(ks);
                                SourceDir.Add("");
                            }
                            //btPaste.Enabled = true;
                            listView1.FocusedItem = null;
                        }
                        else
                        {
                            MessageBox.Show("Chua chon File, Folder");
                        }
                        // pasteToolStripMenuItem.Enabled = true;
                        // pasteToolStripMenuItem1.Enabled = true;
                        pASTEToolStripMenuItem.Enabled = true;
                        pASTEToolStripMenuItem.Enabled = true;
                        listView1.Enabled = true;
                    }
                    catch { }
                }
            }
            else
            {
                foreach (ListViewItem item in listView2.SelectedItems)
                {
                    try
                    {

                        if (listView2.SelectedItems.Count > 0)//copy o listview
                        {

                            listView2.FocusedItem = item;
                            listView2.FocusedItem.ForeColor = Color.LightGray;
                            //item = lvItem2.FocusedItem;
                            if (item.SubItems[1].Text.Trim() == "File Folder")//copy folder
                            {
                                flagCopy.Add(false);
                                flagDir.Add(true);
                                DirectoryInfo d = new DirectoryInfo(item.SubItems[4].Text);
                                ItemName.Add(d.Name);
                                string ks = "";
                                if (textBox2.Text.Length > 3)
                                {
                                    ks = textBox2.Text + @"\" + ItemName[ItemName.Count - 1];
                                }
                                else
                                {
                                    ks = textBox2.Text + ItemName[ItemName.Count - 1];
                                }
                                SourceDir.Add(ks);
                                SourceFile.Add("");

                            }
                            else//copy file
                            {
                                flagCopy.Add(false);
                                flagDir.Add(false);
                                FileInfo d = new FileInfo(item.SubItems[4].Text);
                                ItemName.Add(d.Name);
                                string ks = "";
                                if (textBox2.Text.Length > 3)
                                {
                                    ks = textBox2.Text + @"\" + ItemName[ItemName.Count - 1];
                                }
                                else
                                {
                                    ks = textBox2.Text + ItemName[ItemName.Count - 1];
                                }
                                SourceFile.Add(ks);
                                SourceDir.Add("");
                            }
                            //btPaste.Enabled = true;
                            listView2.FocusedItem = null;
                        }
                        else
                        {
                            MessageBox.Show("Chua chon File, Folder");
                        }
                        // pasteToolStripMenuItem.Enabled = true;
                        // pasteToolStripMenuItem1.Enabled = true;

                    }
                    catch { }
                }

            }
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            lvDoubleclick(listView1.SelectedItems[0].Tag.ToString());
        }
        private void lvDoubleclick(string path)
        {
            FileInfo fi = new FileInfo(path);
            DriveInfo d = new DriveInfo(path);
            if (!d.IsReady)
            {
                MessageBox.Show("Drive isn't ready !", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                textBox1.Text = path;
                updateListView(path);
            }
        }


        private void ListView2_DoubleClick(object sender, EventArgs e)
        {
            lvDoubleclick2(listView2.SelectedItems[0].Tag.ToString());
        }
        private void lvDoubleclick2(string path)
        {
            FileInfo fi = new FileInfo(path);
            DriveInfo d = new DriveInfo(path);
            if (!d.IsReady)
            {
                MessageBox.Show("Drive isn't ready !", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                textBox2.Text = path;
                updateListView2(path);
            }
        }
        public void DeleteItem(ListViewItem item, ListView lv)
        {
            try
            {
                string pathItem = item.SubItems[4].Text;
                if (item.SubItems[1].Text == "File Folder")
                {
                    DirectoryInfo dir = new DirectoryInfo(pathItem);
                    if (!dir.Exists)
                    {
                        MessageBox.Show(" Drive isn't exist ");
                        return;
                    }
                    else
                    {

                        dir.Delete(true);


                    }

                }
                else
                {
                    FileInfo file = new FileInfo(pathItem);
                    if (!file.Exists)
                    {
                        MessageBox.Show(" Drive isn't exist ");
                        return;
                    }
                    else
                    {

                        file.Delete();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ex");
            }
        }
        private void DELETEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0 && listView2.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select File or Folder to delete", "Error");
            }

            if (listView1.SelectedItems.Count > 0)//delete o listview
            {
                ListViewItem item = new ListViewItem();
                //item = lvItem.FocusedItem;
                DialogResult d = MessageBox.Show("Attention! Do you really want to delete ? ", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (d == DialogResult.Yes)
                {
                    foreach (ListViewItem se in listView1.SelectedItems)
                    {
                        DeleteItem(se, listView1);
                        listView1.Items.Remove(se);
                    }

                }
                else
                {
                    return;
                }

            }
            if (listView2.SelectedItems.Count > 0)
            {
                ListViewItem item = new ListViewItem();
                //item = lvItem.FocusedItem;
                DialogResult d = MessageBox.Show("Attention! Do you really want to delete ? ", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (d == DialogResult.Yes)
                {
                    foreach (ListViewItem se in listView1.SelectedItems)
                    {
                        DeleteItem(se, listView2);
                        listView2.Items.Remove(se);
                    }

                }
                else
                {
                    return;
                }
            }
        }

        private void ListView1_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                if (this.listView1.SelectedItems.Count > 0)
                {
                    this.contextMenuStrip1.Show(listView1, e.Location);
                }
                else
                {
                    this.contextMenuStrip2.Show(listView1, e.Location);
                }
            }
        }

        private void NEWFOLDERToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            flagNewFolder = true;
            ListViewItem item = new ListViewItem();

            if (listView1.Focused)
            {
                listView1.LabelEdit = true;
                listView1.FocusedItem = item;
                listView1.Items.Add(item);
                pnfd = textBox1.Text;
                item.Text = newitem();
            }
            if (listView2.Focused)
            {
                listView2.LabelEdit = true;
                listView2.FocusedItem = item;
                listView2.Items.Add(item);
                pnfd = textBox2.Text;
                item.Text = newitem();
            }
            item.ImageKey = "folder";
            item.BeginEdit();
            nfd = "New Folder";
        }

        private void ListView2_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.listView2.SelectedItems.Count > 0)
                {
                    this.contextMenuStrip1.Show(listView2, e.Location);
                }
                else
                {
                    this.contextMenuStrip2.Show(listView2, e.Location);
                }
            }
        }
    }
}
