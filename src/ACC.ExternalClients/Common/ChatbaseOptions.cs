namespace ACC.ExternalClients.Common;

public sealed class ChatbaseOptions
{
    public const string SectionName = "ExternalIntegrations:Chatbase";

    public string Domain { get; set; } = "www.chatbase.co";

    public string ChatbotId { get; set; } = string.Empty;
}
