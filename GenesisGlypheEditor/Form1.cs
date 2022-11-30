using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenesisGlypheEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InstalledFontCollection installedFontCollection = new InstalledFontCollection();
            FontFamily[] families = installedFontCollection.Families;

            foreach (var item in families)
            {
                this.comboBox1.Items.Add(item.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String glyphes = this.textBox5.Text;
            int glypheSize = int.Parse(this.textBox2.Text);
            int lines = glyphes.Length / 10;
            int imageWidth = 10 * glypheSize;
            int imageHeight = (lines + 1) * glypheSize;
            Bitmap fontAtlas = new Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(fontAtlas);


            Console.WriteLine(lines);

            int i = 0;
            int line = 0;
            foreach(var c in glyphes)
            {
                int x = i * glypheSize;
                int y = line * glypheSize;

                g.DrawImage(RenderGlyphe(c.ToString(), glypheSize, Color.Black), new Point(x, y));
                if(i == 9)
                {
                    line++;
                    i = 0;
                }
                else
                {
                    i++;
                }
            }

            this.pictureBox1.Image = fontAtlas;
        }

        private Bitmap RenderGlyphe(String glypheValue, int glypeSize, Color color)
        {
            System.Drawing.Font font = new System.Drawing.Font(this.comboBox1.Text, int.Parse(this.textBox1.Text));
            Bitmap glyphe = new Bitmap(glypeSize, glypeSize);
            Graphics g = Graphics.FromImage(glyphe);

            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            //g.FillRectangle(new SolidBrush(Color.Red), new RectangleF(0, 0, 256, 256));
            g.DrawString(glypheValue, font, new SolidBrush(color), new RectangleF(0, 0, glypeSize, glypeSize), sf);

            return glyphe;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(this.pictureBox1.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Image Files | *.png";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.pictureBox1.Image.Save(saveFileDialog.FileName);
                }
            }
        }

        public Font CreateFont()
        {
            String glyphes = this.textBox5.Text;
            int glypheSize = int.Parse(this.textBox2.Text);
            int lines = glyphes.Length / 10;
            int imageWidth = 10 * glypheSize;
            int imageHeight = (lines + 1) * glypheSize;
            Bitmap fontAtlas = new Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(fontAtlas);

            Font font = new Font();
            font.LetterSpacing = float.Parse(textBox4.Text);
            font.GlypheWidth = int.Parse(textBox2.Text);
            font.GlypheHeight = int.Parse(textBox2.Text);
            font.Name = comboBox1.Text;
            font.Columns = 9;
            font.Rows = lines;

            int i = 0;
            int line = 0;
            foreach (var c in glyphes)
            {
                int x = i * glypheSize;
                int y = line * glypheSize;

                g.DrawImage(RenderGlyphe(c.ToString(), glypheSize, Color.White), new Point(x, y));
                font.Glyphes.Add(new Glyphe(c, i, line));

                if (i == 9)
                {
                    line++;
                    i = 0;
                }
                else
                {
                    i++;
                }
            }

            font.FontAtlas = fontAtlas;
            return font;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Font font = CreateFont();
            this.pictureBox1.Image = font.FontAtlas;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = this.comboBox1.Text;
            saveFileDialog.Filter = "Genesis Font Files | *.gff";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                font.Save(saveFileDialog.FileName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
