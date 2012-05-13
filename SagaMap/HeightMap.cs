using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using SagaLib;
using SagaMap.Manager;

namespace SagaMap
{
    public class HeightMap
    {

        public float water_level;
        private float[,] HeightData;
        public HeightMapInfo info;

        public HeightMap(HeightMapInfo info)
        {
           this.info = info;
           this.water_level = info.water_level;
           this.LoadHMap();
        }

        private void LoadHMap()
        {
            int maxX = this.info.size;
            int maxY = this.info.size;
            
            this.HeightData = new float[maxX, maxY];
            try
            {
                FileStream fs = new FileStream("Mapinfo/" + this.info.name + ".hmap", FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);

                for (int y = 0; y < maxY; y++)
                    for (int x = 0; x < maxX; x++)
                        this.HeightData[x, y] = (float)(((float)r.ReadUInt16() - 32768) / 32768) * (this.info.size / 2);

                r.Close();
                fs.Close();
            }
           catch(Exception ) { Logger.ShowError("Cannot load heightmap: " + info.name, null); }
        }

        public float[] GetRandomPos()
        {
            float[] ret = new float[3];

            for(int i = 0;i < 10000; i++)
            {
                int max  = (int)(((this.info.size / 2) * this.info.scale[0]) + this.info.location[0]);
                int min = (int)((-(this.info.size / 2) * this.info.scale[0]) + this.info.location[0]);

                ret[0] = Global.Random.Next(min, max+1);
                ret[1] = Global.Random.Next(min, max+1);

                this.GetZ(ret[0], ret[1], out ret[2]);

                //if (ret[2] < this.water_level) continue;

                float check;

                this.GetZ(ret[0] + this.info.scale[0], ret[1], out check);
                if (Math.Abs(check - ret[2]) > 100) continue;

                this.GetZ(ret[0] - this.info.scale[0], ret[1], out check);
                if (Math.Abs(check - ret[2]) > 100) continue;

                this.GetZ(ret[0], ret[1] + this.info.scale[1], out check);
                if (Math.Abs(check - ret[2]) > 100) continue;

                this.GetZ(ret[0], ret[1] - this.info.scale[1], out check);
                if (Math.Abs(check - ret[2]) > 100) continue;

                this.GetZ(ret[0] + this.info.scale[0], ret[1] + this.info.scale[1], out check);
                if (Math.Abs(check - ret[2]) > 100) continue;

                this.GetZ(ret[0] + this.info.scale[0], ret[1] - this.info.scale[1], out check);
                if (Math.Abs(check - ret[2]) > 100) continue;

                this.GetZ(ret[0] - this.info.scale[0], ret[1] + this.info.scale[1], out check);
                if (Math.Abs(check - ret[2]) > 100) continue;

                this.GetZ(ret[0] - this.info.scale[0], ret[1] - this.info.scale[1], out check);
                if (Math.Abs(check - ret[2]) > 100) continue;

                break;
            }

            return ret;
        }

        private struct point2D
        {
           public float x, y;
        }

        private struct point3D
        {
            public float x, y, z;
        }

        private static float z_correction = 103.6f;

        private static double det(double a, double b, double c, double d, double e, double f, double g, double h, double i)
        {
            return a * e * i + b * f * g + c * d * h - c * e * g - f * h * a - i * b * d;
        }

        private static double z(double a1, double a2, double a3, double b1, double b2, double b3, double c1, double c2, double c3, double x, double y)
        {

            return (det(a1, b1, c1, a2, b2, c2, a3, b3, c3) - x * det(a2, b2, c2, a3, b3, c3, 1, 1, 1)
            + y * det(a1, b1, c1, a3, b3, c3, 1, 1, 1)) / det(a1, b1, c1, a2, b2, c2, 1, 1, 1);
        }

        public bool GetZ(float x, float y, out float z)
        {
            point2D delta;
            point3D point1, point2, point3, point4;

            ushort mx = (ushort)Math.Ceiling((float)((x - this.info.location[0]) / this.info.scale[0]) + (this.info.size / 2));
            ushort my = (ushort)Math.Ceiling((float)((y - this.info.location[1]) / this.info.scale[1]) + (this.info.size / 2));

            if (mx >= this.info.size || my >= this.info.size)
            {
                z = 0;
                return false;
            }
            
            point1.x = (float)((mx - (this.info.size / 2)) * this.info.scale[0]) + this.info.location[0];
            point1.y = (float)((my - (this.info.size / 2)) * this.info.scale[1]) + this.info.location[1];
            point1.z = (float)(this.HeightData[mx, my] * this.info.scale[2]) + this.info.location[2];

            mx -= 1;
            point2.x = (float)((mx - (this.info.size / 2)) * this.info.scale[0]) + this.info.location[0];
            point2.y = (float)((my - (this.info.size / 2)) * this.info.scale[1]) + this.info.location[1];
            point2.z = (float)(this.HeightData[mx, my] * this.info.scale[2]) + this.info.location[2];

            my -= 1;
            point3.x = (float)((mx - (this.info.size / 2)) * this.info.scale[0]) + this.info.location[0];
            point3.y = (float)((my - (this.info.size / 2)) * this.info.scale[1]) + this.info.location[1];
            point3.z = (float)(this.HeightData[mx, my] * this.info.scale[2]) + this.info.location[2];

            mx += 1;
            point4.x = (float)((mx - (this.info.size / 2)) * this.info.scale[0]) + this.info.location[0];
            point4.y = (float)((my - (this.info.size / 2)) * this.info.scale[1]) + this.info.location[1];
            point4.z = (float)(this.HeightData[mx, my] * this.info.scale[2]) + this.info.location[2];

            delta.x = point1.x - x;
            delta.y = point1.y - y;


            if (delta.x >= delta.y) //use 1,2,3
                z = (float)HeightMap.z(point1.x, point1.y, point1.z, point2.x, point2.y, point2.z, point3.x, point3.y, point3.z, x, y) + z_correction;
            else //use 1,3,4
                z = (float)HeightMap.z(point1.x, point1.y, point1.z, point3.x, point3.y, point3.z, point4.x, point4.y, point4.z, x, y) + z_correction;

            return true;
         }

    }
}
