namespace Validator.Core.Models;

public class ConditionalFailure : Result
{
    public ConditionalFailure()
    {
    }

    public override bool Valid => false;
}

