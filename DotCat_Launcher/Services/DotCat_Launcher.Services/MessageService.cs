using DotCat_Launcher.Services.Interfaces;

namespace DotCat_Launcher.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
