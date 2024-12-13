//using System.Text;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

//namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

//internal abstract class InnerSectionBuilder<TSection> : DocumentSectionBuilder<TSection>
//{
//    public InnerSectionBuilder(IFormatter<TSection> formatter) : base(formatter)
//    {
//    }

//    public void AdjustFormatting(BuildSettings settings)
//    {
//        Formatter.SetMargin(settings.Margin);
//    }

//    private void AppendSubsection<TSubection>(TSubection section,
//        IDocumentSectionBuilder<TSubection> contentBuilder, StringBuilder document)
//    {
//        BuildSettings newParagraph = new()
//        {
//            Margin = Formatter.Options.Padding + 1
//        };

//        document
//            .AppendLine(contentBuilder.Build(section, newParagraph))
//            .AppendLine(Formatter.ContentSeparator);
//    }
//}
