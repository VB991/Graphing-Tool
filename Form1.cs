using System.Diagnostics;
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
        ///  Returns an offset point
        /// </summary>
        /// <param name="point"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        public static Point ReturnOffsetPoint(Point point, int dx, int dy)
        {
            Point newPoint = point;
            newPoint.Offset(dx, dy);
            return newPoint;
        }

        public int nodeDiameter = 50; // Diameter of node in pixels

        // Variables used for drawing edges
        public Point tempStartPoint;
        public Node tempStartNode;
        private bool drawEdge = false;

        // Variables used for moving nodes
        private Point initialPosition;
        private Node? activeNode; // Stores the node that is moving

        private void onNodeClicked(object sender, MouseEventArgs e)
        {
            Node node = sender as Node;
            if (e.Button == MouseButtons.Left) 
            {
                node.BringToFront();

                // Activate the movement of the left-clicked node
                initialPosition = this.mainPanel.PointToClient(Cursor.Position);
                activeNode = node;
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (!drawEdge) // First node right clicked
                {
                    drawEdge = true;
                    tempStartPoint = ReturnOffsetPoint(node.Location, (nodeDiameter / 2), (nodeDiameter / 2));
                    tempStartNode = node;
                }
                else if (drawEdge) // Second node right clicked
                {
                    drawEdge = false;
                    // Creates the edge at the centre of the node
                    Edge edge = new Edge(tempStartPoint,
                        ReturnOffsetPoint(node.Location, (nodeDiameter / 2), (nodeDiameter / 2)), this.mainPanel.Controls.Count
                        );
                    // Connects the start and end point of this edge to it's connected nodes
                    node.addEndEdge(edge);
                    tempStartNode.addStartEdge(edge);
                    this.mainPanel.Controls.Add(edge);
                }
            }
        }

        private void onCursorMovesOverNode(object sender, MouseEventArgs e)
        {
            Node node = sender as Node;
            if (activeNode == node) // If hovered over node is active
            {
                // Offsets position of the node from its original position
                Point newLocation = ReturnOffsetPoint(
                    initialPosition,
                    this.mainPanel.PointToClient(Cursor.Position).X - initialPosition.X,
                    this.mainPanel.PointToClient(Cursor.Position).Y - initialPosition.Y
                );
                node.Location = ReturnOffsetPoint(newLocation,-nodeDiameter/2,-nodeDiameter/2);
                // Update the positions of the connected edges
                foreach (Edge edge in node.EndEdges)
                {
                    edge.EndPoint = newLocation;
                }
                foreach (Edge edge in node.StartEdges)
                {
                    edge.StartPoint = newLocation;
                }
                this.Refresh(); // Repaint entire mainPanel and contents
            }
        }

        private void onNodeReleased(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Deactivate the movement of the released node
                activeNode = null;
            }
        }

        // Used for generation of nodes on the screen
        private void mainPanel_Clicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Node node = new Node(this.mainPanel.Controls.Count); 
                //offsets position from: cursor at top left -> cursor at centre
                node.Location = ReturnOffsetPoint(e.Location, - (nodeDiameter / 2), -(nodeDiameter / 2));
                node.Size = new Size(nodeDiameter, nodeDiameter);
                node.MouseDown += new System.Windows.Forms.MouseEventHandler(this.onNodeClicked);
                node.MouseMove += new System.Windows.Forms.MouseEventHandler(this.onCursorMovesOverNode);
                node.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onNodeReleased);
                this.mainPanel.Controls.Add(node);
            }
        }
    }
}