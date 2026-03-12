namespace ACC.Shared.Utils;

public sealed class ServiceEndpointsOptions
{
    public const string SectionName = "ServiceEndpoints";

    public string ApiBaseUrl { get; set; } = string.Empty;

    public string CompilerBaseUrl { get; set; } = string.Empty;

    public string WebAppBaseUrl { get; set; } = string.Empty;
}
