using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Sculptor.Model
{
    public class Grid : INotifyPropertyChanged
    {
        bool[,,] grid;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public bool this[int i1, int i2, int i3]
        {
            get
            {
                return grid[i1, i2, i3];
            }
            set
            {
                grid[i1, i2, i3] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(grid)));
            }
        }
        public Grid (int width, int height, int depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
            grid = new bool[width, height, depth];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    for (int k = 0; k < depth; k++)
                        grid[i, j, k] = true;
        }

        public MeshGeometry3D GetModel()
        {
            MeshGeometry3D model = new MeshGeometry3D();
            var tr = model.TriangleIndices;
            var g = tr[0];
            int vertexOffset = 0;

            for (int x = 0; x < Width - 1; x++)
                for (int y = 0; y < Height - 1; y++)
                    for (int z = 0; z < Depth; z++)
                    {
                        int hiddenCount = GetHiddenVerticesCount(x, y, z);
                        vertexOffset = model.Positions.Count;
                        var list = GetCubesVertices(x, y, z);
                        model.Positions.Concat(list);

                        List<int> triangles = new List<int>();
                        switch(list.Count)
                        {
                            case 3:

                                break;

                        }
                    }

            return model;
        }

        int GetHiddenVerticesCount(int x, int y, int z)
        {
            int count = 0;
            for (int i = x; i <= x + 1; i++)
                for (int j = y; j <= y + 1; j++)
                    for (int k = z; k <= z + 1; k++)
                        if (!grid[i, j, k])
                            count++;
            return count;
        }

        List<Point3D> GetCubesVertices(int x, int y, int z)
        {
            List<Point3D> points = new List<Point3D>();
            int[] direction;
            int[] point;

            //point (x, y, z)
            point = new[] { x, y, z };
            direction = new[] { 1, 0, 0};
            if (CheckEdge(point, direction).HasValue)
                points.Add(CheckEdge(point, direction).Value);
            direction = new[] { 0, 1, 0 };
            if (CheckEdge(point, direction).HasValue)
                points.Add(CheckEdge(point, direction).Value);
            direction = new[] { 0, 0, 1 };
            if (CheckEdge(point, direction).HasValue)
                points.Add(CheckEdge(point, direction).Value);

            //point (x+1, y+1, z)
            point = new[] { x + 1, y + 1, z };
            direction = new[] { -1, 0, 0 };
            if (CheckEdge(point, direction).HasValue)
                points.Add(CheckEdge(point, direction).Value);
            direction = new[] { 0, -1, 0 };
            if (CheckEdge(point, direction).HasValue)
                points.Add(CheckEdge(point, direction).Value);
            direction = new[] { 0, 0, 1 };
            if (CheckEdge(point, direction).HasValue)
                points.Add(CheckEdge(point, direction).Value);

            //point (x, y+1, z+1)
            point = new[] { x, y + 1, z + 1 };
            direction = new[] { 1, 0, 0 };
            if (CheckEdge(point, direction).HasValue)
                points.Add(CheckEdge(point, direction).Value);
            direction = new[] { 0, -1, 0 };
            if (CheckEdge(point, direction).HasValue)
                points.Add(CheckEdge(point, direction).Value);
            direction = new[] { 0, 0, -1 };
            if (CheckEdge(point, direction).HasValue)
                points.Add(CheckEdge(point, direction).Value);

            //point (x+1, y, z+1)
            point = new[] { x + 1, y, z + 1 };
            direction = new[] { -1, 0, 0 };
            if (CheckEdge(point, direction).HasValue)
                points.Add(CheckEdge(point, direction).Value);
            direction = new[] { 0, 1, 0 };
            if (CheckEdge(point, direction).HasValue)
                points.Add(CheckEdge(point, direction).Value);
            direction = new[] { 0, 0, -1 };
            if (CheckEdge(point, direction).HasValue)
                points.Add(CheckEdge(point, direction).Value);
            return points;

        }

        Point3D? CheckEdge(int [] point, int[] direction)
        {
            Point3D? result = null;
            int x = point[0], y = point[1], z = point[2];
            bool p1 = grid[x, y, z];
            if (grid[x + direction[0], y + direction[1], z + direction[2]] != p1)
                result = new Point3D(x + .5 * direction[0], y + .5 * direction[0], z + .5 * direction[0]);
            return result;
        }
    }
}
