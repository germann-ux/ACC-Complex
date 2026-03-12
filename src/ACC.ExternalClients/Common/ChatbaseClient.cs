using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ACC.ExternalClients.Common;

public sealed class ChatbaseClient(IOptions<ChatbaseOptions> options, ILogger<ChatbaseClient> logger) : IChatbaseClient
{
    private readonly ChatbaseOptions _options = options.Value;
    private readonly ILogger<ChatbaseClient> _logger = logger;

    public string? GetIframeUrl()
    {
        if (string.IsNullOrWhiteSpace(_options.ChatbotId))
        {
            _logger.LogWarning("Chatbase chatbot id is not configured.");
            return null;
        }

        var domain = string.IsNullOrWhiteSpace(_options.Domain) ? "www.chatbase.co" : _options.Domain.Trim();
        return $"https://{domain}/chatbot-iframe/{_options.ChatbotId}";
    }
}
