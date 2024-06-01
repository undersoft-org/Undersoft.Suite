using Undersoft.SDK.Series.Universe.Base;

namespace Undersoft.SDK.Series.Universe
{
    public class UniverseTetraValue : Base.Universe, IDisposable
    {
        public int[] xValue;
        private bool disposedValue = false;

        public UniverseTetraValue(int null_key)
        {
            xValue = new int[] { null_key, null_key, null_key, null_key };
        }

        public override int IndexMax
        {
            get { return xValue[3]; }
        }

        public override int IndexMin
        {
            get { return xValue[0]; }
        }

        public override int Size
        {
            get { return 4; }
        }

        public override void Add(int offsetBase, int offsetFactor, int indexOffset, int x)
        {
            if (xValue[x] == x)
            {
                return;
            }

            xValue[x] = x;
            if (x > xValue[3])
            {
                xValue[3] = x;
                return;
            }
            if (x < xValue[0])
            {
                xValue[0] = x;
                return;
            }
        }

        public override void Add(int x)
        {
            if (xValue[x] == x)
            {
                return;
            }

            xValue[x] = x;
            if (x > xValue[3])
            {
                xValue[3] = x;
                return;
            }

            if (x < xValue[0])
            {
                xValue[0] = x;
                return;
            }
        }

        public override bool Contains(int offsetBase, int offsetFactor, int indexOffset, int x)
        {
            if (xValue[x] == x)
            {
                return true;
            }

            return false;
        }

        public override bool Contains(int x)
        {
            if (xValue[x] == x)
            {
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public override void FirstAdd(int offsetBase, int offsetFactor, int indexOffset, int x)
        {
            xValue[0] = x;
            xValue[3] = x;
            xValue[x] = x;
        }

        public override void FirstAdd(int x)
        {
            xValue[0] = x;
            xValue[3] = x;
            xValue[x] = x;
        }

        public override int Next(int offsetBase, int offsetFactor, int indexOffset, int x)
        {
            if (x >= xValue[3])
                return -1;
            if (xValue[x + 1] != -1)
                return xValue[x + 1];
            if (xValue[x + 2] != -1)
                return xValue[x + 2];
            return xValue[3];
        }

        public override int Next(int x)
        {
            if (x >= xValue[3])
                return -1;
            if (xValue[x + 1] != -1)
                return xValue[x + 1];
            if (xValue[x + 2] != -1)
                return xValue[x + 2];
            return xValue[3];
        }

        public override int Previous(int offsetBase, int offsetFactor, int indexOffset, int x)
        {
            if (x <= xValue[0])
                return -1;
            if (xValue[x - 1] != -1)
                return xValue[x - 1];
            if (xValue[x - 2] != -1)
                return xValue[x - 2];
            return xValue[0];
        }

        public override int Previous(int x)
        {
            if (x <= xValue[0])
                return -1;
            if (xValue[x - 1] != -1)
                return xValue[x - 1];
            if (xValue[x - 2] != -1)
                return xValue[x - 2];
            return xValue[0];
        }

        public override bool Remove(int offsetBase, int offsetFactor, int indexOffset, int x)
        {
            if (xValue[x] != x)
                return false;

            if (xValue[0] == x)
            {
                xValue[x] = -1;
                if (xValue[1] != -1)
                {
                    xValue[0] = xValue[1];
                    return true;
                }
                if (xValue[2] != -1)
                {
                    xValue[0] = xValue[2];
                    return true;
                }
                if (xValue[3] != -1 && xValue[3] != x)
                {
                    xValue[0] = xValue[3];
                    return true;
                }
                xValue[0] = -1;
                xValue[3] = -1;
                return true;
            }

            if (xValue[3] == x)
            {
                xValue[x] = -1;
                if (xValue[2] != -1)
                {
                    xValue[3] = xValue[2];
                    return true;
                }
                if (xValue[1] != -1)
                {
                    xValue[3] = xValue[1];
                    return true;
                }

                xValue[3] = xValue[0];
                return true;
            }
            xValue[x] = -1;
            return true;
        }

        public override bool Remove(int x)
        {
            if (xValue[x] != x)
                return false;

            if (xValue[0] == x)
            {
                xValue[x] = -1;
                if (xValue[1] != -1)
                {
                    xValue[0] = xValue[1];
                    return true;
                }
                if (xValue[2] != -1)
                {
                    xValue[0] = xValue[2];
                    return true;
                }
                if (xValue[3] != -1 && xValue[3] != x)
                {
                    xValue[0] = xValue[3];
                    return true;
                }
                xValue[0] = -1;
                xValue[3] = -1;
                return true;
            }

            if (xValue[3] == x)
            {
                xValue[x] = -1;
                if (xValue[2] != -1)
                {
                    xValue[3] = xValue[2];
                    return true;
                }
                if (xValue[1] != -1)
                {
                    xValue[3] = xValue[1];
                    return true;
                }

                xValue[3] = xValue[0];
                return true;
            }
            xValue[x] = -1;
            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    xValue = null;
                }

                disposedValue = true;
            }
        }
    }
}
