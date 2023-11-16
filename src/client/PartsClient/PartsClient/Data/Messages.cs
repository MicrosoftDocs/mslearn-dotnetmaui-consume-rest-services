using CommunityToolkit.Mvvm.Messaging.Messages;

namespace PartsClient.Data;

public class RefreshMessage : ValueChangedMessage<bool>
{
    public RefreshMessage(bool value) : base(value)
    {
    }
}
