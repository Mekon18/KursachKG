using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphicLib;

namespace Kursach
{
    public partial class Form1 : Form
    {
        delegate double Function(double x);
        Bitmap bmp;
        Task RotatingTask;
        bool _IsRotating;
        Polyhedron polyhedron;
        Polyhedron line;
        Vector _RotatingVector;
        double RotatingSpeed;
        Point3D _LightPoint;
        Point3D _Observation;
        double[,] ZBuffer;        
        object locker = new object();
        int _SelectedIndex;
        Color _PolyhedronColor;
        Color _LineColor;

        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
            _SelectedIndex = 0;
            _PolyhedronColor = Color.Green;
            _LineColor = Color.FromArgb(0, 0, 128);
            bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphic2D();
            double zoom = 1;
            bmp = new Bitmap((int)(pictureBox1.Width / zoom), (int)(pictureBox1.Height / zoom));

            _IsRotating = false;

            TryToGetParametrs(Graphic3D);

        }

        private double Function1(double x)
        {
            double y = Math.Pow(x / 4, 2) - Math.Pow(x / 4, 8);
            return y * 4;
        }

        #region 2D

        private void Graphic2D()
        {
            Axes(bmp);
            DrawFunction(bmp, Function1, -10, 10, 0.02, Color.Green, Color.Blue);
            pictureBox2.Image = bmp;
        }

        private void DrawFunction(Bitmap bmp, Function F, double x1, double x2, double dx, Color FunctionColor, Color LineColor)
        {
            double y;
            for (double x = x1; x < x2; x += dx)
            {
                y = F(x);
                Point p1 = Сonversion(bmp, new PointD(y, -x));
                y = F(x + dx);
                Point p2 = Сonversion(bmp, new PointD(y, -(x + dx)));
                if (y < bmp.Height && 0 < y)
                    Draw.Line(bmp, p1, p2, FunctionColor);
            }
            Point p3 = Сonversion(bmp, new PointD(0.1, -4.5));
            Point p4 = Сonversion(bmp, new PointD(1.5, 4.5));
            Draw.Line(bmp, p3, p4, LineColor);
        }

        private Point Сonversion(Bitmap bmp, PointD p)
        {
            int dx = 20;
            int dy = 20;

            int x = bmp.Width / 2 + (int)(p.X * dx);
            int y = bmp.Height / 2 - (int)(p.Y * dy);
            return new Point(x, y);
        }

        private void Axes(Bitmap bmp)
        {
            Draw.Line(bmp, 1, bmp.Height / 2, bmp.Width, bmp.Height / 2, Color.Black);
            Draw.Line(bmp, bmp.Width, bmp.Height / 2, bmp.Width * 79 / 80, bmp.Height * 39 / 80, Color.Black);
            Draw.Line(bmp, bmp.Width, bmp.Height / 2, bmp.Width * 79 / 80, bmp.Height * 41 / 80, Color.Black);

            Draw.Line(bmp, bmp.Width / 2, 1, bmp.Width / 2, bmp.Height, Color.Black);
            Draw.Line(bmp, bmp.Width / 2, 1, bmp.Width * 39 / 80, bmp.Height * 1 / 80, Color.Black);
            Draw.Line(bmp, bmp.Width / 2, 1, bmp.Width * 41 / 80, bmp.Height * 1 / 80, Color.Black);

            for (int i = -8; i < 10; i += 4)
            {
                var p1 = Сonversion(bmp, new PointD(i, 0.25));
                var p2 = Сonversion(bmp, new PointD(i, -0.25));
                Draw.Line(bmp, p1, p2, Color.Black);
            }
            for (int i = -8; i < 10; i += 4)
            {
                var p1 = Сonversion(bmp, new PointD(0.25, i));
                var p2 = Сonversion(bmp, new PointD(-0.25, i));
                Draw.Line(bmp, p1, p2, Color.Black);
            }
        }

        #endregion

        #region 3D

        private void Graphic3D()
        {
            GetPolyhedrons();

            RotatingTask = new Task(RotationAndDrawing);
            RotatingTask.Start();
        }

