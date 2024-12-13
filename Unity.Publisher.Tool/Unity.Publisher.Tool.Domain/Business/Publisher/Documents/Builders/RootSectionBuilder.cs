//using System.Text;
//using Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Formatting;

//namespace Unity.Publisher.Tool.Domain.Business.Publisher.Documents.Builders;

//internal abstract class RootSectionBuilder<TSection> : DocumentSectionBuilder<TSection>
//{
//    public RootSectionBuilder(IFormatter<TSection> formatter) : base(formatter)
//    {
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
