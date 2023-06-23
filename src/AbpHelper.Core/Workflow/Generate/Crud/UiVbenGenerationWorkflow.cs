using EasyAbp.AbpHelper.Core.Steps.Abp.ModificationCreatorSteps.CSharp;
using EasyAbp.AbpHelper.Core.Steps.Common;
using Elsa.Scripting.JavaScript;
using Elsa.Services;

namespace EasyAbp.AbpHelper.Core.Workflow.Generate.Crud
{
    public static class UiVbenGenerationWorkflow
    {
        public static IActivityBuilder AddUiVbenGenerationWorkflow(this IOutcomeBuilder builder)
        {
            return builder
                    .Then<GroupGenerationStep>(
                        step =>
                        {
                            step.GroupName = "UiVben"; 
                            step.TargetDirectory = new JavaScriptExpression<string>(VariableNames.AspNetCoreDir);
                        }
                    ).WithName("UiVben")
                
                    //.Then<FileFinderStep>(
                    //    step => step.SearchFileName = new JavaScriptExpression<string>("`*${ProjectInfo.Name}EntityFrameworkCoreModule.cs`")
                    //)
                    //.Then<FileModifierStep>()
                ;
        }
    }
}
