using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BeautySalon.UI.Messages;

public sealed class AppointmentUpdatedMessage(Guid value) : ValueChangedMessage<Guid>(value);