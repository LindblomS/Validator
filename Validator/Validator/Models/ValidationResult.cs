namespace Validator.Models;

using System.Collections.Generic;
using Validator.Core.Models;

public class ValidationResult
{
    public ValidationResult(IEnumerable<Failure> failures)
    {
        Failures = failures ?? Enumerable.Empty<Failure>();
    }

    public bool Valid { get => Failures.Any(); }
    public IEnumerable<Failure> Failures { get; private set; }

    public static ValidationResult Empty()
    {
        return new ValidationResult(null);
    }
}
