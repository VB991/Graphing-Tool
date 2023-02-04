using System.Drawing.Text;

namespace Graphing_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int nodeDiameter = 50; //diameter of node in pixels
        {
        private void mainPanel_Clicked(object sender, MouseEventArgs e)
        //e is object containing click event information
        {
            if (e.Button == MouseButtons.Left)
            {
                Node node = new Node();
                Point newLocation = e.Location;
                //offsets position from: cursor at top left -> cursor at centre
                newLocation.Offset(-(nodeDiameter / 2), -(nodeDiameter / 2));
                node.Location = newLocation;
                node.Size = new Size(nodeDiameter, nodeDiameter);   
                this.mainPanel.Controls.Add(node);
            }
            xPos -= 25;
            yPos -= 25;
            this.Text = (xPos.ToString() + " " + yPos.ToString());
            PictureBox node = new PictureBox();
            node.Name = name;
            node.Size = new Size(50, 50);
            node.BackColor = SystemColors.Highlight;
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