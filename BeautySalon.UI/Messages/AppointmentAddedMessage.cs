using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BeautySalon.UI.Messages;

public sealed class AppointmentAddedMessage(Guid value) : ValueChangedMessage<Guid>(value);