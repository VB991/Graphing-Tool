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
        private bool drawEdge = false;
        private Point tempStartPoint;
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
            else if (e.Button == MouseButtons.Right)
            {
                if (!drawEdge)
                {
                    drawEdge= true;
                    tempStartPoint = e.Location;

                } else if (drawEdge)
                {
                    drawEdge= false;
                    Edge edge = new Edge(this.tempStartPoint,e.Location);
                    this.mainPanel.Controls.Add(edge);
                }
            }
        }
    }
}