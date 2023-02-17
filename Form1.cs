using System.Drawing.Text;
using System.Security.Cryptography.Xml;

namespace Graphing_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        ///  Returns the offset of a point
        /// </summary>
        /// <param name="point"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        private Point ReturnOffset(Point point, int dx, int dy)
        {
            Point newPoint = point;
            newPoint.Offset(dx, dy);
            return newPoint;
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

                //offsets position from: cursor at top left -> cursor at centre
                node.Location = ReturnOffset(e.Location, - (nodeDiameter / 2), -(nodeDiameter / 2));
                node.Size = new Size(nodeDiameter, nodeDiameter);
                node.MouseUp += new System.Windows.Forms.MouseEventHandler(this.node_Clicked);
                this.mainPanel.Controls.Add(node);
            }
        }
        private void node_Clicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Node tempNode = sender as Node;
                if (!drawEdge)
                {
                    drawEdge = true;
                    this.tempStartPoint = ReturnOffset(tempNode.Location, (nodeDiameter / 2), (nodeDiameter / 2));
                } 
                else if (drawEdge)
                {
                    drawEdge = false;
                    Edge edge = new Edge(this.tempStartPoint, ReturnOffset(tempNode.Location, (nodeDiameter / 2), (nodeDiameter / 2)));
                    this.mainPanel.Controls.Add(edge);
                }
            }
        }
    }
}