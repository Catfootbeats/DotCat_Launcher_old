using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DotCat_Launcher.Views
{
    /// <summary>
    /// IndexView.xaml 的交互逻辑
    /// </summary>
    public partial class IndexView : UserControl
    {
        private Point mouseLastPosition;
        private double mouseDeltaFactor;

        public IndexView()
        {
            InitializeComponent();
                
        }

        private void Viewport3D_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point newMousePosition = e.GetPosition(this);
                if (mouseLastPosition.X != newMousePosition.X)
                {
                    mouseDeltaFactor = Math.Abs(mouseLastPosition.X - newMousePosition.X);
                    HorizontalTransform(mouseLastPosition.X < newMousePosition.X, mouseDeltaFactor);//水平变换
                }

                if (mouseLastPosition.Y != newMousePosition.Y)// change position in the horizontal direction
                {
                    mouseDeltaFactor = Math.Abs(mouseLastPosition.Y - newMousePosition.Y);
                    VerticalTransform(mouseLastPosition.Y > newMousePosition.Y, mouseDeltaFactor);//垂直变换
                }

                mouseLastPosition = newMousePosition;
            }
        }
        private void VerticalTransform(bool upDown, double angleDeltaFactor)
        {
            Vector3D postion = new(camera.Position.X, camera.Position.Y, camera.Position.Z);
            Vector3D rotateAxis = Vector3D.CrossProduct(postion, camera.UpDirection);
            RotateTransform3D rt3d = new();
            AxisAngleRotation3D rotate = new(rotateAxis, angleDeltaFactor * (upDown ? -1 : 1));
            rt3d.Rotation = rotate;
            Matrix3D matrix = rt3d.Value;
            Point3D newPostition = matrix.Transform(camera.Position);
            camera.Position = newPostition;
            camera.LookDirection = new Vector3D(-newPostition.X, -newPostition.Y, -newPostition.Z);

            //update the up direction
            Vector3D newUpDirection = Vector3D.CrossProduct(camera.LookDirection, rotateAxis);
            newUpDirection.Normalize();
            camera.UpDirection = newUpDirection;
        }
        private void HorizontalTransform(bool leftRight, double angleDeltaFactor)
        {
            Vector3D postion = new(camera.Position.X, camera.Position.Y, camera.Position.Z);
            Vector3D rotateAxis = camera.UpDirection;
            RotateTransform3D rt3d = new();
            AxisAngleRotation3D rotate = new(rotateAxis, angleDeltaFactor * (leftRight ? -1 : 1));
            rt3d.Rotation = rotate;
            Matrix3D matrix = rt3d.Value;
            Point3D newPostition = matrix.Transform(camera.Position);
            camera.Position = newPostition;
            camera.LookDirection = new Vector3D(-newPostition.X, -newPostition.Y, -newPostition.Z);
        }

        private void Viewport3D_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseLastPosition = e.GetPosition(this);
        }
    }
}
