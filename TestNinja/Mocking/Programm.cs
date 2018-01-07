namespace TestNinja.Mocking
{
    public class Programm
    {
        public static void Main()
        {
            var service = new VideoService();
            var title = service.ReadVideoTitle(new FileReader());
        }
    }
}