using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphing_Tool
{
    public class Node : Control
    {

        // List of edges heading away from this node
        private List<Edge> startEdges; 
        public List<Edge> StartEdges { get => startEdges; }
        public void addStartEdge(Edge e) { startEdges.Add(e); }

        // List of edges arriving at this node
        private List<Edge> endEdges;
        public List<Edge> EndEdges { get => endEdges; } 
        public void addEndEdge(Edge e) { endEdges.Add(e); }
        // Knowing which edges a node is connected to allows them to be moved alongside the node


        // Maintaining the pen object improves rendering efficiency.
        private Pen pen;

        public Node(int index)
        {
            this.DoubleBuffered = true;
            this.pen = new Pen(Color.Black, 1);
            this.startEdges = new List<Edge>();
            this.endEdges = new List<Edge>();
        }

        /// <summary>
        /// Renders circle.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Calls the parent paint method.
            base.OnPaint(e);

            // Set the node's region (wihin it's container) as a circle.
            System.Drawing.Drawing2D.GraphicsPath circleRegion = new System.Drawing.Drawing2D.GraphicsPath();
            circleRegion.AddEllipse(this.ClientRectangle);
            this.Region = new System.Drawing.Region(circleRegion);

            // Enables antialiasing and renders the circle.
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle rectangle = this.ClientRectangle;
            rectangle.Inflate(-1, -1); // Shrinks circle by a pixel to prevent it being drawn out of region.
            e.Graphics.DrawEllipse(this.pen, rectangle);
        }
    }
}
