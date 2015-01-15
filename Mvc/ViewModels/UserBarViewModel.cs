namespace Rtc.Mvc.ViewModels
{
    public class UserBarViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public byte[] Photo { get; set; }

        public bool IsOnline { get; set; }

    }
}