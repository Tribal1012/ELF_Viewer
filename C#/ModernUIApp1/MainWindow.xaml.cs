using FirstFloor.ModernUI.Windows.Controls;
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
using static System.Windows.Controls.TextBlock;
using System.IO.Compression;

using ELFView;
using System.Runtime.InteropServices;
using System.IO;

namespace ModernUIApp1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {

        HeaderClass header = new HeaderClass();
        int p32_count = 0;
        int s32_count = 0;
        int p64_count = 0;
        int s64_count = 0; // 출력 count
        int flag = 0; // 64비트인지 아닌지

        string Endian;
        string Version;
        string ELFbit;
        string OSversion;

        DateTime AccessTime;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            DropListBox.Items.Clear();
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name

            byte exit = 0x00;
            const byte bitcheck = 0x01;                             //ELF bit 확인
            byte[] ELF = { 0x7F, 0x45, 0x4C, 0x46 };                //ELF Magic Number



            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                ListBoxItem fileItem = new ListBoxItem();

                fileItem.Content = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
                fileItem.ToolTip = dlg.FileName;

                DropListBox.Items.Add(dlg.FileName);
                //MessageBox.Show(dlg.FileName);

                System.Text.ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(dlg.FileName);                 //String으로 된 파일 이름 char* 인자로 받기 위해 Sbyte로 변환

                FileStream fs = File.OpenRead(dlg.FileName);

                BinaryReader br = new BinaryReader(fs);     //바이너리로 파일 읽기
                //System.IO.File.Open(dlg.FileName, System.IO.FileMode.Open);

                FileInfo finfo = new FileInfo(dlg.FileName);
                long fsize = finfo.Length;

                byte[] data = new byte[fsize];

                data = br.ReadBytes((int)fsize);

                for (int i = 0; i < 4; i++)
                {
                    if (data[i] == ELF[i])
                    {
                        continue;
                    }
                    else
                    {
                        MessageBox.Show("This file isn't ELF file!");
                        exit = 0x01;
                        break;
                    }
                }

                if (exit == 0x01)                       //강제 종료 플래그시 강제 종료
                {
                    this.Close();
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }

                if (data[4] == bitcheck)
                {
                    //32bit input 진행
                    header.ELF32_INPUT(data);
                }
                else
                {

                    //64bit input 진행
                    flag = 1;
                    header.ELF64_INPUT(data);
                }

                AccessTime = finfo.LastAccessTime; // 최근수정시간