        void GetPolyhedrons()
        {
            switch (_SelectedIndex)
            {
                case 0:
                case 1:
                    polyhedron = GetFunctionPolyhedron(Function1, 60, -4, 4, 0.2, _PolyhedronColor);
                    break;
                case 2:
                case 3:
                    polyhedron = Polyhedron.GetGlobe(10, 70, _PolyhedronColor);
                    break;
                case 4:
                case 5:
                    polyhedron = Polyhedron.GetCone(10, 70, 140, _PolyhedronColor);
                    break;
                case 6:
                case 7:
                    polyhedron = Polyhedron.GetСylinder(10, 70, 140, _PolyhedronColor);
                    break;
                case 8:
                case 9:
                    polyhedron = Polyhedron.GetCube(new Point3D(0, 0, 0), 100, _PolyhedronColor);
                    break;
                default:
                    polyhedron = GetFunctionPolyhedron(Function1, 60, -4, 4, 0.2, _PolyhedronColor);
                    break;
            }

            //polyhedron = GetGlobe(10, 70, Color.Green);
            //polyhedron = GetFunctionPolyhedron(Function1, 60, -4, 4, 0.2, Color.Green);
            if (LineCheckBox.Checked)
            {
                line = GetLine(60);
            }
            else
            {
                line = null;
            }

            SetInCenter(polyhedron);
            SetInCenter(line);

        }

        void SetInCenter(Polyhedron polyhedron)
        {
            if (polyhedron != null)
            {
                foreach (var point in polyhedron.Vertexes)
                {
                    double dx = (bmp.Width / 2) - polyhedron.Center.X;
                    double dz = (bmp.Height / 2) - polyhedron.Center.Z;
                    point.X += dx;
                    point.Z += dz;
                }
                polyhedron.ResetCenter();
            }
        }

        private void RotationAndDrawing()
        {
            double angle = Math.PI / (90 / RotatingSpeed);
            do
            {
                try
                {
                    lock (locker)
                    {
                        bmp = new Bitmap(bmp.Width, bmp.Height);
                    }
                    ClearZBuffer();

                    switch (_SelectedIndex)
                    {
                        case 0:
                        case 2:
                        case 4:
                        case 6:
                        case 8:
                            DrawPolyhedronByPhong(bmp, _Observation, _LightPoint, 10, polyhedron);
                            break;
                        case 1:
                        case 3:
                        case 5:
                        case 7:
                        case 9:
                            DrawPolyhedronByZBufferMetod(bmp, polyhedron, _LightPoint);
                            break;
                        default:
                            DrawPolyhedronByPhong(bmp, _Observation, _LightPoint, 10, polyhedron);
                            break;
                    }
                    //DrawPolyhedronByPhong(bmp, _Observation, _LightPoint, 10, polyhedron);
                    DrawPolyhedronByPhong(bmp, _Observation, _LightPoint, 10, line);

                    pictureBox1.Image = bmp;

                    foreach (var point in polyhedron.Vertexes)
                    {
                        point.Rotation(new Point3D(bmp.Width / 2, 0, bmp.Height / 2), _RotatingVector, angle);
                    }
                    foreach (var point in line.Vertexes)
                    {
                        point.Rotation(new Point3D(bmp.Width / 2, 0, bmp.Height / 2), _RotatingVector, angle);
                    }
                }
                catch (Exception e)
                { }
            } while (_IsRotating);
        }

        private void ClearZBuffer()
        {
            ZBuffer = new double[bmp.Width, bmp.Height];
            for (int y = 0; y < ZBuffer.GetLength(1) - 1; y++)
                for (int x = 0; x < ZBuffer.GetLength(0) - 1; x++)
                {
                    ZBuffer[x, y] = double.NegativeInfinity;
                }
        }

