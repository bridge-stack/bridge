﻿namespace Bridge.Workflow;

public class WorkflowStepInline : WorkflowStep<InlineStepBody>
{
    public Func<IStepExecutionContext, ExecutionResult> Body { get; set; }

    public override IStepBody ConstructBody(IServiceProvider serviceProvider)
    {
        return new InlineStepBody(Body);
    }
}