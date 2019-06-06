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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using Microsoft.Win32;

namespace _7._2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double angle = 0.0;

        const int N = 256;
        ModelVisual3D terrain = new ModelVisual3D();
        MeshGeometry3D geometry = new MeshGeometry3D();

        ModelVisual3D triangle = new ModelVisual3D();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Scene_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                angle--;
            }

            if (e.Key == Key.Right)
            {
                angle++;
            }

            AxisAngleRotation3D ax3d = new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle);
            RotateTransform3D rt = new RotateTransform3D(ax3d);
            TranslateTransform3D tr1 = new TranslateTransform3D(-N / 2, 0, -N / 2);
            TranslateTransform3D tr2 = new TranslateTransform3D(N / 2, 0, N / 2);

            Transform3DGroup tg = new Transform3DGroup();
            tg.Children.Add(tr1);
            tg.Children.Add(rt);
            tg.Children.Add(tr2);
            terrain.Transform = tg;
        }

        private void Triangle_Click(object sender, RoutedEventArgs e)
        {
            scene.Children.Clear();
            grid.Background = Brushes.LightCyan;

            PerspectiveCamera camera = new PerspectiveCamera();
            camera.Position = new Point3D(0, 2, 0.1);
            Vector3D lookAt = new Vector3D(0, 0, 0);

            camera.LookDirection = Vector3D.Subtract(lookAt, new Vector3D(0, 2, 0.1));
            camera.FarPlaneDistance = 1000;
            camera.NearPlaneDistance = 1;
            camera.UpDirection = new Vector3D(0, 1, 0);
            camera.FieldOfView = 100;
            scene.Camera = camera;

            MeshGeometry3D geometry = new MeshGeometry3D();
            geometry.Positions.Add(new Point3D(-0.5, 0, -0.5));
            geometry.Positions.Add(new Point3D(-0.5, 0, 0.5));
            geometry.Positions.Add(new Point3D(0.5, 0, 0.5));
            geometry.TriangleIndices.Add(0);
            geometry.TriangleIndices.Add(1);
            geometry.TriangleIndices.Add(2);

            DiffuseMaterial mat = new DiffuseMaterial(new SolidColorBrush(Colors.Green));
            GeometryModel3D model = new GeometryModel3D(geometry, mat);
            triangle.Content = model;
            scene.Children.Add(triangle);
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            TranslateTransform3D tr = new TranslateTransform3D(10, 0, 10);
            //преобразование масштабирования модели (уменьшение в 2 раза по всем осям)
            ScaleTransform3D sc = new ScaleTransform3D(0.5, 0.5, 0.5);
            //преобразование вращения модели вокруг оси Y на 90 градусов
            AxisAngleRotation3D ax3d = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90);
            RotateTransform3D rt = new RotateTransform3D(ax3d);
            //создание группы преобразований
            Transform3DGroup tg = new Transform3DGroup();
            //последовательное применение преобразований (масштабирование, вращение, перенос)
            tg.Children.Add(sc);
            tg.Children.Add(rt);
            //tg.Children.Add(tr);
            //назначение преобразований модели
            triangle.Transform = tg;
        }

        private void Terrain_Click(object sender, RoutedEventArgs e)
        {
            scene.Children.Clear();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            System.Drawing.Bitmap hMap;
            hMap = new System.Drawing.Bitmap(dlg.FileName);


            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    double y = hMap.GetPixel(i, j).R / 10.0;
                    geometry.Positions.Add(new Point3D(i, y, j));
                    double tu = i / Convert.ToDouble(N);
                    double tv = j / Convert.ToDouble(N);
                    geometry.TextureCoordinates.Add(new Point(tu, tv));
                }

            for (int i = 0; i < N - 1; i++)
                for (int j = 0; j < N - 1; j++)
                {
                    int ind0 = i + j * N;
                    int ind1 = (i + 1) + j * N;
                    int ind2 = i + (j + 1) * N;
                    int ind3 = (i + 1) + (j + 1) * N;
                    geometry.TriangleIndices.Add(ind0);
                    geometry.TriangleIndices.Add(ind1);
                    geometry.TriangleIndices.Add(ind3);
                    geometry.TriangleIndices.Add(ind0);
                    geometry.TriangleIndices.Add(ind3);
                    geometry.TriangleIndices.Add(ind2);
                }

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("pack://application:,,,/greensward.jpg", UriKind.Absolute));
            ib.Transform = new ScaleTransform(0.5, 0.5);
            ib.TileMode = TileMode.Tile;
            ib.Stretch = Stretch.Fill;
            DiffuseMaterial mat = new DiffuseMaterial(ib);
            GeometryModel3D model = new GeometryModel3D(geometry, mat);
            terrain.Content = model;
            scene.Children.Add(terrain);

            PerspectiveCamera camera = new PerspectiveCamera();
            camera.Position = new Point3D(N / 2, N / 2, N * 1.5);
            Vector3D lookAt = new Vector3D(N / 2, 0, N / 2);
            camera.LookDirection = Vector3D.Subtract(lookAt, new Vector3D(N / 2, N / 2, N * 2));
            camera.FarPlaneDistance = 1000;
            camera.NearPlaneDistance = 1;
            camera.UpDirection = new Vector3D(0, 1, 0);
            camera.FieldOfView = 75;
            scene.Camera = camera;
            PointLight pl = new PointLight();
            pl.Color = Colors.LightYellow;
            pl.Position = new Point3D(N, N, N / 2);
            ModelVisual3D light = new ModelVisual3D();
            light.Content = pl;
            scene.Children.Add(light);
            scene.KeyDown += Scene_KeyDown;
        }
    }
}
