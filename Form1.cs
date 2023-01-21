namespace Graphing_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void newNode(String name, int xPos, int yPos)
        {
            xPos -= 25;
            yPos -= 25;
            this.Text = (xPos.ToString() + " " + yPos.ToString());
            PictureBox node = new PictureBox();
            node.Name = name;
            node.Size = new Size(50, 50);
            node.BackColor = SystemColors.Highlight;

            node.Location = new Point(xPos, yPos);
            this.mainPanel.Controls.Add(node);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void mainPanel_Clicked(object sender, MouseEventArgs e)
        {
            newNode("node", e.Location.X, e.Location.Y);
        }
    }
}