        public void DrawPolyhedronByZBufferMetod(Bitmap bmp, Polyhedron polyhedron, Point3D Light)
        {
            polyhedron.ResetBodyMatrix();
            for (int polygon = 0; polygon < polyhedron.Faces.Length; polygon++)
            {
                Point3D[] pointsBuf = new Point3D[polyhedron.Faces[polygon].Points.Length + 1];
                polyhedron.Faces[polygon].Points.CopyTo(pointsBuf, 0);

                pointsBuf[pointsBuf.Length - 1] = pointsBuf[0];

                List<Tuple<Point3D, Point3D>> CAP = new List<Tuple<Point3D, Point3D>>();
                int ymin = (int)pointsBuf.Min(x => x.Z);
                int ymax = (int)pointsBuf.Max(x => x.Z);
                double[] coef = polyhedron.Faces[polygon].CalculateCoefficients(new Point3D(0, 0, 0));
                double[] coef1 = polyhedron.Faces[polygon].CalculateCoefficients(polyhedron.Center);
                for (int i = 0; i < 4; i++)
                {
                    coef1[i] *= Math.Sign(coef1[3]);
                }

                Vector LightVector = Vector.Point3DToVector(polyhedron.Center) - Vector.Point3DToVector(Light);
                double L = GetCos1(-new Vector(coef1[0], coef1[1], coef1[2]), LightVector);
                int R = (int)(polyhedron.Faces[polygon].Color.R * L);
                R = R > 0 && R < 255 ? R : R > 0 ? 255 : 0;
                int G = (int)(polyhedron.Faces[polygon].Color.G * L);
                G = G > 0 && G < 255 ? G : G > 0 ? 255 : 0;
                int B = (int)(polyhedron.Faces[polygon].Color.B * L);
                B = B > 0 && B < 255 ? B : B > 0 ? 255 : 0;
                Color color = Color.FromArgb(R, G, B);

                for (int y = ymin + 1; y <= ymax; y++)
                {
                    CAP.Clear();
                    for (int i = 1; i < pointsBuf.Length; i++)
                    {
                        int a = (int)(Math.Min(pointsBuf[i].Z, pointsBuf[i - 1].Z));
                        int b = (int)(Math.Max(pointsBuf[i].Z, pointsBuf[i - 1].Z));
                        if (a < y && b >= y && a != b)
                        {
                            CAP.Add(new Tuple<Point3D, Point3D>(pointsBuf[i], pointsBuf[i - 1]));
                        }
                    }

                    int xmin = (int)Math.Min(CalculateX(CAP[0].Item1, CAP[0].Item2, y), CalculateX(CAP[1].Item1, CAP[1].Item2, y));
                    int xmax = (int)Math.Max(CalculateX(CAP[0].Item1, CAP[0].Item2, y), CalculateX(CAP[1].Item1, CAP[1].Item2, y));
                    for (int x = xmin; x < xmax; x++)
                    {
                        double z = 0;
                        z = (-(coef[0] * x + coef[2] * y + coef[3]) / coef[1]);

                        lock (locker)
                        {
                            if (x > 0 && y > 0 && x < bmp.Width && y < bmp.Height)
                                if (z > ZBuffer[x, bmp.Height - y])
                                {
                                    ZBuffer[x, bmp.Height - y] = z;
                                    bmp.SetPixel(x, bmp.Height - y, color);
                                }
                        }
                    }
                }
            }
        }

