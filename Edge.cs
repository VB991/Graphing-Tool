using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphing_Tool
{
    public class Edge : Control
    {
        private Pen pen; // Maintaining pen object improves rendering efficiency
        private const int hitBoxWidth = 40; // Width of edge's clickable rectangle hitbox

        /*
         * The start and end point of the edge.
         * The euclidian distance between the points must be recalculated when 
         * these are updated. The size is dependant on the euclidian distance between the points
         * so must also be updated. Refresh method called to redraw the control when these are updated.
        */
        private Point startPoint;
        public Point StartPoint
        {
            get { return startPoint; }
            set {
                startPoint = value;
                this.Invalidate(); // Force a redraw
            }
        }
        private Point endPoint;
        public Point EndPoint
        {
            get { return endPoint; }
            set
            {
                endPoint = value;
                this.Invalidate(); // Force a redraw
            }
        }

        private Point midPoint; // The midpoint of the start and end point
        private int euclidianDistance; // Direct distance between the points

        /// <summary>
        /// Initiates new edge control
        /// </summary>
        /// <param name="StartPoint">Start point of edge</param>
        /// <param name="EndPoint">End point of edge</param>
        public Edge(Point StartPoint, Point EndPoint, int index) {
            this.DoubleBuffered = true;
            this.pen = new Pen(Color.Black,1);
            /* These fields mustn't initially be set through their properties (getters and setters) because
             * they must not be null before updateSize is called.
             */
            this.startPoint = StartPoint;
            this.endPoint = EndPoint;
            updateClientRectangle();
        }

        // Integer square root using Newton-Rapshon, 10 iterations
        public static int IntSqrt(int x)
        {
            float a = 1000;
            for (int i = 0; i <= 10; i++)
            {
                a = a - ((a*a-x)/(2*a));
            }
            return (int)a;
        }

        /// <summary>
        /// Recalculates the control's size, position (control's client rectangle) and the euclidian distance between the start and end points.
        /// </summary>
        private void updateClientRectangle()
        {
            /* The size and position of the control determine its clientRectangle. This is the only space where the control
             * can be drawn, so this method is called to ensure it always contains the entire region necesary to draw the rectangle. 
             */

            // Using Pythagoras' theorem
            int dist1 = (endPoint.X - startPoint.X);
            int dist2 = (endPoint.Y - startPoint.Y);
            this.euclidianDistance = IntSqrt(dist1*dist1 + dist2*dist2);

            // Set object size as largest necessary square; width & height are direct distance between start and end points.
            // ("Necessary" means that the line and hitbox can be rotated within clientRectangle and not be out of bounds).
            this.Size = new Size((int)this.euclidianDistance, (int)this.euclidianDistance);

            // Sets midpoint between the start and end points by using the mean ordinates.
            this.midPoint = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

            // Calculate the upper left of the clientRectangle (determined by location attribute).
            this.Location = new Point(this.midPoint.X - (this.Size.Width / 2), this.midPoint.Y - (this.Size.Height / 2));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Calls the parent paint method.
            base.OnPaint(e);
            // Enables antialiasing.
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            updateClientRectangle();

            /* Anti-clockwise angle between the line which connects start and endpoint, and horizontal (converted to degrees).
             * Atan2 handles appropriate angle for quadrants. Negation due to System.Graphics coordinate system working 
             * with positive-positive quadrant at bottom right (I am converting to flip the y axis)
            */
            double angle = Math.Atan2(-(endPoint.Y - startPoint.Y), endPoint.X - startPoint.X) * 180 / Math.PI;
            // Creates rotation matrix of necessary angle. Centered at midpoint (recalculated to be relative to clientRectangle)
            Matrix rotation = new Matrix();
            rotation.RotateAt((float)-angle, // Negative angle due to this method using Clockwise rotation
            new Point(this.midPoint.X - this.Location.X, this.midPoint.Y - this.Location.Y));

            // Creates rotated rectangle for the control's region (which is used as its hitbox)
            Rectangle tempRectangle = new Rectangle(0, (this.euclidianDistance - hitBoxWidth) / 2, this.euclidianDistance, hitBoxWidth);
            GraphicsPath g = new GraphicsPath();
            g.AddRectangle(tempRectangle);
            g.Transform(rotation);
            // Sets this as the region
            this.Region = new System.Drawing.Region(g);

            g.Reset();
            // Draws the actual line representing the edge
            g.AddLine(0, this.midPoint.Y - this.Location.Y, this.Width, this.midPoint.Y - this.Location.Y);
            g.Transform(rotation);
            e.Graphics.DrawPath(this.pen, g);
        }
    }
}