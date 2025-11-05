using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//class post!
public class Post
{
    public string text { get; set; }
    public Image img { get; set; }
    public DateTime dt { get; set; }
    public Post(string text, Image img, DateTime dt)
    {
        this.text = text;
        this.img = img;
        this.dt = dt;
    }
    //date time of publis.
    public override string ToString()
    {
        //day, month, year, hour, minutes
        return $"{dt:dd.MM.yyyy.HH:mm} - {text}";
    }
}
namespace WinFormsApp20
{
    public partial class Form1 : Form
    {
        private List<Post> posts = new List<Post>();
        private TextBox txtb;
        private Button btn1_add_p;
        private Button btn2_load_i;
        private PictureBox pic_p;
        private DateTimePicker dt_pi;
        private ListBox lst_b;
        //////////////////////////////////////////////////////////////
        //soft colors:
        //////////////
        //background color
        private Color backg = Color.FromArgb(245, 235, 255);
        //soft green:
        private Color sf_green = Color.FromArgb(209, 255, 223);
        //soft sky:
        private Color sf_blue = Color.FromArgb(135, 206, 235);
        //peach for img
        //private Color sf_peach = Color.FromArgb(255, 218, 185);
        //dark grey: (for text)
        private Color tex = Color.FromArgb(255, 153, 153);
        //////////////////////////////////////////////////////////////
        public Form1()
        {
            InitializeComponent();
            this.BackColor = backg;
            this.ForeColor = tex;
            this.Font = new Font("Times New Roman", 9, FontStyle.Regular);
            ui();
        }
        private void ui()
        {
            txtb = new TextBox()
            {
                Location = new Point(10, 10),
                Size = new Size(460, 80),
                Multiline = true,
                BackColor = sf_green,
                ForeColor = tex,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Times New Roman", 12, FontStyle.Regular),
            };
            this.Controls.Add(txtb);
            btn1_add_p = new Button()
            {
                Location = new Point(480, 10),
                Size = new Size(120, 40),
                Text = "publish",
                BackColor = sf_green,
                ForeColor = tex,
                Font = new Font("Times New Roman", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn1_add_p.FlatAppearance.BorderColor = sf_blue;
            btn1_add_p.FlatAppearance.BorderSize = 1;
            btn1_add_p.Click += Btn1_add_p_Click;
            //this is for mouse if user will be hover the mouse into button
            //just delete "//" if ypu want to see this
            //btn1_add_p.MouseEnter += Button_MouseEnter;
            //btn1_add_p.MouseLeave += Button_MouseLeave;
            this.Controls.Add(btn1_add_p);

            btn2_load_i = new Button()
            {
                Location = new Point(480, 60),
                Size = new Size(120, 40),
                Text = "upload photo",
                BackColor = sf_green,
                ForeColor = tex,
                Font = new Font("Times New Roman", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn2_load_i.FlatAppearance.BorderColor = sf_blue;
            btn2_load_i.FlatAppearance.BorderSize = 1;
            btn2_load_i.Click += Btn2_load_i_Click;
            //btn2_load_i.MouseEnter += Button_MouseEnter;
            //btn2_load_i.MouseLeave += Button_MouseLeave;
            this.Controls.Add(btn2_load_i);

            pic_p = new PictureBox()
            {
                Location = new Point(10, 100),
                Size = new Size(460, 120),
                BackColor = sf_green,
                BorderStyle = BorderStyle.FixedSingle,
            };
            pic_p.SizeMode = PictureBoxSizeMode.Zoom;
            this.Controls.Add(pic_p);

            dt_pi = new DateTimePicker()
            {
                Location = new Point(480, 110),
                Size = new Size(200, 30),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd.MM.yyyy HH:mm",
                BackColor = sf_green /*Color.FromArgb(204, 255, 229)*/,
                ForeColor = tex,
                Font = new Font("Times New Roman", 9, FontStyle.Regular),
            };
            this.Controls.Add(dt_pi);

            lst_b = new ListBox()
            {
                Location = new Point(480, 150),
                Size = new Size(200, 150),
                BackColor = sf_green,
                ForeColor = tex,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Times New Roman", 9, FontStyle.Regular),
            };
            lst_b.DrawMode = DrawMode.OwnerDrawFixed;
            lst_b.MeasureItem += Lst_b_MeasureItem;
            lst_b.DrawItem += Lst_b_DrawItem;
            this.Controls.Add(lst_b);

            //this is additional option from me
            var ommmm = new Label()
            {
                Location = new Point(10, 225),
                Size = new Size(460, 30),
                //surprise from me, lmao  :^
                Text = "Console.WriteLine(Hello, World!)",
                //ForeColor = Color.FromArgb(___),
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(ommmm);
        }
        private void Lst_b_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= lst_b.Items.Count)
                return;
            var post = (Post)lst_b.Items[e.Index];
            //alternating soft colors for the list items
            Color bgColor = e.Index % 2 == 0 
                ? Color.White 
                : Color.FromArgb(255, 250, 240);
            e.Graphics.FillRectangle(new SolidBrush(bgColor), e.Bounds);
            using (var brush = new SolidBrush(tex))
            {
                e.Graphics.DrawString(post.ToString(), e.Font, brush, new Point(e.Bounds.X + 5, e.Bounds.Y + 5));
            }
            if (post.img != null)
            {
                var imgr = new Rectangle(e.Bounds.Right - 45, e.Bounds.Y + 5, 40, 40);
                e.Graphics.DrawImage(post.img, imgr);

                ////soft frame around the image
                //using (var pen = new Pen(sf_peach, 1))
                //{
                //    e.Graphics.DrawRectangle(pen, imgr);
                //}
            }

            //my experiment: soft dividing line between posts
            //using (var pen = new Pen(Color.FromArgb(220, 220, 220), 1))
            //{
            //    e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Bottom - 1,
            //        e.Bounds.Right, e.Bounds.Bottom - 1);
            //}
        }

        private void Lst_b_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 50;
        }

        private void Btn2_load_i_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.webp";
                ofd.Title = "choose image";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pic_p.Image = Image.FromFile(ofd.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"error with loading: {ex.Message}", "ERROR!!!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Btn1_add_p_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtb.Text))
            {
                MessageBox.Show("enter the text into post!", "ATTENTION!!!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Image img = null;
            if (pic_p.Image != null)
            {
                img = new Bitmap(pic_p.Image);
            }
            var post = new Post(txtb.Text, img, dt_pi.Value);
            posts.Add(post);
            lst_b.Items.Add(post);
            txtb.Clear();
            pic_p.Image = null;

            MessageBox.Show("post publish!!!", "success!!!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //animation of button on hover
        //my experiment
        //private void Button_MouseEnter(object sender, EventArgs e)
        //{
        //    var btn = (Button)sender;
        //    btn.ForeColor = Color.White;
        //}
        //private void Button_MouseLeave(object sender, EventArgs e)
        //{
        //    var btn = (Button)sender;
        //    if (btn.Text == "publish")
        //        btn.BackColor = sf_green;
        //    else
        //        //btn.BackColor = sf_blue;
        //    btn.ForeColor = tex;
        //}
    }
}