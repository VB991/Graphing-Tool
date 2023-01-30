using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphing_Tool
{
    public class Node : Control
    {

        public Node() {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Point newLocation = this.Location;
            newLocation.Offset(-(this.Size.Width/2), -(this.Size.Height/2));
            this.Location = newLocation;

            System.Drawing.Drawing2D.GraphicsPath circleRegion =
            new System.Drawing.Drawing2D.GraphicsPath();

            // Set a new rectangle to the same size as the node's 
            // ClientRectangle property.
            System.Drawing.Rectangle newRectangle = this.ClientRectangle;

            newRectangle.Inflate(-1, -1);
            // Draw the circle
            e.Graphics.DrawEllipse(System.Drawing.Pens.Black, newRectangle);

            newRectangle.Inflate(10, 10);
            // Create a circle within the new rectangle.
            circleRegion.AddEllipse(newRectangle);

            // Set the node's Region property to the newly created 
            // circle region.
            this.Region = new System.Drawing.Region(circleRegion);

        }
    }
}
