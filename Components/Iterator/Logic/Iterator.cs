namespace StudyCentralV2.Components.Iterator.Logic
{
    public class Iterator
    {
        public static int Value;
        public static int Increment = 0;

        public static Task Iterate()
        {
            if (Increment == 0) {
                Increment++;
                Value = 0;
            }

            else if (Increment > 0) {
                Increment++;
                Value++;
            }

            return Task.CompletedTask;
        }
    }
}