        private void DrawPolyhedronByPhong(Bitmap bmp, Point3D Observation, Point3D Light, double p, params Polyhedron[] polyhedrons)
        {
            foreach (var polyhedron in polyhedrons)
            {
                if (polyhedron == null)
                {
                    continue;
                }
                polyhedron.ResetBodyMatrix();

                for (int polygon = 0; polygon < polyhedron.Faces.Length; polygon++)
                {
                    double[] coef = polyhedron.Faces[polygon].CalculateCoefficients(new Point3D(0, 0, 0));
                    double[] coef1 = polyhedron.Faces[polygon].CalculateCoefficients(polyhedron.Center);
                    if (coef1[3] != 0)
                        for (int i = 0; i < 4; i++)
                        {
                            coef1[i] *= Math.Sign(coef1[3]);
                        }

                    Point3D[] pointsBuf = new Point3D[polyhedron.Faces[polygon].Points.Length + 1];
                    polyhedron.Faces[polygon].Points.CopyTo(pointsBuf, 0);

                    pointsBuf[pointsBuf.Length - 1] = pointsBuf[0];

                    Vector[] VforPoints = new Vector[pointsBuf.Length];
                    for (int i = 0; i < VforPoints.Length - 1; i++)
                    {
                        var polygons = polyhedron.Faces.ToList().FindAll(x => x.Points.Contains(pointsBuf[i])); // грани, которым пренадлежит точка pointsBuf[i]
                        Vector normal = new Vector();
                        for (int k = 0; k < polygons.Count; k++)
                        {
                            int index = polyhedron.Faces.ToList().IndexOf(polygons[k]);
                            normal.X += polyhedron.BodyMatrix[0, index];
                            normal.Y += polyhedron.BodyMatrix[1, index];
                            normal.Z += polyhedron.BodyMatrix[2, index];
                        }
                        normal.X /= polygons.Count;
                        normal.Y /= polygons.Count;
                        normal.Z /= polygons.Count;

                        VforPoints[i] = normal;
                    }
                    VforPoints[VforPoints.Length - 1] = VforPoints[0];

                    List<Tuple<Point3D, Point3D, Vector, Vector>> CAP = new List<Tuple<Point3D, Point3D, Vector, Vector>>();
                    int ymin = (int)(pointsBuf.Min(point => point.Z));
                    int ymax = (int)(pointsBuf.Max(point => point.Z));
                    for (int y = ymin + 1; y <= ymax; y++)
                    {
                        CAP.Clear();
                        for (int i = 1; i < pointsBuf.Length; i++)
                        {
                            int a = (int)(Math.Min(pointsBuf[i].Z, pointsBuf[i - 1].Z));
                            int b = (int)(Math.Max(pointsBuf[i].Z, pointsBuf[i - 1].Z));
                            if (a < y && b >= y && a != b)
                            {
                                CAP.Add(new Tuple<Point3D, Point3D, Vector, Vector>(pointsBuf[i], pointsBuf[i - 1], VforPoints[i], VforPoints[i - 1]));
                            }
                        }

                        try
                        {
                            int xmin = (int)Math.Min(CalculateX(CAP[0].Item1, CAP[0].Item2, y), CalculateX(CAP[1].Item1, CAP[1].Item2, y));
                            int xmax = (int)Math.Max(CalculateX(CAP[0].Item1, CAP[0].Item2, y), CalculateX(CAP[1].Item1, CAP[1].Item2, y));

                            double Ya;
                            double Yb;
                            double Yc;
                            double Yd;
                            Vector Va;
                            Vector Vb;
                            Vector Vc;
                            Vector Vd;
                            Tuple<Point3D, Point3D, Vector, Vector> left = CAP[1];
                            Tuple<Point3D, Point3D, Vector, Vector> right = CAP[0];
                            #region
                            if (CalculateX(CAP[0].Item1, CAP[0].Item2, y) < CalculateX(CAP[1].Item1, CAP[1].Item2, y))
                            {
                                left = CAP[0];
                                right = CAP[1];
                            }
                            if (right.Item1.Z > right.Item2.Z)
                            {
                                Ya = right.Item1.Z;
                                Va = right.Item3;
                                Yb = right.Item2.Z;
                                Vb = right.Item4;
                            }
                            else
                            {
                                Ya = right.Item2.Z;
                                Va = right.Item4;
                                Yb = right.Item1.Z;
                                Vb = right.Item3;
                            }

                            if (left.Item1.Z > left.Item2.Z)
                            {
                                Yd = left.Item1.Z;
                                Vd = left.Item3;
                                Yc = left.Item2.Z;
                                Vc = left.Item4;
                            }
                            else
                            {
                                Yd = left.Item2.Z;
                                Vd = left.Item4;
                                Yc = left.Item1.Z;
                                Vc = left.Item3;
                            }
                            #endregion
                            Vector V1 = Vd + (Vc - Vd) * (y - Yd) / (Yc - Yd);
                            Vector V2 = Va + (Vb - Va) * (y - Ya) / (Yb - Ya);

                            for (int x = xmin; x <= xmax; x++)
                            {
                                double z = (-(coef[0] * x + coef[2] * y + coef[3]) / coef[1]);
                                Vector V = V1 + (V2 - V1) * (x - xmin) / (xmax - xmin);
                                V /= V.Length;
                                Vector LightVector = new Vector(x, z, y) - Vector.Point3DToVector(Light);
                                Vector ObservationVector = -new Vector(x, z, y) + Vector.Point3DToVector(Observation);
                                Vector Reflect = LightVector - 2 * V * Vector.ScalarMultiplying(V, LightVector);
                                double alpha = GetCos(Reflect, ObservationVector);
                                double theta = GetCos1(V, LightVector);
                                double I = (alpha > 0 ? Math.Pow(alpha, p) : 0) + theta;

                                int R = (int)(polyhedron.Faces[polygon].Color.R * I);
                                R = R > 0 && R < 255 ? R : R > 0 ? 255 : 0;
                                int G = (int)(polyhedron.Faces[polygon].Color.G * I);
                                G = G > 0 && G < 255 ? G : G > 0 ? 255 : 0;
                                int B = (int)(polyhedron.Faces[polygon].Color.B * I);
                                B = B > 0 && B < 255 ? B : B > 0 ? 255 : 0;
                                Color color = Color.FromArgb(R, G, B);

                                lock (locker)
                                {
                                    if (x > 0 && y > 0 && x < bmp.Width && y < bmp.Height)
                                        if (z > ZBuffer[x, bmp.Height - y])
                                        {
                                            ZBuffer[x, bmp.Height - y] = z;
                                            bmp.SetPixel(x, bmp.Height - y, color);
                                        }
                                }
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }
        }

        private double GetCos(Vector v1, Vector v2)
        {
            return Vector.ScalarMultiplying(v1, v2) / (v1.Length * v2.Length);
        }

        private double GetCos1(Vector v1, Vector v2)
        {
            if (GetCos(new Vector(0, -1, 0), v1) > 0)
                return GetCos(v1, v2);
            else
                return GetCos(-v1, v2);
        }

        private int CalculateX(Point3D p1, Point3D p2, int y)
        {
            double x;
            x = ((p1.X - p2.X) * y + (p2.X * p1.Z - p1.X * p2.Z)) / (p1.Z - p2.Z);
            return (int)x;
        }

        private Polyhedron GetLine(int heingt)
        {
            Point3D a = new Point3D(-25, 25, -heingt / 2);
            Point3D b = new Point3D(-25, 25, heingt / 2);
            Point3D c = new Point3D(145, 10, heingt / 2);
            Point3D d = new Point3D(145, 10, -heingt / 2);

            Polygon polygon = new Polygon(new Point3D[] { a, b, c, d }) { Color = _LineColor };
            return new Polyhedron(new Polygon[] { polygon }, a, b, c, d);
        }
        private Polyhedron GetLine2(int heingt)
        {
            Point3D a = new Point3D(-25, 25, -heingt / 2);
            Point3D b = new Point3D(-25, 25, heingt / 2);
            Point3D c = new Point3D(145, 10, heingt / 2);
            Point3D d = new Point3D(145, 10, -heingt / 2);

            Polygon polygon = new Polygon(new Point3D[] { a, b, c, d }) { Color = Color.Blue };
            return new Polyhedron(new Polygon[] { polygon }, a, b, c, d);
        }

        private Polyhedron GetFunctionPolyhedron(Function F, double length, double x1, double x2, double dx, Color color)
        {
            List<Point3D> vertexes = new List<Point3D>();
            List<Polygon> polygons = new List<Polygon>();

            for (double x = x1; x <= x2 + dx; x += dx)
            {
                var y = F(x);
                Point p = new Point((int)(x * 20), (int)(y * 20));

                vertexes.Add(new Point3D(p.X, p.Y, -length / 2));
                vertexes.Add(new Point3D(p.X, p.Y, length / 2));
            }

            for (int i = 0; i < vertexes.Count - 2; i += 2)
            {
                Polygon polygon = new Polygon(new Point3D[] { vertexes[i], vertexes[i + 2], vertexes[i + 3], vertexes[i + 1] }) { Color = color };
                polygons.Add(polygon);
            }

            return new Polyhedron(polygons.ToArray(), vertexes.ToArray());
        }
        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (RotatingTask.Status == TaskStatus.Running)
                _IsRotating = false;
            else
            {
                _IsRotating = true;
                RotatingTask = new Task(RotationAndDrawing);

                RotatingTask.Start();
            }
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {

            _IsRotating = false;
            RotatingTask.Wait();
            _IsRotating = true;

            TryToGetParametrs(Graphic3D);
        }

        private bool TryToGetRotatingVector(out Vector vector)
        {
            vector = null;
            bool isOK;
            string text = textBoxRotatingVector.Text;
            int CommaIndex = text.IndexOf(';');
            isOK = double.TryParse(text.Substring(0, CommaIndex), out double X);
            if (isOK)
            {
                text = text.Remove(0, CommaIndex + 1);
                CommaIndex = text.IndexOf(';');
                isOK = double.TryParse(text.Substring(0, CommaIndex), out double Y);
                if (isOK)
                {
                    text = text.Remove(0, CommaIndex + 1);
                    isOK = double.TryParse(text, out double Z);
                    if (isOK)
                        vector = new Vector(X, Y, Z);
                }
            }
            return isOK;
        }

        private bool TryToGetLightPoint(out Point3D point)
        {
            point = null;
            bool isOK;
            string text = textBoxLightPoint.Text;
            int CommaIndex = text.IndexOf(';');
            isOK = double.TryParse(text.Substring(0, CommaIndex), out double X);
            if (isOK)
            {
                text = text.Remove(0, CommaIndex + 1);
                CommaIndex = text.IndexOf(';');
                isOK = double.TryParse(text.Substring(0, CommaIndex), out double Y);
                if (isOK)
                {
                    text = text.Remove(0, CommaIndex + 1);
                    isOK = double.TryParse(text, out double Z);
                    if (isOK)
                        point = new Point3D(X, Y, Z);
                }
            }
            return isOK;
        }

        private bool TryToGetObservationPoint(out Point3D point)
        {
            point = null;
            bool isOK;
            string text = textBoxObservation.Text;
            int CommaIndex = text.IndexOf(';');
            isOK = double.TryParse(text.Substring(0, CommaIndex), out double X);
            if (isOK)
            {
                text = text.Remove(0, CommaIndex + 1);
                CommaIndex = text.IndexOf(';');
                isOK = double.TryParse(text.Substring(0, CommaIndex), out double Y);
                if (isOK)
                {
                    text = text.Remove(0, CommaIndex + 1);
                    isOK = double.TryParse(text, out double Z);
                    if (isOK)
                        point = new Point3D(X, Y, Z);
                }
            }
            return isOK;
        }

        private bool TryToGetRotatingSpeed(out double Speed)
        {
            return double.TryParse(textBoxRotatingSpeed.Text, out Speed);
        }

        private bool TryToGetParametrs(Action action)
        {
            bool IsOK;
            IsOK = TryToGetRotatingVector(out _RotatingVector);
            if (IsOK)
            {
                IsOK = TryToGetLightPoint(out _LightPoint);
                if (IsOK)
                {
                    IsOK = TryToGetObservationPoint(out _Observation);
                    if (IsOK)
                    {
                        IsOK = TryToGetRotatingSpeed(out RotatingSpeed);
                        if (IsOK)
                            action();
                        else
                        {
                            MessageBox.Show("Не правильно введена скорость.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не правильно введены координаты наблюдателя.");
                    }
                }
                else
                {
                    MessageBox.Show("Не правильно введены координаты источника света.");
                }
            }
            else
            {
                MessageBox.Show("Не правильно введен вектор вращения.");
            }
            return IsOK;
        }

        private void RunOnEnterPressing(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Restart();
            }
        }

        private void textBoxRotatingVector_KeyDown(object sender, KeyEventArgs e)
        {
            RunOnEnterPressing(e);
        }

        private void textBoxLightPoint_KeyDown(object sender, KeyEventArgs e)
        {
            RunOnEnterPressing(e);
        }

        private void textBoxObservation_KeyDown(object sender, KeyEventArgs e)
        {
            RunOnEnterPressing(e);
        }

        private void textBoxRotatingSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            RunOnEnterPressing(e);
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            RunOnEnterPressing(e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SelectedIndex = comboBox1.SelectedIndex;
        }

        private void buttonPolyhedronColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _PolyhedronColor = colorDialog1.Color;
                Parallel.For(0, polyhedron.Faces.Length, x => { polyhedron.Faces[x].Color = _PolyhedronColor; });
                Restart();
            }
        }

        private void buttonLineColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _LineColor = colorDialog1.Color;
                Parallel.For(0, line.Faces.Length, x => { line.Faces[x].Color = _LineColor; });
                Restart();
            }
        }

        private void LineCheckBox_CheckedChanged(object sender, EventArgs e)
        {

            Restart();
        }

        void Restart()
        {
            _IsRotating = false;
            RotatingTask.Wait();
            _IsRotating = true;
            TryToGetParametrs(Graphic3D);
        }

        System.Media.SoundPlayer sp;
        bool playing;
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (sp == null)
            {
                sp = new System.Media.SoundPlayer("GraphicLib.wav");
                sp.Load();
            }
            if(playing)
            {
                sp.Stop();
                playing = false;
            }
            else
            {
                sp.PlayLooping();
                playing = true;
            }

        }
    }
}
