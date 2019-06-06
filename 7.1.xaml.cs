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

namespace Labradornaya7._1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        System.Windows.Threading.DispatcherTimer Timer;

        Rectangle myRect = new Rectangle();
        Path path = new Path();
        Ellipse myEllipse = new Ellipse();

        int currentFrame = 1, currentRow = 0, cr = 6;
        int frameW = 100, frameH = 100;

        public MainWindow()
        {
            InitializeComponent();
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(dispatcherTimer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            scene.Focusable = true;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var frameLeft = currentFrame * frameW;
            var frameTop = currentRow * frameH;
            (myRect.Fill as ImageBrush).Viewbox = new Rect(frameLeft, frameTop, frameLeft + frameW, frameTop + frameH);
            if (currentFrame % cr == 0)
            {
                currentRow++;
                currentFrame = 0;
            }
            currentFrame++;
        }

        private void line_Click(object sender, RoutedEventArgs e)
        {
            scene.Children.Clear();
            Line myLine = new Line();
            //установка цвета линии
            myLine.Stroke = System.Windows.Media.Brushes.Black;
            //координаты начала линии
            myLine.X1 = 1;
            myLine.Y1 = 1;
            //координаты конца линии
            myLine.X2 = 771;
            myLine.Y2 = 329;
            //параметры выравнивания в сцене
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            //толщина линии
            myLine.StrokeThickness = 3;
            //добавление линии в сцену
            scene.Children.Add(myLine);
        }

        private void ellipse_Click(object sender, RoutedEventArgs e)
        {
            scene.Children.Clear();
            ImageBrush ib = new ImageBrush();
            //позиция изображения будет указана как координаты левого верхнего угла
            //изображение будет растянуто по размерам прямоугольника, описанного вокруг фигуры
            ib.AlignmentX = AlignmentX.Left;
            ib.AlignmentY = AlignmentY.Top;
            //загрузка изображения и назначение кисти
            ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/roflan.png", UriKind.Absolute));
            myEllipse.Fill = ib;
            //толщина и цвет обводки
            myEllipse.StrokeThickness = 2;
            myEllipse.Stroke = Brushes.Black;
            //размеры овала
            myEllipse.Width = 150;
            myEllipse.Height = 200;
            //позиция овала
            myEllipse.Margin = new Thickness(216, 91, 0, 0);
            //добавление овала в сцену
            scene.Children.Add(myEllipse);
            //Point pos = Mouse.GetPosition(scene);
            //Rect ellipse = myEllipse.RenderTransform.TransformBounds(myEllipse.RenderedGeometry.Bounds);
            //if (ellipse.Contains(pos)==true)
            //{
            //    ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/haHAA.png",
            //   UriKind.Absolute));
            //    myEllipse.Fill = ib;
            //}
            //else
            //{
            //    ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/roflan.png", UriKind.Absolute));
            //    myEllipse.Fill = ib;
            //}

            myEllipse.MouseEnter += MyEllipse_MouseEnter;

        }

        private void MyEllipse_MouseEnter(object sender, MouseEventArgs e)
        {
            //создание новой кисти
            ImageBrush ib = new ImageBrush();

            Rect ellipse = myEllipse.RenderTransform.TransformBounds(myEllipse.RenderedGeometry.Bounds);
            //загрузка нового изображения и назначение кисти

            ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/haHAA.png",
           UriKind.Absolute));
            myEllipse.Fill = ib;
        }

        private void rectangle_Click(object sender, RoutedEventArgs e)
        {
            scene.Children.Clear();
            myRect = new Rectangle();
            myRect.Height = 100;
            myRect.Width = 100;
            ImageBrush ib = new ImageBrush();
            ib.AlignmentX = AlignmentX.Left;
            ib.AlignmentY = AlignmentY.Top;
            ib.Stretch = Stretch.None;
            ib.Viewbox = new Rect(0, 0, 100, 100);
            ib.ViewboxUnits = BrushMappingMode.Absolute;
            ib.ImageSource = new BitmapImage(new Uri("C:\\Users\\Admin\\Desktop\\VictoriaSprites.gif", UriKind.Absolute));
            myRect.Fill = ib;
            myRect.Margin = new Thickness(0, 0, 0, 0);
            scene.Children.Add(myRect);
            Timer.Start();
        }

        private void transform_Click(object sender, RoutedEventArgs e)
        {
            TransformGroup tg = new TransformGroup();
            //создание преобразования переноса в точку 50:50
            TranslateTransform tt = new TranslateTransform(50, 50);
            //создание поворота на 45 градусов вокруг точки 50:50
            RotateTransform rt = new RotateTransform(45, 50, 50);
            //добавление преобразований поворота и переноса в группу
            tg.Children.Add(rt);
            tg.Children.Add(tt);
            //назначение группы для фигуры
            path.RenderTransform = tg;

        }

        private void polygon_Click(object sender, RoutedEventArgs e)
        {
            scene.Children.Clear();
            Polygon myPolygon = new Polygon();
            //установка цвета обводки, цвета заливки и толщины обводки
            myPolygon.Stroke = Brushes.Black;
            ImageBrush ib = new ImageBrush();
            //позиция изображения будет указана как координаты левого верхнего угла
            //изображение будет растянуто по размерам прямоугольника, описанного вокруг фигуры
            ib.AlignmentX = AlignmentX.Left;
            ib.AlignmentY = AlignmentY.Top;
            //загрузка изображения и назначение кисти
            ib.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/11shrek.png", UriKind.Absolute));
            myPolygon.Fill = ib;
            //позиционирование объекта
            myPolygon.HorizontalAlignment = HorizontalAlignment.Center;
            myPolygon.VerticalAlignment = VerticalAlignment.Center;
            //создание точек многоугольника
            Point Point1 = new Point(54, 135);
            Point Point2 = new Point(69, 31);
            Point Point3 = new Point(420, 50);
            Point Point4 = new Point(50, 100);
            Point Point5 = new Point(0, -70);
            //создание и заполнение коллекции точек
            PointCollection myPointCollection = new PointCollection();
            myPointCollection.Add(Point1);
            myPointCollection.Add(Point2);
            myPointCollection.Add(Point3);
            myPointCollection.Add(Point4);
            myPointCollection.Add(Point5);
            //установка коллекции точек в объект многоугольник
            myPolygon.Points = myPointCollection;
            myPolygon.Margin = new Thickness(311, 90, 0, 0);
            //добавление многоугольника в сцену
            scene.Children.Add(myPolygon);
            
            Point pos = Mouse.GetPosition(scene);
            Rect polygon = myPolygon.RenderTransform.TransformBounds(myPolygon.RenderedGeometry.Bounds);

            if (polygon.Contains(pos) == true) { MessageBox.Show("Точка входит!"); }

        }

        private void polyline_Click(object sender, RoutedEventArgs e)
        {
            scene.Children.Clear();
            Polyline myPolyline = new Polyline();
            //установка цвета обводки, цвета заливки и толщины обводки
            myPolyline.Stroke = Brushes.Black;
            myPolyline.StrokeThickness = 2;
            //позиционирование объекта
            myPolyline.HorizontalAlignment = HorizontalAlignment.Center;
            myPolyline.VerticalAlignment = VerticalAlignment.Center;
            //создание точек многоугольника
            Point Point1 = new Point(54, 135);
            Point Point2 = new Point(69, 31);
            Point Point3 = new Point(420, 50);
            Point Point4 = new Point(50, 100);
            Point Point5 = new Point(0, 50);
            //создание и заполнение коллекции точек
            PointCollection myPointCollection = new PointCollection();
            myPointCollection.Add(Point1);
            myPointCollection.Add(Point2);
            myPointCollection.Add(Point3);
            myPointCollection.Add(Point4);
            myPointCollection.Add(Point5);
            //установка коллекции точек в объект многоугольник
            myPolyline.Points = myPointCollection;
            myPolyline.Margin = new Thickness(311, 90, 0, 0);
            //добавление многоугольника в сцену
            scene.Children.Add(myPolyline);
        }

        private void path_Click(object sender, RoutedEventArgs e)
        {
            scene.Children.Clear();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;
            //создание двух сегментов пути при помощи кривых Безье
            //параметры - (первая контрольная точка, вторая контрольная точка, конец кривой)
            BezierSegment bezierCurve1 = new BezierSegment(new Point(0, 0), new Point(0, 50), new Point(50, 90),
            true);
            BezierSegment bezierCurve2 = new BezierSegment(new Point(100, 50), new Point(100, 0), new Point(50,
            30), true);
            //создание коллекции сегментов и добавление к ней кривых
            PathSegmentCollection psc = new PathSegmentCollection();
            psc.Add(bezierCurve1);
            psc.Add(bezierCurve2);
            //создание объекта фигуры и установка начальной точки пути
            PathFigure pf = new PathFigure();
            pf.Segments = psc;
            pf.StartPoint = new Point(50, 30);
            //создание коллекции фигур
            PathFigureCollection pfc = new PathFigureCollection();
            pfc.Add(pf);
            //создание геометрии пути
            PathGeometry pg = new PathGeometry();
            pg.Figures = pfc;
            //создание набора геометрий
            GeometryGroup pathGeometryGroup = new GeometryGroup();
            pathGeometryGroup.Children.Add(pg);
            //
            path.Data = pathGeometryGroup;
            //добавление объекта путь в сцену
            scene.Children.Add(path);
        }
    }
}
