using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace UnoClockXamarin.Skia.Tizen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new UnoClockXamarin.App(), args);
            host.Run();
        }
    }
}
