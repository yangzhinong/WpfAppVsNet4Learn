namespace DemoFody
{
    [Equals]
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        [IgnoreDuringEquals]
        public int Z { get; set; }

        [CustomEqualsInternal]
        bool CustomLogic(Point other)
        {
            return Z == other.Z || Z == 0 || other.Z == 0;
        }

        public static bool operator == (Point left, Point right) => Operator.Weave(left, right);
        public static bool operator != (Point left, Point right) => Operator.Weave(left, right);
    }
}
