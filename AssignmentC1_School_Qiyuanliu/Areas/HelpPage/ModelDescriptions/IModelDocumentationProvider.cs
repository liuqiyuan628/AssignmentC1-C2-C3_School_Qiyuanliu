using System;
using System.Reflection;

namespace AssignmentC1_School_Qiyuanliu.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}