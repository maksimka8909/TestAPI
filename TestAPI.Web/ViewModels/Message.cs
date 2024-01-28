namespace TestAPI.ViewModels;

public class Message
{
    public string Result { get; set; }

    public Message(string result)
    {
        Result = result;
    }
}