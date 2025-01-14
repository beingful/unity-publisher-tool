using Unity.Publisher.Tool.Infrastructure.Http.Responses.Html;

namespace Unity.Publisher.Tool.Infrastructure.Api.PublisherApi.LogIn.Models;

public sealed record class CallbackPageResult(IHtmlContent HtmlContent);
