namespace DemoIL
{
    public class ClassEq
    {
        private string _x;

        public string X { get => _x; set => _x = value; }

        public string Y { get; set; }


        public string EqX { get => _x; set
            {
                if (_x != value) _x = value;
            } 
        }

        public string EqXTrim { get => _x; set
            {
                var valTrim = value?.Trim();
                if (string.CompareOrdinal(_x, valTrim) != 0)
                {
                    _x=valTrim;
                }
            }
        }
    }
}
