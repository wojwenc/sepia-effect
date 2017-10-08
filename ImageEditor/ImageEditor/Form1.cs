using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEditor {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void pictureBox1_Click(object sender, EventArgs e) {

        }


        Image img;
        bool loaded = false;

        void loadImg() {
            try {
                if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                    img = Image.FromFile(openFileDialog1.FileName);
                    pictureBox1.Image = img;
                    loaded = true;
                }
            } catch(Exception ex) {
                MessageBox.Show("Wrong file format");
            }
        }





        void saveImg() {
            if (loaded) {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
                saveFileDialog1.Title = "Save an Image File";
                saveFileDialog1.ShowDialog();

                if (saveFileDialog1.FileName != "") {
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                    switch (saveFileDialog1.FilterIndex) {
                        case 1:
                            pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                        case 2:
                            pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case 3:
                            pictureBox1.Image.Save(fs,
                               System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                    }

                    fs.Close();
                }
            } else { MessageBox.Show("You need to load an image first"); }
        }



        void sepia() {
            if (!loaded) {
                MessageBox.Show("You need to load an image first");
            } else {

                Image img = pictureBox1.Image;                            
                Bitmap bmpInverted = new Bitmap(img.Width, img.Height);   

                ImageAttributes ia = new ImageAttributes();               
                ColorMatrix cmPicture = new ColorMatrix(new float[][]       
                {
                    new float[]{.393f, .349f, .272f, 0, 0},
            new float[]{.769f, .686f, .534f, 0, 0},
            new float[]{.189f, .168f, .131f, 0, 0},
            new float[]{0, 0, 0, 1, 0},
            new float[]{0, 0, 0, 0, 1}
                });
                ia.SetColorMatrix(cmPicture);
                Graphics g = Graphics.FromImage(bmpInverted);   

                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);




                g.Dispose();                           
                pictureBox1.Image = bmpInverted;

            }
        }

        private void button1_Click(object sender, EventArgs e) {
            loadImg();
        }

        private void button2_Click(object sender, EventArgs e) {
            sepia();
        }

        private void button3_Click(object sender, EventArgs e) {
            saveImg();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e) {
            loadImg();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            saveImg();
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e) {
            sepia();
        }
    }
}
