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

        private void mainPanel_Clicked(object sender, MouseEventArgs e)
        //e is object containing click event information
        {
            if (e.Button == MouseButtons.Left)
            {
                Node node = new Node();
                node.Location = e.Location;
                node.Size = new Size(nodeDiameter, nodeDiameter);   
                this.mainPanel.Controls.Add(node);
                //.Location.X and Y is cursor position relative to mainPanel
            }
        }
    }
}