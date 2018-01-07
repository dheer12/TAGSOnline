using System;
using System.Reflection;

namespace TAGS_BARCODE_WEBAPP_V4.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}