                MessageBox.Show("complete!");
                //fsize, data[4](ELF bit), data[5](endian 방식), data[6](ELF file version)
                //string str1 = fileinfo.Extension;확장자
                //string str2 = fileinfo.ISReadOnly;읽기 전용
                //string str3 = fileinfo.Attributes;특성
                fs.Close();


            }
        }


        private void FilesDropped(object sender, DragEventArgs e) // Drag & Drop 나중에 복붙만 하면 됨 , 절대경로 반환 필요!?
        {
            byte exit = 0x00;
            const byte bitcheck = 0x01;                             //ELF bit 확인
            byte[] ELF = { 0x7F, 0x45, 0x4C, 0x46 };                //ELF Magic Number

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                DropListBox.Items.Clear();

                string[] droppedFilePaths =
                e.Data.GetData(DataFormats.FileDrop, true) as string[];

                foreach (string droppedFilePath in droppedFilePaths)
                {
                    ListBoxItem fileItem = new ListBoxItem();

                    fileItem.Content = System.IO.Path.GetFileNameWithoutExtension(droppedFilePath);
                    fileItem.ToolTip = droppedFilePath;

                    DropListBox.Items.Add(droppedFilePath);

                    System.Text.ASCIIEncoding encoding = new ASCIIEncoding();
                    Byte[] bytes = encoding.GetBytes(droppedFilePath);                 //String으로 된 파일 이름 char* 인자로 받기 위해 Sbyte로 변환

                    FileStream fs = File.OpenRead(droppedFilePath);

                    BinaryReader br = new BinaryReader(fs);     //바이너리로 파일 읽기
                                                                //System.IO.File.Open(dlg.FileName, System.IO.FileMode.Open);

                    FileInfo finfo = new FileInfo(droppedFilePath);
                    long fsize = finfo.Length;

                    byte[] data = new byte[fsize];

                    data = br.ReadBytes((int)fsize);

                    for (int i = 0; i < 4; i++)
                    {
                        if (data[i] == ELF[i])
                        {
                            continue;
                        }
                        else
                        {
                            MessageBox.Show("This file isn't ELF file!");
                            exit = 0x01;
                            break;
                        }
                    }

                    if (exit == 0x01)                       //강제 종료 플래그시 강제 종료
                    {
                        this.Close();
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }

                    if (data[4] == bitcheck)
                    {
                        MessageBox.Show("Complete!");
                        //32bit input 진행
                        header.ELF32_INPUT(data);
                    }
                    else
                    {
                        MessageBox.Show("Complete!");
                        //64bit input 진행
                        flag = 1;
                        header.ELF64_INPUT(data);
                    }
                   
                    AccessTime = finfo.LastAccessTime; // 최근수정시간

                    MessageBox.Show("complete!");
                    //fsize, data[4](ELF bit), data[5](endian 방식), data[6](ELF file version)
                    //string str1 = fileinfo.Extension;확장자
                    //string str2 = fileinfo.ISReadOnly;읽기 전용
                    //string str3 = fileinfo.Attributes;특성
                    fs.Close();
                }

            }

        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
           
            
            if (flag == 1)
            {
                OSversion = header.ELFOSABI(header.elf64_header.magic[7]);
                ELFbit = header.ELFbitcheck(header.elf64_header.magic[4]);
                Endian = header.ELFendian(header.elf64_header.magic[5]);
                Version = header.ELFversion(header.elf64_header.magic[6]);
                

                textBox.Text = "Class : " + ELFbit + "\n\n" + "Data : " + Endian + "\n\n" + "Version : " + Version + "\n\n" + "OS/ABI : " + OSversion  + "\n\n"
                    + "LastAccessTime : " + AccessTime + "\n\n";
               
            }
            else
            {
                OSversion = header.ELFOSABI(header.elf32_header.magic[7]);
                ELFbit = header.ELFbitcheck(header.elf32_header.magic[4]);
                Endian = header.ELFendian(header.elf32_header.magic[5]);
                Version = header.ELFversion(header.elf32_header.magic[6]);


                textBox.Text = "Class : " + ELFbit + "\n\n" + "Data : " + Endian + "\n\n" + "Version : " + Version + "\n\n" + "OS/ABI : " + OSversion + "\n\n"
                   + "LastAccessTime : " + AccessTime + "\n\n";
            }


        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            short e32type = header.elf32_header.outtype(header.elf32_header.type); //32bit
            int e32version = header.elf32_header.outversion(header.elf32_header.version);
            int e32ent = header.elf32_header.outentry(header.elf32_header.entry);
            int e32pho = header.elf32_header.outphoff(header.elf32_header.phoff);
            int e32sho = header.elf32_header.outshoff(header.elf32_header.shoff);
            int e32flags = header.elf32_header.outflags(header.elf32_header.flags);
            short e32ehsize = header.elf32_header.outehsize(header.elf32_header.ehsize);
            short e32phentsize = header.elf32_header.outphsize(header.elf32_header.phentsize);
            short e32phnum = header.elf32_header.outphnum(header.elf32_header.phnum);
            short e32shentsize = header.elf32_header.outshsize(header.elf32_header.shentsize);
            short e32shnum = header.elf32_header.outshnum(header.elf32_header.shnum);
            short e32shstrndx = header.elf32_header.outshstrndx(header.elf32_header.shstrndx);
            short e32machine = header.elf32_header.outmachine(header.elf32_header.machine);

            short e64type = header.elf64_header.outtype(header.elf64_header.type); //64bit
            int e64version = header.elf64_header.outversion(header.elf64_header.version);
            long e64ent = header.elf64_header.outtype(header.elf64_header.entry);
            long e64pho = header.elf64_header.outtype(header.elf64_header.phoff);
            long e64sho = header.elf64_header.outtype(header.elf64_header.shoff);
            int e64flags = header.elf64_header.outtype(header.elf64_header.flags);
            short e64ehsize = header.elf64_header.outtype(header.elf64_header.ehsize);
            short e64phentsize = header.elf64_header.outtype(header.elf64_header.phentsize);
            short e64phnum = header.elf64_header.outtype(header.elf64_header.phnum);
            short e64shentsize = header.elf64_header.outtype(header.elf64_header.shentsize);
            short e64shnum = header.elf64_header.outtype(header.elf64_header.shnum);
            short e64shstrndx = header.elf64_header.outtype(header.elf64_header.shstrndx);
            short e64machine = header.elf64_header.outmachine(header.elf64_header.machine);

            p32_count = e32phnum;
            s32_count = e32shnum;

            p64_count = e64phnum;
            s64_count = e64shnum;

            string e32Version = e32version.ToString("X8");
            string e32Entry = e32ent.ToString("X8");
            string e32Phoff = e32pho.ToString("X8");
            string e32Shoff = e32sho.ToString("X8");
            string e32Flags = e32flags.ToString("X8");

            string e64Version = e64version.ToString("X8");
            string e64Entry = e64ent.ToString("X8");
            string e64Phoff = e64pho.ToString("X8");
            string e64Shoff = e64sho.ToString("X8");
            string e64Flags = e64flags.ToString("X8");

            string s_e32machine = header.elf32_header.elf32output_machine(e32machine);
            string s_e32ty = header.elf32_header.elf32output_type(e32type);
            string s_e32Version = string.Concat(e32Version);
            string s_e32Entry = string.Concat(e32Entry);
            string s_e32Phoff = string.Concat(e32Phoff);
            string s_e32Shoff = string.Concat(e32Shoff);
            string s_e32Flags = string.Concat(e32Flags);
            string s_e32Ehsize = string.Concat(e32ehsize);
            string s_e32Phsize = string.Concat(e32phentsize);
            string s_e32Phnum = string.Concat(e32phnum);
            string s_e32Shsize = string.Concat(e32shentsize);
            string s_e32Shnum = string.Concat(e32shnum);
            string s_e32Shstrndx = string.Concat(e32shstrndx);

            string s_e64machine = header.elf64_header.elf64output_machine(e64machine);
            string s_e64ty = header.elf64_header.elf64output_type(e64type);
            string s_e64Version = string.Concat(e64Version);
            string s_e64Entry = string.Concat(e64Entry);
            string s_e64Phoff = string.Concat(e64Phoff);
            string s_e64Shoff = string.Concat(e64Shoff);
            string s_e64Flags = string.Concat(e64Flags);
            string s_e64Ehsize = string.Concat(e64ehsize);
            string s_e64Phsize = string.Concat(e64phentsize);
            string s_e64Phnum = string.Concat(e64phnum);
            string s_e64Shsize = string.Concat(e64shentsize);
            string s_e64Shnum = string.Concat(e64shnum);
            string s_e64Shstrndx = string.Concat(e64shstrndx);




            if (flag == 1)
            {
                textBox.Text = "Type : " + s_e64ty + "\n\n" + "Machine : " + s_e64machine + "\n\n" + "Version : " + "0x" + s_e64Version + "\n\n" + "Entry point address : " + "0x" + s_e64Entry + "\n\n" + "Start of program headers : " + "0x" + s_e64Phoff + "\n\n"
                   + "Start of section headers : " + "0x" + s_e64Shoff + "\n\n" + "Flags : " + "0x" + s_e64Flags + "\n\n" + "Size of this headers : " + s_e64Ehsize + "byte" + "\n\n"
                   + "Size of program headers : " + s_e64Phsize + "byte" + "\n\n" + "Number of program headers : " + s_e64Phnum + "\n\n" + "Size of program headers : " + s_e64Shsize + "byte" + "\n\n"
                   + "Number of section headers : " + s_e64Shnum + "\n\n" + "Section header string table index : " + s_e64Shstrndx;
            }
            else
            {
                textBox.Text = "Type : " + s_e32ty + "\n\n" + "Machine : " + s_e32machine + "\n\n" + "Version : " + "0x" + s_e32Version + "\n\n" + "Entry point address : " + "0x" + s_e32Entry + "\n\n" + "Start of program headers : " + "0x" + s_e32Phoff + "\n\n"
                   + "Start of section headers : " + "0x" + s_e32Shoff + "\n\n" + "Flags : " + "0x" + s_e32Flags + "\n\n" + "Size of this headers : " + s_e32Ehsize + "byte" + "\n\n"
                   + "Size of program headers : " + s_e32Phsize + "byte" + "\n\n" + "Number of program headers : " + s_e32Phnum + "\n\n" + "Size of program headers : " + s_e32Shsize + "byte" + "\n\n"
                   + "Number of section headers : " + s_e32Shnum + "\n\n" + "Section header string table index : " + s_e32Shstrndx;
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

            textBox.Text = "Num" + "\t\t" + "Type" + "\t\t" + "Offset" + "\t\t" + "Vaddr" + "\t\t" + "Paddr" + "\t\t" + "Filesz" + "\t\t" + "Memsz" + "\t\t" + "Flags" + "\t\t\t" + "Align" + "\n";

            if (flag == 1)
            {
                for (int i = 0; i < p64_count; i++)
                {

                    int p64type = header.p64_header[i].outtype(header.p64_header[i].type);
                    long p64offset = header.p64_header[i].outoffset(header.p64_header[i].offset);
                    long p64vaddr = header.p64_header[i].outvaddr(header.p64_header[i].vaddr);
                    long p64paddr = header.p64_header[i].outpaddr(header.p64_header[i].paddr);
                    long p64filesz = header.p64_header[i].outfilesz(header.p64_header[i].filesz);
                    long p64memsz = header.p64_header[i].outmemsz(header.p64_header[i].memsz);
                    long p64flags = header.p64_header[i].outflags(header.p64_header[i].flags);
                    long p64align = header.p64_header[i].outalign(header.p64_header[i].align);

                    string p64Type = p64type.ToString("X8");
                    string p64Offset = p64offset.ToString("X8");
                    string p64Vaddr = p64vaddr.ToString("X8");
                    string p64Paddr = p64paddr.ToString("X8");
                    string p64Filesz = p64filesz.ToString("X8");
                    string p64Memsz = p64memsz.ToString("X8");
                    string p64Flags = p64flags.ToString("X8");
                    string p64Align = p64align.ToString("X8");

                    string s_p64type = header.p64_header[i].program64output_type(p64type);
                    string s_p64flags = header.p64_header[i].program64output_flags(p64flags);
                    string s_p64offset = string.Concat(p64Offset);
                    string s_p64vaddr = string.Concat(p64Vaddr);
                    string s_p64paddr = string.Concat(p64Paddr);
                    string s_p64filesz = string.Concat(p64Filesz);
                    string s_p64memsz = string.Concat(p64Memsz);
                    string s_p64align = string.Concat(p64Align);


                    textBox.Text += (i + 1) + "\t\t" + s_p64type + "\t" + "0x" + s_p64offset + "\t" + "0x" + s_p64vaddr + "\t" + "0x" + s_p64paddr + "\t" + "0x" + s_p64filesz + "\t"
                                 + "0x" + s_p64memsz + "\t" + s_p64flags + "\t" + "0x" + s_p64align + "\n";

                }
            }
            else
            {
                for (int i = 0; i < p32_count; i++)
                {

                    int p32type = header.p32_header[i].outtype(header.p32_header[i].type);
                    int p32offset = header.p32_header[i].outoffset(header.p32_header[i].offset);
                    int p32vaddr = header.p32_header[i].outvaddr(header.p32_header[i].vaddr);
                    int p32paddr = header.p32_header[i].outpaddr(header.p32_header[i].paddr);
                    int p32filesz = header.p32_header[i].outfilesz(header.p32_header[i].filesz);
                    int p32memsz = header.p32_header[i].outmemsz(header.p32_header[i].memsz);
                    int p32flags = header.p32_header[i].outflags(header.p32_header[i].flags);
                    int p32align = header.p32_header[i].outalign(header.p32_header[i].align);

                    string p32Type = p32type.ToString("X8");
                    string p32Offset = p32offset.ToString("X8");
                    string p32Vaddr = p32vaddr.ToString("X8");
                    string p32Paddr = p32paddr.ToString("X8");
                    string p32Filesz = p32filesz.ToString("X8");
                    string p32Memsz = p32memsz.ToString("X8");
                    string p32Flags = p32flags.ToString("X8");
                    string p32Align = p32align.ToString("X8");

                    string s_p32type = header.p32_header[i].program32output_type(p32type);
                    string s_p32flags = header.p32_header[i].program32output_flags(p32flags);
                    string s_p32offset = string.Concat(p32Offset);
                    string s_p32vaddr = string.Concat(p32Vaddr);
                    string s_p32paddr = string.Concat(p32Paddr);
                    string s_p32filesz = string.Concat(p32Filesz);
                    string s_p32memsz = string.Concat(p32Memsz);
                    string s_p32align = string.Concat(p32Align);


                    textBox.Text += (i + 1) + "\t\t" + s_p32type + "\t" + "0x" + s_p32offset + "\t" + "0x" + s_p32vaddr + "\t" + "0x" + s_p32paddr + "\t" + "0x" + s_p32filesz + "\t"
                                 + "0x" + s_p32memsz + "\t" + s_p32flags + "\t" + "0x" + s_p32align + "\n";


                }
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            int i;
            textBox.Text = "Num" + "\t\t" + "Name" + "\t\t\t" + "Type" + "\t\t\t" + "Addr" + "\t\t" + "Offset" + "\t\t" + "Size" + "\t\t" + "Flags" + "\t\t\t" + "Link" + "\n";

            if (flag == 1)
            {

                for (i = 0; i < s64_count; i++)
                {
                    int s64name = header.s64_header[i].outname(header.s64_header[i].name);
                    int s64type = header.s64_header[i].outtype(header.s64_header[i].type);
                    long s64addr = header.s64_header[i].outaddr(header.s64_header[i].addr);
                    long s64offset = header.s64_header[i].outoffset(header.s64_header[i].offset);
                    long s64size = header.s64_header[i].outsize(header.s64_header[i].size);
                    long s64flags = header.s64_header[i].outflags(header.s64_header[i].flags);
                    int s64link = header.s64_header[i].outlink(header.s32_header[i].link);

                    string s64Name = s64name.ToString("X8");
                    string s64Type = s64type.ToString("X8");
                    string s64Addr = s64addr.ToString("X8");
                    string s64Offset = s64offset.ToString("X8");
                    string s64Size = s64size.ToString("X8");
                    string s64Flags = s64flags.ToString("X8");
                    string s64Link = s64link.ToString("X8");

                    string s_s64name = header.s64_header[i].section64output_name(s64name);
                    string s_s64type = header.s64_header[i].section64output_type(s64type);
                    string s_s64flags = header.s64_header[i].section64output_flags(s64flags);
                    string s_s64addr = string.Concat(s64Addr);
                    string s_s64offset = string.Concat(s64Offset);
                    string s_s64size = string.Concat(s64Size);
                    string s_s64link = string.Concat(s64Link);


                    textBox.Text += (i + 1) + "\t\t" + s_s64name + "\t" + s_s64type + "\t" + "0x" + s_s64addr + "\t" + "0x" + s_s64offset + "\t"
                        + "0x" + s_s64size + "\t" + s_s64flags + "\t" + "0x" + s_s64link + "\n";


                }
            }
            else
            {

                for (i = 0; i < s32_count; i++)
                {

                    int s32name = header.s32_header[i].outname(header.s32_header[i].name);
                    int s32type = header.s32_header[i].outtype(header.s32_header[i].type);
                    int s32addr = header.s32_header[i].outaddr(header.s32_header[i].addr);
                    int s32offset = header.s32_header[i].outoffset(header.s32_header[i].offset);
                    int s32size = header.s32_header[i].outsize(header.s32_header[i].size);
                    int s32flags = header.s32_header[i].outflags(header.s32_header[i].flags);
                    int s32link = header.s32_header[i].outlink(header.s32_header[i].link);

                    string s32Name = s32name.ToString("X8");
                    string s32Type = s32type.ToString("X8");
                    string s32Addr = s32addr.ToString("X8");
                    string s32Offset = s32offset.ToString("X8");
                    string s32Size = s32size.ToString("X8");
                    string s32Flags = s32flags.ToString("X8");
                    string s32Link = s32link.ToString("X8");

                    string s_s32name = header.s32_header[i].section32output_name(s32name);
                    string s_s32type = header.s32_header[i].section32output_type(s32type);
                    string s_s32flags = header.s32_header[i].section32output_flags(s32flags);
                    string s_s32addr = string.Concat(s32Addr);
                    string s_s32offset = string.Concat(s32Offset);
                    string s_s32size = string.Concat(s32Size);
                    string s_s32link = string.Concat(s32Link);

                    textBox.Text += (i + 1) + "\t\t" + s_s32name + "\t" + s_s32type + "\t" + "0x" + s_s32addr + "\t" + "0x" + s_s32offset + "\t"
                        + "0x" + s_s32size + "\t" + s_s32flags + "\t" + "0x" + s_s32link + "\n";


                }
            }
        }
        /// <summary>
        /// ////////////////새롭게 추가하였습니다//////////////////////////////////////////
        /// </summary>
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckedHandle(sender as CheckBox);
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckedHandle(sender as CheckBox);
        }

        double BWidth;
        double BHeight;

        void CheckedHandle(CheckBox checkBox)
        {
            bool flag = checkBox.IsChecked.Value;

            if (flag == true)
            {
                BWidth = this.Width;
                BHeight = this.Height;
                //MessageBox.Show("어흥!");
                this.Height = 870;
            }
            else if (flag == false)
            {
                //MessageBox.Show("갸흥!");
                this.Height = BHeight;
            }
            else
            {
                MessageBox.Show("Error!!!!!!!!!!!");
            }
        }
        private void TreeView_Loaded(object sender, RoutedEventArgs e)
        {
            //ELF File Header Tree 구조
            TreeViewItem header32item = new TreeViewItem();
            header32item.Header = "32bit ELF File";

            //2중 Tree 구조
            TreeViewItem ELF_Header = new TreeViewItem();
            TreeViewItem Program_Header = new TreeViewItem();
            TreeViewItem Section_Header = new TreeViewItem();
            TreeViewItem Magic = new TreeViewItem();
            TreeViewItem Type = new TreeViewItem();
            TreeViewItem Machine = new TreeViewItem();
            TreeViewItem _Version = new TreeViewItem();
            TreeViewItem Entry_Point = new TreeViewItem();
            TreeViewItem Program_Header_Offset = new TreeViewItem();
            TreeViewItem Section_Header_Offset = new TreeViewItem();
            TreeViewItem Flags = new TreeViewItem();
            TreeViewItem ELF_Header_Size = new TreeViewItem();
            TreeViewItem Program_Header_Entry_Size = new TreeViewItem();
            TreeViewItem Program_Header_Number = new TreeViewItem();
            TreeViewItem Section_Header_Entry_Size = new TreeViewItem();
            TreeViewItem Section_Header_Number = new TreeViewItem();
            TreeViewItem Section_Header_String_ndx = new TreeViewItem();
            ELF_Header.Header = "32bit ELF Header";
            Magic.Header = "32bit Magic";
            Magic.ItemsSource = new string[] { "32bit Magic Number", "32bit ELF Bit", "32bit Endian", "32bit File Version", "32bit OSABI", "32bit Padding" };
            Type.Header = "32bit Type";
            Machine.Header = "32bit Machine";
            _Version.Header = "32bit Version";
            Entry_Point.Header = "32bit Entry Point";
            Program_Header_Offset.Header = "32bit Program Header Offset";
            Section_Header_Offset.Header = "32bit Section Header Offset";
            Flags.Header = "32bit Flags";
            ELF_Header_Size.Header = "32bit ELF Header Size";
            Program_Header_Entry_Size.Header = "32bit Program Header Entry Size";
            Program_Header_Number.Header = "32bit Program Header Number";
            Section_Header_Entry_Size.Header = "32bit Section Header Entry Size";
            Section_Header_Number.Header = "32bit Section Header Number";
            Section_Header_String_ndx.Header = "32bit Section Header String ndx";
            header32item.Items.Add(ELF_Header);
            ELF_Header.Items.Add(Magic);
            ELF_Header.Items.Add(Type);
            ELF_Header.Items.Add(Machine);
            ELF_Header.Items.Add(_Version);
            ELF_Header.Items.Add(Entry_Point);
            ELF_Header.Items.Add(Program_Header_Offset);
            ELF_Header.Items.Add(Section_Header_Offset);
            ELF_Header.Items.Add(Flags);
            ELF_Header.Items.Add(ELF_Header_Size);
            ELF_Header.Items.Add(Program_Header_Entry_Size);
            ELF_Header.Items.Add(Program_Header_Number);
            ELF_Header.Items.Add(Section_Header_Entry_Size);
            ELF_Header.Items.Add(Section_Header_Number);
            ELF_Header.Items.Add(Section_Header_String_ndx);
            Program_Header.Header = "32bit Program Header";
            Program_Header.ItemsSource = new string[] { "32bit PType", "32bit POffset", "32bit PVirtual Address", "32bit PPhysical Address", "32bit PFile Size", "32bit PMemory Size", "32bit PFlags", "32bit PAlign" };
            header32item.Items.Add(Program_Header);
            Section_Header.Header = "32bit Section Header";
            Section_Header.ItemsSource = new string[] { "32bit SName", "32bit SType", "32bit SAddress", "32bit SOffset", "32bit SSize", "32bit SFlags", "32bit SLink", "32bit SInformation", "32bit SAddress Align", "32bit SEntry Size" };
            header32item.Items.Add(Section_Header);

            /*64bit 구조 */
            TreeViewItem header64item = new TreeViewItem();
            header64item.Header = "64bit ELF File";

            TreeViewItem ELF_Header2 = new TreeViewItem();
            TreeViewItem Program_Header2 = new TreeViewItem();
            TreeViewItem Section_Header2 = new TreeViewItem();
            TreeViewItem Magic2 = new TreeViewItem();
            TreeViewItem Type2 = new TreeViewItem();
            TreeViewItem Machine2 = new TreeViewItem();
            TreeViewItem _Version2 = new TreeViewItem();
            TreeViewItem Entry_Point2 = new TreeViewItem();
            TreeViewItem Program_Header_Offset2 = new TreeViewItem();
            TreeViewItem Section_Header_Offset2 = new TreeViewItem();
            TreeViewItem Flags2 = new TreeViewItem();
            TreeViewItem ELF_Header_Size2 = new TreeViewItem();
            TreeViewItem Program_Header_Entry_Size2 = new TreeViewItem();
            TreeViewItem Program_Header_Number2 = new TreeViewItem();
            TreeViewItem Section_Header_Entry_Size2 = new TreeViewItem();
            TreeViewItem Section_Header_Number2 = new TreeViewItem();
            TreeViewItem Section_Header_String_ndx2 = new TreeViewItem();
            ELF_Header2.Header = "64bit ELF Header";
            Magic2.Header = "64bit Magic";
            Magic2.ItemsSource = new string[] { "64bit Magic Number", "64bit ELF Bit", "64bit Endian", "64bit File Version", "64bit OSABI", "64bit Padding" };
            Type2.Header = "64bit Type";
            Machine2.Header = "64bit Machine";
            _Version2.Header = "64bit Version";
            Entry_Point2.Header = "64bit Entry Point";
            Program_Header_Offset2.Header = "64bit Program Header Offset";
            Section_Header_Offset2.Header = "64bit Section Header Offset";
            Flags2.Header = "64bit Flags";
            ELF_Header_Size2.Header = "64bit ELF Header Size";
            Program_Header_Entry_Size2.Header = "64bit Program Header Entry Size";
            Program_Header_Number2.Header = "64bit Program Header Number";
            Section_Header_Entry_Size2.Header = "64bit Section Header Entry Size";
            Section_Header_Number2.Header = "64bit Section Header Number";
            Section_Header_String_ndx2.Header = "64bit Section Header String ndx";
            header64item.Items.Add(ELF_Header2);
            ELF_Header2.Items.Add(Magic2);
            ELF_Header2.Items.Add(Type2);
            ELF_Header2.Items.Add(Machine2);
            ELF_Header2.Items.Add(_Version2);
            ELF_Header2.Items.Add(Entry_Point2);
            ELF_Header2.Items.Add(Program_Header_Offset2);
            ELF_Header2.Items.Add(Section_Header_Offset2);
            ELF_Header2.Items.Add(Flags2);
            ELF_Header2.Items.Add(ELF_Header_Size2);
            ELF_Header2.Items.Add(Program_Header_Entry_Size2);
            ELF_Header2.Items.Add(Program_Header_Number2);
            ELF_Header2.Items.Add(Section_Header_Entry_Size2);
            ELF_Header2.Items.Add(Section_Header_Number2);
            ELF_Header2.Items.Add(Section_Header_String_ndx2);
            Program_Header2.Header = "64bit Program Header";
            Program_Header2.ItemsSource = new string[] { "64bit PType", "64bit PFlags", "64bit POffset", "64bit PVirtual Address", "64bit PPhysical Address", "64bit PFile Size", "64bit PMemory Size", "64bit PAlign" };
            header64item.Items.Add(Program_Header2);
            Section_Header2.Header = "64bit Section Header";
            Section_Header2.ItemsSource = new string[] { "64bit SName", "64bit SType", "64bit SAddress", "64bit SOffset", "64bit SSize", "64bit SFlags", "64bit SLink", "64bit SInformation", "64bit SAddress Align", "64bit SEntry Size" };
            header64item.Items.Add(Section_Header2);

            // ... Get TreeView reference and add both items.
            var tree = sender as TreeView;
            var tree2 = sender as TreeView;
            tree.Items.Add(header32item);
            tree.Items.Add(header64item);

        }

        private void TreeView_SelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            var tree = sender as TreeView;
            short p32num = header.elf32_header.outphnum(header.elf32_header.phnum);
            short s32num = header.elf32_header.outshnum(header.elf32_header.shnum);
            short p64num = header.elf32_header.outphnum(header.elf64_header.phnum);
            short s64num = header.elf32_header.outshnum(header.elf64_header.shnum);

            // ... Determine type of SelectedItem.                  32bit일 때와 64bit의 경우가 나눠지므로 bit 확인을 위해 나눠지는 조건문 필요
            if (tree.SelectedItem is TreeViewItem)
            {
                // ... Handle a TreeViewItem.
                var item = tree.SelectedItem as TreeViewItem;
                switch (item.Header.ToString())
                {
                    case "32bit ELF File":
                        this.StextBlock.Text = "In computing, the Executable and Linkable Format (ELF, formerly called Extensible Linking Format) is a common standard file format for executables, object code, shared libraries, and core dumps.";
                        this.PtextBlock.Text = item.Header.ToString();
                        break;
                    case "32bit ELF Header":
                        this.StextBlock.Text = "Define Program Header and Section Header of File Location";
                        this.PtextBlock.Text = item.Header.ToString();
                        break;
                    case "32bit Program Header":
                        this.StextBlock.Text = "Program Header Write array of many struct which write a lot of segment";
                        this.PtextBlock.Text = item.Header.ToString();
                        break;
                    case "32bit Section Header":
                        this.StextBlock.Text = "Every Section have Section Header of itself in object file";
                        this.PtextBlock.Text = item.Header.ToString();
                        break;
                    case "32bit Magic":
                        this.StextBlock.Text = "ELF identification";
                        this.PtextBlock.Text = "Magic" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < 16; i++)
                        {
                            this.PtextBlock.Text += header.elf32_header.magic[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t";
                        for (int i = 0; i < 16; i++)
                        {
                            if (header.elf32_header.magic[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.magic[i]).ToString();
                            }
                        }
                        break;
                    case "32bit Type":
                        this.StextBlock.Text = "File Type";
                        this.PtextBlock.Text = "Type" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.type[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf32_header.type[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.type[i]).ToString();
                            }
                        }
                        break;
                    case "32bit Machine":
                        this.StextBlock.Text = "OS Machine";
                        this.PtextBlock.Text = "Machine" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.machine[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf32_header.machine[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.machine[i]).ToString();
                            }
                        }
                        break;
                    case "32bit Version":
                        this.StextBlock.Text = "Object File Version";
                        this.PtextBlock.Text = "Version" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 3; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.version[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t";
                        for (int i = 0; i < 4; i++)
                        {
                            if (header.elf32_header.version[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.version[i]).ToString();
                            }
                        }
                        break;
                    case "32bit Entry Point":
                        this.StextBlock.Text = "Virtual Address of Entry Point";
                        this.PtextBlock.Text = "Entry Point" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 3; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.entry[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t";
                        for (int i = 0; i < 4; i++)
                        {
                            if (header.elf32_header.entry[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.entry[i]).ToString();
                            }
                        }
                        break;
                    case "32bit Program Header Offset":
                        this.StextBlock.Text = "File Offset of The Program Header";
                        this.PtextBlock.Text = "Program Header Offset" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 3; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.phoff[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t";
                        for (int i = 0; i < 4; i++)
                        {
                            if (header.elf32_header.phoff[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.phoff[i]).ToString();
                            }
                        }
                        break;
                    case "32bit Section Header Offset":
                        this.StextBlock.Text = "File Offset of The Section Header";
                        this.PtextBlock.Text = "Section Header Offset" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 3; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.shoff[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t";
                        for (int i = 0; i < 4; i++)
                        {
                            if (header.elf32_header.shoff[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.shoff[i]).ToString();
                            }
                        }
                        break;
                    case "32bit Flags":
                        this.StextBlock.Text = "Arch Specific Flags";
                        this.PtextBlock.Text = " Flags" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 3; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.flags[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t";
                        for (int i = 0; i < 4; i++)
                        {
                            if (header.elf32_header.flags[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.flags[i]).ToString();
                            }
                        }
                        break;
                    case "32bit ELF Header Size":
                        this.StextBlock.Text = "ELF Header Size";
                        this.PtextBlock.Text = "ELF Header Size" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.ehsize[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf32_header.ehsize[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.ehsize[i]).ToString();
                            }
                        }
                        break;
                    case "32bit Program Header Entry Size":
                        this.StextBlock.Text = "Size of One Entry in the Program Header";
                        this.PtextBlock.Text = "Program Header Entry Size" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.phentsize[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf32_header.phentsize[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.phentsize[i]).ToString();
                            }
                        }
                        break;
                    case "32bit Program Header Number":
                        this.StextBlock.Text = "Number of Entries in the Program Header";
                        this.PtextBlock.Text = "Program Header Number" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.phnum[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf32_header.phnum[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.phnum[i]).ToString();
                            }
                        }
                        break;
                    case "32bit Section Header Entry Size":
                        this.StextBlock.Text = "Size of One Entry in the Section Header";
                        this.PtextBlock.Text = "Section Header Entry Size" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.shentsize[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf32_header.shentsize[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.shentsize[i]).ToString();
                            }
                        }
                        break;
                    case "32bit Section Header Number":
                        this.StextBlock.Text = "Size of One Entry in the Section Header";
                        this.PtextBlock.Text = "Section Header Number" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.shnum[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf32_header.shnum[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.shnum[i]).ToString();
                            }
                        }
                        break;
                    case "32bit Section Header String ndx":
                        this.StextBlock.Text = "Section Header Table idx of the Section Name String Table";
                        this.PtextBlock.Text = "Section Header String ndx" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf32_header.shstrndx[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf32_header.shstrndx[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.shstrndx[i]).ToString();
                            }
                        }
                        break;
                    case "64bit ELF File":
                        this.StextBlock.Text = "In computing, the Executable and Linkable Format (ELF, formerly called Extensible Linking Format) is a common standard file format for executables, object code, shared libraries, and core dumps.";
                        this.PtextBlock.Text = item.Header.ToString();
                        break;
                    case "64bit ELF Header":
                        this.StextBlock.Text = "Define Program Header and Section Header of File Location";
                        this.PtextBlock.Text = item.Header.ToString();
                        break;
                    case "64bit Program Header":
                        this.StextBlock.Text = "Program Header Write array of many struct which write a lot of segment";
                        this.PtextBlock.Text = item.Header.ToString();
                        break;
                    case "64bit Section Header":
                        this.StextBlock.Text = "Every Section have Section Header of itself in object file";
                        this.PtextBlock.Text = item.Header.ToString();
                        break;
                    case "64bit Magic":
                        this.StextBlock.Text = "ELF identification";
                        this.PtextBlock.Text = "Magic" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < 16; i++)
                        {
                            this.PtextBlock.Text += header.elf64_header.magic[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t";
                        for (int i = 0; i < 16; i++)
                        {
                            if (header.elf64_header.magic[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.magic[i]).ToString();
                            }
                        }
                        break;
                    case "64bit Type":
                        this.StextBlock.Text = "File Type";
                        this.PtextBlock.Text = "Type" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.type[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf64_header.type[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.type[i]).ToString();
                            }
                        }
                        break;
                    case "64bit Machine":
                        this.StextBlock.Text = "OS Machine";
                        this.PtextBlock.Text = "Machine" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.machine[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf64_header.machine[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.machine[i]).ToString();
                            }
                        }
                        break;
                    case "64bit Version":
                        this.StextBlock.Text = "Object File Version";
                        this.PtextBlock.Text = "Version" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 3; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.version[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t";
                        for (int i = 0; i < 4; i++)
                        {
                            if (header.elf64_header.version[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.version[i]).ToString();
                            }
                        }
                        break;
                    case "64bit Entry Point":
                        this.StextBlock.Text = "Virtual Address of Entry Point";
                        this.PtextBlock.Text = "Entry Point" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 7; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.entry[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t";
                        for (int i = 0; i < 8; i++)
                        {
                            if (header.elf64_header.entry[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.entry[i]).ToString();
                            }
                        }
                        break;
                    case "64bit Program Header Offset":
                        this.StextBlock.Text = "File Offset of The Program Header";
                        this.PtextBlock.Text = "Program Header Offset" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 7; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.phoff[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t";
                        for (int i = 0; i < 8; i++)
                        {
                            if (header.elf64_header.phoff[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.phoff[i]).ToString();
                            }
                        }
                        break;
                    case "64bit Section Header Offset":
                        this.StextBlock.Text = "File Offset of The Section Header";
                        this.PtextBlock.Text = "Section Header Offset" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 7; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.shoff[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t";
                        for (int i = 0; i < 8; i++)
                        {
                            if (header.elf64_header.shoff[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.shoff[i]).ToString();
                            }
                        }
                        break;
                    case "64bit Flags":
                        this.StextBlock.Text = "Arch Specific Flags";
                        this.PtextBlock.Text = "Flags" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 3; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.flags[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t";
                        for (int i = 0; i < 4; i++)
                        {
                            if (header.elf64_header.flags[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.flags[i]).ToString();
                            }
                        }
                        break;
                    case "64bit ELF Header Size":
                        this.StextBlock.Text = "ELF Header Size";
                        this.PtextBlock.Text = "ELF Header Size" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.ehsize[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf64_header.ehsize[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.ehsize[i]).ToString();
                            }
                        }
                        break;
                    case "64bit Program Header Entry Size":
                        this.StextBlock.Text = "Size of One Entry in the Program Header";
                        this.PtextBlock.Text = "Program Header Entry Size" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.phentsize[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf64_header.phentsize[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.phentsize[i]).ToString();
                            }
                        }
                        break;
                    case "64bit Program Header Number":
                        this.StextBlock.Text = "Number of Entries in the Program Header";
                        this.PtextBlock.Text = "Program Header Number" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.phnum[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf64_header.phnum[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.phnum[i]).ToString();
                            }
                        }
                        break;
                    case "64bit Section Header Entry Size":
                        this.StextBlock.Text = "Size of One Entry in the Section Header";
                        this.PtextBlock.Text = "Section Header Entry Size" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.shentsize[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf64_header.shentsize[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.shentsize[i]).ToString();
                            }
                        }
                        break;
                    case "64bit Section Header Number":
                        this.StextBlock.Text = "Size of One Entry in the Section Header";
                        this.PtextBlock.Text = "Section Header Number" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.shnum[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf64_header.shnum[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.shnum[i]).ToString();
                            }
                        }
                        break;
                    case "64bit Section Header String ndx":
                        this.StextBlock.Text = "Section Header Table idx of the Section Name String Table";
                        this.PtextBlock.Text = "Section Header Number" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 1; i >= 0; i--)
                        {
                            this.PtextBlock.Text += header.elf64_header.shstrndx[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t\t";
                        for (int i = 0; i < 2; i++)
                        {
                            if (header.elf64_header.shstrndx[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.shstrndx[i]).ToString();
                            }
                        }
                        break;
                    default:
                        MessageBox.Show("TreeView Error!");
                        break;
                }
                //this.PtextBlock.Text = "Selected header: " + item.Header.ToString();
            }
            else if (tree.SelectedItem is string)
            {
                // ... Handle a string.
                switch (tree.SelectedItem.ToString())
                {
                    case "32bit Magic Number":
                        this.StextBlock.Text = "This Hex Value define ELF Format in ELF File";
                        this.PtextBlock.Text = "Magic Number" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < 4; i++)
                        {
                            this.PtextBlock.Text += header.elf32_header.magic[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t";
                        for (int i = 0; i < 4; i++)
                        {
                            if (header.elf32_header.magic[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.magic[i]).ToString();
                            }
                        }
                        break;
                    case "32bit ELF Bit":
                        this.StextBlock.Text = "This Hex Value request OS bit for Excuting ELF File";
                        this.PtextBlock.Text = "ELF Bit" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        this.PtextBlock.Text += header.elf32_header.magic[5].ToString("X2") + "\t\t\t\t\t\t";
                        if (header.elf32_header.magic[5] <= 32)
                        {
                            this.PtextBlock.Text += ". ";
                        }
                        else
                        {
                            this.PtextBlock.Text += Convert.ToChar(header.elf32_header.magic[5]).ToString();
                        }
                        break;
                    case "32bit Endian":
                        this.StextBlock.Text = "This Hex Value define OS Endian struct";
                        this.PtextBlock.Text = "Endian" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        this.PtextBlock.Text += header.elf32_header.magic[6].ToString("X2") + "\t\t\t\t\t\t";
                        if (header.elf32_header.magic[6] <= 32)
                        {
                            this.PtextBlock.Text += ". ";
                        }
                        else
                        {
                            this.PtextBlock.Text += Convert.ToChar(header.elf32_header.magic[6]).ToString();
                        }
                        break;
                    case "32bit File Version":
                        this.StextBlock.Text = "This Hex Value is ELF File Version Notification";
                        this.PtextBlock.Text = "File Version" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        this.PtextBlock.Text += header.elf32_header.magic[7].ToString("X2") + "\t\t\t\t\t\t";
                        if (header.elf32_header.magic[7] <= 32)
                        {
                            this.PtextBlock.Text += ". ";
                        }
                        else
                        {
                            this.PtextBlock.Text += Convert.ToChar(header.elf32_header.magic[7]).ToString();
                        }
                        break;
                    case "32bit OSABI":
                        this.StextBlock.Text = "This Hex Value is OS/ABI for ELF File";
                        this.PtextBlock.Text = "OS/ABI" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        this.PtextBlock.Text += header.elf32_header.magic[8].ToString("X2") + "\t\t\t\t\t\t";
                        if (header.elf32_header.magic[8] <= 32)
                        {
                            this.PtextBlock.Text += ". ";
                        }
                        else
                        {
                            this.PtextBlock.Text += Convert.ToChar(header.elf32_header.magic[8]).ToString();
                        }
                        break;
                    case "32bit Padding":
                        this.StextBlock.Text = "This is Padding";
                        this.PtextBlock.Text = "Padding" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 9; i <= 15; i++)
                        {
                            this.PtextBlock.Text += header.elf32_header.magic[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t";
                        for (int i = 9; i < 16; i++)
                        {
                            if (header.elf32_header.magic[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf32_header.magic[i]).ToString();
                            }
                        }
                        break;
                    case "32bit PType":
                        this.StextBlock.Text = "Type of the Segment";
                        this.PtextBlock.Text = "PType" + "\n\n" + "Num" + "\t\t"+ "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p32num; i++)
                        {
                            this.PtextBlock.Text += (i+1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.p32_header[i].type[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.p32_header[i].type[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p32_header[i].type[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit PFlags":
                        this.StextBlock.Text = "Flags relevant to the Segment";
                        this.PtextBlock.Text = "PFlags" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.p32_header[i].flags[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.p32_header[i].flags[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p32_header[i].flags[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit POffset":
                        this.StextBlock.Text = "The Offset from the beginning of the file at which the first byte of the Segment reside";
                        this.PtextBlock.Text = "POffset" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.p32_header[i].offset[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.p32_header[i].offset[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p32_header[i].offset[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit PVirtual Address":
                        this.StextBlock.Text = "The Virtual Address at which the first byte of the Segmet reside in Memory";
                        this.PtextBlock.Text = "PVirtual Address" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.p32_header[i].vaddr[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.p32_header[i].vaddr[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p32_header[i].paddr[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit PPhysical Address":
                        this.StextBlock.Text = "Physical Address of the Segment";
                        this.PtextBlock.Text = "PPhysical Address" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.p32_header[i].paddr[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.p32_header[i].paddr[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p32_header[i].paddr[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit PFile Size":
                        this.StextBlock.Text = "The Number of bytes in the file image of the segment";
                        this.PtextBlock.Text = "PPhysical Address" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.p32_header[i].filesz[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.p32_header[i].filesz[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p32_header[i].filesz[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit PMemory Size":
                        this.StextBlock.Text = "The Number of bytes in the Memory image of the segment";
                        this.PtextBlock.Text = "PMemory Size" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.p32_header[i].memsz[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.p32_header[i].memsz[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p32_header[i].memsz[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit PAlign":
                        this.StextBlock.Text = "Address Alignment";
                        this.PtextBlock.Text = "PAlign" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.p32_header[i].align[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.p32_header[i].align[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p32_header[i].align[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit SName":
                        this.StextBlock.Text = "Name of the Section";
                        this.PtextBlock.Text = "SName" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s32_header[i].name[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s32_header[i].name[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s32_header[i].name[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit SType":
                        this.StextBlock.Text = "Categorizes the Section's Contents and semantics";
                        this.PtextBlock.Text = "SType" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s32_header[i].type[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s32_header[i].type[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s32_header[i].type[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit SFlags":
                        this.StextBlock.Text = "Section Attribute (bit flags)";
                        this.PtextBlock.Text = "SFlags" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s32_header[i].flags[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s32_header[i].flags[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s32_header[i].flags[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit SAddress":
                        this.StextBlock.Text = "Address at which the Section's first byte should reside";
                        this.PtextBlock.Text = "SAddress" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s32_header[i].addr[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s32_header[i].addr[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s32_header[i].addr[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit SOffset":
                        this.StextBlock.Text = "File Offset of the Section in the file";
                        this.PtextBlock.Text = "SOffset" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s32_header[i].offset[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s32_header[i].offset[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s32_header[i].offset[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit SSize":
                        this.StextBlock.Text = "Section Size";
                        this.PtextBlock.Text = "SSize" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s32_header[i].size[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s32_header[i].size[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s32_header[i].size[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit SLink":
                        this.StextBlock.Text = "Section Header Table index link";
                        this.PtextBlock.Text = "SLink" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s32_header[i].link[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s32_header[i].link[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s32_header[i].link[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit SInformation":
                        this.StextBlock.Text = "extra information";
                        this.PtextBlock.Text = "SInformation" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s32_header[i].info[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s32_header[i].info[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s32_header[i].info[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break; 
                    case "32bit SAddress Align":
                        this.StextBlock.Text = "Address Alignment";
                        this.PtextBlock.Text = "SAddress Align" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s32_header[i].addralign[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s32_header[i].addralign[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s32_header[i].addralign[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "32bit SEntry Size":
                        this.StextBlock.Text = "Size of Entries in the Section if it holds a table of fixed-size entries";
                        this.PtextBlock.Text = "SEntry Size" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s32num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s32_header[i].entsize[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s32_header[i].entsize[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s32_header[i].entsize[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit Magic Number":
                        this.StextBlock.Text = "This Hex Value define ELF Format in ELF File";
                        this.PtextBlock.Text = "Magic Number" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < 4; i++)
                        {
                            this.PtextBlock.Text += header.elf64_header.magic[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t\t";
                        for (int i = 0; i < 4; i++)
                        {
                            if (header.elf64_header.magic[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.magic[i]).ToString();
                            }
                        }
                        break;
                    case "64bit ELF Bit":
                        this.StextBlock.Text = "This Hex Value request OS bit for Excuting ELF File";
                        this.PtextBlock.Text = "ELF Bit" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        this.PtextBlock.Text += header.elf64_header.magic[5].ToString("X2") + "\t\t\t\t\t\t";
                        if (header.elf64_header.magic[6] <= 32)
                        {
                            this.PtextBlock.Text += ". ";
                        }
                        else
                        {
                            this.PtextBlock.Text += Convert.ToChar(header.elf64_header.magic[5]).ToString();
                        }
                        break;
                    case "64bit Endian":
                        this.StextBlock.Text = "This Hex Value define OS Endian struct";
                        this.PtextBlock.Text = "Endian" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        this.PtextBlock.Text += header.elf64_header.magic[6].ToString("X2") + "\t\t\t\t\t\t";
                        if (header.elf64_header.magic[6] <= 32)
                        {
                            this.PtextBlock.Text += ". ";
                        }
                        else
                        {
                            this.PtextBlock.Text += Convert.ToChar(header.elf64_header.magic[6]).ToString(); 
                        }
                        break;
                    case "64bit File Version":
                        this.StextBlock.Text = "This Hex Value is ELF File Version Notification";
                        this.PtextBlock.Text = "File Version" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        this.PtextBlock.Text += header.elf64_header.magic[7].ToString("X2") + "\t\t\t\t\t\t";
                        if (header.elf64_header.magic[7] <= 32)
                        {
                            this.PtextBlock.Text += ". ";
                        }
                        else
                        {
                            this.PtextBlock.Text += Convert.ToChar(header.elf64_header.magic[7]).ToString();
                        }
                            break;
                    case "64bit OSABI":
                        this.StextBlock.Text = "This Hex Value is OS/ABI for ELF File";
                        this.PtextBlock.Text = "OS/ABI" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";

                        this.PtextBlock.Text += header.elf64_header.magic[8].ToString("X2") + "\t\t\t\t\t\t";
                        if (header.elf64_header.magic[8] <= 32)
                        {
                            this.PtextBlock.Text += ". ";
                        }
                        else
                        {
                            this.PtextBlock.Text += Convert.ToChar(header.elf64_header.magic[8]).ToString();
                        }
                        break;
                    case "64bit Padding":
                        this.StextBlock.Text = "This is Padding";
                        this.PtextBlock.Text = "Padding" + "\n\n" + "Hex" + "\t\t\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 9; i <= 15; i++)
                        {
                            this.PtextBlock.Text += header.elf64_header.magic[i].ToString("X2") + " ";
                        }
                        this.PtextBlock.Text += "\t\t\t\t";
                        for (int i = 9; i < 16; i++)
                        {
                            if (header.elf64_header.magic[i] <= 32)
                            {
                                this.PtextBlock.Text += ". ";
                            }
                            else
                            {
                                this.PtextBlock.Text += Convert.ToChar(header.elf64_header.magic[i]).ToString();
                            }
                        }
                        break;
                    case "64bit PType":
                        this.StextBlock.Text = "Type of the Segment";
                        this.PtextBlock.Text = "PType" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.p64_header[i].type[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.p64_header[i].type[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p64_header[i].type[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit PFlags":
                        this.StextBlock.Text = "Flags relevant to the Segment";
                        this.PtextBlock.Text = "PFlags" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.p64_header[i].flags[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.p64_header[i].flags[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p64_header[i].flags[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit POffset":
                        this.StextBlock.Text = "The Offset from the beginning of the file at which the first byte of the Segment reside";
                        this.PtextBlock.Text = "POffset" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                this.PtextBlock.Text += header.p64_header[i].offset[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                if (header.p64_header[i].offset[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p64_header[i].offset[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit PVirtual Address":
                        this.StextBlock.Text = "The Virtual Address at which the first byte of the Segmet reside in Memory";
                        this.PtextBlock.Text = "PVirtual Address" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                this.PtextBlock.Text += header.p64_header[i].vaddr[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                if (header.p64_header[i].vaddr[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p64_header[i].vaddr[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit PPhysical Address":
                        this.StextBlock.Text = "Physical Address of the Segment";
                        this.PtextBlock.Text = "PPhysical Address" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                this.PtextBlock.Text += header.p64_header[i].paddr[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                if (header.p64_header[i].paddr[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p64_header[i].paddr[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit PFile Size":
                        this.StextBlock.Text = "The Number of bytes in the file image of the segment";
                        this.PtextBlock.Text = "PFile Size" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                this.PtextBlock.Text += header.p64_header[i].filesz[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                if (header.p64_header[i].filesz[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p64_header[i].filesz[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit PMemory Size":
                        this.StextBlock.Text = "The Number of bytes in the Memory image of the segment";
                        this.PtextBlock.Text = "PMemory Size" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                this.PtextBlock.Text += header.p64_header[i].memsz[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                if (header.p64_header[i].memsz[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p64_header[i].memsz[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit PAlign":
                        this.StextBlock.Text = "Address Alignment";
                        this.PtextBlock.Text = "PAlign" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < p64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                this.PtextBlock.Text += header.p64_header[i].align[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                if (header.p64_header[i].align[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.p64_header[i].align[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit SName":
                        this.StextBlock.Text = "Name of the Section";
                        this.PtextBlock.Text = "SName" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s64_header[i].name[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s64_header[i].name[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s64_header[i].name[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit SType":
                        this.StextBlock.Text = "Categorizes the Section's Contents and semantics";
                        this.PtextBlock.Text = "SType" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s64_header[i].type[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s64_header[i].type[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s64_header[i].type[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit SFlags":
                        this.StextBlock.Text = "Section Attribute (bit flags)";
                        this.PtextBlock.Text = "SFlags" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                this.PtextBlock.Text += header.s64_header[i].flags[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                if (header.s64_header[i].flags[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s64_header[i].flags[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit SAddress":
                        this.StextBlock.Text = "Address at which the Section's first byte should reside";
                        this.PtextBlock.Text = "SAddress" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                this.PtextBlock.Text += header.s64_header[i].addr[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                if (header.s64_header[i].addr[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s64_header[i].addr[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit SOffset":
                        this.StextBlock.Text = "File Offset of the Section in the file";
                        this.PtextBlock.Text = "SOffset" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                this.PtextBlock.Text += header.s64_header[i].offset[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                if (header.s64_header[i].offset[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s64_header[i].offset[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit SSize":
                        this.StextBlock.Text = "Section Size";
                        this.PtextBlock.Text = "SSize" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                this.PtextBlock.Text += header.s64_header[i].size[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                if (header.s64_header[i].size[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s64_header[i].size[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit SLink":
                        this.StextBlock.Text = "Section Header Table index link";
                        this.PtextBlock.Text = "SLink" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s64_header[i].link[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s64_header[i].link[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s64_header[i].link[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit SInformation":
                        this.StextBlock.Text = "extra information";
                        this.PtextBlock.Text = "SInformation" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                this.PtextBlock.Text += header.s64_header[i].info[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t\t";
                            for (int j = 0; j < 4; j++)
                            {
                                if (header.s64_header[i].info[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s64_header[i].info[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit SAddress Align":
                        this.StextBlock.Text = "Address Alignment";
                        this.PtextBlock.Text = "SAddress Align" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                this.PtextBlock.Text += header.s64_header[i].addralign[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                if (header.s64_header[i].addralign[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s64_header[i].addralign[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    case "64bit SEntry Size":
                        this.StextBlock.Text = "Size of Entries in the Section if it holds a table of fixed-size entries";
                        this.PtextBlock.Text = "SEntry Size" + "\n\n" + "Num" + "\t\t" + "Hex" + "\t\t\t\t" + "ASCII" + "\n";
                        for (int i = 0; i < s64num; i++)
                        {
                            this.PtextBlock.Text += (i + 1) + "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                this.PtextBlock.Text += header.s64_header[i].entsize[j].ToString("X2") + " ";
                            }
                            this.PtextBlock.Text += "\t\t";
                            for (int j = 0; j < 8; j++)
                            {
                                if (header.s64_header[i].entsize[j] <= 32)
                                {
                                    this.PtextBlock.Text += ". ";
                                }
                                else
                                {
                                    this.PtextBlock.Text += Convert.ToChar(header.s64_header[i].entsize[j]).ToString();
                                }
                            }
                            this.PtextBlock.Text += "\n";
                        }
                        break;
                    default:
                        MessageBox.Show("TreeView Error!");
                        break;
                        //this.PtextBlock.Text = "Selected: " + tree.SelectedItem.ToString();
                }
            }
        }
    }
}
